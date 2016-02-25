using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Asteroids
{
    public static class Globals
    {
        public const int lanes = 8;
        public const int windowX = 1200;
        public const int windowY = 800;
        public static Random rand = new Random();
        public const float spawnsPerSecond = 4f;
        public static float minSpeed = 5;
        public static float maxSpeed = 10; //plus one
        
        public static float FU_APPROACH_DIST = 50;
        public static float MAX_AG_SHIP_SPEED = 0.2f;

        public static int MAX_SHOT_LEVEL = 10;
        public static float FU_POWERUP_SCAN_DIST = 10;
    }
}
