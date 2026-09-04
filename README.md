# FileUtilityZero

A Windows desktop utility that recursively scans a folder and all of its
subfolders, collecting file metadata for every file found, and exports the
results to CSV.

## Features

- Recursive scan of a folder tree, collecting name, path, size, and
  timestamp data for every file
- Live progress display while a scan is running
- Automatic CSV export during the scan, plus a separate on-demand export
- Optional SHA-256 file hashing (off by default — increases scan time)
- Optional file categorization by type (e.g. Code, Image, Document — off by
  default)
- Rejects unsupported UNC network paths before scanning, to avoid
  unintended SMB authentication attempts against remote hosts

## Requirements

- Windows
- [.NET 10 Desktop Runtime](https://dotnet.microsoft.com/download/dotnet/10.0)
  (or the .NET 10 SDK, if building from source)

## Installation

Clone the repository and build with Visual Studio 2022 (or later) or the
.NET CLI:

```bash
git clone https://github.com/<your-org-or-username>/FileUtilityZero.git
cd FileUtilityZero
dotnet build
```

Run `FileUtilityZero.UI` from Visual Studio, or launch the built executable
directly.

## Usage

1. Set the working directory. Either type in the path to the folder you
   wish to scan or click the **Browse** button and select the folder you
   wish to scan. Local drives and mapped network drives are supported; UNC
   network paths (e.g. `\\server\share`) are not.
2. Optionally, check **Include file hash (SHA-256)** and/or
   **Include file category** if you want those fields in your results.
   Both are off by default, since they slow down scans of large directory
   trees.
3. Click **Run**. The app will gather information from all files in all
   folders and subfolders in the path selected. The status window will
   populate while the scan is running. As it runs, the results are also
   written automatically to a timestamped CSV file named
   `files_auto_<date>-<time>.csv` in `C:\File Utility Zero`.
4. Once the scan is complete, click **Export csv** to export the same
   collected data to a separate timestamped CSV file named
   `files_export_<date>_<time>.csv`, also saved in `C:\File Utility Zero`.
5. Click **Exit** to close the application.

## License

This project is licensed under the [GPL-3.0 License](LICENSE).
