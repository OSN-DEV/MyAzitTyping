namespace MyAzitTyping.AppUtil {
    class Constants {
        /// <summary>
        /// setting file
        /// </summary>
        public static readonly string SettingFile = OsnCsLib.Common.Util.GetAppPath() + @"app.settings";

        /// <summary>
        /// key mapping definition file
        /// </summary>
        public static readonly string RomanTable = OsnCsLib.Common.Util.GetAppPath() + @"romantable.txt";

        /// <summary>
        /// max random question counts
        /// </summary>
        public static readonly int MaxRandomCount = 30;
    }
}
