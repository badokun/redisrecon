using System;
using System.CodeDom;
using System.Diagnostics;
using System.IO;
using RedisRecon.Shared;
using ServiceStack.Redis;

namespace RedisRecon.Attacker
{
    public class DynamiteBuilder 
    {
        private readonly Side _side;
        private readonly Battle _battle;
        private readonly string _dynamiteFilePath;
        private readonly StreamWriter _dynamiteFile;
        private string _setKey;
        private string _tntFilePath;
        private StreamWriter _tntFile;
        private string _setKeyWithPayload;
        const string command = "SADD";

        public DynamiteBuilder(Side side, Battle battle)
        {
            _side = side;
            _battle = battle;
            _setKey = string.Format("urn:battle:p:{0}:{1}", _battle.Id, _side);
            _setKeyWithPayload = string.Format("urn:battle:k:{0}:{1}", _battle.Id, _side);

            var directoryName = typeof (DynamiteBuilder).Name;
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }

            _dynamiteFilePath = Path.Combine(directoryName, Path.GetFileName(Path.GetTempFileName()));
            _dynamiteFile = new StreamWriter(_dynamiteFilePath);

            _tntFilePath = Path.Combine(directoryName, Path.GetFileName(Path.GetTempFileName()));
            _tntFile = new StreamWriter(_tntFilePath);
        }

        public void AddGunpowder(Bullet bullet)
        {
          //  var key = string.Format("urn:b:{0}:{1}:{2}", _battle.Id, _side, bullet.Key);
            
            _dynamiteFile.WriteLine($"*3\r\n${command.Length}\r\n{command}\r\n${_setKey.Length}\r\n{_setKey}\r\n${bullet.Key.Length}\r\n{bullet.Key}\r\n");

            var payload = string.Format("{0}|{1}", bullet.Key, bullet.Payload);
            _tntFile.WriteLine($"*3\r\n${command.Length}\r\n{command}\r\n${_setKeyWithPayload.Length}\r\n{_setKeyWithPayload}\r\n${payload.Length}\r\n{payload}\r\n");

            //_dynamiteFile.WriteLine($"*3\r\n$3\r\nSET\r\n${key.Length}\r\n{key}\r\n${bullet.Payload.Length}\r\n{bullet.Payload}\r\n");

            //_tntFile.WriteLine($"*3\r\n$3\r\nSET\r\n${key.Length}\r\n{key}\r\n${bullet.Payload.Length}\r\n{bullet.Payload}\r\n");
        }

        public void Detonate()
        {
            _dynamiteFile.Close();
            _dynamiteFile.Dispose();

            _tntFile.Close();
            _tntFile.Dispose();

            var redisBinaries = @"..\..\..\packages\redis-64.3.0.503\tools";
            /*
             * C:\Projects\redisrecon\src\packages\redis-64.3.0.503\tools>type C:\Projects\redisrecon\src\RedisRecon.Attacker.Console\bin\Debug\DynamiteBuilder\tmp5D48.tmp | redis-cli.exe --pipe
             */

            var sw = Stopwatch.StartNew();
            var process = Process.Start(new ProcessStartInfo("cmd.exe",
                string.Format("/c \"type {0} | redis-cli.exe --pipe\"", Path.GetFullPath(_dynamiteFilePath)))
            {
                WorkingDirectory = redisBinaries,
                WindowStyle = ProcessWindowStyle.Hidden
            });

            process.WaitForExit();
            sw.Stop();
            Console.WriteLine($"Detonation Time: {sw.Elapsed.TotalSeconds} seconds");


            sw = Stopwatch.StartNew();
            process = Process.Start(new ProcessStartInfo("cmd.exe",
                string.Format("/c \"type {0} | redis-cli.exe --pipe\"", Path.GetFullPath(_tntFilePath)))
            {
                WorkingDirectory = redisBinaries,
                WindowStyle = ProcessWindowStyle.Hidden
            });

            process.WaitForExit();
            sw.Stop();
            Console.WriteLine($"TNT Time: {sw.Elapsed.TotalSeconds} seconds");



            File.Delete(_dynamiteFilePath);
        }
    }
}