using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KevinKeyserParticle
{
    public static class RandomExtension
    {
        public static Vector2 NextVector2(this Random randomGenerator, Vector2 min, Vector2 max)
        {
            return new Vector2((float)randomGenerator.NextDouble() * (max.X - min.X) + min.X, (float)randomGenerator.NextDouble() * (max.Y - min.Y) + min.Y);
        }

        public static TimeSpan NextTimeSpan(this Random randomGenerator, TimeSpan min, TimeSpan max)
        {
            return TimeSpan.FromTicks((long)(randomGenerator.NextDouble() * (max.Ticks - min.Ticks) + min.Ticks));
        }
    }
}
