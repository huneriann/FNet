using FNet.FSA.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace FNet.FSA.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<Core.Model.PathInfo> dirInfos;
        public ObservableCollection<Core.Model.PathInfo> DirInfos
        {
            get { return dirInfos; }
            set { dirInfos = value; OnChanged(); }
        }

        private Core.Model.PathInfo selectedInfo;
        public Core.Model.PathInfo SelectedInfo
        {
            get { return selectedInfo; }
            set { selectedInfo = value; OnChanged(); }
        }


        private bool isRunning;
        public bool IsRunning
        {
            get { return isRunning; }
            set { isRunning = value; OnChanged(); }
        }

        private Visibility _visibility;
        public Visibility _Visibility
        {
            get { return _visibility; }
            set { _visibility = value; OnChanged(); }
        }

        private DetailsPage detailsPage;
        #endregion

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            detailsPage = new DetailsPage();
            detailsFrame.Content = detailsPage;

            DirInfos = new ObservableCollection<Core.Model.PathInfo>();
            SelectedInfo = new Core.Model.PathInfo();

            IsRunning = false;
            _Visibility = Visibility.Collapsed;
        }

        private void runBtn_Click(object sender, RoutedEventArgs e)
        {
            Analyzer.GetObject().Path = pathTxtBox.Text;
            Analyzer.GetObject().log = Log;
            Analyzer.GetObject().Execute();
            IsRunning = true;
            _Visibility = Visibility.Visible;
        }

        private void stopBtn_Click(object sender, RoutedEventArgs e)
        {
            Analyzer.GetObject().Dispose();
            IsRunning = false;
            _Visibility = Visibility.Collapsed;
        }

        private void Log(Core.Model.PathInfo info)
        {
            Thread t = new Thread(new ParameterizedThreadStart(AddToList));
            t.Start(info);
        }

        private void AddToList(object info)
        {
            this.Dispatcher.Invoke(() =>
            {
                Core.Model.PathInfo dirInfo = (Core.Model.PathInfo)info;
                DirInfos.Add(new Core.Model.PathInfo(dirInfo.Path, dirInfo.State, dirInfo.Info.Trim()));
            });
        }

        private void mainDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedInfo != null)
            {
                detailsPage.UpdateInterface(SelectedInfo);
            }
        }


        #region MVVM
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnChanged([CallerMemberName] string propname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
        #endregion
    }
}
