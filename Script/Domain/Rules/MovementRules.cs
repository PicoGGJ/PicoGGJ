namespace Domain.Rules
{
    public class MovementRules
    {
        private string Directions;

        public float[] Direction(string directions)
        {
            Directions = directions.ToLower();
            switch (Directions)
            {
                case "left":
                    return new float[] { -1, 0 };
                case "right":
                    return new float[] { 1, 0 };
                case "jump":
                    return new float[] { 0, 1 };
                default:
                    return new float[] { 0, 0 };
            }
        }
    }
}
