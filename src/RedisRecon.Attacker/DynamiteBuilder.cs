using System;
using System.Diagnostics;
using System.IO;
using RedisRecon.Shared;
using ServiceStack.Redis;

namespace RedisRecon.Attacker
{
    public class DynamiteBuilder 
    {
        private readonly string _dynamiteFilePath;
        private readonly StreamWriter _dynamiteFile;

        public DynamiteBuilder()
        {
            var directoryName = typeof (DynamiteBuilder).Name;
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
            _dynamiteFilePath = Path.Combine(directoryName, Path.GetFileName(Path.GetTempFileName()));
            _dynamiteFile = new StreamWriter(_dynamiteFilePath);
        }

        public void Add(Bullet bullet)
        {
            _dynamiteFile.WriteLine($"*3\r\n$3\r\nSET\r\n${bullet.Key.Length}\r\n{bullet.Key}\r\n${bullet.Payload.Length}\r\n{bullet.Payload}\r\n");
        }

        public void Detonate()
        {
            _dynamiteFile.Close();
            _dynamiteFile.Dispose();

            var redisBinaries = @"..\..\..\packages\redis-64.3.0.503\tools";
            /*
             * C:\Projects\redisrecon\src\packages\redis-64.3.0.503\tools>type C:\Projects\redisrecon\src\RedisRecon.Attacker.Console\bin\Debug\DynamiteBuilder\tmp5D48.tmp | redis-cli.exe --pipe
             */

            var sw = Stopwatch.StartNew();
            
            var process = Process.Start(new ProcessStartInfo("cmd.exe",
                string.Format("/c \"type {0} | redis-cli.exe --pipe\"", Path.GetFullPath(_dynamiteFilePath)))
            {
                WorkingDirectory = redisBinaries,
                WindowStyle = ProcessWindowStyle.Maximized
            });

            process.WaitForExit();
            sw.Stop();
            Console.WriteLine($"Detonation Time: {sw.Elapsed.TotalSeconds} seconds");
        }
    }
}