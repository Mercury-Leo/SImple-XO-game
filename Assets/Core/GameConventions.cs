namespace Core {
    public static class GameConventions {
        public enum Moves {
            Empty = 0,
            X = 1,
            O = 2,
        }

        public enum BoardStates {
            Playing,
            Draw,
            Win,
            Lose,
        }

        public enum Scenes {
            Game,
            MainMenu,
        }

        public const float TurnTime = 5f;
        public const float ComputerTurnTime = 2f;
        public const int DefaultMoveIndex = -1;
        public const int BoardSize = 3;
        public const int BoardLength = BoardSize * BoardSize;
        public const int MaxTries = 1000;
        public const string TimeFormat = @"s\:ff";
        public const string PlayerName = "Player 1";
        public const string OtherPlayerName = "Player 2";
    }
}