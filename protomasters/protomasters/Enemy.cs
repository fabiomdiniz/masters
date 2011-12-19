using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace protomasters
{
    class Enemy : Entity
    {
        public double inAction = 0.0;
        public string inAttack = "";
        public double meleeTime;
        public int parryTime = 6000;
        public int parryCount = 0;
        private int parryMax = 3;

        public bool inDamage = false;
        public bool unparriable = false;

        public Enemy(float speed = 8.0f, float health = 100.0f, float strength = 1.0f, int meleeSpeed = 500) :
            base("WalkingLeft", speed, health, strength) 
        {
            this.meleeTime = meleeSpeed;
            startAction = DateTime.Now;
        }

        public void UpdateAction(Player player)
        {
            int direction;
            TimeSpan delta = (DateTime.Now - startAction);
            inAction = Math.Max(0.0, inAction - delta.TotalMilliseconds);
            if (inAction > 0.0)
                animations.PlayAnimation(animations.AnimationKey);
            else if (this.animations.Rect().Intersects(player.animations.Rect()))
            {
                if (parryCount >= parryMax)
                {
                    broke();
                }
                else if (inAttack == "")
                {
                    System.Random generator = new System.Random();
                    inAction = parryTime;
                    startAction = DateTime.Now;
                    if (generator.NextDouble() > 0.3)
                    {
                        inAttack = "Attack1";
                        this.animations.color = Color.Blue;
                    }
                    else
                    {
                        inAttack = "Attack2";
                        this.animations.color = Color.Yellow;
                    }
                    animations.PlayAnimation("Stoped" + getSense());
                }
                else if (inAction == 0.0)
                    successfullAttack(player);
                else
                    animations.PlayAnimation(animations.AnimationKey);
            }
            else
            {
                if (player.animations.position.X < this.animations.position.X)
                {
                    this.animations.PlayAnimation("WalkingLeft");
                    direction = -1;
                }
                else
                {
                    this.animations.PlayAnimation("WalkingRight");
                    direction = 1;
                }
                last_movement.X = direction;
                this.animations.position.X += direction * speed;

                if (this.animations.Rect().Intersects(player.animations.Rect()))
                   animations.PlayAnimation("Stoped" + getSense());
            }

            animations.old_position = animations.position;
            // Make sure that the player does not go out of bounds
        }

        private void successfullAttack(Player player)
        {
            player.inDamage = inAttack;
            player.enemyStr = strength;
            inAction = meleeTime;
            startAction = DateTime.Now;
            animations.PlayAnimation(inAttack + getSense());
            inAttack = "";
            unparriable = false;
            this.animations.color = Color.Black;
            parryCount = 0;
        }

        public void parried()
        {
            animations.PlayAnimation("Parried" + getSense());
            inAttack = "";
            inAction = 500.0;
            this.animations.color = Color.Black;
            startAction = DateTime.Now;
            parryCount += 1;
        }
        public void broke()
        {
            animations.PlayAnimation("Broke" + getSense());
            parryCount = 0;
            inAttack = "";
            inAction = 5500.0;
            this.animations.color = Color.Black;
            startAction = DateTime.Now;
        }

    }
}
