using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace protomasters
{
    class Player : Entity
    {
        GamePadState previousGamePadState;
        bool inAttack = false;
        public string inDamage = "";
        
        int meleeSpeed;
        public float enemyStr;

        public Player(float speed = 8.0f, float health = 100.0f, int meleeSpeed = 300) :
            base("StopedRight", speed, health) 
        {
            previousGamePadState = GamePad.GetState(PlayerIndex.One);
            startAction = DateTime.Now;
            this.meleeSpeed = meleeSpeed;
        }

        public void UpdateAction(GamePadState state, GraphicsDevice gdevice)
        {
            animations.position.X += state.ThumbSticks.Left.X * speed;
            TimeSpan delta = (DateTime.Now-startAction);
            
            inAttack = delta.TotalMilliseconds < meleeSpeed;

            if (inDamage != "")
            {
                if ((inDamage == "Attack1" &&
                    state.Buttons.X == ButtonState.Pressed) ||(inDamage == "Attack2" &&
                    state.Buttons.Y == ButtonState.Pressed))
                {
                    inDamage = "";
                }
                else
                {
                    if (last_movement.X > 0.0)
                        animations.PlayAnimation("DamageRight");
                    else
                        animations.PlayAnimation("DamageLeft");
                    health -= enemyStr;
                }
            }
            else if (inAttack)
                animations.PlayAnimation(animations.AnimationKey);
            else if (previousGamePadState.Buttons.X == ButtonState.Released &&
                    state.Buttons.X == ButtonState.Pressed)
            {
                if (last_movement.X > 0.0)
                    animations.PlayAnimation("Attack1Right");
                else
                    animations.PlayAnimation("Attack1Left");
                startAction = DateTime.Now;
                inAttack = true;
            }
            else if (previousGamePadState.Buttons.Y == ButtonState.Released &&
                    state.Buttons.Y == ButtonState.Pressed)
            {
                if (last_movement.X > 0.0)
                    animations.PlayAnimation("Attack2Right");
                else
                    animations.PlayAnimation("Attack2Left");
                startAction = DateTime.Now;
                inAttack = true;
            }
            else if (animations.old_position.X == animations.position.X)
            {
                if (last_movement.X > 0.0)
                    animations.PlayAnimation("StopedRight");
                else
                    animations.PlayAnimation("StopedLeft");
            }
            else
            {
                last_movement = state.ThumbSticks.Left;
                if (state.ThumbSticks.Left.X > 0.0)
                {
                    animations.PlayAnimation("WalkingRight");
                }
                else
                {
                    animations.PlayAnimation("WalkingLeft");
                }
            }
            

            animations.old_position = animations.position;
            previousGamePadState = state;
            // Make sure that the player does not go out of bounds
            animations.position.X = MathHelper.Clamp(animations.position.X, 0, gdevice.Viewport.Width - animations.frameWidth);
            animations.position.Y = MathHelper.Clamp(animations.position.Y, 0, gdevice.Viewport.Height - animations.frameHeight);
        }
    }
}
