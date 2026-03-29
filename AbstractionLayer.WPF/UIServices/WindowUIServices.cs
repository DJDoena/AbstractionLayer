using System;

namespace DoenaSoft.AbstractionLayer.UIServices
{
    /// <summary>
    /// Standard implementation of <see cref="IUIServices"/> for <see cref="System.Windows.Window"/>.
    /// </summary>
    public sealed class WindowUIServices : IUIServices
    {
        #region UIServices

        /// <summary>
        /// Shows a MessageBox.
        /// </summary>
        /// <param name="text">The main text</param>
        /// <param name="caption">The MessageBox title</param>
        /// <param name="buttons">The buttons to be shown</param>
        /// <param name="icon">The MessageBox icon</param>
        /// <returns>Which button was pressed</returns>
        public MessageResult ShowMessageBox(string text, string caption, MessageButtons buttons, MessageIcon icon)
        {
            var windowsButtons = this.GetButtons(buttons);

            var windowsIcon = this.GetIcon(icon);

            var windowsResult = System.Windows.MessageBox.Show(text, caption, windowsButtons, windowsIcon);

            var result = this.GetResult(windowsResult);

            return result;
        }

        /// <summary>
        /// Opens a "open file" dialog.
        /// </summary>
        /// <param name="options">The dialog options</param>
        /// <param name="fileName">The selected file name</param>
        /// <returns>Whether the dialog was confirmed or cancelled</returns>
        public bool ShowOpenFileDialog(OpenFileDialogOptions options, out string fileName)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var ofd = new Microsoft.Win32.OpenFileDialog();

            SetOpenFileDialogOptions(ofd, options);

            ofd.Multiselect = false;

            var result = ofd.ShowDialog();

            if (result.HasValue && result.Value)
            {
                fileName = ofd.FileName;

                return true;
            }
            else
            {
                fileName = null;

                return false;
            }
        }

        /// <summary>
        /// Opens a "open file" dialog.
        /// </summary>
        /// <param name="options">The dialog options</param>
        /// <param name="fileNames">The selected file names</param>
        /// <returns>Whether the dialog was confirmed or cancelled</returns>
        public bool ShowOpenFileDialog(OpenFileDialogOptions options, out string[] fileNames)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var ofd = new Microsoft.Win32.OpenFileDialog();

            SetOpenFileDialogOptions(ofd, options);

            ofd.Multiselect = true;

            var result = ofd.ShowDialog();

            if (result.HasValue && result.Value)
            {
                fileNames = ofd.FileNames;

                return true;
            }
            else
            {
                fileNames = null;

                return false;
            }
        }

        /// <summary>
        /// Opens a "save file" dialog.
        /// </summary>
        /// <param name="options">The dialog options</param>
        /// <param name="fileName">The selected file name</param>
        /// <returns>Whether the dialog was confirmed or cancelled</returns>
        public bool ShowSaveFileDialog(SaveFileDialogOptions options, out string fileName)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var sfd = new Microsoft.Win32.SaveFileDialog();

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

            if (options.ValidateName.HasValue)
            {
                sfd.ValidateNames = options.ValidateName.HasValue;
            }

            if (options.Title != null)
            {
                sfd.Title = options.Title;
            }

            if (options.FileName != null)
            {
                sfd.FileName = options.FileName;
            }

            var result = sfd.ShowDialog();

            if (result.HasValue && result.Value)
            {
                fileName = sfd.FileName;

                return true;
            }
            else
            {
                fileName = null;

                return false;
            }
        }

        /// <summary>
        /// Shows a "select folder" dialog.
        /// </summary>
        /// <param name="options">The dialog options</param>
        /// <param name="folder">The selected folder</param>
        /// <returns>Whether the dialog was confirmed or cancelled</returns>
        public bool ShowFolderBrowserDialog(FolderBrowserDialogOptions options, out string folder)
        {
            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (options == null)
                {
                    throw new ArgumentNullException(nameof(options));
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

                    return true;
                }
                else
                {
                    folder = null;

                    return false;
                }
            }
        }

        #endregion

        #region MessageBox

        private System.Windows.MessageBoxButton GetButtons(MessageButtons buttons)
        {
            switch (buttons)
            {
                case MessageButtons.YesNo:
                    {
                        return System.Windows.MessageBoxButton.YesNo;
                    }
                case MessageButtons.YesNoCancel:
                    {
                        return System.Windows.MessageBoxButton.YesNoCancel;
                    }
                case MessageButtons.OK:
                    {
                        return System.Windows.MessageBoxButton.OK;
                    }
                default:
                    {
                        throw new NotSupportedException();
                    }
            }
        }

        private System.Windows.MessageBoxImage GetIcon(MessageIcon icon)
        {
            switch (icon)
            {
                case MessageIcon.Error:
                    {
                        return System.Windows.MessageBoxImage.Error;
                    }
                case MessageIcon.Warning:
                    {
                        return System.Windows.MessageBoxImage.Warning;
                    }
                case MessageIcon.Information:
                    {
                        return System.Windows.MessageBoxImage.Information;
                    }
                case MessageIcon.Question:
                    {
                        return System.Windows.MessageBoxImage.Question;
                    }
                default:
                    {
                        throw new NotSupportedException();
                    }
            }
        }

        private MessageResult GetResult(System.Windows.MessageBoxResult result)
        {
            switch (result)
            {
                case System.Windows.MessageBoxResult.Yes:
                    {
                        return MessageResult.Yes;
                    }
                case System.Windows.MessageBoxResult.No:
                    {
                        return MessageResult.No;
                    }
                case System.Windows.MessageBoxResult.OK:
                    {
                        return MessageResult.OK;
                    }
                case System.Windows.MessageBoxResult.Cancel:
                    {
                        return MessageResult.Cancel;
                    }
                default:
                    {
                        throw new NotSupportedException();
                    }
            }
        }

        #endregion

        #region OpenFileDialog

        private static void SetOpenFileDialogOptions(Microsoft.Win32.OpenFileDialog ofd, OpenFileDialogOptions options)
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