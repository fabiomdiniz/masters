using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace protomasters
{
    [Serializable]
    public class SpriteSheet
    {
        public Texture2D texture;

        public Animation animation;

        public List<Rectangle> frames;

        /// <summary>
        /// Largura do quadro
        /// </summary>
        public int frameWidth = 0;

        /// <summary>
        /// Altura do quadro
        /// </summary>
        public int frameHeight = 0;

        public Vector2 Size
        {
            get { return new Vector2(frameWidth, frameHeight); }
        }

        public Vector2 origin;
        public int columns;
        public int rows;
        public Vector2 position;

        public SpriteEffects flip = SpriteEffects.None;

        public SpriteSheet(Texture2D texture, int columns, int rows, int startFrame, int framesCount, SpriteEffects flip = SpriteEffects.None)
        {
            this.texture = texture;
            this.position = new Vector2();
            this.frameWidth = texture.Width / columns;
            this.frameHeight = texture.Height / rows;
            this.frames = new List<Rectangle>();
            // Cria todos os quadros da imagem
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < columns; j++)
                    this.frames.Add(new Rectangle(j * frameWidth,
                        i * frameHeight, frameWidth, frameHeight));

            this.origin = new Vector2(frameWidth / 2, frameHeight / 2);
            this.animation = new Animation(this.frames, startFrame, framesCount);
            this.flip = flip;
        }
    }
}