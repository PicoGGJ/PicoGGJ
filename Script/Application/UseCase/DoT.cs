using Domain.Entity;
using Domain.Rules;

namespace Application.UseCase
{
    public class DoT
    {
        private HpReduce DOT;
        Player players;
        public DoT()
        {
            var Directions = new MovementRules();
            players = new(100 ,Directions);
            DOT = new(players);
        }

        public void DamageOverTime(int amount)
        {
            DOT.ReduceOverTime(amount);
        }

        public int GetHp()
        {
            return DOT.GetHP();
        }
    }
}