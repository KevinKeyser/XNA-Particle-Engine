using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace KevinKeyserParticle
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        ParticleEngine particleEngine;
        Vector2 speed = new Vector2(10);

        SpriteFont font;
        string word = "Nate";
        Vector2 textPosition;
        Rectangle textBounds;
        RenderTarget2D render;

        Texture2D pixel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            graphics.PreferredBackBufferWidth = 1000;
            graphics.ApplyChanges();
            spriteBatch = new SpriteBatch(GraphicsDevice);
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData<Color>(new Color[] { Color.White });
            font = Content.Load<SpriteFont>("Impact250");
            textPosition = (new Vector2(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height) - font.MeasureString(word)) / 2;
            textBounds = new Rectangle((int)textPosition.X, (int)textPosition.Y, (int)font.MeasureString(word).X, (int)font.MeasureString(word).Y);
            particleEngine = new ParticleEngine(Content.Load<Texture2D>("KevinsShapes"), new Vector2(250, 250), TimeSpan.FromMilliseconds(0), 10000);
            particleEngine.MinSpeed = new Vector2(-1, -1);
            particleEngine.MaxSpeed = new Vector2(1, 1);
            particleEngine.MinScale = .1f;
            particleEngine.MaxScale = .25f;
            particleEngine.MinLife = TimeSpan.FromMilliseconds(5000);
            particleEngine.MaxLife = TimeSpan.FromMilliseconds(10000);
            particleEngine.SpawnAmount = 50;
            particleEngine.SourceRectangles[0] = new Rectangle[] { new Rectangle(0, 0, 100, 100), new Rectangle(100, 0, 100, 100), new Rectangle(200, 0, 100, 100), new Rectangle(300, 0, 100, 100), new Rectangle(400, 0, 100, 100), new Rectangle(500, 0, 100, 100) };
            particleEngine.Colors = new List<ColorLerp>() { new ColorLerp(Color.Cyan, Color.Transparent), new ColorLerp(Color.Magenta, Color.Transparent), new ColorLerp(Color.Yellow, Color.Transparent), new ColorLerp(Color.Black, Color.Transparent) };
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            particleEngine.Update(gameTime);
            particleEngine.Position += speed;
            if (particleEngine.Position.X < textBounds.X)
            {
                speed.X = Math.Abs(speed.X);
            }
            if (particleEngine.Position.Y < textBounds.Y)
            {
                speed.Y = Math.Abs(speed.Y);
            }
            if (particleEngine.Position.X > textBounds.Width + textBounds.X)
            {
                speed.X = -Math.Abs(speed.X);
            }
            if (particleEngine.Position.Y > textBounds.Height + textBounds.Y)
            {
                speed.Y = -Math.Abs(speed.Y);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if(render == null)
            {
                render = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
                GraphicsDevice.SetRenderTarget(render);
                GraphicsDevice.Clear(Color.Black);
                spriteBatch.Begin();

                spriteBatch.DrawString(font, word, textPosition, Color.White);
                spriteBatch.End();
                GraphicsDevice.SetRenderTarget(null);
                Color[] Data = new Color[render.Width * render.Height];
                render.GetData<Color>(Data);
                for(int i = 0; i < Data.Length; i++)
                {
	                if(Data[i] != Color.Black)
                    {
                        Data[i] = Color.Transparent;
                    }
                }
                render.SetData<Color>(Data);
            }

            GraphicsDevice.Clear(Color.Black);


            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive);

            particleEngine.Draw(spriteBatch);

            spriteBatch.End();
            spriteBatch.Begin();
            spriteBatch.Draw(render, Vector2.Zero, Color.White);
            //spriteBatch.Draw(pixel, textBounds, Color.Lerp(Color.Red, Color.Transparent, .5f));
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
