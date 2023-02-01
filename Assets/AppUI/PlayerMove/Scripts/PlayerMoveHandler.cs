using AppUI.MoveTracker.Scripts;
using AppUI.PlayerTracker.Scripts;
using Core.Player.Scripts;
using Core.SpriteDataBase.Scripts;
using UnityEngine;

namespace AppUI.PlayerMove.Scripts {
    public class PlayerMoveHandler : MonoBehaviour {
        [Header("Player")]
        [SerializeField] private MovePresenter _playerMovePresenter;
        [SerializeField] private TitlePresenter _playerTitlePresenter;
        [Header("Other Player")]
        [SerializeField] private MovePresenter _otherPlayerMovePresenter;
        [SerializeField] private TitlePresenter _otherPlayerTitlePresenter;

        [Header("Data")]
        [SerializeField] private SpriteDataBaseSO _spriteDataBase;

        public void InitializePlayerMove(IPlayer player, IPlayer otherPlayer) {
            if (player is null | otherPlayer is null)
                return;
            InitializePlayerComponents(player, _playerMovePresenter, _playerTitlePresenter);
            InitializePlayerComponents(otherPlayer, _otherPlayerMovePresenter, _otherPlayerTitlePresenter);
        }

        public void SetActivePlayerTurn(IPlayer player) {
            if (player is null)
                return;
            _playerMovePresenter.ToggleMode(player.Move);
            _otherPlayerMovePresenter.ToggleMode(player.Move);
        }
        
        private void InitializePlayerComponents(IPlayer player, MovePresenter movePresenter, TitlePresenter titlePresenter) {
            if (player is null)
                return;
            if (movePresenter is null)
                return;
            if (titlePresenter is null)
                return;
            
            movePresenter.SetImage(_spriteDataBase.GetSprite(player.Move));
            movePresenter.SetMove(player.Move);
            titlePresenter.SetTitle(player.Name);
        }
    }
}
