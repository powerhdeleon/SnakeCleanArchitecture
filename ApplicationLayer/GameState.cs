using Domain;

namespace ApplicationLayer
{
    public class GameState
    {
        public required int Width { get; init; }
        public required int Height { get; init; }
        public required IEnumerable<Position> Snake { get; init; }
        public required Position Food { get; init; }
        public required bool IsOver { get; init; }
        public required int Score { get; init; }
    }
}
