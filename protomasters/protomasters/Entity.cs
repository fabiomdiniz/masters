using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace protomasters
{
    class Entity
    {
        public float speed;

        public float strength;

        public float health;

        public AnimatedSprite animations;
        
        public Vector2 last_movement;

        public DateTime startAction;
        public double inAction = 0.0;
        public string inAttack = "";
        public double meleeTime;
        public int parryTime = 6000;
        public int parryCount = 0;
        public int parryMax = 3;
        public float enemyStr;
        public string inDamage = "";

        public Color color = Color.Black;

        public Entity(string defaultKey, float speed = 8.0f, float health = 100.0f, float strength = 10.0f)
        {
            this.speed = speed;
            this.health = health;
            this.strength = strength;
            this.animations = new AnimatedSprite(defaultKey);
        }

        public string getSense()
        {
            if (last_movement.X > 0.0)
                return "Right";
            else
                return "Left";
        }

        public void parried()
        {
            animations.PlayAnimation("Parried" + getSense());
            inAttack = "";
            inAction = 500.0;
            this.animations.color = color;
            startAction = DateTime.Now;
            parryCount += 1;
        }
        public void broke()
        {
            animations.PlayAnimation("Broke" + getSense());
            parryCount = 0;
            inAttack = "";
            inAction = 14000.0;
            this.animations.color = color;
            startAction = DateTime.Now;
        }

        public bool isBroke()
        {
            return animations.AnimationKey.Contains("Broke");
        }

        public bool isAttacking()
        {
            return animations.AnimationKey.Contains("Attack");
        }

        public void parry()
        {
            if (last_movement.X > 0.0)
                animations.PlayAnimation("ParryRight");
            else
                animations.PlayAnimation("ParryLeft");
            inAction = 500.0;
            startAction = DateTime.Now;
        }

        public void damaged()
        {
            if (last_movement.X > 0.0)
                animations.PlayAnimation("DamageRight");
            else
                animations.PlayAnimation("DamageLeft");
            health -= enemyStr;
            inDamage = "";
            inAction = 3000.0;
            startAction = DateTime.Now;
        }
    }
}
