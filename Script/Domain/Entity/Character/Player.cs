using Domain.Rules;
using System;

namespace Domain.Entity
{
    public class Player
    {
        public int Health { get; private set; }
        public MovementRules MovementRules { get; private set; }
        
        public Player(int health, MovementRules movement)
        {
            Health = health;
            MovementRules = movement;
        }
    }
}
