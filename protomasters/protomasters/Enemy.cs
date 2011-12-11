﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace protomasters
{
    class Enemy : Entity
    {
        public bool inAttack = false;
        public int meleeSpeed;

        public Enemy(float speed = 8.0f, float health = 100.0f, float strength = 10.0f, int meleeSpeed = 500) :
            base("WalkingLeft", speed, health, strength) 
        {
            this.meleeSpeed = meleeSpeed;
            startAction = DateTime.Now;
        }

        public void UpdateAction(Player player)
        {
            int direction;
            TimeSpan delta = (DateTime.Now - startAction);
            inAttack = delta.TotalMilliseconds < meleeSpeed;
            if (inAttack)
                animations.PlayAnimation(animations.AnimationKey);
            else if (this.animations.Rect().Intersects(player.animations.Rect()))
            {
                if (this.animations.AnimationKey.IndexOf("Attack") == -1)
                {
                    System.Random generator = new System.Random();
                    String attack;
                    inAttack = true;
                    startAction = DateTime.Now;
                    if (generator.NextDouble() > 0.3)
                        attack = "Attack1";
                    else
                        attack = "Attack2";
                    if (last_movement.X > 0.0)
                        animations.PlayAnimation(attack + "Right");
                    else
                        animations.PlayAnimation(attack + "Left");
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
            }
        }
    }
}
