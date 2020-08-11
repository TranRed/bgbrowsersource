using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using System;
using System.IO;

namespace BGOverlayUpdater
{
    public partial class SettingsControl : UserControl
    {

        public SettingsControl(Settings settings)
        {
            InitializeComponent();
            UpdateSettings(settings);
        }

        public void UpdateSettings(Settings settings)
        {
            jsFileLocation.Text = settings.jsFileLocation;
        }

        void resetClicked(object sender, RoutedEventArgs e)
        {
            overlayUpdater.resetSession();
        }

        void fileDialogClicked(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            

            int lastSlash = jsFileLocation.Text.LastIndexOf("\\");
            fileDialog.InitialDirectory = jsFileLocation.Text.Substring(0, lastSlash);
            fileDialog.Filter = "JavaScript Files (.js)|*.js";

            if (fileDialog.ShowDialog() == true)
            {
                jsFileLocation.Text = fileDialog.FileName;
            }
            
        }

    }
}