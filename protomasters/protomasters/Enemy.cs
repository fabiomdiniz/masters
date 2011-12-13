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
        public double meleeTime;

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
                if (this.animations.AnimationKey.IndexOf("Attack") == -1)
                {
                    System.Random generator = new System.Random();
                    String attack;
                    inAction = meleeTime;
                    startAction = DateTime.Now;
                    if (generator.NextDouble() > 0.3)
                        attack = "Attack1";
                    else
                        attack = "Attack2";
                    if (last_movement.X > 0.0)
                        animations.PlayAnimation(attack + "Right");
                    else
                        animations.PlayAnimation(attack + "Left");
                    player.inDamage = attack;
                    player.enemyStr = strength;
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
    }
}
