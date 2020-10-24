using gui.ModelView;
using core;
using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Runtime.InteropServices;

namespace gui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly StartupCollectionModelView modelView = new StartupCollectionModelView();

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void ItemDropped(object sender, DragEventArgs e)
        {
            try
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                {
                    string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];

                    foreach (var file in files)
                    {
                        modelView.AddFile(file);
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error dropping files");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dgParameters.DataContext = modelView;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            modelView.SaveConfig();
        }

        private void ParamsList_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if (dgParameters.SelectedItem == null) return;
                var li = (StartupParameter)dgParameters.SelectedItem;
                modelView.RemoveFile(li);
            }
            catch (InvalidCastException) { /* invalid item */ }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var senderascombo = sender as ComboBox;
            var parent = senderascombo?.Parent;
            if (parent is null) return;

            var row = parent as DataGridCell;


            if (row?.DataContext is StartupParameter param)
            {
                modelView.UpdateParameterMethod(param, (StartupMethod)senderascombo.SelectedItem);
            }
        }

        private void dgParameters_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (dgParameters.SelectedItem == null) return;
            if (!(dgParameters.SelectedItem is StartupParameter param)) return;
            if (!File.Exists(param.FullPath)) return;

            System.Diagnostics.Process.Start(param.FullPath.GetParent());

        }
    }
}
