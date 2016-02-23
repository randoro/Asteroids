using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public static class Globals
    {
        public const int lanes = 8;
        public static Random rand = new Random();
        public const float spawnsPerSecond = 4f;
        public static float minSpeed = 5;
        public static float maxSpeed = 10; //plus one
        public static int MAX_SHOT_LEVEL = 10;
        public static float FU_APPROACH_DIST = 50;
        public static float MAX_AG_SHIP_SPEED = 0.2f;
    }
}
