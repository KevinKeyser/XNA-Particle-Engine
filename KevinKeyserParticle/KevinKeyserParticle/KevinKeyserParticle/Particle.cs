using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace KevinKeyserParticle
{
    public class Particle
    {
        private Texture2D texture;

        public Texture2D Texture
        {
            get { return texture; }
            set { texture = value; }
        }

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private Rectangle sourceRectangle;

        public Rectangle SourceRectangle
        {
            get { return sourceRectangle; }
            set { sourceRectangle = value; }
        }
        
        private Color color;

        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

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

        private float rotation;

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        private Vector2 origin;

        public Vector2 Origin
        {
            get { return origin; }
            set { origin = value; }
        }

        private Vector2 scale;

        public Vector2 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        private SpriteEffects effects;

        public SpriteEffects Effects
        {
            get { return effects; }
            set { effects = value; }
        }

        private float layerDepth;

        public float LayerDepth
        {
            get { return layerDepth; }
            set { layerDepth = value; }
        }

        private TimeSpan life;

        public TimeSpan Life
        {
            get { return life; }
            set { life = value; }
        }

        private TimeSpan elapsedLife;

        private Vector2 velocity;

        public Vector2 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }

        public bool isDead;

        public Particle(Texture2D texture, Vector2 position, TimeSpan life, Vector2 velocity)
        {
            this.texture = texture;
            this.position = position;
            sourceRectangle = new Rectangle(0,0,texture.Width, texture.Height);
            color = Color.White;
            startColor = color;
            endColor = color;
            rotation = 0;
            origin = Vector2.Zero;
            scale = Vector2.One;
            effects = SpriteEffects.None;
            layerDepth = 0;
            this.life = life;
            elapsedLife = TimeSpan.Zero;
            this.velocity = velocity;
            isDead = false;
        }

        public virtual void Update(GameTime gameTime)
        {
            elapsedLife += gameTime.ElapsedGameTime;
            if (elapsedLife >= life)
            {
                isDead = true;
            }
            color = Color.Lerp(startColor, endColor, (float)elapsedLife.Ticks / life.Ticks);
            position += velocity;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }
    }
}
