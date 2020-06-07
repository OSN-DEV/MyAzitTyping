using OsnCsLib.Data;

namespace MyAzitTyping.Data {
    public class AppRepository : AppDataBase<AppRepository> {

        #region Declaration   
        private static string _settingFile;
        #endregion

        #region Public Property
        public bool IsRandom30 { set; get; } = false;
        public bool ShowRomaji { set; get; } = false;
        #endregion

        #region Public Method
        public static AppRepository Init(string file) {
            _settingFile = file;
            GetInstanceBase(file);
            if (!System.IO.File.Exists(file)) {
                _instance.Save();
            }
            return _instance;
        }

        /// <summary>
        /// get instance
        /// </summary>
        /// <returns></returns>
        public static AppRepository GetInstance() {
            return GetInstanceBase();
        }

        /// <summary>
        /// save settings
        /// </summary>
        public void Save() {
            GetInstanceBase().SaveToXml(_settingFile);
        }
        #endregion
    }
}
