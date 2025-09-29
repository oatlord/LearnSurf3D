import os, re, uuid, collections, shutil

PROJECT_ROOT = os.getcwd()
ASSETS_ROOT = os.path.join(PROJECT_ROOT, "Assets")

fileid_re = re.compile(r"^--- !u!\d+ &(\d+)")
guid_re = re.compile(r"guid:\s*([a-f0-9]{32})")

# Track changes for summary
summary_changes = collections.defaultdict(lambda: {"new": None, "files": []})

def fix_scene_fileids(filepath):
    """Fix duplicate &fileIDs in .unity or .prefab YAML files."""
    with open(filepath, "r", encoding="utf-8") as f:
        lines = f.readlines()

    seen = {}
    changed = False

    for i, line in enumerate(lines):
        m = fileid_re.match(line)
        if m:
            fid = m.group(1)
            if fid in seen:
                new_fid = str(uuid.uuid4().int % 10**9)
                print(f"⚠️ Duplicate fileID {fid} in {filepath} (line {i+1}), replaced with {new_fid}")
                lines[i] = line.replace(fid, new_fid)
                changed = True
            else:
                seen[fid] = i+1

    if changed:
        backup = filepath + ".bak"
        shutil.copy(filepath, backup)
        with open(filepath, "w", encoding="utf-8") as f:
            f.writelines(lines)
        print(f"✅ Fixed fileIDs in {filepath} (backup at {backup})")

def find_meta_duplicates():
    """Scan Assets/ for duplicate GUIDs, return mapping {guid: [paths]}."""
    seen = {}
    duplicates = collections.defaultdict(list)

    for subdir, _, files in os.walk(ASSETS_ROOT):
        for file in files:
            if file.endswith(".meta"):
                path = os.path.join(subdir, file)
                with open(path, "r", encoding="utf-8") as f:
                    for line in f:
                        m = guid_re.search(line)
                        if m:
                            guid = m.group(1)
                            if guid in seen:
                                duplicates[guid].append(path)
                            else:
                                seen[guid] = path
                            break

    return duplicates, seen

def replace_guid_in_yaml(filepath, replacements):
    """Replace GUIDs in .unity/.prefab with updated ones."""
    with open(filepath, "r", encoding="utf-8") as f:
        content = f.read()

    changed = False
    for old_guid, new_guid in replacements.items():
        if old_guid in content:
            content = content.replace(old_guid, new_guid)
            changed = True
            summary_changes[old_guid]["files"].append(filepath)

    if changed:
        backup = filepath + ".bak"
        shutil.copy(filepath, backup)
        with open(filepath, "w", encoding="utf-8") as f:
            f.write(content)

def fix_meta_guids(mode="merge"):
    """
    Fix duplicate GUIDs.
    mode="merge": keep one, delete others, update refs.
    mode="regen": assign new GUIDs to duplicates, update refs.
    """
    duplicates, seen = find_meta_duplicates()
    if not duplicates:
        print("✅ No duplicate GUIDs in Assets/")
        return

    replacements = {}

    for guid, dups in duplicates.items():
        keep_path = seen[guid]
        if mode == "merge":
            print(f"⚠️ GUID {guid} duplicated:")
            print(f"   Keeping {keep_path}")
            summary_changes[guid]["new"] = guid
            for dup in dups:
                print(f"   Deleting {dup}")
                os.remove(dup)
                replacements[guid] = guid  

        elif mode == "regen":
            print(f"⚠️ GUID {guid} duplicated, regenerating:")
            print(f"   Keeping {keep_path}")
            for dup in dups:
                new_guid = uuid.uuid4().hex
                print(f"   Assigning NEW GUID {new_guid} to {dup}")
                with open(dup, "r", encoding="utf-8") as f:
                    content = f.read()
                content = re.sub(r"guid:\s*[a-f0-9]{32}", f"guid: {new_guid}", content)
                with open(dup, "w", encoding="utf-8") as f:
                    f.write(content)
                replacements[guid] = new_guid
                summary_changes[guid]["new"] = new_guid

    # Apply replacements
    if replacements:
        print("\n🔎 Updating references in scenes/prefabs...")
        for subdir, _, files in os.walk(ASSETS_ROOT):
            for file in files:
                if file.endswith((".unity", ".prefab")):
                    replace_guid_in_yaml(os.path.join(subdir, file), replacements)

    print("✅ GUID fixing complete.")

def print_summary():
    """Print summary of GUID changes."""
    if not summary_changes:
        print("\n📋 Summary: No GUID changes.")
        return

    print("\n📋 Summary of GUID fixes:")
    for old_guid, data in summary_changes.items():
        new_guid = data["new"]
        files = data["files"]
        status = f"{old_guid} → {new_guid}" if old_guid != new_guid else f"{old_guid} (kept)"
        print(f"  {status}")
        for f in files:
            print(f"     ↳ updated in {f}")

def main():
    print("🔎 Fixing duplicate fileIDs in scenes/prefabs...")
    for subdir, _, files in os.walk(ASSETS_ROOT):
        for file in files:
            if file.endswith((".unity", ".prefab")):
                fix_scene_fileids(os.path.join(subdir, file))

    print("\n🔎 Fixing duplicate GUIDs in Assets/...")

    mode = input("Choose mode: [merge/regen] ").strip().lower()
    if mode not in ("merge", "regen"):
        print("❌ Invalid mode, defaulting to merge.")
        mode = "merge"

    fix_meta_guids(mode)
    print_summary()

    print("\n🎉 Repair completed. Reopen Unity and let it reimport assets.")

if __name__ == "__main__":
    main()