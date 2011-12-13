﻿using System;
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
        double inAction = 0.0;
        public string inDamage = "";
        
        double meleeSpeed;
        public float enemyStr;

        public bool pressedAttack1()
        {
            return previousGamePadState.Buttons.X == ButtonState.Released &&
                    GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed;
        }

        public bool pressedAttack2()
        {
            return previousGamePadState.Buttons.Y == ButtonState.Released &&
                    GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed;
        }

        public bool Deflected()
        {
            bool temp = deflected;
            deflected = false;
            return temp;
        }

        private bool deflected;

        public Player(float speed = 8.0f, float health = 100.0f, double meleeSpeed = 300.0) :
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

            inAction = Math.Max(0.0, inAction - delta.TotalMilliseconds);

            if (inDamage != "")
            {
                if ((inDamage == "Attack1" && pressedAttack1()) ||
                    (inDamage == "Attack2" && pressedAttack2()))
                {
                    deflected = true;
                }
                else
                {
                    if (last_movement.X > 0.0)
                        animations.PlayAnimation("DamageRight");
                    else
                        animations.PlayAnimation("DamageLeft");
                    health -= enemyStr;
                    inDamage = "";
                    inAction = 300.0;
                    startAction = DateTime.Now;
                }
            }
            else if (inAction > 0)
                animations.PlayAnimation(animations.AnimationKey);
            else if (pressedAttack1())
            {
                if (last_movement.X > 0.0)
                    animations.PlayAnimation("Attack1Right");
                else
                    animations.PlayAnimation("Attack1Left");
                startAction = DateTime.Now;
                inAction = meleeSpeed;
            }
            else if (pressedAttack2())
            {
                if (last_movement.X > 0.0)
                    animations.PlayAnimation("Attack2Right");
                else
                    animations.PlayAnimation("Attack2Left");
                startAction = DateTime.Now;
                inAction = meleeSpeed;
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
