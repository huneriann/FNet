using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Shapes;
using FNet.FSA.Core;

namespace FNet.FSA.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<FNet.FSA.Core.Model.DirectoryInfo> dirInfos;
        public ObservableCollection<FNet.FSA.Core.Model.DirectoryInfo> DirInfos
        {
            get { return dirInfos; } 
            set { dirInfos = value; OnChanged(); }
        }


        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            
            DirInfos = new ObservableCollection<Core.Model.DirectoryInfo>();
        }

        private void runBtn_Click(object sender, RoutedEventArgs e)
        {
            FSA.Core.Analyzer.GetObject().Path = pathTxtBox.Text;
            FSA.Core.Analyzer.GetObject().log = Log;
            FSA.Core.Analyzer.GetObject().Execute();
        }

        private void Log(FSA.Core.Model.DirectoryInfo info)
        {
            Thread t = new Thread(new ParameterizedThreadStart(AddToList));
            t.Start(info);
        }

        private void AddToList(object info)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                Core.Model.DirectoryInfo dirInfo = (Core.Model.DirectoryInfo)info;
                DirInfos.Add(new Core.Model.DirectoryInfo(dirInfo.Path, dirInfo.State));
            });
        }


        //PropertyChanged event realisation
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnChanged([CallerMemberName]string propname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
    }
}
