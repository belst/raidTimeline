﻿using raidTimelineLogic;
using System;
using System.Diagnostics;
using System.IO;

namespace raidTimeline
{
	static class Program
	{
		static void Main(string[] args)
		{
			var path = ParseArgs(args, "-path");
			var outputFileName = ParseArgs(args, "-output", "index.html");
			var token = ParseArgs(args, "-token");
			var watch = ParseArgs(args, "-watch");
			var number = ParseArgs(args, "-number", "25");

			if(!Directory.Exists(path))
			{
				Console.WriteLine("Non-valid path");
				Environment.Exit(1);
			}

			var tc = new TimelineCreator();
			if (token != null)
			{
				tc.CreateTimelineFileFromWeb(path, outputFileName, token, int.Parse(number));
			}
			else if (watch != null)
			{
				tc.CreateTimelineFileFromDisk(path, outputFileName);
				tc.run(path, outputFileName);
			}
			else
            {
				tc.CreateTimelineFileFromDisk(path, outputFileName);
			}

            var htmlFilePath = Path.Combine(path, outputFileName);

            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = htmlFilePath,
                UseShellExecute = true
            };
            Process.Start(psi);
        }

		static string ParseArgs(string[] args, string search, string defaultValue = null)
		{
			var pathIndex = Array.IndexOf(args, search);

			if (pathIndex >= 0 && args.Length >= pathIndex + 1)
			{
				return args[pathIndex + 1];
			}
			else
			{
				return defaultValue;
			}
		}
	}
}
