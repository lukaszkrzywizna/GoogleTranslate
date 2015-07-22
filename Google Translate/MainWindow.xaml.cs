using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Automation;
using System.Runtime.InteropServices;
using System.Runtime.ExceptionServices;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Specialized;

namespace Google_Translate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string path = Path.GetTempPath() + "GoogleTranslate";
        private string xmlText;
        private DataType _typeFlag;
        enum DataType
        {
            Image, Audio, Text, FileToDrop, Other
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Send_Click(object sender, RoutedEventArgs e)
        {
            string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", Input.Text, "en|pl");
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            webClient.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0");
            webClient.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            string result = webClient.DownloadString(url);
            result = result.Substring(result.IndexOf("<span title=\"") + "<span title=\"".Length);
            result = result.Substring(result.IndexOf(">") + 1);
            result = result.Substring(0, result.IndexOf("</span>"));
            Result.Content = result.Trim();

            Thread.Sleep(4000);
           
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

            System.Windows.Forms.SendKeys.SendWait("^c");
            if (System.Windows.Forms.Clipboard.ContainsText())
            {
                var selectedText = System.Windows.Forms.Clipboard.GetText();
                Input.Text = selectedText;
            }

            Clipboard.Clear();

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
                    Clipboard.SetText(temp as string);
                    break;
                case DataType.Other:

                    break;
            }
        }


        //var element = AutomationElement.FocusedElement;

        //if (element != null)
        //{
        //    object pattern;
        //    if (element.TryGetCurrentPattern(TextPattern.Pattern, out pattern))
        //    {
        //        var tp = (TextPattern)pattern;
        //        var sb = new StringBuilder();

        //        foreach (var r in tp.GetSelection())
        //        {
        //            sb.AppendLine(r.GetText(-1));
        //        }

        //        var selectedText = sb.ToString();
        //    }
        //}


        //SendKeys.SendWait("^c");
        //if (System.Windows.Forms.Clipboard.ContainsText())
        //{
        //    var selectedText = System.Windows.Forms.Clipboard.GetText();
        //}

        //Thread thread = new Thread(() =>
        //{

        //});

        //thread.Start();
        //thread.Join(12000);

        //GetTextFromControlAtMousePosition();
        private string GetTextFromFocusedControl()
        {
            try
            {
                int activeWinPtr = GetForegroundWindow().ToInt32();
                int activeThreadId = 0, processId;
                activeThreadId = GetWindowThreadProcessId(activeWinPtr, out processId);
                int currentThreadId = GetCurrentThreadId();

                if (activeThreadId != currentThreadId)
                {
                    AttachThreadInput(activeThreadId, currentThreadId, true);
                }

                IntPtr activeCtrlId = GetFocus();

                return GetText(activeCtrlId);
            }
            catch (Exception exp)
            {
                System.Windows.MessageBox.Show(exp.Message);
                return exp.Message;
            }
        }

        //Get the text of the control at the mouse position
        private string GetTextFromControlAtMousePosition()
        {
            try
            {
                Point p;
                if (GetCursorPos(out p))
                {
                    IntPtr ptr = WindowFromPoint(p);
                    if (ptr != IntPtr.Zero)
                    {
                        return GetText(ptr);
                    }
                }
                return "";
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }

        //Get the text of a control with its handle
        private string GetText(IntPtr handle)
        {
            int maxLength = 500;
            IntPtr buffer = Marshal.AllocHGlobal((maxLength + 1) * 2);
            SendMessageW(handle, WM_GETTEXT, maxLength, buffer);
            string w = Marshal.PtrToStringUni(buffer);
            Marshal.FreeHGlobal(buffer);
            return w;
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);

        [DllImport("user32.dll", EntryPoint = "WindowFromPoint", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr WindowFromPoint(Point pt);

        [DllImport("user32.dll", EntryPoint = "SendMessageW")]
        public static extern int SendMessageW([InAttribute] System.IntPtr hWnd, int Msg, int wParam, IntPtr lParam);
        public const int WM_GETTEXT = 13;

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        internal static extern IntPtr GetFocus();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowThreadProcessId(int handle, out int processId);

        [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        internal static extern int AttachThreadInput(int idAttach, int idAttachTo, bool fAttach);
        [DllImport("kernel32.dll")]
        internal static extern int GetCurrentThreadId();

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpString, int nMaxCount);



        //System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    dispatcherTimer.Tick += dispatcherTimer_Tick;
        //    dispatcherTimer.Interval = new TimeSpan(0, 0, 5);
        //    dispatcherTimer.Start();
        //}


        //private void dispatcherTimer_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        TextSelectionReader text = new TextSelectionReader();
        //        var aa = text.TryGetSelectedTextFromActiveControl();
        //        System.Windows.MessageBox.Show(aa ?? "null");
        //    }
        //    catch (Exception exp)
        //    {
        //        System.Windows.MessageBox.Show(exp.Message);
        //    }
        //}

    }
}
