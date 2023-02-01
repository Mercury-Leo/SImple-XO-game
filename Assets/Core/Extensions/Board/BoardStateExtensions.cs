namespace Core.Extensions.Board {
    public static class BoardStateExtensions {
        private const string Win = "wins!";
        private const string Lose = "loses!";
        private const string Draw = "Draw!";

        public static string GetMessage(this GameConventions.BoardStates state) {
            return state switch {
                GameConventions.BoardStates.Win => Win,
                GameConventions.BoardStates.Lose => Lose,
                GameConventions.BoardStates.Draw => Draw,
                GameConventions.BoardStates.Playing => string.Empty,
                _ => string.Empty
            };
        }
    }
}
