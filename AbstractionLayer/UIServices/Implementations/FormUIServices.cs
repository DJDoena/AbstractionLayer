namespace DoenaSoft.AbstractionLayer.UIServices.Implementations
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Standard implementation of <see cref="IUIServices"/> for <see cref="Form"/>.
    /// </summary>
    public sealed class FormUIServices : IUIServices
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
        public Result ShowMessageBox(string text, string caption, Buttons buttons, Icon icon)
        {
            var formsButtons = GetButtons(buttons);

            var formsIcon = GetIcon(icon);

            var formsResult = MessageBox.Show(text, caption, formsButtons, formsIcon);

            var result = GetResult(formsResult);

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

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                SetOpenFileDialogOptions(ofd, options);

                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
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

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                SetOpenFileDialogOptions(ofd, options);

                ofd.Multiselect = true;

                if (ofd.ShowDialog() == DialogResult.OK)
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

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
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

                if (sfd.ShowDialog() == DialogResult.OK)
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
        }

        /// <summary>
        /// Shows a "select folder" dialog.
        /// </summary>
        /// <param name="options">The dialog options</param>
        /// <param name="folder">The selected folder</param>
        /// <returns>Whether the dialog was confirmed or cancelled</returns>
        public bool ShowFolderBrowserDialog(FolderBrowserDialogOptions options, out string folder)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
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

                if (fbd.ShowDialog() == DialogResult.OK)
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

        private MessageBoxButtons GetButtons(Buttons buttons)
        {
            switch (buttons)
            {
                case Buttons.YesNo:
                    {
                        return MessageBoxButtons.YesNo;
                    }
                case Buttons.YesNoCancel:
                    {
                        return MessageBoxButtons.YesNoCancel;
                    }
                case Buttons.OK:
                    {
                        return MessageBoxButtons.OK;
                    }
                default:
                    {
                        throw new NotSupportedException();
                    }
            }
        }

        private MessageBoxIcon GetIcon(Icon icon)
        {
            switch (icon)
            {
                case Icon.Error:
                    {
                        return MessageBoxIcon.Error;
                    }
                case (Icon.Warning):
                    {
                        return MessageBoxIcon.Warning;
                    }
                case Icon.Information:
                    {
                        return MessageBoxIcon.Information;
                    }
                case Icon.Question:
                    {
                        return MessageBoxIcon.Question;
                    }
                default:
                    {
                        throw new NotSupportedException();
                    }
            }
        }

        private Result GetResult(DialogResult result)
        {
            switch (result)
            {
                case DialogResult.Yes:
                    {
                        return Result.Yes;
                    }
                case DialogResult.No:
                    {
                        return Result.No;
                    }
                case DialogResult.OK:
                    {
                        return (Result.OK);
                    }
                case DialogResult.Cancel:
                    {
                        return Result.Cancel;
                    }
                default:
                    {
                        throw new NotSupportedException();
                    }
            }
        }

        #endregion

        #region OpenFileDialog

        private static void SetOpenFileDialogOptions(OpenFileDialog ofd, OpenFileDialogOptions options)
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