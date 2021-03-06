using System.Collections.Generic;

namespace Syncfiles
{
    public class FileScanner
	{
		public static Report Scan(string folder)
		{
			var folderEntry = GetEntryFromDir(folder);
			var entries = folderEntry.Items;
			return new Report(entries);
		}

		public static ReportFile GetEntryFromFile(string file)
		{
			var fileInfo = new MediaFileInfo(file);
			var result = new ReportFile(fileInfo.FilePath, fileInfo.Hash);
			return result;
		}

		public static ReportFolder GetEntryFromDir(string folder)
		{
			var entries = new List<IReportItem>();
			var files = System.IO.Directory.EnumerateFiles(folder);
			var dirs = System.IO.Directory.EnumerateDirectories(folder);

			foreach(var file in files)
			{
				var reportEntry = GetEntryFromFile(file);
				entries.Add(reportEntry);
			}

			foreach(var dir in dirs)
			{
				var reportDir = GetEntryFromDir(dir);
				entries.Add(reportDir);
			}
			
		    return new ReportFolder(folder, entries.ToArray());
		}
	}
}
