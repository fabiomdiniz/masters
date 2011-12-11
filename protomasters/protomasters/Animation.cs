using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace protomasters
{
    [Serializable]
    public class Animation
    {
        #region [ Fields ]

        /// <summary>
        /// Lista contendo a sequência de quadros da animação
        /// </summary>
        public List<Rectangle> Frames
        {
            get { return this.frames; }
        }
        private List<Rectangle> frames = new List<Rectangle>();

        /// <summary>
        /// Intervalo de tempo de passagem dos frames da animação
        /// </summary>
        public float Interval
        {
            get { return this.interval; }
            set { this.interval = value; }
        }
        private float interval = 0.25f;

        /// <summary>
        /// Define se existe repetição na animação
        /// </summary>
        public bool IsLooping
        {
            get { return this.isLooping; }
            set { this.isLooping = value; }
        }
        private bool isLooping = true;

        /// <summary>
        /// Quantidade de frames da aniamação
        /// </summary>
        public int FramesCount
        {
            get { return this.framesCount; }
        }
        private int framesCount = 0;

        #endregion

        #region [ Constructor ]

        /// <summary>
        /// Construtor de uma animação
        /// </summary>
        /// <param name="name">Nome da animação que será utilizada como chave</param>
        /// <param name="frameSize">Tamanho do quadro</param>
        /// <param name="startFrame">Quadro inicial</param>
        /// <param name="framesCount">Quantidade de quadros</param>
        public Animation(List<Rectangle> frames, int startFrame, int framesCount, bool isLooping = true)
        {
            this.framesCount = framesCount;
            // Armazena os quadros referêntes a esta animação
            for (int i = startFrame; i < startFrame + framesCount; i++)
                Frames.Add(frames[i]);
            this.isLooping = isLooping;
        }

        #endregion
    }
}
