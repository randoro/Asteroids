using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public static class Globals
    {
        public const int windowX = 1200;
        public const int windowY = 800;
        public static Random rand = new Random();
        public static float VisionDistance = 50;
        public static int MaxBalls = 50;
    }
}
