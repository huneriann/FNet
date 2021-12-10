using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace FNet.FSA.WPF
{
    /// <summary>
    /// Interaction logic for DetailsPage.xaml
    /// </summary>
    public partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            InitializeComponent();
        }

        public void UpdateInterface(Core.Model.DirectoryInfo info)
        {
            typeTxtBlock.Text = info.State.ToString();
            pathTxtBlock.Text = info.Path;
            dateTimeTxtBlock.Text = info.DateTime.ToString("dd.MM.yyyy HH:mm:ss");
            infoTxtBlock.Text = info.Info;

            FileInfo fileInfo = new FileInfo(info.Path);
            try
            {
                fileDirectoryTxtBlock.Text = fileInfo.DirectoryName;
            }
            catch { }
            try
            {
                existsFileTxtBlock.Text = fileInfo.Exists.ToString();
            }
            catch { }
            try
            {
                isReadOnlyFileTxtBlock.Text = fileInfo.IsReadOnly.ToString();
            }
            catch { }
            try
            {
                sizeFileTxtBlock.Text = fileInfo.Length.ToString() + " bytes";
            }
            catch { }
            try
            {
                creationDateTimeTxtBlock.Text = fileInfo.CreationTime.ToString("dd.MM.yyyy HH:mm:ss");
            }
            catch { }
            try
            {
                changeDateTimeTxtBlock.Text = fileInfo.LastAccessTime.ToString("dd.MM.yyyy HH:mm:ss");
            }
            catch { }
            try
            {
                string modifiedBy = fileInfo.GetAccessControl()
                    .GetOwner(typeof(System.Security.Principal.NTAccount)).ToString();
                modifiedByTxtBlock.Text = modifiedBy;
            }
            catch { }
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
    }
}
