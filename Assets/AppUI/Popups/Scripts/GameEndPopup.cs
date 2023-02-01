using AppUI.Components.Scripts;
using Core;
using Core.Extensions.Board;
using Core.Player.Scripts;

namespace AppUI.Popups.Scripts {
    public class GameEndPopup : TextBase {
        public void SetPlayer(IPlayer player) {
            if (player.CurrentState is GameConventions.BoardStates.Draw) {
                SetText(player.CurrentState.GetMessage());
                return;
            }
            SetText(player.Name + " " + player.CurrentState.GetMessage());
        }
    }
}