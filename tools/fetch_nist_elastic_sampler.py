#!/usr/bin/env python3
# (260331Ch) Download NIST SRD 64 sampler files and store compact float32 E_XX.BIN files.

from __future__ import annotations

import argparse
import os
import struct
import tempfile
from pathlib import Path

from playwright.sync_api import sync_playwright


# DEFAULT_OUTPUT_DIR = Path(os.environ["LOCALAPPDATA"]) / "ReciPro" / "MonteCarlo" / "NistElasticSampler"  # (260331Ch) 旧既定値
DEFAULT_OUTPUT_DIR = Path(__file__).resolve().parent.parent / "ReciPro" / "NistElasticSampler"  # (260331Ch) 配布用に repo 配下へ保存
NIST_ELASTIC_BINARY_MAGIC = 0x4253454E  # (260331Ch) 'NESB' little endian
NIST_ELASTIC_BINARY_VERSION = 1  # (260331Ch)
NIST_ELASTIC_ENERGY_COUNT = 101  # (260331Ch)
NIST_ELASTIC_PHI_COUNT = 2001  # (260331Ch)


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Download NIST SRD64 elastic-scattering sampler files.")
    parser.add_argument("--output-dir", default=str(DEFAULT_OUTPUT_DIR), help="Destination directory for E_XX.BIN files.")
    parser.add_argument("--force", action="store_true", help="Redownload files even if they already exist.")
    parser.add_argument("--retries", type=int, default=3, help="Retry count per atomic number when the NIST download times out.")  # (260331Ch)
    parser.add_argument("atomic_numbers", nargs="+", type=int, help="Atomic numbers to download.")
    return parser.parse_args()


def convert_text_sampler_to_binary(text_path: Path, binary_path: Path) -> None:
    with text_path.open("r", encoding="utf-8", errors="ignore") as src, binary_path.open("wb") as dst:
        dst.write(struct.pack("<4i", NIST_ELASTIC_BINARY_MAGIC, NIST_ELASTIC_BINARY_VERSION, NIST_ELASTIC_ENERGY_COUNT, NIST_ELASTIC_PHI_COUNT))
        for _ in range(NIST_ELASTIC_ENERGY_COUNT):
            block = src.readline()
            sigma = src.readline()
            if not block or not sigma:
                raise ValueError(f"Incomplete sampler header in {text_path}")

            dst.write(struct.pack("<f", float(sigma.strip())))
            phi_values: list[float] = []
            for _ in range(NIST_ELASTIC_PHI_COUNT):
                phi = src.readline()
                if not phi:
                    raise ValueError(f"Incomplete sampler CPD data in {text_path}")
                phi_values.append(float(phi.strip()))
            dst.write(struct.pack(f"<{NIST_ELASTIC_PHI_COUNT}f", *phi_values))


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
            destination = output_dir / f"E_{atomic_number:02d}.BIN"
            legacy_text_path = output_dir / f"E_{atomic_number:02d}.TXT"
            if destination.exists() and not args.force:
                print(f"skip {atomic_number:02d} -> {destination}")
                continue
            if legacy_text_path.exists() and not args.force:
                convert_text_sampler_to_binary(legacy_text_path, destination)
                print(f"converted {atomic_number:02d} -> {destination}")
                continue

            url = f"https://srdata.nist.gov/srd64/ElasticSimpler/SimplerDownload/{atomic_number}"
            last_error = None
            for attempt in range(1, max(args.retries, 1) + 1):  # (260331Ch)
                temp_path = None
                try:
                    page.goto(url, wait_until="domcontentloaded", timeout=60000)
                    page.wait_for_timeout(5000)
                    with page.expect_download(timeout=60000) as download_info:
                        page.get_by_text("Numerical data for the sampler").click()
                    download = download_info.value
                    with tempfile.NamedTemporaryFile(delete=False, suffix=".TXT") as temp_file:
                        temp_path = Path(temp_file.name)
                    download.save_as(str(temp_path))
                    convert_text_sampler_to_binary(temp_path, destination)
                    print(f"saved {atomic_number:02d} -> {destination}")
                    last_error = None
                    break
                except Exception as exc:
                    last_error = exc
                    print(f"retry {atomic_number:02d} attempt {attempt}/{max(args.retries, 1)}: {exc}")  # (260331Ch)
                    page.wait_for_timeout(2000)
                finally:
                    if temp_path is not None and temp_path.exists():
                        temp_path.unlink()
            if last_error is not None:
                raise last_error
        browser.close()
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
