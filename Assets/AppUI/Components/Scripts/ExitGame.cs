using Core.Extensions.Exit;
using UnityEngine;
using UnityEngine.UI;

namespace AppUI.Components.Scripts {
    public class ExitGame : MonoBehaviour {
        [SerializeField] private Button _exit;

        private void OnEnable() {
            _exit.onClick?.AddListener(Quit);
        }

        private void OnDisable() {
            _exit.onClick?.RemoveListener(Quit);
        }

        private void Quit() {
            ExitExtensions.Quit();
        }
    }
}