using Domain.Rules;

namespace Application.UseCase{
    public class MovementDirections
    {
        MovementRules Directions { get; }

        public MovementDirections()
        {
            Directions = new();
        }

        public float[] Direction(string directions)
        {
            return Directions.Direction(directions);
        }
    }
}