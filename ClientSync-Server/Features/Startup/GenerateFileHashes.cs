using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using ClientSync_Server.Models;
using ClientSync_Server.Utils;

namespace ClientSync_Server.Features.Startup
{
    public class GenerateFileHashes
    {
        private static readonly Guid NamespaceId = Guid.Parse("d8943522-22a9-4b90-be66-3a927e975bf5");
        public List<FileHash> ModList { get; } = new List<FileHash>();
        public List<FileHash> ConfigList { get; } = new List<FileHash>();

        public void FileDict()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            IEnumerable<string> modFiles = new List<string>();
            IEnumerable<string> modConfigs =  new List<string>();
            try
            {
                modFiles =
                    Directory.EnumerateFiles(@"mods", "", SearchOption.TopDirectoryOnly);
            } catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Mods folder doesn't exist.");
            }
            
            try
            {
                modConfigs =
                    Directory.EnumerateFiles(@"config", "", SearchOption.AllDirectories);
            } catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Config folder doesn't exist.");
            }

            foreach (var file in modFiles)
            {
#if DEBUG
                Console.WriteLine($"Generating hash: {file}");
#endif
                ModList.Add(new FileHash()
                    {Id = GuidUtility.Create(NamespaceId, file), Path = file, Hash = CalculateMd5(file)});
            }

            foreach (var config in modConfigs)
            {
#if DEBUG
                Console.WriteLine($"Generating hash: {config}");
#endif
                ConfigList.Add(new FileHash()
                    {Id = GuidUtility.Create(NamespaceId, config), Path = config, Hash = CalculateMd5(config)});
            }

            stopwatch.Stop();
            Console.WriteLine("Generating " + (ModList.Count + ConfigList.Count) + " hashes took: " + stopwatch.ElapsedMilliseconds + "ms");
        }

        private static string CalculateMd5(string filename)
        {
            using var md5 = MD5.Create();
            using var stream = File.OpenRead(filename);
            var hash = md5.ComputeHash(stream);
            return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
        }
    }
}