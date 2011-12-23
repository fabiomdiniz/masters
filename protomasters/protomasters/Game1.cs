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




namespace protomasters
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        /// <summary>
        /// Entrada atual do teclado
        /// </summary>
        ///
        KeyboardState keyState = Keyboard.GetState();
        /// <summary>
        /// Entrada antiga do teclado
        /// </summary>
        KeyboardState oldKeyState = Keyboard.GetState();

        /// <summary>
        /// Personagem animado
        /// </summary>
        Player player;

        EnemiesManager enemiesManager = new EnemiesManager();

        SpriteFont font;
        Texture2D background;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("infoFont");
            background = Content.Load<Texture2D>("background");

            // Cria o personagem
            player = new Player(8.0f);
            // Posiciona o personagem na tela
            player.animations.position = new Vector2(100, GraphicsDevice.Viewport.Height-200);

            player.animations.AddAnimation("Stoped", Content.Load<Texture2D>("zero_walking"), 16, 1, 0, 1);
            player.animations.AddAnimation("Walking", Content.Load<Texture2D>("zero_walking"), 16, 1, 0, 16);
            player.animations.AddAnimation("Attack1", Content.Load<Texture2D>("zero_attack_1"), 1, 1, 0, 1, false);
            player.animations.AddAnimation("Attack2", Content.Load<Texture2D>("zero_attack_2"), 1, 1, 0, 1, false);
            player.animations.AddAnimation("Damage", Content.Load<Texture2D>("zero_damage"), 1, 1, 0, 1, false);
            player.animations.AddAnimation("Parry", Content.Load<Texture2D>("zero_parry"), 1, 1, 0, 1, false);
            player.animations.AddAnimation("Parried", Content.Load<Texture2D>("zero_parried"), 1, 1, 0, 1, false);
            player.animations.AddAnimation("Broke", Content.Load<Texture2D>("zero_broke"), 1, 1, 0, 1, false);

            enemiesManager.SpawnGenericEnemy(new Vector2(700, GraphicsDevice.Viewport.Height-200), Content); 
        
        }

        protected override void Update(GameTime gameTime)
        {
            enemiesManager.UpdateAction(player);
            player.UpdateAction(GamePad.GetState(PlayerIndex.One), GraphicsDevice);
            player.animations.Update(gameTime);
            if (enemiesManager.enemies.Count == 0)
                enemiesManager.SpawnGenericEnemy(new Vector2(700, GraphicsDevice.Viewport.Height - 200), Content);
            enemiesManager.Update(gameTime);            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkCyan);
            spriteBatch.Begin();

            spriteBatch.Draw(background, Vector2.Zero, Color.White);

            if (player.health <= 0)
            {
                spriteBatch.DrawString(font, "YOU LOSE!",
                new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 30, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White, 0.0f, Vector2.Zero, 4.0f, SpriteEffects.None, 0);
            }
            else
            {
                // Desenha o personagem
                player.animations.Draw(spriteBatch);

                if (player.Deflected())
                {
                    spriteBatch.DrawString(font, "DEFLECTED!",
                    new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X, GraphicsDevice.Viewport.TitleSafeArea.Y + 30), Color.White);

                }

                enemiesManager.Draw(spriteBatch);
                spriteBatch.DrawString(font, "Health: " + player.health,
                    new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + 30, GraphicsDevice.Viewport.TitleSafeArea.Y), Color.White);

                for (int i = 0; i < enemiesManager.enemies.Count; i++)
                {
                    spriteBatch.DrawString(font, "Enemy " + (i + 1) + ": " + enemiesManager.enemies[i].health,
                    new Vector2(GraphicsDevice.Viewport.TitleSafeArea.X + GraphicsDevice.Viewport.Width - 300,
                        GraphicsDevice.Viewport.TitleSafeArea.Y + i * 30), Color.White);

                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
