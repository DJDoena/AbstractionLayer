namespace DoenaSoft.AbstractionLayer.UIServices.Implementations
{
    using System;
    using System.Windows;
    using Microsoft.Win32;

    public sealed class WindowUIServices : IUIServices
    {
        #region UIServices

        public Result ShowMessageBox(String text
            , String caption
            , Buttons genericButtons
            , Icon genericIcon)
        {
            MessageBoxButton buttons = GetButtons(genericButtons);

            MessageBoxImage icon = GetIcon(genericIcon);

            MessageBoxResult result = MessageBox.Show(text, caption, buttons, icon);

            Result genericResult = GetResult(result);

            return (genericResult);
        }

        public Boolean ShowOpenFileDialog(OpenFileDialogOptions options
           , out String fileName)
        {
            if (options == null)
            {
                throw (new ArgumentNullException(nameof(options)));
            }

            OpenFileDialog ofd = new OpenFileDialog();

            SetOpenFileDialogOptions(ofd, options);

            ofd.Multiselect = false;

            Nullable<Boolean> result = ofd.ShowDialog();

            if ((result.HasValue) && (result.Value))
            {
                fileName = ofd.FileName;

                return (true);
            }
            else
            {
                fileName = null;

                return (false);
            }
        }

        public Boolean ShowOpenFileDialog(OpenFileDialogOptions options
            , out String[] fileNames)
        {
            if (options == null)
            {
                throw (new ArgumentNullException(nameof(options)));
            }

            OpenFileDialog ofd = new OpenFileDialog();

            SetOpenFileDialogOptions(ofd, options);

            ofd.Multiselect = true;

            Nullable<Boolean> result = ofd.ShowDialog();

            if ((result.HasValue) && (result.Value))
            {
                fileNames = ofd.FileNames;

                return (true);
            }
            else
            {
                fileNames = null;

                return (false);
            }
        }

        public Boolean ShowSaveFileDialog(SaveFileDialogOptions options
            , out String fileName)
        {
            if (options == null)
            {
                throw (new ArgumentNullException(nameof(options)));
            }

            SaveFileDialog sfd = new SaveFileDialog();

            if (options.InitialFolder != null)
            {
                sfd.InitialDirectory = options.InitialFolder;
            }

            if (options.AddExtension.HasValue)
            {
                sfd.AddExtension = options.AddExtension.Value;
            }

            if (options.DefaultExt != null)
            {
                sfd.DefaultExt = options.DefaultExt;
            }

            if (options.Filter != null)
            {
                sfd.Filter = options.Filter;
            }

            if (options.RestoreFolder.HasValue)
            {
                sfd.RestoreDirectory = options.RestoreFolder.Value;
            }

            if (options.OverwritePrompt.HasValue)
            {
                sfd.OverwritePrompt = options.OverwritePrompt.HasValue;
            }

            if (options.ValidateNames.HasValue)
            {
                sfd.ValidateNames = options.ValidateNames.HasValue;
            }

            if (options.Title != null)
            {
                sfd.Title = options.Title;
            }

            if (options.FileName != null)
            {
                sfd.FileName = options.FileName;
            }

            Nullable<Boolean> result = sfd.ShowDialog();

            if ((result.HasValue) && (result.Value))
            {
                fileName = sfd.FileName;

                return (true);
            }
            else
            {
                fileName = null;

                return (false);
            }
        }

        public Boolean ShowFolderBrowserDialog(FolderBrowserDialogOptions options
            , out String folder)
        {
            using (System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (options == null)
                {
                    throw (new ArgumentNullException(nameof(options)));
                }

                if (options.Description != null)
                {
                    fbd.Description = options.Description;
                }

                if (options.RootFolder.HasValue)
                {
                    fbd.RootFolder = options.RootFolder.Value;
                }

                if (options.SelectedPath != null)
                {
                    fbd.SelectedPath = options.SelectedPath;
                }

                if (options.ShowNewFolderButton.HasValue)
                {
                    fbd.ShowNewFolderButton = options.ShowNewFolderButton.Value;
                }

                if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    folder = fbd.SelectedPath;

                    return (true);
                }
                else
                {
                    folder = null;

                    return (false);
                }
            }
        }

        #endregion

        #region MessageBox

        private MessageBoxButton GetButtons(Buttons buttons)
        {
            switch (buttons)
            {
                case (Buttons.YesNo):
                    {
                        return (MessageBoxButton.YesNo);
                    }
                case (Buttons.YesNoCancel):
                    {
                        return (MessageBoxButton.YesNoCancel);
                    }
                case (Buttons.OK):
                    {
                        return (MessageBoxButton.OK);
                    }
                default:
                    {
                        throw (new NotSupportedException());
                    }
            }
        }

        private MessageBoxImage GetIcon(Icon icon)
        {
            switch (icon)
            {
                case (Icon.Error):
                    {
                        return (MessageBoxImage.Error);
                    }
                case (Icon.Warning):
                    {
                        return (MessageBoxImage.Warning);
                    }
                case (Icon.Information):
                    {
                        return (MessageBoxImage.Information);
                    }
                case (Icon.Question):
                    {
                        return (MessageBoxImage.Question);
                    }
                default:
                    {
                        throw (new NotSupportedException());
                    }
            }
        }

        private Result GetResult(MessageBoxResult result)
        {
            switch (result)
            {
                case (MessageBoxResult.Yes):
                    {
                        return (Result.Yes);
                    }
                case (MessageBoxResult.No):
                    {
                        return (Result.No);
                    }
                case (MessageBoxResult.OK):
                    {
                        return (Result.OK);
                    }
                case (MessageBoxResult.Cancel):
                    {
                        return (Result.Cancel);
                    }
                default:
                    {
                        throw (new NotSupportedException());
                    }
            }
        }

        #endregion

        #region OpenFileDialog

        private static void SetOpenFileDialogOptions(OpenFileDialog ofd
            , OpenFileDialogOptions options)
        {
            if (options.InitialFolder != null)
            {
                ofd.InitialDirectory = options.InitialFolder;
            }

            if (options.CheckFileExists.HasValue)
            {
                ofd.CheckFileExists = options.CheckFileExists.Value;
            }

            if (options.Filter != null)
            {
                ofd.Filter = options.Filter;
            }

            if (options.RestoreFolder.HasValue)
            {
                ofd.RestoreDirectory = options.RestoreFolder.Value;
            }

            if (options.Title != null)
            {
                ofd.Title = options.Title;
            }

            if (options.FileName != null)
            {
                ofd.FileName = options.FileName;
            }
        }

        #endregion
    }
}