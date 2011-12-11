using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace protomasters
{
    class EnemiesManager
    {
        public List<Enemy> enemies = new List<Enemy>();



        public void Draw(SpriteBatch spritebatch)
        {
            foreach(Enemy enemy in enemies)
            {
                enemy.animations.Draw(spritebatch);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.animations.Update(gameTime);
            }
        }

        public void SpawnGenericEnemy(Vector2 position, ContentManager content)
        {
            enemies.Add(new GenericEnemy(content));
            enemies[enemies.Count - 1].animations.position = position;
        }


        public void UpdateAction(Player player)
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.UpdateAction(player);
            }
        }
    }
}
