namespace IIR
{
    public class Point
    {
        public float x { get; set; }
        public float y { get; set; }

        public Point AddVector(int ticks, Point vectorToAdd)
        {
            return new Point
            {
                x = this.x + ticks * vectorToAdd.x,
                y = this.y + ticks * vectorToAdd.y
            };
        }
    }
}