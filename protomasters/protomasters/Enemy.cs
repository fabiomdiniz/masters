using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace protomasters
{
    class Enemy : Entity
    {

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

            Console.WriteLine(inAction);

            if(check_attack(player))
            {
                enemyStr = player.strength;
                damaged();
            }
            else if (inAction > 0.0)
                animations.PlayAnimation(animations.AnimationKey);
            else if (this.animations.Rect().Intersects(player.animations.Rect()))
            {
                if (parryCount >= parryMax)
                {
                    broke();
                    player.canAttack = true;
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
                    player.canAttack = true;
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
            this.animations.color = color;
            parryCount = 0;
            player.canAttack = false;
        }

        public bool check_attack(Player player)
        {
            return this.animations.Rect().Intersects(player.animations.Rect()) &&
                   isBroke() && player.pressedAttack();
        }

    }
}
