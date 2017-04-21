using System;

namespace DoenaSoft.AbstractionLayer.UIServices
{
    public interface IUIServices
    {
        Result ShowMessageBox(String text
            , String caption
            , Buttons buttons
            , Icon icon);

        Boolean ShowOpenFileDialog(OpenFileDialogOptions options
            , out String fileName);

        Boolean ShowOpenFileDialog(OpenFileDialogOptions options
            , out String[] fileNames);

        Boolean ShowSaveFileDialog(SaveFileDialogOptions options
            , out String fileName);

        Boolean ShowFolderBrowserDialog(FolderBrowserDialogOptions options
            , out String folder);
    }
}