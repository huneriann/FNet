using System;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Core.Model.PathInfo> pathInfo;
        public ObservableCollection<Core.Model.PathInfo> PathInfo
        {
            get { return pathInfo; }
            set { pathInfo = value; OnChanged(); }
        }

        public DetailsPage()
        {
            InitializeComponent();
            this.DataContext = this;

            PathInfo = new ObservableCollection<Core.Model.PathInfo>();
        }

        public void UpdateInterface(Core.Model.PathInfo info)
        {
            //prGrid.SelectedObject = info;
            mainTreeView.Items.Clear();

            TreeViewItem pathTreeViewItem = new TreeViewItem();
            pathTreeViewItem.Header = "Path Info";

            var pathStateTxtBlock = new TextBlock();
            pathStateTxtBlock.Text = "State: " + info.State;
            var dateTimeTxtBlock = new TextBlock();
            dateTimeTxtBlock.Text = "Date Time: " + info.DateTime.ToString("dd.MM.yyyy HH:mm:ss");
            var infoTxtBlock = new TextBlock();
            infoTxtBlock.Text = "Info: " + info.Info;
            var isFileTxtBlock = new TextBlock();
            isFileTxtBlock.Text = "Is File: " + info.FileInfo.Exists;
            var isDirectoryTxtBlock = new TextBlock();
            isDirectoryTxtBlock.Text = "Is Directory: " + info.DirectoryInfo.Exists;

            pathTreeViewItem.Items.Add(infoTxtBlock);
            pathTreeViewItem.Items.Add(pathStateTxtBlock);
            pathTreeViewItem.Items.Add(dateTimeTxtBlock);
            pathTreeViewItem.Items.Add(isFileTxtBlock);
            pathTreeViewItem.Items.Add(isDirectoryTxtBlock);

            var subTreeViewItem = new TreeViewItem();
            if (new System.IO.DirectoryInfo(info.Path).Exists)
            {
                subTreeViewItem.Header = "Directory Info";

                subTreeViewItem.Items.Add("Full Path: " + info.DirectoryInfo.FullName);
                subTreeViewItem.Items.Add("Directory Path: " + info.DirectoryInfo.Name);
                subTreeViewItem.Items.Add("Attributes: " + info.DirectoryInfo.Attributes);
                subTreeViewItem.Items.Add("Creation DateTime: " + info.DirectoryInfo.CreationTime.ToString("dd.MM.yyyy HH:mm:ss"));
                subTreeViewItem.Items.Add("Extension: " + info.DirectoryInfo.Extension);
                subTreeViewItem.Items.Add("Last Modified DateTime: " + info.DirectoryInfo.LastAccessTime.ToString("dd.MM.yyyy HH:mm:ss"));
                subTreeViewItem.Items.Add("Parent: " + info.DirectoryInfo.Parent);
                subTreeViewItem.Items.Add("Root: " + info.DirectoryInfo.Root);
                try
                {
                    subTreeViewItem.Items.Add("Subdirectories Count: " + info.DirectoryInfo.GetDirectories().Length);
                    subTreeViewItem.Items.Add("Files Count: " + info.DirectoryInfo.GetFiles().Length);
                }
                catch (Exception ex)
                {
                    subTreeViewItem.Items.Add("Exception: " + ex.Message);
                }
            }
            if (new System.IO.FileInfo(info.Path).Exists)
            {
                subTreeViewItem.Header = "File Info";

                subTreeViewItem.Items.Add("Full Path: " + info.FileInfo.FullName);
                subTreeViewItem.Items.Add("File Path: " + info.FileInfo.Name);
                subTreeViewItem.Items.Add("Directory Path: " + info.FileInfo.DirectoryName);
                subTreeViewItem.Items.Add("Extension: " + info.FileInfo.Extension);
                subTreeViewItem.Items.Add("Attributes: " + info.FileInfo.Attributes);
                subTreeViewItem.Items.Add("Creation DateTime: " + info.FileInfo.CreationTime.ToString("dd.MM.yyyy HH:mm:ss"));
                subTreeViewItem.Items.Add("Is Read Only: " + info.FileInfo.IsReadOnly.ToString());
                subTreeViewItem.Items.Add("Last Modified DateTime: " + info.FileInfo.LastAccessTime.ToString("dd.MM.yyyy HH:mm:ss"));
                subTreeViewItem.Items.Add("Size (bytes): " + info.FileInfo.Length.ToString());
            }

            subTreeViewItem.IsExpanded = true;
            pathTreeViewItem.Items.Add(subTreeViewItem);
            pathTreeViewItem.IsExpanded = true;
            mainTreeView.Items.Add(pathTreeViewItem);
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
        private void OnChanged([CallerMemberName] string propname = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
        #endregion
    }
}
