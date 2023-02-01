using System;
using System.Collections;
using System.Threading;
using Core.Extensions.Coroutine;
using Core.Extensions.Random;
using Core.Game.Board.Scripts;
using Core.Game.Turn.Scripts;
using Core.Player.Builder;
using Core.Player.Scripts;
using UnityEngine;
using static Core.GameConventions;

namespace Core.Game.Scripts {
    public class GameManager : MonoBehaviour {
        [Header("Managers")] 
        [SerializeField] private TurnManager _turnManager;
        [SerializeField] private BoardManager _boardManager;

        [SerializeField] private PlayerBuilder _playerBuilder;

        private IPlayer _player;
        private IPlayer _otherPlayer;
        private IPlayer _activePlayer;
        private CancellationTokenSource _source;

        private readonly WaitForSecondsRealtime _computerTurnTime = new WaitForSecondsRealtime(ComputerTurnTime);
        private const float MoveXChance = 50f;

        public bool IsGameRunning { get; private set; }

        public IPlayer ActivePlayer {
            get => _activePlayer;
            private set {
                _activePlayer = value;
                OnActivePlayerChanged?.Invoke(_activePlayer);
            }
        }

        public Action OnGameStart { get; set; }
        public Action OnGameRestart { get; set; }
        public Action<bool> OnPlayerVsComputer { get; set; }
        public Action<IPlayer> OnGameOver { get; set; }
        public Action<IPlayer> OnActivePlayerChanged { get; set; }
        public Action<IPlayer, IPlayer> OnPlayersMovesSet { get; set; }

        private void Start() {
            InitializePlayers();
        }

        private void OnEnable() {
            _turnManager.OnTurnLose += GameOver;
            _turnManager.OnTurnBegin += TryPerformPlayerTurn;

            _boardManager.OnTileSelected += PlayerAction;
            _boardManager.OnGameOver += DecideWinner;
            _boardManager.OnMoveMade += OnMoveMade;
        }

        private void OnDisable() {
            _turnManager.OnTurnLose -= GameOver;
            _turnManager.OnTurnBegin -= TryPerformPlayerTurn;

            _boardManager.OnTileSelected -= PlayerAction;
            _boardManager.OnGameOver -= DecideWinner;
            _boardManager.OnMoveMade -= OnMoveMade;
        }

        public void SetPlayers(IPlayer player, IPlayer otherPlayer) {
            _player = player;
            _otherPlayer = otherPlayer;
            _player.Name = PlayerName;
            _otherPlayer.Name = OtherPlayerName;
            AssignRandomMoves(_player, _otherPlayer);
            ActivateUndoButton(player, otherPlayer);
            SetActivePlayer();
        }

        public void StartGame() {
            OnGameStart?.Invoke();
            IsGameRunning = true;
            CancelPlayerMove();
            AssignRandomMoves(_player, _otherPlayer);
            CleanBoard();
            SetActivePlayer();
            _turnManager.BeginTurn();
        }

        public void RestartGame() {
            OnGameRestart?.Invoke();
            CancelPlayerMove();
            AssignRandomMoves(_player, _otherPlayer);
            CleanBoard();
            IsGameRunning = false;
        }

        public void UndoMove() {
            if (!IsGameRunning)
                return;
            CancelPlayerMove();
            _boardManager.UndoLastRound();
            _turnManager.RestartTurn();
        }

        private void GameOver() {
            SetPlayerBoardStates(BoardStates.Lose);
            StopGame();
        }

        private void InitializePlayers() {
            var selection = _playerBuilder.GetPlayers();
            if (selection.player is null || selection.otherPlayer is null) {
                Debug.LogError("Failed to create players!", this);
                return;
            }

            SetPlayers(selection.player, selection.otherPlayer);
        }

        /// <summary>
        /// Performs an action by the player via input action
        /// If the player clicked a tile it shall trigger the PlayerAction
        /// </summary>
        /// <param name="index"></param>
        private void PlayerAction(int index) {
            if (!IsGameRunning)
                return;

            if (!ActivePlayer.UsesInput())
                return;
            
            EndTurn(index);
        }

        /// <summary>
        /// Tries to perform an automatic move, usually by the computer
        /// </summary>
        private void TryPerformPlayerTurn() {
            var move = ActivePlayer.GetNextMove();
            if (move == -1)
                return;
            PlayerTurn(move);
        }

        private void CleanBoard() {
            _boardManager.ResetBoard();
            _turnManager.StopTurns();
        }

        /// <summary>
        /// Updates the players of the move that was performed
        /// </summary>
        /// <param name="index"></param>
        /// <param name="move"></param>
        private void OnMoveMade(int index, Moves move) {
            _otherPlayer.PerformMove(index, move);
            _player.PerformMove(index, move);
        }

        private void EndTurn(int index) {
            if (!IsGameRunning)
                return;
            _boardManager.SetTile(index, ActivePlayer);
            ChangeActivePlayer();
            NextTurn();
        }

        private void NextTurn() {
            if (IsGameRunning) {
                _turnManager.BeginTurn();
                return;
            }

            StopGame();
        }

        private void StopGame() {
            IsGameRunning = false;
            _turnManager.StopTurns();
            _player.ResetPlayer();
            _otherPlayer.ResetPlayer();
        }

        /// <summary>
        /// ActivePlayer is always the last player to make a turn
        /// </summary>
        /// <param name="boardState"></param>
        private void DecideWinner(BoardStates boardState) {
            if (boardState is BoardStates.Playing)
                return;

            SetPlayerBoardStates(boardState);
            IsGameRunning = false;
        }

        private void SetPlayerBoardStates(BoardStates state) {
            switch (state) {
                case BoardStates.Win: {
                    SetPlayerWinState(state);
                    break;
                }
                case BoardStates.Draw:
                    _otherPlayer.EndGame(BoardStates.Draw);
                    _player.EndGame(BoardStates.Draw);
                    break;
                case BoardStates.Lose:
                    SetPlayerWinState(state);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }

            OnGameOver?.Invoke(ActivePlayer);
        }

        private void SetPlayerWinState(BoardStates state) {
            if (_activePlayer == _player) {
                _player.EndGame(state is BoardStates.Win ? BoardStates.Win : BoardStates.Lose);
                _otherPlayer.EndGame(state is BoardStates.Win ? BoardStates.Lose : BoardStates.Win);
            }
            else if (_activePlayer == _otherPlayer) {
                _otherPlayer.EndGame(state is BoardStates.Win ? BoardStates.Win : BoardStates.Lose);
                _player.EndGame(state is BoardStates.Win ? BoardStates.Lose : BoardStates.Win);
            }
        }

        /// <summary>
        /// Performs the player's (computer) turn
        /// Adds a cancellationToken to allow for undo button to stop the move
        /// </summary>
        /// <param name="index"></param>
        private void PlayerTurn(int index) {
            if (!IsGameRunning)
                return;

            _source = new CancellationTokenSource();
            this.StartCoroutine(PerformPlayerTurn(index), _source.Token);
        }

        /// <summary>
        /// Waits a set amount of time before performing the player's turn
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private IEnumerator PerformPlayerTurn(int index) {
            yield return _computerTurnTime;
            EndTurn(index);
        }

        private void SetActivePlayer() {
            if (_player is null || _otherPlayer is null)
                return;
            ActivePlayer = _player.Move is Moves.X ? _player : _otherPlayer;
        }

        private void AssignRandomMoves(IPlayer player, IPlayer otherPlayer) {
            player.Move = RandomExtensions.GetChance(MoveXChance) ? Moves.X : Moves.O;
            otherPlayer.Move = _player.Move is Moves.X ? Moves.O : Moves.X;
            OnPlayersMovesSet?.Invoke(_player, _otherPlayer);
        }

        private void ChangeActivePlayer() {
            ActivePlayer = ActivePlayer == _player ? _otherPlayer : _player;
        }

        private void CancelPlayerMove() {
            if (_source is null)
                return;
            _source.Cancel();
            _source = null;
        }

        private void ActivateUndoButton(IPlayer player, IPlayer otherPlayer) {
            // XOR to check if one is false and the other is true
            var flag = player.UsesInput() ^ otherPlayer.UsesInput();
            OnPlayerVsComputer?.Invoke(flag);
        }
    }
}