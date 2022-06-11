namespace _Scripts.Utility
{
    public static class GameStateManager
    {
        public static GameState CurrentGameState { get; set; } = GameState.Running;
        public enum GameState
        {
            Running,
            Shooting
        }
    }
}