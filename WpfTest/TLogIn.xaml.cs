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

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for TLogIn.xaml
    /// </summary>
    public partial class TLogIn : Window
    {
        public delegate void LoginDelegate(bool close);

        public LoginDelegate loginDelegate;

        private bool closeMainWnd = true;

        public TLogIn()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if( tbLogin.Text == "1" )
            {
                closeMainWnd = false;
                this.Close();
                
            }
            else
            {
                //loginDelegate(true);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            loginDelegate(closeMainWnd);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            closeMainWnd = true;
            this.Close();
        }
    }
}
