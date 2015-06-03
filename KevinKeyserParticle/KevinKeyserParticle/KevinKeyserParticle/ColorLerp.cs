using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace KevinKeyserParticle
{
    public class ColorLerp
    {
        private Color startColor;

        public Color StartColor
        {
            get { return startColor; }
            set { startColor = value; }
        }

        private Color endColor;

        public Color EndColor
        {
            get { return endColor; }
            set { endColor = value; }
        }

        public ColorLerp(Color startColor, Color endColor)
        {
            this.startColor = startColor;
            this.endColor = endColor;
        }
    }
}
