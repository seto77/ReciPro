#!/usr/bin/env python3
# (260331Ch) Download NIST SRD 64 sampler files and store E_XX.TXT in a local cache.

from __future__ import annotations

import argparse
import os
from pathlib import Path

from playwright.sync_api import sync_playwright


DEFAULT_OUTPUT_DIR = Path(os.environ["LOCALAPPDATA"]) / "ReciPro" / "MonteCarlo" / "NistElasticSampler"


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Download NIST SRD64 elastic-scattering sampler files.")
    parser.add_argument("--output-dir", default=str(DEFAULT_OUTPUT_DIR), help="Destination directory for E_XX.TXT files.")
    parser.add_argument("--force", action="store_true", help="Redownload files even if they already exist.")
    parser.add_argument("atomic_numbers", nargs="+", type=int, help="Atomic numbers to download.")
    return parser.parse_args()


def main() -> int:
    args = parse_args()
    output_dir = Path(args.output_dir)
    output_dir.mkdir(parents=True, exist_ok=True)

    atomic_numbers = sorted(dict.fromkeys(args.atomic_numbers))
    invalid = [z for z in atomic_numbers if z < 1 or z > 96]
    if invalid:
        raise SystemExit(f"Unsupported atomic numbers: {invalid}. Expected 1..96.")

    with sync_playwright() as playwright:
        browser = playwright.chromium.launch(headless=True)
        context = browser.new_context(accept_downloads=True)
        page = context.new_page()
        for atomic_number in atomic_numbers:
            destination = output_dir / f"E_{atomic_number:02d}.TXT"
            if destination.exists() and not args.force:
                print(f"skip {atomic_number:02d} -> {destination}")
                continue

            url = f"https://srdata.nist.gov/srd64/ElasticSimpler/SimplerDownload/{atomic_number}"
            page.goto(url, wait_until="domcontentloaded", timeout=60000)
            page.wait_for_timeout(5000)
            with page.expect_download(timeout=60000) as download_info:
                page.get_by_text("Numerical data for the sampler").click()
            download = download_info.value
            download.save_as(destination)
            print(f"saved {atomic_number:02d} -> {destination}")
        browser.close()
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
