using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace protomasters
{
    class Enemy : Entity
    {
        public Enemy(float speed = 8.0f, float health = 100.0f, float strength = 10.0f) :
            base(speed, health, strength) { }
    }
}
