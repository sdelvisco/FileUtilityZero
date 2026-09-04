# FileUtilityZero

Utility application to scan a path and all subfolders and get FileInfo data for all files.

Usage:
1. Set the working directory. Either type in the path to the folder you wish to scan or click the "Browse" button and select the folder you wish to scan. Local drives and mapped network drives are supported; UNC network paths (e.g. `\\server\share`) are not.
2. Click the "Run" button. The app will gather information from all files in all folders and subfolders in the path selected. The status windows will populate while the scan is running. As it runs, the results are also written automatically to a timestamped CSV file named `files_auto_<date>-<time>.csv` in `C:\File Utility Zero`.
3. Once the scan is complete, click the "Export csv" button to export the same collected data to a separate timestamped CSV file named `files_export_<date>_<time>.csv`, also saved in `C:\File Utility Zero`.
4. Click "Exit" to close the application.
