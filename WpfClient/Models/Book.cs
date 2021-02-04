using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.Models
{
    public class Book : INotifyPropertyChanged
    {
        private int bookID;
        private string bookName;
        private int bookCount;

        public int BookID
        {
            get { return bookID; }
            set
            {
                bookID = value;
                OnPropertyChanged("BookID");
            }
        }

        public string BookName
        {
            get { return bookName; }
            set
            {
                bookName = value;
                OnPropertyChanged("BookName");
            }
        }


        public int BookCount
        {
            get { return bookCount; }
            set
            {
                bookCount = value;
                OnPropertyChanged("BookCount");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
