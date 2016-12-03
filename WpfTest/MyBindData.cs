using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest
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
    }
}
