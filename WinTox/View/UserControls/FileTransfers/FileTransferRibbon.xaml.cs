﻿using System;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using WinTox.ViewModel.FileTransfers;

namespace WinTox.View.UserControls.FileTransfers
{
    public sealed partial class FileTransferRibbon : UserControl
    {
        public FileTransferRibbon()
        {
            InitializeComponent();
        }

        private void FileTransferRibbonLoaded(object sender, RoutedEventArgs e)
        {
            var transferViewModel = (OneFileTransferViewModel) DataContext;
            VisualStateManager.GoToState(this, transferViewModel.State.ToString(), true);
        }

        private async void AcceptButtonClick(object sender, RoutedEventArgs e)
        {
            var folderPicker = new FolderPicker();
            folderPicker.FileTypeFilter.Add("*");
            var saveFolder = await folderPicker.PickSingleFolderAsync();
            if (saveFolder == null)
                return;

            var transferViewModel = (OneFileTransferViewModel) DataContext;
            var saveFile =
                await saveFolder.CreateFileAsync(transferViewModel.Name, CreationCollisionOption.GenerateUniqueName);
            await transferViewModel.AcceptTransferByUser(saveFile);
        }
    }
}