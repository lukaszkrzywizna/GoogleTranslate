using NHotkey;
using NHotkey.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Google_Translate
{
    public class HotKeyActioner
    {

        #region Properties
        public static ModifierKeys FirstKey = ModifierKeys.None;
        public static ModifierKeys SecondKey = ModifierKeys.None;
        public static Key ThirdKey = Key.F2;

        private static Action methodToExecute;

        public static List<object> selectedKeys;

        #endregion

        public static bool Initialize()
        {
            var fKey = ConfigurationManager.AppSettings["FirstKey"];
            var sKey = ConfigurationManager.AppSettings["SecondKey"];
            var tKey = ConfigurationManager.AppSettings["ThirdKey"];

            FirstKey = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), fKey.ToString());
            SecondKey = (ModifierKeys)Enum.Parse(typeof(ModifierKeys), sKey.ToString());
            ThirdKey = (Key)Enum.Parse(typeof(Key), tKey.ToString());

            return RegisterHotKey();
        }

        internal static List<object> GetSelectedKeys()
        {
            if (selectedKeys == null)
            {
                selectedKeys = new List<object>();
                foreach (var key in Enum.GetValues(typeof(Key)))
                {
                    if (Regex.IsMatch(key.ToString(), "[0-9]") && key.ToString().Length == 2 || Regex.IsMatch(key.ToString(), "[F]+[0-9]([012])?"))
                    {
                        selectedKeys.Add(key);
                    }
                }
            }

            return selectedKeys;
        }
        internal static void AssignActionMethod(Action doIt)
        {
            methodToExecute += doIt;
        }

        internal static void DoAction(object sender, HotkeyEventArgs e)
        {
            if (methodToExecute != null)
            {
                methodToExecute.Invoke();
            }
        }

        internal static bool RegisterHotKey(object firstKey, object secondKey, object thirdKey)
        {
            try
            {
                HotkeyManager.Current.AddOrReplace("HotKeyTranslate", (Key)thirdKey, (ModifierKeys)firstKey | (ModifierKeys)secondKey, DoAction);
                FirstKey = (ModifierKeys)firstKey;
                SecondKey = (ModifierKeys)secondKey;
                ThirdKey = (Key)thirdKey;
                return true;
            }
            catch (HotkeyAlreadyRegisteredException ex)
            {
                return false;
            }
        }

        internal static bool RegisterHotKey()
        {
            try
            {
                HotkeyManager.Current.AddOrReplace("HotKeyTranslate", ThirdKey, FirstKey | SecondKey, DoAction);
                return true;
            }
            catch (HotkeyAlreadyRegisteredException ex)
            {
                return false;
            }
        }

        internal static void SaveAndUnregister(string source, string result)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["FirstKey"].Value = FirstKey.ToString();
            config.AppSettings.Settings["SecondKey"].Value = SecondKey.ToString();
            config.AppSettings.Settings["ThirdKey"].Value = ThirdKey.ToString();
            config.AppSettings.Settings["Source"].Value = source;
            config.AppSettings.Settings["Result"].Value = result;
            config.Save(ConfigurationSaveMode.Full);
            ConfigurationManager.RefreshSection("appSettings");

            HotkeyManager.Current.Remove("HotKeyTranslate");
        }
    }
}
