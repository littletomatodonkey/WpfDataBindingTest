using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.DataBinding
{
    class MyBindData : INotifyPropertyChanged
    {
        private string _name;
        private string _comment;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                if( PropertyChanged != null )
                {
                    Changed("Name");
                }
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
                if (PropertyChanged != null)
                {
                    Changed("Comment");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Changed(string PropertyName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(PropertyName));
        }
    }

    class DataSrc
    {
        public static ObservableCollection<MyBindData> ssdata;

        public static ObservableCollection<NodeTree> nodeTrees;
    }

    class NodeTree
    {
        public NodeTree()
        {
            this.Nodes = new ObservableCollection<NodeTree>();
            this.ParentID = -1;
            this.IsExpanded = false;
        }

        public ClassType Type { get; set; }

        public string Icon { get; set; }

        public string Name { get; set; }

        public int ID { get; set; }

        public int ParentID { get; set; }

        public bool IsExpanded { get; set; }

        public double Lat{ get; set; }
        public double Lon{ get; set; }
        public double Hei{ get; set; }
        
        public ObservableCollection<NodeTree> Nodes { get; set; }
    }

    class ClassOne : NodeTree
    {
        public double Radius { get; set; }
    }

    class ClassTwo : NodeTree
    {
        public int Importance { get; set; }
        public int Time { get; set; }
    }

    enum ClassType
    {
        Root = 0,
        PClassOne = 1,
        PClaseeTwo = 2,
        ClassOne = 4,
        ClassTwo = 8,
    }
}
