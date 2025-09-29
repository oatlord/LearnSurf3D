import os
import re
from collections import defaultdict

# Regex to match Unity GUID lines
guid_pattern = re.compile(r"guid:\s*([a-f0-9]{32})")

# Dictionary to hold guid -> list of (filepath, line_number)
guids = defaultdict(list)

# Start from current directory (run this from Unity project root)
root_dir = os.getcwd()

for subdir, _, files in os.walk(root_dir):
    for file in files:
        if file.endswith(".meta"):
            filepath = os.path.join(subdir, file)
            try:
                with open(filepath, "r", encoding="utf-8") as f:
                    for i, line in enumerate(f, start=1):
                        match = guid_pattern.search(line)
                        if match:
                            guid = match.group(1)
                            guids[guid].append((filepath, i))
            except Exception as e:
                print(f"⚠️ Could not read {filepath}: {e}")

# Print duplicates
duplicates_found = False
for guid, locations in guids.items():
    if len(locations) > 1:
        duplicates_found = True
        print(f"\n🚨 Duplicate GUID found: {guid}")
        for filepath, line in locations:
            print(f"   {filepath}:{line}")

if not duplicates_found:
    print("\n✅ No duplicate GUIDs found.")
