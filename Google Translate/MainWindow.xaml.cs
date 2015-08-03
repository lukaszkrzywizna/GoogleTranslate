using System;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Specialized;
using NHotkey.Wpf;
using NHotkey;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using HtmlAgilityPack;
using System.Xml.Serialization;
using System.Xml;
using System.Configuration;

namespace Google_Translate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private DataType _typeFlag;
        private enum DataType
        {
            Image, Audio, Text, FileToDrop, Other
        };

        private BrushConverter _conventer = new BrushConverter();

        private Brush _clicked, _notClicked;

        private string _sourceLang;
        private string _resultLang;

        private List<bool> _successArray = new List<bool>();

        private Dictionary<string, string> languages = new Dictionary<string, string>()
        {
            { "Auto-detect", "auto" },
            { "Polish", "pl" },
            { "English", "en" },
            { "German", "de" },
            { "Spanish", "es" },
            { "Czech", "cs" },
            { "Slovak", "sk" },
            { "Dutch", "nl" },
            { "French", "fr" },
            { "Irish", "ga" },
            { "Italian", "it" },
            { "Latin", "la" },
            { "Macedonian", "mk" },
            { "Portuguese", "pt" },
            { "Russian", "ru" },
            { "Ukrainian", "uk" },
            { "Norwegian", "no" }

        };
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
        }

        #region Translate and organise clipboard
        /// <summary>
        /// Method start whole translate proccess. Saves content of clipboard and copy selected text. 
        /// Then execute Translate() method.
        /// </summary>
        private void StartAction()
        {
            _successArray.Clear();

            if (!Topmost)
            {
                this.Show();
                this.Topmost = true;
                this.Topmost = false;
            }

            try
            {
                object temp = null;

                if (Clipboard.ContainsImage())
                {
                    temp = Clipboard.GetImage();
                    _typeFlag = DataType.Image;
                }
                else if (Clipboard.ContainsAudio())
                {
                    temp = Clipboard.GetAudioStream();
                    _typeFlag = DataType.Audio;
                }
                else if (Clipboard.ContainsText())
                {
                    temp = Clipboard.GetText();
                    _typeFlag = DataType.Text;
                }
                else if (Clipboard.ContainsFileDropList())
                {
                    temp = Clipboard.GetFileDropList();
                    _typeFlag = DataType.FileToDrop;
                }
                else
                {
                    _typeFlag = DataType.Other;
                }

                Clear(10, 10, _successArray);

                if (SendWait("^c", 50, 10, _successArray))
                {
                    Input.Text = GetText(10, 10, _successArray);
                }

                Clear(10, 10, _successArray);

                switch (_typeFlag)
                {
                    case DataType.Audio:
                        Clipboard.SetAudio(temp as Stream);
                        break;
                    case DataType.Image:
                        Clipboard.SetImage(temp as BitmapSource);
                        break;
                    case DataType.FileToDrop:
                        Clipboard.SetFileDropList(temp as StringCollection);
                        break;
                    case DataType.Text:
                        SetText(temp as string, 10, 10, _successArray);
                        break;
                    case DataType.Other:

                        break;
                }

                if (_successArray.Contains(false))
                {
                    Result.Text = "/błąd/";
                    return;
                }

                Translate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upss! Błąd! Spróbuj jeszcze raz :)");
            }

        }

        /// <summary>
        /// Main translate method. It creates Webclient and takes result 
        /// </summary>
        private void Translate()
        {
            if (String.IsNullOrEmpty(Input.Text))
            {
                return;
            }
            if (_sourceLang == _resultLang)
            {
                SetSource.SelectedIndex = SetSource.Items.IndexOf("Auto-detect");
            }

            Result.Text = String.Empty;

            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", Input.Text,
                String.Format("{0}|{1}", _sourceLang, _resultLang));

            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
            webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            string result = String.Empty;
            try
            {
                result = webClient.DownloadString(url);
            }
            catch (Exception ex)
            {
                Result.Text = ex.Message;
                return;
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);

            foreach (HtmlNode span in doc.DocumentNode.SelectNodes("//span[@title]"))
            {
                Result.Text += span.InnerText + " ";
            }

        }

        #endregion


        #region Clipboard Helper Methods

        private void SetText(string text, int tries, int timespan, List<bool> success)
        {
            for (int i = 0; i < tries; i++)
            {
                try
                {
                    Thread.Sleep(timespan);
                    Clipboard.SetText(text);
                    success.Add(true);
                    return;
                }
                catch (COMException ex)
                {
                    Console.WriteLine("SetText. Try numb: {0}", i);
                    if (i == tries - 1)
                    {
                        success.Add(false);
                        return;
                    }
                }
            }
        }

        private string GetText(int tries, int timespan, List<bool> success)
        {
            for (int i = 0; i < tries; i++)
            {
                try
                {
                    Thread.Sleep(timespan);
                    var temp = Clipboard.GetText();
                    Console.WriteLine("GetText: {0}", temp);
                    success.Add(true);
                    return temp;
                }
                catch (COMException ex)
                {
                    Console.WriteLine("GetText. Try numb: {0}", i);
                    if (i == tries - 1)
                    {
                        success.Add(false);
                        return String.Empty;
                    }
                }
            }
            return String.Empty;
        }

        private bool ContainsText(int tries, int timespan, List<bool> success)
        {
            for (int i = 0; i < tries; i++)
            {
                try
                {
                    Thread.Sleep(timespan);
                    var temp = Clipboard.ContainsText();
                    if (!temp) continue;
                    success.Add(true);
                    Console.WriteLine(temp);
                    return temp;
                }
                catch (COMException ex)
                {
                    Console.WriteLine("ContainsText. Try numb: {0}", i);
                    if (i == tries - 1)
                    {
                        success.Add(false);
                        return false;
                    }
                }
            }
            return false;
        }

        private void Clear(int tries, int timespan, List<bool> success)
        {
            for (int i = 0; i < tries; i++)
            {
                try
                {
                    Thread.Sleep(timespan);
                    Clipboard.Clear();
                    success.Add(true);
                    return;
                }
                catch (COMException ex)
                {
                    Console.WriteLine("Clear. Try numb: {0}", i);
                    if (i == tries - 1)
                    {
                        success.Add(false);
                        return;
                    }
                }
            }
        }

        private bool SendWait(string keys, int tries, int timespan, List<bool> success)
        {
            for (int i = 0; i < tries; i++)
            {
                System.Windows.Forms.SendKeys.SendWait("^c");
                Thread.Sleep(timespan);
                var go = ContainsText(10, 10, success);
                if (go)
                {
                    return go;
                }
            }
            return false;
        }

        #endregion

        #region Event Handlers

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshWindow();

            if (!HotKeyActioner.Initialize())
            {
                this.Close();
            }
            
            HotKeyActioner.AssignActionMethod(StartAction);

            this.SizeToContent = SizeToContent.Height;

            PrepareComboBoxes();

            myNotifyIcon.TrayLeftMouseDown += MyNotifyIcon_TrayLeftMouseDown;

            Input.SizeChanged += Input_SizeChanged;

            _clicked = (Brush)_conventer.ConvertFromString("#4B8DF8");
            _notClicked = (Brush)_conventer.ConvertFromString("#E3E3E3");
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HotKeyActioner.SaveAndUnregister(SetSource.SelectedItem.ToString(), GetResult.SelectedItem.ToString());
            myNotifyIcon.Dispose();
        }

        private void PrepareComboBoxes()
        {
            string source = ConfigurationManager.AppSettings["Source"];
            string result = ConfigurationManager.AppSettings["Result"];

            _sourceLang = languages[source];
            _resultLang = languages[result];

            var list1 = languages.Keys.ToList();
            list1.Sort();

            var list2 = languages.Keys.ToList();
            list2.Sort();
            list2.Remove("Auto-detect");

            SetSource.ItemsSource = list1;
            GetResult.ItemsSource = list2;

            SetSource.SelectedIndex = list1.IndexOf(source);
            GetResult.SelectedIndex = list2.IndexOf(result);
        }

        private void RefreshWindow()
        {
            var desktopWorkingArea = SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Right - this.ActualWidth;
            this.Top = desktopWorkingArea.Bottom - this.ActualHeight;
        }

        private void Input_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshWindow();
        }


        private void MyNotifyIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {

            if (!Topmost)
            {
                this.Show();
                this.Topmost = true;
                this.Topmost = false;
            }

        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            Translate();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnHide_Click(object sender, RoutedEventArgs e)
        {
            if (!Topmost)
            {
                this.Hide();
            }
        }

        /// <summary>
        /// Pin/Unpin MainWindow and change BtnPin style
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPin_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
            if (this.Topmost)
            {
                BtnPin.Style = (Style)this.Resources["BlueButton"];
                PinBorder.Background = _clicked;
            }
            else
            {
                BtnPin.Style = (Style)this.Resources["GrayButton"];
                PinBorder.Background = _notClicked;
            }
        }

        /// <summary>
        /// Start translate when Enter was hit;
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Translate();
            }
        }
        /// <summary>
        /// Assign language to input property when selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetSource_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _sourceLang = languages[SetSource.SelectedItem.ToString()];
        }
        /// <summary>
        /// Assign language to result property when selection changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetResult_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _resultLang = languages[GetResult.SelectedItem.ToString()];
        }
        /// <summary>
        /// Opens Window with Shortcut settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenShortcut_Click(object sender, RoutedEventArgs e)
        {
            var shortShortcut = new ChangeShortcut();
            shortShortcut.Show();
        }
        /// <summary>
        /// Convert the way of translated language
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConvertLang_Click(object sender, RoutedEventArgs e)
        {
            int indexForSource = SetSource.Items.IndexOf(GetResult.SelectedItem);
            int indexForResult = GetResult.Items.IndexOf(SetSource.SelectedItem);
            SetSource.SelectedIndex = indexForSource;
            GetResult.SelectedIndex = indexForResult;
        }

        #endregion
    }
}
