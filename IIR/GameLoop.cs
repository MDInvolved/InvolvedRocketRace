using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIR
{
    public class GameLoop
    {
        private readonly string _directory;
        private readonly string _outputFileName;
        private int _started = 0;
        private bool _resetNow = true;

        public GameLoop(string directory, string outputFileName)
        {
            _directory = directory;
            _outputFileName = outputFileName;
        }

        private InputCommand StartRocket()
        {
            var command = new InputCommand().SpeedUp();

            return command;
        }

        private InputCommand EnsureStarted()
        {
            if (_started > 1)
                return null;

            _started++;
            return StartRocket();
        }

        public void NextTick(Rocket rocket, Obstacles obstacles)
        {
            _resetNow = !_resetNow;
            var command = EnsureStarted() ?? (_resetNow ? new InputCommand().Reset() : ProcessNextTick(rocket, obstacles));
            FlushCommand(command);
        }

        private InputCommand ProcessNextTick(Rocket rocket, Obstacles obstacles)
        {
            var obstaclesOnCrashCourse = rocket.WillCrash(obstacles);

            if (obstaclesOnCrashCourse)
            {
                Console.WriteLine("will collide with obstacles");
                var aboveMiddle = (rocket.BottomRight.y + rocket.TopRight.y) / 2 >= 258;
                return aboveMiddle ? new InputCommand().GoDown().SlowDown() : new InputCommand().GoUp().SlowDown();
            }

            return new InputCommand().SpeedUp();
        }

        private void FlushCommand(InputCommand command)
        {
            File.WriteAllText(Path.Combine(_directory, _outputFileName), command.ToString());
        }
    }
}
