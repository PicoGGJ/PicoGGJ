using Domain.Rules;
using System;

namespace Domain.Entity
{
    public class Player
    {
        public int Health { get; private set; }
        public MovementRules MovementRules { get; private set; }
        public HpReduce HpReduce { get; private set; }

        public bool Mask = false;
        public Player(int health, MovementRules movement)
        {
            Health = health;
            MovementRules = movement;
            HpReduce = new HpReduce(this);
        }

        public void DoT(int amount)
        {
            if (amount <= 0 || Mask == true) return;
            Health -= amount;
        }
    }
}
