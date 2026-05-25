#!/usr/bin/env python
"""Migrate the ReciPro GitHub Wiki Markdown into the MkDocs source tree."""

from __future__ import annotations

import os
import re
import shutil
import sys
import unicodedata
from pathlib import Path, PurePosixPath


# 260525Ch: Keep the migration repeatable while the Pages structure settles.
REPO_ROOT = Path(__file__).resolve().parents[2]
WIKI_ROOT = REPO_ROOT.parent / "ReciPro.wiki"
DOCS_SRC = REPO_ROOT / "docs" / "src"

WIKI_LINK_RE = re.compile(r"\[\[([^\[\]]+?)\]\]")
ASSET_LINK_RE = re.compile(
    r"(?P<prefix>\(|src=[\"'])(?P<path>(?:cap-(?:en|ja)-(?:auto|manual)|references)/[^)\"']+)(?P<suffix>\)|[\"'])"
)


def slugify(value: str) -> str:
    value = unicodedata.normalize("NFKD", value)
    value = value.encode("ascii", "ignore").decode("ascii")
    value = re.sub(r"[^A-Za-z0-9]+", "-", value).strip("-").lower()
    return value or "page"


def normalize_page_name(value: str) -> str:
    value = value.strip()
    if value.lower().endswith(".md"):
        value = value[:-3]
    value = value.replace("\\|", "|")
    value = value.replace("/", " ")
    value = re.sub(r"[^\w]+", " ", value, flags=re.UNICODE)
    return " ".join(value.casefold().split())


def wiki_pages() -> list[Path]:
    return sorted(
        p
        for p in WIKI_ROOT.glob("*.md")
        if p.name != "_Sidebar.md" and (p.name == "Home.md" or p.name.startswith(("en.", "ja.")))
    )


def target_for_source(source: Path) -> PurePosixPath:
    name = source.name
    stem = source.stem

    if name == "Home.md":
        return PurePosixPath("index.md")
    if name == "ja.Home.md":
        return PurePosixPath("ja/index.md")

    if stem.startswith("en."):
        return PurePosixPath("en") / f"{slugify(stem[3:])}.md"
    if stem.startswith("ja."):
        return PurePosixPath("ja") / f"{slugify(stem[3:])}.md"

    raise ValueError(f"Unsupported wiki page name: {source.name}")


def page_aliases(source: Path) -> set[str]:
    stem = source.stem
    aliases = {stem, stem.replace("-", " ")}

    if source.name == "Home.md":
        aliases.update({"Home", "en.Home"})
    elif stem.startswith(("en.", "ja.")):
        lang, rest = stem.split(".", 1)
        aliases.add(f"{lang}.{rest.replace('-', ' ')}")

    return {normalize_page_name(alias) for alias in aliases}


def build_page_map(pages: list[Path]) -> dict[str, PurePosixPath]:
    page_map: dict[str, PurePosixPath] = {}

    for source in pages:
        target = target_for_source(source)
        for alias in page_aliases(source):
            page_map.setdefault(alias, target)

    return page_map


def relative_link(current: PurePosixPath, target: PurePosixPath) -> str:
    start = current.parent
    start_text = "." if str(start) == "." else start.as_posix()
    return os.path.relpath(target.as_posix(), start_text).replace("\\", "/")


def convert_wiki_links(text: str, current: PurePosixPath, page_map: dict[str, PurePosixPath], unresolved: list[str]) -> str:
    def repl(match: re.Match[str]) -> str:
        body = match.group(1).replace("\\|", "|").strip()
        if "|" in body:
            label, page = body.split("|", 1)
        else:
            label = body
            page = body

        key = normalize_page_name(page)
        target = page_map.get(key)
        if target is None:
            unresolved.append(f"{current}: {page}")
            return label.strip()

        return f"[{label.strip()}]({relative_link(current, target)})"

    return WIKI_LINK_RE.sub(repl, text)


def convert_asset_links(text: str, current: PurePosixPath) -> str:
    def repl(match: re.Match[str]) -> str:
        target = PurePosixPath("assets") / PurePosixPath(match.group("path"))
        return f"{match.group('prefix')}{relative_link(current, target)}{match.group('suffix')}"

    return ASSET_LINK_RE.sub(repl, text)


def normalize_known_anchors(text: str) -> str:
    text = text.replace("[ファンクション](#ファンクション)", "[ファンクション](#functions)")
    text = text.replace("## ファンクション\n", "## ファンクション {#functions}\n")
    return text


def copy_assets() -> None:
    assets_root = DOCS_SRC / "assets"
    assets_root.mkdir(parents=True, exist_ok=True)

    for name in ("cap-en-auto", "cap-ja-auto", "cap-en-manual", "cap-ja-manual", "references"):
        source = WIKI_ROOT / name
        if not source.exists():
            continue

        target = assets_root / name
        if target.exists():
            shutil.rmtree(target)
        shutil.copytree(source, target, ignore=shutil.ignore_patterns("_capture-log.tsv"))


def clean_generated_pages() -> None:
    for path in (DOCS_SRC / "index.md", DOCS_SRC / "en", DOCS_SRC / "ja"):
        if path.is_dir():
            shutil.rmtree(path)
        elif path.exists():
            path.unlink()


def migrate() -> int:
    if not WIKI_ROOT.exists():
        print(f"Wiki repository was not found: {WIKI_ROOT}", file=sys.stderr)
        return 1

    pages = wiki_pages()
    page_map = build_page_map(pages)
    unresolved: list[str] = []

    clean_generated_pages()
    copy_assets()

    for source in pages:
        target_rel = target_for_source(source)
        target_abs = DOCS_SRC / Path(*target_rel.parts)
        target_abs.parent.mkdir(parents=True, exist_ok=True)

        text = source.read_text(encoding="utf-8-sig")
        text = convert_wiki_links(text, target_rel, page_map, unresolved)
        text = convert_asset_links(text, target_rel)
        text = normalize_known_anchors(text)
        target_abs.write_text(text, encoding="utf-8", newline="\n")

    if unresolved:
        print("Unresolved wiki links:", file=sys.stderr)
        for item in unresolved:
            print(f"  {item}", file=sys.stderr)
        return 1

    print(f"Migrated {len(pages)} wiki pages into {DOCS_SRC}")
    return 0


if __name__ == "__main__":
    raise SystemExit(migrate())
