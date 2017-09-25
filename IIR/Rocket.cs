using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IIR
{
    public class Rocket
    {
        public Point TopLeft { get; set; }
        public Point TopRight { get; set; }
        public Point BottomRight { get; set; }
        public Point BottomLeft { get; set; }
        public Point Velocity { get; set; }

        private int[] _xs => (new[] { TopLeft.x, TopRight.x, BottomRight.x, BottomLeft.x }).Select(x => (int)Math.Ceiling(x)).ToArray();
        private int[] _ys => (new[] { TopLeft.y, TopRight.y, BottomRight.y, BottomLeft.y }).Select(x => (int)Math.Ceiling(x)).ToArray();

        public Rocket GetFuturePosition(int ticks)
        {
            return new Rocket()
            {
                TopLeft = this.TopLeft.AddVector(ticks, this.Velocity),
                TopRight = this.TopRight.AddVector(ticks, this.Velocity),
                BottomRight = this.BottomRight.AddVector(ticks, this.Velocity),
                BottomLeft = this.BottomLeft.AddVector(ticks, this.Velocity),
            };
        }

        public bool WillCrash(Obstacles obstacles)
        {
            foreach (var tick in Enumerable.Range((int)Math.Ceiling(this.Velocity.x), 30 * (int)Math.Ceiling(this.Velocity.x)))
            {
                var futurePosition = this.GetFuturePosition(tick);
                foreach (var obstacle in obstacles.Items.OrderBy(o => o.BottomLeft.x))
                {
                    if (futurePosition.IsObstacleInRange(obstacle))
                        return true;
                }
            }
            return false;
        }

        private bool IsObstacleInRange(Obstacle obstacle)
        {
            var lowestX = _xs.Min();
            var lowestY = _ys.Min();
            var highestX = _xs.Max();
            var highestY = _ys.Max();

            var lowestObstacleX = obstacle.Xs.Min();
            var lowestObstacleY = obstacle.Ys.Min();
            var highestObstacleX = obstacle.Xs.Max();
            var highestObstacleY = obstacle.Ys.Max();

            var rocketRectangle = new Rectangle(lowestX, lowestY, highestX - lowestX, highestY - lowestY);
            var obstacleRectangle = new Rectangle(lowestObstacleX, lowestObstacleY, highestObstacleX - lowestObstacleX, highestObstacleY - lowestObstacleY);

            return rocketRectangle.IntersectsWith(obstacleRectangle);
        }
    }
}