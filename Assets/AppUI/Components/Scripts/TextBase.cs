using TMPro;
using UnityEngine;

namespace AppUI.Components.Scripts {
    public abstract class TextBase : MonoBehaviour {
        [SerializeField] protected TMP_Text _text;

        protected virtual string Message {
            get => _text.text;
            set {
                if (string.IsNullOrEmpty(value))
                    return;
                _text.text = value;
            }
        }

        public virtual void SetText(string text) {
            Message = text;
        }
    }
}
