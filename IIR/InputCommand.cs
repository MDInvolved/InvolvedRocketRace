using Newtonsoft.Json;

namespace IIR
{
    public class InputCommand
    {
        public bool ForwardThrust { get; set; }
        public bool UpwardThrust { get; set; }
        public bool DownwardThrust { get; set; }
        public bool ColdGasBreak { get; set; }

        public InputCommand Reset()
        {
            ForwardThrust = false;
            ColdGasBreak = false;
            DownwardThrust = false;
            UpwardThrust = false;
            return this;
        }

        public InputCommand SpeedUp()
        {
            ForwardThrust = true;
            ColdGasBreak = false;
            return this;
        }

        public InputCommand SlowDown()
        {
            ColdGasBreak = true;
            ForwardThrust = false;
            return this;
        }

        public InputCommand GoUp()
        {
            UpwardThrust = true;
            DownwardThrust = false;
            return this;
        }

        public InputCommand GoDown()
        {
            DownwardThrust = true;
            UpwardThrust = false;
            return this;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}