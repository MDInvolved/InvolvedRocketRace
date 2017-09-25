using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IIR
{
    class Program
    {
        private static string _directory = "C:\\Users\\deschama\\Desktop\\code retreat";

        private static string _obstaclesFileName = "obstacles.json";
        private static string _rocketFileName = "rocket.json";
        private static string _outputFileName = "IRR.json";
        private static GameLoop _theGameLoop;

        private static Obstacles _obstacles;

        static void Main(string[] args)
        {
            _theGameLoop = new GameLoop(_directory, _outputFileName);
            Init();
            Console.ReadLine();
        }

        static void Init()
        {
            new FileWatcher(_directory, _obstaclesFileName, (x, y) => OnObstaclesFileChange());
            new FileWatcher(_directory, _rocketFileName, (x, y) => OnRocketFileChange());
        }

        private static void OnRocketFileChange()
        {
            using (var fileStream =
                File.Open(Path.Combine(_directory, _rocketFileName), FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var file = new StreamReader(fileStream))
            {
                var content = file.ReadLine();
                var rocket = JsonConvert.DeserializeObject<Rocket>(content);
                if (_obstacles != null)
                    _theGameLoop.NextTick(rocket, _obstacles);
            }
        }

        private static void OnObstaclesFileChange()
        {
            using (var file = new StreamReader(Path.Combine(_directory, _obstaclesFileName)))
            {
                var content = file.ReadLine();
                _obstacles = JsonConvert.DeserializeObject<Obstacles>(content);
                Console.WriteLine($"{_obstacles.Items.Count()} Obstacles found");
            }
        }
    }
}
