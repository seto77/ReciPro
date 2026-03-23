# -*- coding: utf-8 -*-
"""Replace wiki image references from local images/ to raw GitHub URLs."""
import os
import re

WIKI_DIR = r"C:\Users\seto\AppData\Local\Temp\ReciPro.wiki"
BASE_URL = "https://raw.githubusercontent.com/seto77/ReciPro/master/ReciPro/doc/capture"

# Mapping: short name used in images/ -> original capture filename
RENAME_MAP = {
    "FormDiffractionSimulator.waveLengthControl.png":
        "FormDiffractionSimulator.groupBoxSpotProperty.panel2.flowLayoutPanel11.waveLengthControl.png",
    "FormDiffractionSimulator.beamMode.png":
        "FormDiffractionSimulator.groupBoxSpotProperty.panel2.flowLayoutPanel5.flowLayoutPanel10.png",
    "FormDiffractionSimulator.intensityCalc.png":
        "FormDiffractionSimulator.groupBoxSpotProperty.panel2.flowLayoutPanel3.png",
    "FormDiffractionSimulator.bethe.png":
        "FormDiffractionSimulator.groupBoxSpotProperty.panel2.flowLayoutPanelBethe.png",
    "FormDiffractionSimulator.appearance.png":
        "FormDiffractionSimulator.groupBoxSpotProperty.panel2.flowLayoutPanelAppearance.png",
    "FormDiffractionSimulator.ped.png":
        "FormDiffractionSimulator.groupBoxSpotProperty.panel2.flowLayoutPanelPED.png",
    "FormMain.tabPageBasicInfo.png":
        "FormMain.toolStripContainer1.splitContainer.groupBox6.crystalControl.tabControl.tabPageBasicInfo.png",
    "FormMain.tabPageAtom.png":
        "FormMain.toolStripContainer1.splitContainer.groupBox6.crystalControl.tabControl.tabPageAtom.png",
    "FormMain.tabPageReference.png":
        "FormMain.toolStripContainer1.splitContainer.groupBox6.crystalControl.tabControl.tabPageReference.png",
    "FormMain.tabPageEOS.png":
        "FormMain.toolStripContainer1.splitContainer.groupBox6.crystalControl.tabControl.tabPageEOS.png",
    "FormMain.tabPageElasticity.png":
        "FormMain.toolStripContainer1.splitContainer.groupBox6.crystalControl.tabControl.tabPageElasticity.png",
    "FormMain.menuStrip.png":
        "FormMain.toolStripContainer1.menuStrip1.png",
    "FormImageSimulator.splitContainer.png":
        "FormImageSimulator.splitContainer1.png",
}

def fix_image_ref(match):
    """Replace images/xxx.png with raw GitHub URL."""
    filename = match.group(1)
    # If it was renamed, map back to original
    original = RENAME_MAP.get(filename, filename)
    return f"]({BASE_URL}/{original})"

count = 0
for fname in os.listdir(WIKI_DIR):
    if not fname.endswith(".md"):
        continue
    fpath = os.path.join(WIKI_DIR, fname)
    with open(fpath, "r", encoding="utf-8") as f:
        content = f.read()

    # Match ](images/FILENAME.png)
    new_content = re.sub(r"\]\(images/([^)]+\.png)\)", fix_image_ref, content)

    if new_content != content:
        with open(fpath, "w", encoding="utf-8") as f:
            f.write(new_content)
        replacements = content.count("images/") - new_content.count("images/")
        print(f"Updated: {fname} ({replacements} refs)")
        count += 1

print(f"\nDone: {count} files updated")
