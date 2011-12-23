using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace protomasters
{
    class GenericEnemy : Enemy
    {
        public GenericEnemy(ContentManager content, float speed = 4.0f, float health = 100.0f, float strength = 10.0f) :
            base(speed, health, strength)
        {
            this.color = Color.LightSeaGreen;
            this.animations.color = color;
            this.animations.AddAnimation("Stoped", content.Load<Texture2D>("zero_walking"), 16, 1, 0, 1);
            this.animations.AddAnimation("Walking", content.Load<Texture2D>("zero_walking"), 16, 1, 0, 16);
            this.animations.AddAnimation("Attack1", content.Load<Texture2D>("zero_attack_1"), 1, 1, 0, 1, false);
            this.animations.AddAnimation("Attack2", content.Load<Texture2D>("zero_attack_2"), 1, 1, 0, 1, false);
            this.animations.AddAnimation("Parry", content.Load<Texture2D>("zero_parry"), 1, 1, 0, 1, false);
            this.animations.AddAnimation("Parried", content.Load<Texture2D>("zero_parried"), 1, 1, 0, 1, false);
            this.animations.AddAnimation("Broke", content.Load<Texture2D>("zero_broke"), 1, 1, 0, 1, false);
            this.animations.AddAnimation("Damage", content.Load<Texture2D>("zero_damage"), 1, 1, 0, 1, false);

            System.Random generator = new System.Random();
            this.meleeTime = (double)generator.Next(11000, 12000);
        }
    }
}
