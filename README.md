# FileUtilityZero

A Windows desktop utility that recursively scans a folder and all of its
subfolders, collecting file metadata for every file found, and exports the
results to CSV.

## Features

- Recursive scan of a folder tree, collecting name, path, size, and
  timestamp data for every file
- Results shown in a sortable, searchable grid that populates as the scan
  runs, plus an indeterminate progress bar while it's in progress
- On-demand CSV export of the current results
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
   folders and subfolders in the path selected. A progress bar is shown
   while the scan runs (in the background, so the app stays responsive),
   and results are added to the grid as they're found.
4. Once the scan is complete, optionally type into the search box above
   the grid to filter the results (matches any column, case-insensitive),
   or click a column header to sort by that column - click it again to
   reverse the sort direction.
5. Click **Export csv** to export the full set of results (regardless of
   any text currently in the search box) to a timestamped CSV file named
   `files_export_<date>_<time>.csv`, saved in `C:\File Utility Zero`.
6. Click **Exit** to close the application.

## Building a Windows Installer

For maintainers cutting a release: see [BUILDING.md](BUILDING.md) for how
to produce a self-contained `Setup.exe` with Inno Setup.

## License

This project is licensed under the [GPL-3.0 License](LICENSE.txt).
