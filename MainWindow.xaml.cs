using MyAzitTyping.AppUtil;
using MyAzitTyping.Component;
using MyAzitTyping.Data;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace MyAzitTyping {
    /// <summary>
    /// My Azit Typing main
    /// </summary>
    public partial class MainWindow : Window {

        #region Declaration
        private bool _isPracticeMode = true;
        private KeyCombination _keyCombination = new KeyCombination();
        private RomanTableModel _romanTable = new RomanTableModel();
        #endregion

        #region Constructor
        public MainWindow() {
            InitializeComponent();
            this.Initialize();
        }
        #endregion

        #region Event
        private void Window_PreviewKeyDown(object sender, KeyEventArgs e) {
            if (this._isPracticeMode) {
                if (Key.Escape == e.Key) {
                    e.Handled = true;
                    this.StopPractice();
                } else {
                    //e.Handled = true;
                    //this._keyCombination.IsHit(e.Key.ToString().ToLower());
                }
            } else {
                if(Key.Space == e.Key) {
                    e.Handled = true;
                    this.StartPractice();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_TextInput(object sender, TextCompositionEventArgs e) {
            if (this._isPracticeMode) {
                e.Handled = true;
                this._keyCombination.IsHit(e.Text.ToLower());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartStop_Click(object sender, RoutedEventArgs e) {
            if (this._isPracticeMode) {
                this.StopPractice();
            } else {
                this.StartPractice();
            }
        }

        /// <summary>
        /// complete questions
        /// </summary>
        private void CompleteQuestion() {
            if (!this.SetQuestion()) {
                this.StopPractice();
            }
        }
        #endregion

        #region Private Method
        /// <summary>
        /// initialize app
        /// </summary>
        private void Initialize() {
            // set app title
            var assembly = Assembly.GetExecutingAssembly().GetName();
            var ver = assembly.Version;
            this.Title = $"{assembly.Name}({ver.Major}.{ver.Minor}.{ver.Build}.{ver.Revision})";

            // load keymapping files
            this._romanTable.Load();

            // restore settings
            var settings = AppRepository.Init(Constants.SettingFile);
            this.cRandom.IsChecked = settings.IsRandom30;
            this.cShowRomaji.IsChecked = settings.ShowRomaji;

            // others
            this._keyCombination.Complete += this.CompleteQuestion;

            this._isPracticeMode = false;
            this.SetScreenWithMode();
        }

        /// <summary>
        /// set screen according to practice mode
        /// </summary>
        private void SetScreenWithMode() {
            this.cRandom.Visibility = this._isPracticeMode ? Visibility.Hidden : Visibility.Visible;
            this.cShowRomaji.Visibility = this._isPracticeMode ? Visibility.Hidden : Visibility.Visible;
            this.cKana.Visibility = this._isPracticeMode ? Visibility.Visible : Visibility.Hidden;
            this.cKey.Visibility = this._isPracticeMode ? Visibility.Visible : Visibility.Hidden;
            this.cStartStop.Content = this._isPracticeMode ? "Stop" : "Start";
        }

        /// <summary>
        /// start parctice
        /// </summary>
        private void StartPractice() {
            var settings = AppRepository.GetInstance();
            settings.IsRandom30 = (true == this.cRandom.IsChecked);
            settings.ShowRomaji = (true == this.cShowRomaji.IsChecked);
            settings.Save();

            this._romanTable.Reset(settings.IsRandom30);
            this._isPracticeMode = true;
            this.SetQuestion();
            this.SetScreenWithMode();
        }

        /// <summary>
        /// stop practice
        /// </summary>
        private void StopPractice() {
            this._isPracticeMode = false;
            this.SetScreenWithMode();
        }
        
        /// <summary>
        /// set question
        /// </summary>
        /// <param name="hiragana"></param>
        /// <param name="combination"></param>
        /// <returns>true:success, false:otherwise</returns>
        private bool SetQuestion() {
            var (result, kana, key) = this._romanTable.GetNext();
            if (!result) {
                return false;
            }
            this.cKana.Text = kana;
            this._keyCombination.Set(key, AppRepository.GetInstance().ShowRomaji);
            this.cKey.Inlines.Clear();
            foreach (var r in this._keyCombination.Keys) {
                this.cKey.Inlines.Add(r);
            }
            return true;
        }
        #endregion
    }
}
