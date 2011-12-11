using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace protomasters
{
    public class AnimatedSprite
    {
        #region [ Fields ]
        /// <summary>
        /// Posição do objeto animado no mundo
        /// </summary>
        public Vector2 position;

        /// <summary>
        /// Posição anterior do objeto animado no mundo
        /// </summary>
        public Vector2 old_position;

        /// <summary>
        /// Dicionário contendo as animações do personagem
        /// </summary>
        public Dictionary<string, SpriteSheet> spriteSheets = new Dictionary<string,SpriteSheet>();

        /// <summary>
        /// Indice do frame da animação em execução
        /// </summary>
        public int FrameIndex
        {
            get { return this.frameIndex; }
        }
        private int frameIndex = 0;

        /// <summary>
        /// Nome da animação
        /// </summary>
        private string animationKey;
        public string oldAnimationKey = "";

            public string AnimationKey
        {
            get { return this.animationKey; }
        }

        /// <summary>
        /// Cor de desenho da imagem
        /// </summary>
        public Color color = Color.White;

        /// <summary>
        /// Rotação da imagem
        /// </summary>
        protected float rotation = 0.0f;

        /// <summary>
        /// Escala da imagem
        /// </summary>
        protected float scale = 3.0f;

        /// <summary>
        /// Efeitos de imagem
        /// </summary>
        public SpriteEffects Flip
        {
            get { return this.flip; }
            set { this.flip = value; }
        }
        protected SpriteEffects flip = SpriteEffects.None;

        public Rectangle Rect()
        {
            return new Rectangle((int)position.X,
                                 (int)position.Y,
                                 frameWidth,
                                 frameHeight);
        }

        /// <summary>
        /// Layer de desenho
        /// </summary>
        protected float layerDepth = 0.0f;

        /// <summary>
        /// Tempo decorrido do frame atual da animação
        /// </summary>
        private float timeElapsed = 0.0f;
        public int frameWidth;
        public int frameHeight;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Construtor do objeto animado
        /// </summary>
        /// <param name="texture">Imagem (Sprite Sheet)</param>
        /// <param name="columns">Quantidade de colunas</param>
        /// <param name="rows">Quantidade de linhas</param>
        public AnimatedSprite(string defaultKey) 
        {
            this.animationKey = defaultKey;
        }

        #endregion

        #region [ Update ]

        /// <summary>
        /// Atualiza a animação do objeto
        /// </summary>
        /// <param name="gameTime">Tempo de jogo</param>
        public void Update(GameTime gameTime)
        {
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timeElapsed > spriteSheets[AnimationKey].animation.Interval)
            {
                if (spriteSheets[AnimationKey].animation.IsLooping)
                {
                    frameIndex = (frameIndex + 1) % spriteSheets[AnimationKey].animation.FramesCount;
                }
                else
                {
                    animationKey = this.oldAnimationKey;
                    frameIndex = (int)MathHelper.Min(frameIndex + 1, spriteSheets[AnimationKey].animation.FramesCount - 1);
                }
                timeElapsed = 0.0f;
            }
        }

        #endregion

        #region [ Draw ]

        /// <summary>
        /// Desenha o objeto animado
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteSheets[AnimationKey].texture,
                position,
                spriteSheets[AnimationKey].frames[frameIndex],
                color,
                rotation,
                spriteSheets[AnimationKey].origin,
                scale,
                spriteSheets[AnimationKey].flip,
                layerDepth);
        }

        #endregion

        #region [ Add Animations ]

        /// <summary>
        /// Adiciona novas animações
        /// </summary>
        /// <param name="name">Nome chave da animação</param>
        /// <param name="newAnimation">Animação definida</param>
        public void AddAnimation(string name, Texture2D texture, int columns, int rows, int startFrame, int framesCount, bool isloop = true)
        {
            spriteSheets.Add(name + "Right", new SpriteSheet(texture, columns, rows, startFrame, framesCount,SpriteEffects.None, isloop));
            spriteSheets.Add(name + "Left", new SpriteSheet(texture, columns, rows, startFrame, framesCount, SpriteEffects.FlipHorizontally, isloop));
            this.frameWidth = texture.Width / columns;
            this.frameHeight = texture.Height / rows;
        }

        #endregion

        #region [ Play Animation ]

        /// <summary>
        /// Inicia ou continua uma animação.
        /// </summary>
        public void PlayAnimation(string name)
        {
            // Se a animação for a mesma em execução, não reinicia a animação
            if (name == AnimationKey)
                return;

            // Inicia uma nova animação
            this.oldAnimationKey = this.animationKey;
            this.animationKey = name;
            this.frameIndex = 0;
            this.timeElapsed = 0.0f;
        }

        #endregion
    }
}
