import re
import random
import os
import json

def generate_new_id(existing_ids):
    """Generate a new unique positive integer ID."""
    while True:
        new_id = random.randint(10000000, 2000000000)
        if new_id not in existing_ids:
            return new_id

def repair_unity_yaml(scene_path, output_path=None, log_path=None):
    with open(scene_path, "r", encoding="utf-8") as f:
        lines = f.readlines()

    fileid_pattern = re.compile(r"^--- !u!(\d+) &(\d+)")
    ref_pattern = re.compile(r"\{fileID: (\d+)(, guid: [^}]*)?\}")

    seen_ids = {}
    id_replacements = {}
    all_ids = set()

    # First pass: detect duplicates
    for idx, line in enumerate(lines):
        match = fileid_pattern.match(line)
        if match:
            obj_id = int(match.group(2))
            if obj_id in seen_ids:
                # Duplicate detected → generate new ID
                new_id = generate_new_id(all_ids)
                id_replacements.setdefault(obj_id, []).append((idx, new_id))
                all_ids.add(new_id)
            else:
                seen_ids[obj_id] = idx
                all_ids.add(obj_id)

    # Second pass: rewrite file
    for obj_id, replacements in id_replacements.items():
        for (line_idx, new_id) in replacements:
            # Replace the YAML header fileID
            lines[line_idx] = re.sub(rf"&{obj_id}", f"&{new_id}", lines[line_idx])
            # Also fix references in the rest of the file
            for i, l in enumerate(lines):
                if i == line_idx:
                    continue
                if ref_pattern.search(l):
                    lines[i] = re.sub(rf"\{{fileID: {obj_id}([,}}])", f"{{fileID: {new_id}\\1", lines[i])

    # Write output scene file
    out_path = output_path or (scene_path + ".fixed")
    with open(out_path, "w", encoding="utf-8") as f:
        f.writelines(lines)

    # Write undo log
    log_file = log_path or (scene_path + ".idmap.json")
    with open(log_file, "w", encoding="utf-8") as f:
        json.dump(id_replacements, f, indent=2)

    print(f"✅ Repair complete! Scene saved to {out_path}")
    print(f"📝 Undo log saved to {log_file}")

    if id_replacements:
        print("🔧 Changed these IDs:")
        for old_id, reps in id_replacements.items():
            for (_, new_id) in reps:
                print(f"   {old_id} → {new_id}")
    else:
        print("ℹ️ No duplicates found.")


# Example usage
if __name__ == "__main__":
    scene_file = r"C:\Users\Ren\UNITY\LearnSurf3DFixTestCopy\Assets\Scenes\LearnSurf3DWorkplace.unity"
    repair_unity_yaml(scene_file)