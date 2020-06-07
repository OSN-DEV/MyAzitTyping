using System.Collections.Generic;
using System.Windows;

namespace MyAzitTyping.AppUtil {
    internal class ErrorMessage {
        #region Declaration
        public enum ErrMsgId {
            RomanTableReadError,
            RomanTableNotFound,
        }
        private static Dictionary<ErrMsgId, string> _message = new Dictionary<ErrMsgId, string>() {
            { ErrMsgId.RomanTableReadError, "romanstable.txtの{0}行目が不正です。"},
            { ErrMsgId.RomanTableNotFound, "romanstable.txtがみつかりません。"},

        };
        #endregion

        #region Public Method
        /// <summary>
        /// show error message
        /// </summary>
        /// <param name="id"></param>
        public static void Show(ErrMsgId id) {
            MessageBox.Show(_message[id], "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        /// <summary>
        /// show error message
        /// </summary>
        /// <param name="id"></param>
        /// <param name="Replace"></param>
        public static void Show(ErrMsgId id, string Replace) {
            MessageBox.Show(_message[id].Replace("{0}", Replace), "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        #endregion

    }
}
