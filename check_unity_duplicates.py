import re
from collections import defaultdict

# Path to your scene file
scene_file = "Assets/Scenes/LearnSurf3DWorkplace.unity"

# Store occurrences with line numbers
occurrences = defaultdict(list)

# Read all lines so we can grab context later
with open(scene_file, "r", encoding="utf-8", errors="ignore") as f:
    lines = f.readlines()

# Scan for fileIDs
for lineno, line in enumerate(lines, start=1):
    match = re.search(r'&\d+', line)
    if match:
        occurrences[match.group()].append(lineno)

print("🔎 Checking for duplicate fileIDs...\n")

duplicates_found = False
for fid, line_numbers in occurrences.items():
    if len(line_numbers) > 1:
        duplicates_found = True
        print(f"\n❌ Duplicate {fid} found on lines {', '.join(map(str, line_numbers))}")
        
        # Show context around each occurrence
        for ln in line_numbers:
            start = max(0, ln - 2)   # two lines before
            end = min(len(lines), ln + 3)  # two lines after
            snippet = ''.join(lines[start:end]).strip()
            print(f"\n--- Context near line {ln} ---")
            print(snippet)
            print("------------------------------")

if not duplicates_found:
    print("✅ No duplicates found. FileIDs are unique.")
