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

namespace WpfTest.DataBinding
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


            InitializeTreeView();
            tvNode.ItemsSource = DataSrc.nodeTrees;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FcnTestTools.TestImportFcnWithStruct();
        }

        /// <summary>
        /// initialize treeview data and generate root nodes
        /// </summary>
        private void InitializeTreeView()
        {
            DataSrc.nodeTrees = new ObservableCollection<NodeTree>();
            DataSrc.nodeTrees.Add(new NodeTree()
            {
                ID = 0,
                Name = "root",
                ParentID = -1,
                Type = ClassType.Root,
            });
            DataSrc.nodeTrees[0].Nodes.Add(new NodeTree()
            {
                ID = 1,
                Name = "class_one",
                ParentID = 0,
                Type = ClassType.PClassOne,
            });
            DataSrc.nodeTrees[0].Nodes.Add(new NodeTree()
            {
                ID = 2,
                Name = "clasee_two",
                ParentID = 0,
                Type = ClassType.PClaseeTwo,
            });
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

        

        


        #region datagrid data binding and operation
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

        #endregion

        #region treeview data binding and operations

        /// <summary>
        /// when treeview selected changed, print the node info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tvNode_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            NodeTree t = tvNode.SelectedItem as NodeTree;
            switch(t.Type)
            {
                case ClassType.Root:
                case ClassType.PClassOne:
                case ClassType.PClaseeTwo:
                    Console.WriteLine( "root or parent node : " + t.Name );
                    break;
                case ClassType.ClassOne:
                    ClassOne one = t as ClassOne;
                    Console.WriteLine( string.Format("class one : {0}, radius : {1}." , one.Name, one.Radius ) );
                    break;
                case ClassType.ClassTwo:
                    ClassTwo two = t as ClassTwo;
                    Console.WriteLine(string.Format("class two : {0}, importance : {1}, time : {2}.", two.Name, two.Importance, two.Time));
                    break;
                default:
                    break;
                
            }
        }

        /// <summary>
        /// Add Node to the info
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNode_Click(object sender, RoutedEventArgs e)
        {
            switch( cbType.SelectedIndex )
            {
                case 0:
                    ClassOne n = new ClassOne()
                    {
                        ID = 5,
                        Name = tbNodeName.Text,
                        ParentID = 1,
                        Type = ClassType.ClassOne,

                        Radius = Convert.ToDouble(tbRadius.Text),
                    };
                    DataSrc.nodeTrees[0].Nodes[0].Nodes.Add(n);
                    
                    break;
                case 1:
                    ClassTwo no = new ClassTwo()
                    {
                        ID = 6,
                        Name = tbNodeName.Text,
                        ParentID = 2,
                        Type = ClassType.ClassTwo,

                        Importance = Convert.ToInt32(tbimportance.Text),
                        Time = Convert.ToInt32(tbTime.Text),
                    };
                    DataSrc.nodeTrees[0].Nodes[1].Nodes.Add(no);
                    break;
                default:
                    break;
            }
            RefreshTreeView();

        }

        /// <summary>
        /// refresh the tree view after changing the data
        /// expand the tree at the sanme time
        /// </summary>
        private void RefreshTreeView()
        {
            DataSrc.nodeTrees[0].IsExpanded = true;
            DataSrc.nodeTrees[0].Nodes[0].IsExpanded = true;
            DataSrc.nodeTrees[0].Nodes[1].IsExpanded = true;
            //tvNode.Items.Refresh();
        }

        private void btnDelNode_Click(object sender, RoutedEventArgs e)
        {

        }

        #endregion

        private void dgBind_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            Console.WriteLine("edit end!!!!!!");
            for (int i = 0; i < DataSrc.ssdata.Count; i++)
            {
                Console.WriteLine(DataSrc.ssdata[i].Name + ", " + DataSrc.ssdata[i].Comment);
            }
        }

    }
}
