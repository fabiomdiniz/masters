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
   /* public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Firebrick);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }*/

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
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Cria o personagem
            player = new Player(8.0f);
            // Posiciona o personagem na tela
            player.animations.position = new Vector2(100, GraphicsDevice.Viewport.Height-200);

            player.animations.AddAnimation("Stoped", Content.Load<Texture2D>("zero_walking"), 16, 1, 0, 1);
            player.animations.AddAnimation("Walking", Content.Load<Texture2D>("zero_walking"), 16, 1, 0, 16);
            player.animations.AddAnimation("Attack1", Content.Load<Texture2D>("zero_attack_1"), 1, 1, 0, 1);
            player.animations.AddAnimation("Attack2", Content.Load<Texture2D>("zero_attack_2"), 1, 1, 0, 1);
            
            /*
            // Baixo
            // Cria as animações para baixo
            Animation stopedDownAnimation = new Animation(player.Frames, 0, 2);
            player.AddAnimation("StopedDown", stopedDownAnimation);
            // Cria as animações para baixo andando
            Animation walkingDownAnimation = new Animation(player.Frames, 2, 4);
            walkingDownAnimation.IsLooping = true;
            player.AddAnimation("WalkingDown", walkingDownAnimation);
            */
            // Esquerda
            // Cria as animações para esquerda
         /*   Animation stopeLeftAnimation = new Animation(player.Frames, 6, 2);
            player.AddAnimation("StopedLeft", stopeLeftAnimation);
            // Cria as animações para esquerda andando
            Animation walkingLeftAnimation = new Animation(player.Frames, 8, 4);
            walkingLeftAnimation.IsLooping = true;
            player.AddAnimation("WalkingLeft", walkingLeftAnimation);
            /*
            // Cima
            // Cria as animações para cima parado
            Animation stopedUpAnimation = new Animation(player.Frames, 12, 2);
            player.AddAnimation("StopedUp", stopedUpAnimation);
            // Cria as animações para cima andando
            Animation walkingUpAnimation = new Animation(player.Frames, 14, 4);
            walkingUpAnimation.IsLooping = true;
            player.AddAnimation("WalkingUp", walkingUpAnimation);
            */
            // Direita
            // Cria as animações para direita parado
           /* Animation stopedRightAnimation = new Animation(player.Frames, 18, 2);
            player.AddAnimation("StopedRight", stopedRightAnimation);
            // Cria as animações para direita andando
            Animation walkingRightAnimation = new Animation(player.Frames, 20, 4);
            walkingRightAnimation.IsLooping = true;
            player.AddAnimation("WalkingRight", walkingRightAnimation);

            // Inicializa a animação do personagem parado a direita
            player.PlayAnimation("StopedRight");*/
        }

        protected override void Update(GameTime gameTime)
        {
            player.UpdateAction(GamePad.GetState(PlayerIndex.One), GraphicsDevice);

            // Atualiza o personagem*/
            player.animations.Update(gameTime);

            base.Update(gameTime);

            /*
            oldKeyState = keyState;
            keyState = Keyboard.GetState();
            
            if (keyState.IsKeyDown(Keys.Right))
            {
                player.PlayAnimation("WalkingRight");
                player.position += new Vector2(60.0f, 0.0f) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (keyState.IsKeyUp(Keys.Right) && oldKeyState.IsKeyDown(Keys.Right))
            {
                player.PlayAnimation("StopedRight");
            }
            else if (keyState.IsKeyDown(Keys.Left))
            {
                player.PlayAnimation("WalkingLeft");
                player.position += new Vector2(-60.0f, 0.0f) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (keyState.IsKeyUp(Keys.Left) && oldKeyState.IsKeyDown(Keys.Left))
            {
                player.PlayAnimation("StopedLeft");
            }
            else
            {
                player.UpdatePosition(GamePad.GetState(PlayerIndex.One).ThumbSticks.Left);
            }

            /*
            else if (keyState.IsKeyDown(Keys.Up))
            {
                player.PlayAnimation("WalkingUp");
                player.Position += new Vector2(0.0f, -60.0f) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (keyState.IsKeyUp(Keys.Up) && oldKeyState.IsKeyDown(Keys.Up))
            {
                player.PlayAnimation("StopedUp");
            }
            else if (keyState.IsKeyDown(Keys.Down))
            {
                player.PlayAnimation("WalkingDown");
                player.Position += new Vector2(0.0f, 60.0f) * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (keyState.IsKeyUp(Keys.Down) && oldKeyState.IsKeyDown(Keys.Down))
            {
                player.PlayAnimation("StopedDown");
            }*/
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkCyan);
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            // Desenha o personagem
            player.animations.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }

}
