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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;
using System.Runtime.InteropServices;

namespace WpfTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MyBindData sData;

        public MainWindow()
        {
            InitializeComponent();
            sData = new MyBindData()
                {
                    Name = "hello",
                    Comment = "world",
                };
            
            DataSrc.ssdata = new ObservableCollection<MyBindData>();
            DataSrc.ssdata.Add(new MyBindData()
            {
                Name = "gry",
                Comment = "Test",
            });

            dgBind.ItemsSource = DataSrc.ssdata;
            grid1.DataContext = sData;

        }

        /// <summary>
        /// login in function
        /// </summary>
        private void LoginIn()
        {
            TLogIn login = new TLogIn();
            login.loginDelegate += new TLogIn.LoginDelegate(SureClose);
            login.ShowDialog();
        }

        /// <summary>
        /// read txt file from 
        /// </summary>
        private void TestReadFileByLine()
        {
            string f;// = "";
            string fn = "./test.txt";
            StreamReader sw = new StreamReader(fn, Encoding.Default);
            while ((f = sw.ReadLine()) != null)
            {
                string[] ss = f.Split(new char[] { ' ' });
                for (int i = 0; i < ss.Length; i++)
                    Console.WriteLine(ss[i]);
            }
        }

        /// <summary>
        /// delegate method to close the main window
        /// </summary>
        /// <param name="close"></param>
        private void SureClose(bool close)
        {
            if( close )
            {
                this.Close();
            }
        }

        private void ChooseFileToOpen()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "mission load";
            //dialog.Filter = "233|*.txt|all files|*.*";
            dialog.InitialDirectory = "C:";
            dialog.ShowDialog();
            Console.WriteLine(dialog.FileName);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TestReferDll();   
        }

        private void TestReferDll()
        {
            int a = 135;
            int b = 5;
            Console.WriteLine("-------------------");
            Console.WriteLine(ReferDll.Add(a, b));
            Console.WriteLine(ReferDll.Minus(a, b));
            Console.WriteLine(ReferDll.g_Multi(a, b));
            Console.WriteLine(ReferDll.g_Div(a, b));

            int[] arr1 = { 1, 2, 3 };
            int[] arr2 = { 3, 2, 5 };
            Console.WriteLine(ReferDll.DotProd(arr1, arr2, arr1.Length));

            string str = "hello world";
            Console.WriteLine(ReferDll.CharArr(str, str.Length));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DataSrc.ssdata.Add(new MyBindData()
                {
                    Name = tbName.Text,
                    Comment = tbCommet.Text,
                });
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if( dgBind.SelectedIndex != -1 )
            {
                DataSrc.ssdata.RemoveAt(dgBind.SelectedIndex);
            }
        }

        private void btnOutput_Click(object sender, RoutedEventArgs e)
        {
            for(int i=0;i<DataSrc.ssdata.Count;i++)
            {
                Console.WriteLine("Name : " + DataSrc.ssdata[i].Name + ", Comment : " + DataSrc.ssdata[i].Comment);
            }
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if( dgBind.SelectedIndex != -1 )
            {
                DataSrc.ssdata[dgBind.SelectedIndex].Name = tbName.Text;
                DataSrc.ssdata[dgBind.SelectedIndex].Comment = tbCommet.Text;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("sData---Name : " + sData.Name + ", Comment : " + sData.Comment);
        }
    }
}
