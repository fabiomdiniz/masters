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
        public Player(float speed = 8.0f, float health = 100.0f) :
            base(speed, health) { }

        public void UpdateAction(GamePadState state, GraphicsDevice gdevice)
        {
            animations.position.X += state.ThumbSticks.Left.X * speed;

            if (state.Buttons.X == ButtonState.Pressed)
            {
                if (last_movement.X > 0.0)
                    animations.PlayAnimation("Attack1Right");
                else
                    animations.PlayAnimation("Attack1Left");
            }
            else if (state.Buttons.Y == ButtonState.Pressed)
            {
                if (last_movement.X > 0.0)
                    animations.PlayAnimation("Attack2Right");
                else
                    animations.PlayAnimation("Attack2Left");
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

            // Make sure that the player does not go out of bounds
            animations.position.X = MathHelper.Clamp(animations.position.X, 0, gdevice.Viewport.Width - animations.frameWidth);
            animations.position.Y = MathHelper.Clamp(animations.position.Y, 0, gdevice.Viewport.Height - animations.frameHeight);
        }
    }
}
