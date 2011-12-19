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
                if(!enemy.unparriable)
                    check_parry(enemy, player);
            }
        }

        private void check_parry(Enemy enemy, Player player)
        {
            bool parry = false;
            bool misparry = false;
            if (player.pressedAttack1())
            {
                parry = (enemy.inAttack == "Attack1");
                misparry = !parry;
            }
            else if (player.pressedAttack2())
            {
                parry = (enemy.inAttack == "Attack2");
                misparry = !parry;
            }
            if (parry)
            {
                enemy.inAttack = "";
                player.parry();
                enemy.parried();
                Console.WriteLine(enemy.parryCount);
            }
            enemy.unparriable = misparry;
        }
    }
}
