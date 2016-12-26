using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTest.GUdpClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IPAddress groupAddress;

        private int portNumber = 8080;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tbIpAddress.Text = GetLocalIpAddress();
            tbPortNumber.Text = portNumber.ToString();
        }

        private void btnSendMsg_Click(object sender, RoutedEventArgs e)
        {
            Send( tbSendContent.Text );
        }

        private void btnGetMyIP_Click(object sender, RoutedEventArgs e)
        {
            tbIpAddress.Text = GetLocalIpAddress();
        }

        /// <summary>
        /// send message to the destination
        /// </summary>
        /// <param name="message"></param>
        private void Send(String message)
        {
            if (!Int32.TryParse(tbPortNumber.Text, out portNumber) || !IPAddress.TryParse(tbIpAddress.Text, out groupAddress) || portNumber <= 0)
            {
                MessageBox.Show("format wrong...");
                return;
            }

            UdpClient sender = new UdpClient();
            IPEndPoint groupEP = new IPEndPoint(groupAddress, portNumber);
            try
            {
                Console.WriteLine("Sending datagram : {0}", message);
                byte[] bytes = Encoding.Default.GetBytes(message);
                sender.Send(bytes, bytes.Length, groupEP);
                sender.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

        }



        /// <summary>
        /// get local ip address as the default server address
        /// </summary>
        private string GetLocalIpAddress()
        {
            //获取本机可用IP地址
            string addr = "127.0.0.1";
            IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ipa in ips)
            {
                if (ipa.AddressFamily == AddressFamily.InterNetwork)
                {
                    addr = ipa.ToString();
                    break;
                }
            }
            return addr;
        }

        

        

        

    }
}
