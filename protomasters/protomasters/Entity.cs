﻿using System;
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

        public Entity(float speed = 8.0f, float health = 100.0f, float strength = 10.0f)
        {
            this.speed = speed;
            this.health = health;
            this.strength = strength;
            this.animations = new AnimatedSprite();
        }
    }
}