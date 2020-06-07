using MyAzitTyping.AppUtil;
using OsnCsLib.File;
using System;
using System.Collections.Generic;

namespace MyAzitTyping.Data {
    class RomanTableModel {
        #region Declartion
        private List<string> _kanas = new List<string>();
        private List<string> _keys = new List<string>();
        private int _index = 0;
        private int _count = 0;
        private bool _isRandom = false;
        private Random _rnd = new Random();
        #endregion

        #region Public Method
        /// <summary>
        /// load romans table
        /// </summary>
        public void Load() {
            using(var file = new FileOperator(Constants.RomanTable)) {
                if(!file.Exists()) {
                    ErrorMessage.Show(ErrorMessage.ErrMsgId.RomanTableNotFound);
                    return;
                }

                file.OpenForRead();
                while(!file.Eof) {
                    var data = file.ReadLine();
                    if (0 == data.Trim().Length) {
                        continue;
                    }
                    var pair = data.Split('\t');
                    this._keys.Add(pair[0]);
                    this._kanas.Add(pair[1] + (3 == pair.Length? pair[2]: ""));
                }
            }
        }

        /// <summary>
        /// reset index
        /// </summary>
        /// <param name="isRandom">true:if random</param>
        public void Reset(bool isRandom) {
            this._index = 0;
            this._count = isRandom?Constants.MaxRandomCount:this._kanas.Count;
            this._isRandom = isRandom;
        }

        /// <summary>
        /// get next kana and key
        /// </summary>
        /// <returns>result - true: if has next</returns>
        public(bool result, string kana, string key) GetNext() {
            if (this._index == this._count) {
                return (false, "", "");
            }
            int index;
            if (_isRandom) {
                index = this._rnd.Next(this._count);
            } else {
                index = this._index;
            }
            var r = (true, this._kanas[index], this._keys[index]);
            this._index++;
            return r;
        }

        #endregion
    }
}
