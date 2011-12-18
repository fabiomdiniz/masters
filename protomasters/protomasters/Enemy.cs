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
        public int parryTime = 4000;

        public bool inDamage = false;

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
                if (inAttack == "")
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
                    if (last_movement.X > 0.0)
                        animations.PlayAnimation("StopedRight");
                    else
                        animations.PlayAnimation("StopedLeft");

                }
                else if (inAction == 0.0)
                {
                    player.inDamage = inAttack;
                    player.enemyStr = strength;
                    inAction = meleeTime;
                    startAction = DateTime.Now;
                    if (last_movement.X > 0.0)
                        animations.PlayAnimation(inAttack+"Right");
                    else
                        animations.PlayAnimation(inAttack + "Left");
                    inAttack = "";
                    this.animations.color = Color.Black;
                }
                else
                {
                    animations.PlayAnimation(animations.AnimationKey);
                }
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
                {
                    if (last_movement.X > 0.0)
                        animations.PlayAnimation("StopedRight");
                    else
                        animations.PlayAnimation("StopedLeft");
                }
            }

            animations.old_position = animations.position;
            // Make sure that the player does not go out of bounds
        }

        public void parried()
        {
            if (last_movement.X > 0.0)
                animations.PlayAnimation("ParriedRight");
            else
                animations.PlayAnimation("ParriedLeft");
            inAttack = "";
            inAction = 500.0;
            this.animations.color = Color.Black;
            startAction = DateTime.Now;
        }
    }
}
