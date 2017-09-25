using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IIR
{
    public class Obstacle
    {
        public Point TopLeft { get; set; }
        public Point TopRight { get; set; }
        public Point BottomRight { get; set; }
        public Point BottomLeft { get; set; }
        
        public int[] Xs => (new[] { TopLeft.x, TopRight.x, BottomRight.x, BottomLeft.x }).Select(x => (int)Math.Ceiling(x)).ToArray();
        public int[] Ys => (new[] { TopLeft.y, TopRight.y, BottomRight.y, BottomLeft.y }).Select(x => (int)Math.Ceiling(x)).ToArray();

    }
}
