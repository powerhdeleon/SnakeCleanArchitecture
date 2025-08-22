namespace Domain
{
    public enum Direction { Up, Down, Left, Right }
    public class DirectionOps
    {
        public static bool IsOpposite(Direction a, Direction b) =>
           (a == Direction.Up && b == Direction.Down) ||
           (a == Direction.Down && b == Direction.Up) ||
           (a == Direction.Left && b == Direction.Right) ||
           (a == Direction.Right && b == Direction.Left);
    }
}
