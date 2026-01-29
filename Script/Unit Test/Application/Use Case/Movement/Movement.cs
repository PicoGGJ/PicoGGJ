using Application.UseCase;
using Domain.Entity;
using NUnit.Framework;
public class Movement
{
    [Test]
    public void MoveControll()
    {
        var directions = new MovementDirections();
        directions.Direction("left");
        var players = new Player(100, directions.Directions);

        Assert.AreEqual(new float[] {-1, 0}, players.MovementRules.Direction("left"));
        Assert.AreEqual(new float[] { 0, 0 }, players.MovementRules.Direction("p")); 
    }

}
