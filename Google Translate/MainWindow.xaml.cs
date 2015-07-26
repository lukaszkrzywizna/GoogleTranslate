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
using System.Windows.Media;
using HtmlAgilityPack;

namespace Google_Translate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private DataType _typeFlag;
        enum DataType
        {
            Image, Audio, Text, FileToDrop, Other
        };

        BrushConverter conventer = new BrushConverter();

        Brush clicked, notClicked;

        Dictionary<string, string> languages = new Dictionary<string, string>()
        {
            { "Polish", "pl" },
            { "English", "en" }
        };

        string inputLang;
        string resultLang;

        private List<bool> _successArray = new List<bool>();

        public MainWindow()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
        }
        private void OnShowUp(object sender, HotkeyEventArgs e)
        {
            _successArray.Clear();

            if (!Topmost)
            {
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshWindow();
            try
            {
                HotkeyManager.Current.AddOrReplace("Text", Key.F2, ModifierKeys.None, OnShowUp);
            }
            catch (HotkeyAlreadyRegisteredException ex)
            {
                this.Close();
            }
            this.SizeToContent = SizeToContent.Height;

            inputLang = languages["English"];
            resultLang = languages["Polish"];

            myNotifyIcon.TrayLeftMouseDown += MyNotifyIcon_TrayLeftMouseDown;

            Input.SizeChanged += Input_SizeChanged;
            clicked = (Brush)conventer.ConvertFromString("#C0C0C0");
            notClicked = (Brush)conventer.ConvertFromString("#E3E3E3");


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

        private bool _isHiding;
        private void MyNotifyIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {

            if (!Topmost)
            {
                this.Show();
                this.Topmost = true;
                this.Topmost = false;
            }

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HotkeyManager.Current.Remove("Text");
            myNotifyIcon.Dispose();
        }

        private void Translate()
        {
            if (String.IsNullOrEmpty(Input.Text))
            {
                return;
            }

            Result.Text = String.Empty;

            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", Input.Text, 
                String.Format("{0}|{1}", inputLang, resultLang));
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
            webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            string result = webClient.DownloadString(url);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(result);

            foreach (HtmlNode span in doc.DocumentNode.SelectNodes("//span[@title]"))
            {
                Result.Text += span.InnerText + " ";
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

        private void BtnPin_Click(object sender, RoutedEventArgs e)
        {
            this.Topmost = !this.Topmost;
            if (this.Topmost)
            {
                BtnPin.Background = clicked;
                PinBorder.Background = clicked;
            }
            else
            {

                BtnPin.Background = notClicked;
                PinBorder.Background = notClicked;
            }
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Translate();
            }
        }

        private void PlToEn_Click(object sender, RoutedEventArgs e)
        {
            inputLang = languages["Polish"];
            resultLang = languages["English"];

            PlToEn.Background = clicked;
            PlToEnBorder.Background = clicked;

            EnToPl.Background = notClicked;
            EnToPlBorder.Background = notClicked;
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (!Topmost)
            {
                this.Hide();
            }
        }

        private void EnToPl_Click(object sender, RoutedEventArgs e)
        {
            inputLang = languages["English"];
            resultLang = languages["Polish"];

            EnToPl.Background = clicked;
            EnToPlBorder.Background = clicked;

            PlToEn.Background = notClicked;
            PlToEnBorder.Background = notClicked;
        }
    }
}
