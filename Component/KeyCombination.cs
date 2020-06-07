using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Media;

namespace MyAzitTyping.Component {
    internal class KeyCombination {

        #region Declaration
        private List<Run> _keys = new List<Run>();

        private static class TextColor {
            public static readonly SolidColorBrush None = new SolidColorBrush(Colors.Transparent);
            public static readonly SolidColorBrush On = new SolidColorBrush(Color.FromRgb(51,51,51));
            public static readonly SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(200, 200, 200));
        }

        private int _index = 0;
        #endregion

        #region User Event
        public delegate void CompleteHandler();
        public event CompleteHandler Complete;
        #endregion

        #region Public Property
        public List<Run> Keys {
            get { return this._keys; }
        }
        #endregion

        #region Constructor
        public KeyCombination() {
        }
        #endregion

        #region PublicMethod
        /// <summary>
        /// set key combination
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="showRomaji"></param>
        public void Set(string keys, bool showRomaji) {
            this._keys.Clear();
            for(int i=0; i < keys.Length;i++) {
                var r = new Run(keys.Substring(i, 1).ToLower());
                r.Foreground = showRomaji? TextColor.Off : TextColor.None;
                this._keys.Add(r);
            }
            this._index = 0;
        }


        /// <summary>
        /// check key is hit. if key is correct, set index next value and complete event if need.
        /// </summary>
        /// <param name="key">input value</param>
        /// <returns></returns>
        public bool IsHit(string key) {
            if (this._keys[_index].Text != key) {
                for(int i=this._index; i< this._keys.Count;i++) {
                    this._keys[i].Foreground = TextColor.Off;
                }
                return false;
            }
            this._keys[_index].Foreground = TextColor.On;
            this._index++;
            if (this._index == this._keys.Count) {
                this.Complete?.Invoke();
            }
            return true;
        }
        #endregion
    }
}
