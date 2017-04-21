using System;
using System.Windows.Forms;

namespace DoenaSoft.AbstractionLayer.UIServices.Implementations
{
    public sealed class FormUIServices : IUIServices
    {
        #region UIServices

        public Result ShowMessageBox(String text
            , String caption
            , Buttons genericButtons
            , Icon genericIcon)
        {
            MessageBoxButtons buttons = GetButtons(genericButtons);

            MessageBoxIcon icon = GetIcon(genericIcon);

            DialogResult result = MessageBox.Show(text, caption, buttons, icon);

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

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                SetOpenFileDialogOptions(ofd, options);

                ofd.Multiselect = false;

                if (ofd.ShowDialog() == DialogResult.OK)
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
        }

        public Boolean ShowOpenFileDialog(OpenFileDialogOptions options
            , out String[] fileNames)
        {
            if (options == null)
            {
                throw (new ArgumentNullException(nameof(options)));
            }

            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                SetOpenFileDialogOptions(ofd, options);

                ofd.Multiselect = true;

                if (ofd.ShowDialog() == DialogResult.OK)
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
        }

        public Boolean ShowSaveFileDialog(SaveFileDialogOptions options
            , out String fileName)
        {
            if (options == null)
            {
                throw (new ArgumentNullException(nameof(options)));
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                if (options.InitialDirectory != null)
                {
                    sfd.InitialDirectory = options.InitialDirectory;
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

                if (options.RestoreDirectory.HasValue)
                {
                    sfd.RestoreDirectory = options.RestoreDirectory.Value;
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

                if (sfd.ShowDialog() == DialogResult.OK)
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
        }

        public Boolean ShowFolderBrowserDialog(FolderBrowserDialogOptions options
            , out String folder)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
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

                if (fbd.ShowDialog() == DialogResult.OK)
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

        private MessageBoxButtons GetButtons(Buttons buttons)
        {
            switch (buttons)
            {
                case (Buttons.YesNo):
                    {
                        return (MessageBoxButtons.YesNo);
                    }
                case (Buttons.YesNoCancel):
                    {
                        return (MessageBoxButtons.YesNoCancel);
                    }
                case (Buttons.OK):
                    {
                        return (MessageBoxButtons.OK);
                    }
                default:
                    {
                        throw (new NotSupportedException());
                    }
            }
        }

        private MessageBoxIcon GetIcon(Icon icon)
        {
            switch (icon)
            {
                case (Icon.Error):
                    {
                        return (MessageBoxIcon.Error);
                    }
                case (Icon.Warning):
                    {
                        return (MessageBoxIcon.Warning);
                    }
                case (Icon.Information):
                    {
                        return (MessageBoxIcon.Information);
                    }
                case (Icon.Question):
                    {
                        return (MessageBoxIcon.Question);
                    }
                default:
                    {
                        throw (new NotSupportedException());
                    }
            }
        }

        private Result GetResult(DialogResult result)
        {
            switch (result)
            {
                case (DialogResult.Yes):
                    {
                        return (Result.Yes);
                    }
                case (DialogResult.No):
                    {
                        return (Result.No);
                    }
                case (DialogResult.OK):
                    {
                        return (Result.OK);
                    }
                case (DialogResult.Cancel):
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
            if (options.InitialDirectory != null)
            {
                ofd.InitialDirectory = options.InitialDirectory;
            }

            if (options.CheckFileExists.HasValue)
            {
                ofd.CheckFileExists = options.CheckFileExists.Value;
            }

            if (options.Filter != null)
            {
                ofd.Filter = options.Filter;
            }

            if (options.RestoreDirectory.HasValue)
            {
                ofd.RestoreDirectory = options.RestoreDirectory.Value;
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