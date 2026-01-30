using Domain.Entity;

public class HpReduce
{
    private Player players;
    private float multiplier = 1.2f;
    private bool firstDamage;
    public HpReduce(Player players)
    {
        this.players = players;
    }

    public void ReduceOverTime(int amount)
    {
        if (firstDamage)
        {
            players.DoT((int)amount);
            return;
        }
        players.DoT((int)(amount*multiplier));
    }

    public int GetHP()
    {
        var hp = players.Health;
        return hp;
    }

}
