using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using NHotkey.Wpf;
using NHotkey;

namespace Google_Translate
{
    /// <summary>
    /// Interaction logic for ChangeShortcut.xaml
    /// </summary>
    public partial class ChangeShortcut : Window
    {
        public ChangeShortcut()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FirstKey.ItemsSource = Enum.GetValues(typeof(ModifierKeys));
            SecondKey.ItemsSource = Enum.GetValues(typeof(ModifierKeys));
            ThirdKey.ItemsSource = HotKeyActioner.GetSelectedKeys();
            
            FirstKey.SelectedIndex = FirstKey.Items.IndexOf((object)HotKeyActioner.FirstKey);
            SecondKey.SelectedIndex = SecondKey.Items.IndexOf((object)HotKeyActioner.SecondKey);
            ThirdKey.SelectedIndex = ThirdKey.Items.IndexOf((object)HotKeyActioner.ThirdKey);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(!HotKeyActioner.RegisterHotKey(FirstKey.SelectedItem, SecondKey.SelectedItem, ThirdKey.SelectedItem))
            {
                MessageBox.Show("Nie udało się zarejestrować skrótu. Spróbuj ponownie później", "Błąd");
            }
            else
            {
                this.Close();
            }
        }
        
    }
}
