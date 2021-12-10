using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace FNet.FSA.WPF
{
    /// <summary>
    /// Interaction logic for DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page, INotifyPropertyChanged
    {
        private Core.Model.PathInfo pathInfo;
        public Core.Model.PathInfo PathInfo
        {
            get { return pathInfo; }
            set { pathInfo = value; OnChanged(); }
        }

        public DetailsPage()
        {
            InitializeComponent();
            this.DataContext = this;

            PathInfo = new Core.Model.PathInfo();
        }

        public void UpdateInterface(Core.Model.PathInfo info)
        {
            PathInfo = info;
        }

        private void expandBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (object item in mainTreeView.Items)
            {
                TreeViewItem treeItem = mainTreeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeItem != null)
                    treeItem.IsExpanded = true;
            }
        }

        private void collapseBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (object item in mainTreeView.Items)
            {
                TreeViewItem treeItem = mainTreeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                if (treeItem != null)
                    treeItem.IsExpanded = false;
            }
        }

        #region MVVM
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnChanged([CallerMemberName]string propname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
        #endregion
    }
}
