#!/usr/bin/env python3
"""
GitHub Traffic Collector — single-repository version for GitHub Actions.

Collects traffic statistics (views, clones, referrers, popular paths,
release downloads, stars/forks/open issues/watchers) for the repository
given by GITHUB_REPOSITORY and records them in ./traffic.md.

260716Cl 追加: https://gist.github.com/seto77/91b025f9091b5e1d5d5b39adcc597a5d
(github_traffic.py) からの移植。全リポジトリ横断処理 (/users/{USER}/repos) と
Contents API による PUT を廃し、GITHUB_REPOSITORY で指定される単一リポジトリ +
ローカルファイル書き込み (workflow 側で git commit) に簡略化。
データ保持ポリシー (daily 14日 / weekly 14週 / monthly 12か月 / yearly 無期限、
Referrers・Popular Paths は weekly 2週 / monthly 3か月) は元のロジックを踏襲。
ネットワークアクセスは https://api.github.com のみ (移植時に全文確認済み)。

Env:
  GH_TOKEN           GitHub token (required; GITHUB_TOKEN or PAT with 'repo')
  GITHUB_REPOSITORY  'owner/repo' (set automatically by GitHub Actions; required)
  TRAFFIC_DRY_RUN    '1' to skip writing traffic.md and print summary only
  TRAFFIC_OUTPUT     output file path (default: traffic.md)
"""

import datetime as dt
import json
import os
import re
import sys
import time
from urllib import request, error

try:
    sys.stdout.reconfigure(encoding="utf-8")
    sys.stderr.reconfigure(encoding="utf-8")
except Exception:
    pass

TOKEN = os.environ.get("GH_TOKEN")
if not TOKEN:
    print("ERROR: GH_TOKEN env var is required", file=sys.stderr)
    sys.exit(2)
DRY_RUN = os.environ.get("TRAFFIC_DRY_RUN") == "1"
OUT_PATH = os.environ.get("TRAFFIC_OUTPUT", "traffic.md")
TODAY = dt.date.today()
TODAY_STR = TODAY.isoformat()

API = "https://api.github.com"
UA = "seto77-traffic-collector/1.0"

SECTION_ORDER = ["Views", "Clones", "Referrers", "Popular Paths", "Downloads", "Stats"]
SUB_ORDER = ["Daily", "Weekly", "Monthly", "Yearly"]
SUB_LABEL = {
    "Daily": "Daily (最大14日保持)",
    "Weekly": "Weekly (最大14週保持)",
    "Monthly": "Monthly (最大12か月保持)",
    "Yearly": "Yearly (無制限)",
}
COLUMNS = {
    ("Views", "Daily"): ["Date", "Total Views", "Unique Visitors"],
    ("Views", "Weekly"): ["Period", "Total Views", "Unique Visitors"],
    ("Views", "Monthly"): ["Period", "Total Views", "Unique Visitors"],
    ("Views", "Yearly"): ["Period", "Total Views", "Unique Visitors"],
    ("Clones", "Daily"): ["Date", "Total Clones", "Unique Cloners"],
    ("Clones", "Weekly"): ["Period", "Total Clones", "Unique Cloners"],
    ("Clones", "Monthly"): ["Period", "Total Clones", "Unique Cloners"],
    ("Clones", "Yearly"): ["Period", "Total Clones", "Unique Cloners"],
    ("Referrers", "Daily"): ["Date Collected", "Referrer", "Total Count", "Unique"],
    ("Referrers", "Weekly"): ["Period", "Referrer", "Total Count", "Unique"],
    ("Referrers", "Monthly"): ["Period", "Referrer", "Total Count", "Unique"],
    ("Referrers", "Yearly"): ["Period", "Referrer", "Total Count", "Unique"],
    ("Popular Paths", "Daily"): ["Date Collected", "Path", "Title", "Total Count", "Unique"],
    ("Popular Paths", "Weekly"): ["Period", "Path", "Title", "Total Count", "Unique"],
    ("Popular Paths", "Monthly"): ["Period", "Path", "Title", "Total Count", "Unique"],
    ("Popular Paths", "Yearly"): ["Period", "Path", "Title", "Total Count", "Unique"],
    ("Downloads", "All"): ["Release Tag", "Release Date", "Asset Name", "Download Count"],
    ("Stats", "Daily"): ["Date", "Stars", "Forks", "Open Issues", "Watchers"],
    ("Stats", "Weekly"): ["Period", "Stars", "Forks", "Open Issues", "Watchers"],
    ("Stats", "Monthly"): ["Period", "Stars", "Forks", "Open Issues", "Watchers"],
    ("Stats", "Yearly"): ["Period", "Stars", "Forks", "Open Issues", "Watchers"],
}


def api_call(method, path, body=None, max_retry=1):
    url = API + path
    headers = {
        "Authorization": f"Bearer {TOKEN}",
        "Accept": "application/vnd.github+json",
        "User-Agent": UA,
    }
    data = None
    if body is not None:
        data = json.dumps(body).encode("utf-8")
        headers["Content-Type"] = "application/json"
    last = None
    for attempt in range(max_retry + 1):
        try:
            req = request.Request(url, data=data, headers=headers, method=method)
            with request.urlopen(req, timeout=30) as r:
                raw = r.read()
                return r.status, (json.loads(raw) if raw else {})
        except error.HTTPError as e:
            try:
                err_body = json.loads(e.read())
            except Exception:
                err_body = {}
            if e.code in (403, 404, 409, 422):
                return e.code, err_body
            last = e
        except error.URLError as e:
            last = e
        if attempt < max_retry:
            time.sleep(1.5)
    raise last


def iso_week(d):
    y, w, _ = d.isocalendar()
    return f"{y}-W{w:02d}"


def iso_month(d):
    return f"{d.year}-{d.month:02d}"


def iso_year(d):
    return str(d.year)


def parse_ymd(s):
    try:
        return dt.date.fromisoformat(s)
    except Exception:
        return None


def parse_week(s):
    m = re.match(r"(\d{4})-W(\d{1,2})", s)
    if not m:
        return None
    try:
        return dt.date.fromisocalendar(int(m.group(1)), int(m.group(2)), 1)
    except Exception:
        return None


def parse_month(s):
    m = re.match(r"^(\d{4})-(\d{1,2})$", s)
    if not m:
        return None
    try:
        return dt.date(int(m.group(1)), int(m.group(2)), 1)
    except Exception:
        return None


def parse_year(s):
    try:
        return dt.date(int(s), 1, 1)
    except Exception:
        return None


SEP_RE = re.compile(r"^\|[\s\-\|:]+\|\s*$")


def normalize_sub_label(h3_text):
    lower = h3_text.lower()
    for sub in SUB_ORDER:
        if lower.startswith(sub.lower()):
            return SUB_LABEL[sub]
    return h3_text


def parse_md(content):
    """Parse existing traffic.md into {section: {subsection_label: [rows]}}.

    Rows are lists of strings (cell values). Downloads rows go under 'All'.
    Table header/separator lines are discarded; only data rows kept.
    """
    result = {"_meta": {}}
    lines = content.splitlines()
    for ln in lines:
        m = re.match(r"<!--\s*meta:\s*(.*?)\s*-->", ln.strip())
        if m:
            for kv in m.group(1).split():
                if "=" in kv:
                    k, v = kv.split("=", 1)
                    result["_meta"][k] = v
    i = 0
    cur_sec = None
    cur_sub_label = None
    while i < len(lines):
        line = lines[i]
        stripped = line.strip()
        if stripped.startswith("## "):
            cur_sec = stripped[3:].strip()
            cur_sub_label = None
            result.setdefault(cur_sec, {})
            i += 1
            continue
        if stripped.startswith("### "):
            cur_sub_label = normalize_sub_label(stripped[4:].strip())
            if cur_sec is not None:
                result[cur_sec].setdefault(cur_sub_label, [])
            i += 1
            continue
        if cur_sec and stripped.startswith("|") and i + 1 < len(lines) and SEP_RE.match(lines[i + 1].strip()):
            # table: header (i), separator (i+1), data (i+2..)
            j = i + 2
            rows = []
            while j < len(lines):
                ln = lines[j].strip()
                if not ln or not ln.startswith("|") or SEP_RE.match(ln):
                    break
                cells = [c.strip() for c in ln.strip("|").split("|")]
                rows.append(cells)
                j += 1
            target = cur_sub_label if cur_sub_label else "All"
            result[cur_sec].setdefault(target, [])
            result[cur_sec][target].extend(rows)
            i = j
            continue
        i += 1
    return result


def render_table(cols, rows):
    out = ["| " + " | ".join(cols) + " |", "| " + " | ".join(["----"] * len(cols)) + " |"]
    for row in rows:
        padded = (list(row) + [""] * len(cols))[: len(cols)]
        out.append("| " + " | ".join(str(c) for c in padded) + " |")
    return out


def subs_for(sec):
    if sec == "Downloads":
        return []
    if sec in ("Referrers", "Popular Paths"):
        return ["Weekly", "Monthly", "Yearly"]
    return SUB_ORDER


def display_sub_label(sec, sub):
    if sec in ("Referrers", "Popular Paths"):
        if sub == "Weekly":
            return "Weekly (最大2週保持)"
        if sub == "Monthly":
            return "Monthly (最大3か月保持)"
    return SUB_LABEL[sub]


def build_md(repo, data):
    out = [f"# Traffic Data: {repo}", "", f"Last updated: {TODAY_STR}", ""]
    for sec in SECTION_ORDER:
        out.append(f"## {sec}")
        out.append("")
        if sec == "Downloads":
            cols = COLUMNS[("Downloads", "All")]
            rows = data.get(sec, {}).get("All", [])
            out.extend(render_table(cols, rows))
            total = 0
            for r in rows:
                if len(r) >= 4 and r[3].lstrip("-").isdigit():
                    total += int(r[3])
            out.append("")
            out.append(f"**TOTAL: {total}**")
            out.append("")
            continue
        for sub in subs_for(sec):
            label = SUB_LABEL[sub]
            cols = COLUMNS[(sec, sub)]
            rows = data.get(sec, {}).get(label, [])
            out.append(f"### {display_sub_label(sec, sub)}")
            out.extend(render_table(cols, rows))
            out.append("")
    meta = data.get("_meta", {})
    if meta:
        parts = " ".join(f"{k}={v}" for k, v in sorted(meta.items()))
        out.append(f"<!-- meta: {parts} -->")
    return "\n".join(out).rstrip() + "\n"


def row_date(sec, sub, row):
    if not row:
        return None
    first = row[0]
    if sec in ("Views", "Clones", "Stats") and sub == "Daily":
        return parse_ymd(first)
    if sec in ("Referrers", "Popular Paths") and sub == "Daily":
        return parse_ymd(first)
    if sub == "Weekly":
        return parse_week(first)
    if sub == "Monthly":
        return parse_month(first)
    if sub == "Yearly":
        return parse_year(first)
    return None


def apply_retention(sec, sub, rows, today):
    if sub == "Yearly":
        return rows
    if sub == "Daily":
        cutoff = today - dt.timedelta(days=14)
    elif sub == "Weekly":
        weeks = 2 if sec in ("Referrers", "Popular Paths") else 14
        cutoff = today - dt.timedelta(weeks=weeks)
    elif sub == "Monthly":
        months = 3 if sec in ("Referrers", "Popular Paths") else 12
        y, m = today.year, today.month - months
        while m <= 0:
            y -= 1
            m += 12
        cutoff = dt.date(y, m, 1)
    else:
        return rows
    out = []
    for r in rows:
        d = row_date(sec, sub, r)
        if d and d > cutoff:
            out.append(r)
    return out


def sort_rows(sec, sub, rows):
    def key(r):
        d = row_date(sec, sub, r) or dt.date(1970, 1, 1)
        if sec == "Referrers":
            try:
                cnt = int(r[2])
            except Exception:
                cnt = 0
            return (d, cnt)
        if sec == "Popular Paths":
            try:
                cnt = int(r[3])
            except Exception:
                cnt = 0
            return (d, cnt)
        return (d, 0)
    rows.sort(key=key, reverse=True)
    return rows


def update_views_clones(section_data, api_resp, key_name):
    daily_label = SUB_LABEL["Daily"]
    daily_rows = section_data.setdefault(daily_label, [])
    existing_dates = {r[0] for r in daily_rows if r}
    # 260716Cl 追加 (元Gistの集計二重加算/当日部分値凍結の修正):
    # (1) daily保持cutoff以前の日付はdaily表から削除済みでもAPIの14日窓に残るため、
    #     毎回「新規」扱いで再追加されweekly/monthly/yearlyへ二重加算されていた
    #     → cutoff以前はそもそも取り込まない (apply_retentionのdaily条件 d > cutoff と一致させる)
    # (2) 当日(UTC)は部分集計値であり、一度取り込むと確定値に更新されない
    #     → 当日分はスキップし、翌日以降に確定値で取り込む
    cutoff = TODAY - dt.timedelta(days=14)
    added = []
    for entry in api_resp.get(key_name, []) or []:
        date = entry["timestamp"][:10]
        d = parse_ymd(date)
        if d is None or d <= cutoff or d >= TODAY:
            continue
        if date in existing_dates:
            continue
        daily_rows.append([date, str(entry["count"]), str(entry["uniques"])])
        existing_dates.add(date)
        added.append((date, int(entry["count"]), int(entry["uniques"])))
    for date_str, count, uniques in added:
        d = parse_ymd(date_str)
        if not d:
            continue
        for sub, keyfn in (("Weekly", iso_week), ("Monthly", iso_month), ("Yearly", iso_year)):
            label = SUB_LABEL[sub]
            rows = section_data.setdefault(label, [])
            period = keyfn(d)
            found = next((r for r in rows if r and r[0] == period), None)
            if found:
                try:
                    found[1] = str(int(found[1]) + count)
                    found[2] = str(int(found[2]) + uniques)
                except (ValueError, IndexError):
                    pass
            else:
                rows.append([period, str(count), str(uniques)])
    return len(added)


def update_referrers(section_data, api_resp, today, last_collected):
    if last_collected == today.isoformat():
        return 0
    section_data.pop(SUB_LABEL["Daily"], None)
    added_count = 0
    for entry in api_resp or []:
        ref = entry.get("referrer", "")
        count = int(entry.get("count", 0))
        uniques = int(entry.get("uniques", 0))
        added_count += 1
        for sub, keyfn in (("Weekly", iso_week), ("Monthly", iso_month), ("Yearly", iso_year)):
            label = SUB_LABEL[sub]
            rows = section_data.setdefault(label, [])
            period = keyfn(today)
            found = next((r for r in rows if r and len(r) >= 4 and r[0] == period and r[1] == ref), None)
            if found:
                try:
                    found[2] = str(int(found[2]) + count)
                    found[3] = str(int(found[3]) + uniques)
                except (ValueError, IndexError):
                    pass
            else:
                rows.append([period, ref, str(count), str(uniques)])
    return added_count


def update_paths(section_data, api_resp, today, last_collected):
    if last_collected == today.isoformat():
        return 0
    section_data.pop(SUB_LABEL["Daily"], None)
    added_count = 0
    for entry in api_resp or []:
        path = entry.get("path", "")
        title = entry.get("title", path)
        count = int(entry.get("count", 0))
        uniques = int(entry.get("uniques", 0))
        added_count += 1
        for sub, keyfn in (("Weekly", iso_week), ("Monthly", iso_month), ("Yearly", iso_year)):
            label = SUB_LABEL[sub]
            rows = section_data.setdefault(label, [])
            period = keyfn(today)
            found = next((r for r in rows if r and len(r) >= 5 and r[0] == period and r[1] == path), None)
            if found:
                try:
                    found[2] = title
                    found[3] = str(int(found[3]) + count)
                    found[4] = str(int(found[4]) + uniques)
                except (ValueError, IndexError):
                    pass
            else:
                rows.append([period, path, title, str(count), str(uniques)])
    return added_count


def update_stats(section_data, repo_info, today):
    stars = str(repo_info.get("stargazers_count", 0))
    forks = str(repo_info.get("forks_count", 0))
    issues = str(repo_info.get("open_issues_count", 0))
    watchers = str(repo_info.get("subscribers_count", repo_info.get("watchers_count", 0)))
    today_str = today.isoformat()
    daily_label = SUB_LABEL["Daily"]
    daily_rows = section_data.setdefault(daily_label, [])
    idx = next((i for i, r in enumerate(daily_rows) if r and r[0] == today_str), None)
    new_row = [today_str, stars, forks, issues, watchers]
    if idx is not None:
        daily_rows[idx] = new_row
    else:
        daily_rows.append(new_row)
    for sub, keyfn in (("Weekly", iso_week), ("Monthly", iso_month), ("Yearly", iso_year)):
        label = SUB_LABEL[sub]
        rows = section_data.setdefault(label, [])
        period = keyfn(today)
        idx = next((i for i, r in enumerate(rows) if r and r[0] == period), None)
        agg = [period, stars, forks, issues, watchers]
        if idx is not None:
            rows[idx] = agg
        else:
            rows.append(agg)


def update_downloads(section_data, releases):
    rows = []
    for rel in releases or []:
        tag = rel.get("tag_name", "")
        pub = (rel.get("published_at") or "")[:10]
        for asset in rel.get("assets", []) or []:
            rows.append([tag, pub, asset.get("name", ""), str(asset.get("download_count", 0))])
    rows.sort(key=lambda r: r[1], reverse=True)
    section_data["All"] = rows


def process_repo(owner, repo):
    print(f"=== {owner}/{repo} ===", flush=True)

    data = {}
    if os.path.exists(OUT_PATH):
        with open(OUT_PATH, encoding="utf-8") as f:
            data = parse_md(f.read())

    for sec in SECTION_ORDER:
        data.setdefault(sec, {})

    try:
        st_v, views = api_call("GET", f"/repos/{owner}/{repo}/traffic/views")
        st_c, clones = api_call("GET", f"/repos/{owner}/{repo}/traffic/clones")
        st_r, referrers = api_call("GET", f"/repos/{owner}/{repo}/traffic/popular/referrers")
        st_p, paths = api_call("GET", f"/repos/{owner}/{repo}/traffic/popular/paths")
        st_rel, releases = api_call("GET", f"/repos/{owner}/{repo}/releases")
        st_info, repo_info = api_call("GET", f"/repos/{owner}/{repo}")
    except Exception as e:
        print(f"  error fetching traffic APIs: {e}")
        return "error"

    # traffic APIは権限不足でも403応答が返る(api_callは(403, body)を返却)ため、無音の空更新にせず明示的に失敗させる。
    # releases/repo_infoも失敗時はDownloads空化・Statsゼロ化がそのままコミットされるためガードする (260716Cl)
    for name, st, resp in (("views", st_v, views), ("clones", st_c, clones),
                           ("referrers", st_r, referrers), ("paths", st_p, paths),
                           ("releases", st_rel, releases), ("repo_info", st_info, repo_info)):
        if st != 200:
            msg = resp.get("message", "") if isinstance(resp, dict) else ""
            print(f"  error: traffic API '{name}' HTTP {st} {msg}")
            return "error"

    if isinstance(views, dict):
        added_v = update_views_clones(data["Views"], views, "views")
    else:
        added_v = 0
    if isinstance(clones, dict):
        added_c = update_views_clones(data["Clones"], clones, "clones")
    else:
        added_c = 0
    meta = data.setdefault("_meta", {})
    last_ref = meta.get("last_collected_referrers", "")
    last_paths = meta.get("last_collected_paths", "")
    added_r = update_referrers(data["Referrers"], referrers if isinstance(referrers, list) else [], TODAY, last_ref)
    added_p = update_paths(data["Popular Paths"], paths if isinstance(paths, list) else [], TODAY, last_paths)
    meta["last_collected_referrers"] = TODAY_STR
    meta["last_collected_paths"] = TODAY_STR
    update_downloads(data["Downloads"], releases if isinstance(releases, list) else [])
    update_stats(data["Stats"], repo_info if isinstance(repo_info, dict) else {}, TODAY)

    for sec in SECTION_ORDER:
        if sec == "Downloads":
            continue
        for sub in subs_for(sec):
            label = SUB_LABEL[sub]
            rows = data[sec].get(label, [])
            rows = apply_retention(sec, sub, rows, TODAY)
            rows = sort_rows(sec, sub, rows)
            data[sec][label] = rows

    md = build_md(repo, data)

    if DRY_RUN:
        print(f"  dry-run: {len(md)} bytes | added v={added_v} c={added_c} r={added_r} p={added_p}")
        return "dry-run"

    with open(OUT_PATH, "w", encoding="utf-8", newline="\n") as f:
        f.write(md)
    print(f"  wrote {OUT_PATH}: {len(md)} bytes | added v={added_v} c={added_c} r={added_r} p={added_p}")
    return "updated"


def main():
    gh_repo = os.environ.get("GITHUB_REPOSITORY", "")
    if "/" not in gh_repo:
        print("ERROR: GITHUB_REPOSITORY env var ('owner/repo') is required", file=sys.stderr)
        sys.exit(2)
    owner, repo = gh_repo.split("/", 1)
    print(f"GitHub Traffic Collector — repo={owner}/{repo} today={TODAY_STR} dry_run={DRY_RUN} out={OUT_PATH}")

    try:
        result = process_repo(owner, repo)
    except Exception as e:
        print(f"UNCAUGHT EXCEPTION: {e}")
        result = "error"

    print("---")
    print(f"RESULT: {result}")
    if result == "error":
        sys.exit(1)


if __name__ == "__main__":
    main()
