import os


def remove_bom_from_file(path):
    try:
        with open(path, "rb") as f:
            data = f.read()
        if data.startswith(b'\xef\xbb\xbf'):
            with open(path, "wb") as f:
                f.write(data[3:])
            print(f"✅ Removed BOM: {path}")
    except Exception as e:
        print(f"⚠️ Skipped {path}: {e}")


def main():
    base_dir = os.getcwd()
    print(f"🔍 Scanning all files under: {base_dir}\n")

    for root, dirs, files in os.walk(base_dir):
        # Skip dot-prefixed folders and common .NET build folders
        dirs[:] = [
            d for d in dirs
            if not d.startswith('.') and d.lower() not in {"bin", "obj", "packages"}
        ]

        for name in files:
            file_path = os.path.join(root, name)
            remove_bom_from_file(file_path)

    print("\n✅ Done scanning all repositories!")


if __name__ == "__main__":
    main()
