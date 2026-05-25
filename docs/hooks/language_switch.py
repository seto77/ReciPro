"""Inject ReciPro language links into MkDocs pages."""

from __future__ import annotations

import os
import re
from pathlib import PurePosixPath


# 260525Ch: Keep the language switch in one build hook, not in every Markdown page.
LANGUAGE_SWITCH_RE = re.compile(r"(?m)^🌐 .*(?:English|日本語).*\n(?:\n)?")
TARGET_OVERRIDES = {
    PurePosixPath("en/0-1-crystal-orientation-control.md"): PurePosixPath("ja/0-main-window.md"),
}


def _relative_link(current: PurePosixPath, target: PurePosixPath) -> str:
    start = current.parent
    start_text = "." if str(start) == "." else start.as_posix()
    return os.path.relpath(target.as_posix(), start_text).replace("\\", "/")


def _language_targets(source: PurePosixPath, available: set[PurePosixPath]) -> tuple[str, PurePosixPath, PurePosixPath] | None:
    if source == PurePosixPath("index.md"):
        current_lang = "en"
        english = source
        japanese = PurePosixPath("ja/index.md")
    elif len(source.parts) >= 2 and source.parts[0] == "en":
        current_lang = "en"
        english = source
        japanese = PurePosixPath("ja") / source.name
    elif len(source.parts) >= 2 and source.parts[0] == "ja":
        current_lang = "ja"
        japanese = source
        english = PurePosixPath("index.md") if source.name == "index.md" else PurePosixPath("en") / source.name
    else:
        return None

    japanese = TARGET_OVERRIDES.get(source, japanese)

    if english not in available:
        english = PurePosixPath("index.md")
    if japanese not in available:
        japanese = PurePosixPath("ja/index.md")

    return current_lang, english, japanese


def on_page_markdown(markdown: str, page, config, files) -> str:
    source = PurePosixPath(page.file.src_uri)
    available = {
        PurePosixPath(file.src_uri)
        for file in files
        if getattr(file, "src_uri", "").endswith(".md")
    }
    targets = _language_targets(source, available)

    markdown = LANGUAGE_SWITCH_RE.sub("", markdown, count=1)
    if targets is None:
        return markdown

    current_lang, english, japanese = targets
    english_link = "**English**" if current_lang == "en" else f"[English]({_relative_link(source, english)})"
    japanese_link = "**日本語**" if current_lang == "ja" else f"[日本語]({_relative_link(source, japanese)})"
    switch = f"🌐 {english_link}  |  {japanese_link}\n\n"

    marker = "<!-- nav -->\n\n"
    if markdown.startswith(marker):
        return marker + switch + markdown[len(marker):].lstrip("\n")

    return switch + markdown
