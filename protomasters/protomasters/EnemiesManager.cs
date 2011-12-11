using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace protomasters
{
    class EnemiesManager
    {
        public List<Enemy> enemies;

        public void Draw(SpriteBatch spritebatch)
        {
            foreach(Enemy enemy in enemies)
            {
                enemy.animations.Draw(spritebatch);
            }
        }


    }
}
