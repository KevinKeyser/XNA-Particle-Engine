using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KevinKeyserParticle
{
    public class ParticleEngine
    {
        private List<Texture2D> particleTextures;

        public List<Texture2D> ParticleTextures
        {
            get { return particleTextures; }
            set { particleTextures = value; }
        }

        private List<Rectangle[]> sourceRectangles;

        public List<Rectangle[]> SourceRectangles
        {
            get { return sourceRectangles; }
            set { sourceRectangles = value; }
        }

        private Vector2 position;

	    public Vector2 Position
	    {
		    get { return position;}
		    set { position = value;}
	    }

        private TimeSpan elapsedTime;

        private int spawnAmount;

        public int SpawnAmount
        {
            get { return spawnAmount; }
            set { spawnAmount = value; }
        }       

        private TimeSpan spawnRate;

        public TimeSpan SpawnRate
        {
            get { return spawnRate; }
            set { spawnRate = value; }
        }

        private int maxParticles;

        public int MaxParticles
        {
            get { return maxParticles; }
            set { maxParticles = value; }
        }

        private Vector2 minSpeed;

        public Vector2 MinSpeed
        {
            get { return minSpeed; }
            set { minSpeed = value; }
        }

        private Vector2 maxSpeed;

        public Vector2 MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        private TimeSpan minLife;

        public TimeSpan MinLife
        {
            get { return minLife; }
            set { minLife = value; }
        }

        private TimeSpan maxLife;

        public TimeSpan MaxLife
        {
            get { return maxLife; }
            set { maxLife = value; }
        }

        private List<ColorLerp> colors;

        public List<ColorLerp> Colors
        {
            get { return colors; }
            set { colors = value; }
        }

        private float minScale;

        public float MinScale
        {
            get { return minScale; }
            set { minScale = value; }
        }

        private float maxScale;

        public float MaxScale
        {
            get { return maxScale; }
            set { maxScale = value; }
        }
        
        Random randomGenerator;

        List<Particle> particles;

        public ParticleEngine(Texture2D particleTexture, Vector2 position, TimeSpan spawnRate, int maxParticles)
        {
            particleTextures = new List<Texture2D>() { particleTexture };
            sourceRectangles = new List<Rectangle[]>() { new Rectangle[] { new Rectangle(0, 0, particleTexture.Width, particleTexture.Height) } };
            this.position = position;
            elapsedTime = TimeSpan.Zero;
            this.spawnRate = spawnRate;
            spawnAmount = 1;
            this.maxParticles = maxParticles;
            randomGenerator = new Random();
            particles = new List<Particle>();
            minSpeed = new Vector2(-5, -5);
            maxSpeed = new Vector2(5, 5);
            minLife = TimeSpan.FromMilliseconds(250);
            maxLife = TimeSpan.FromMilliseconds(1000);
            colors = new List<ColorLerp>() { new ColorLerp(Color.White, Color.White) };
            minScale = .5f;
            maxScale = 1;
        }


        public virtual void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime >= spawnRate)
            {
                elapsedTime = TimeSpan.Zero;
                for (int i = 0; i < spawnAmount; i++)
                {
                    int source = randomGenerator.Next(particleTextures.Count);
                    particles.Add(new Particle(particleTextures[source], position, randomGenerator.NextTimeSpan(minLife, maxLife), randomGenerator.NextVector2(minSpeed, maxSpeed)));
                    particles[particles.Count - 1].SourceRectangle = sourceRectangles[source][randomGenerator.Next(sourceRectangles[source].Length)];
                    particles[particles.Count - 1].StartColor = colors[randomGenerator.Next(colors.Count)].StartColor;
                    particles[particles.Count - 1].EndColor = colors[randomGenerator.Next(colors.Count)].EndColor;
                    float scale = (float)randomGenerator.NextDouble() * (maxScale - minScale) + minScale;
                    particles[particles.Count - 1].Scale = new Vector2(scale);
                }
            }

            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].Update(gameTime);
                if (particles[i].isDead)
                {
                    particles.RemoveAt(i);
                    i--;
                }
            }
        }
        
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(Particle particle in particles)
            {
                particle.Draw(spriteBatch);
            }
        }
    }
}
