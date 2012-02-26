using System;
using System.IO;
using System.Windows.Input;
using Microsoft.Win32;

namespace AchtungPolizei.Plugins.Impl
{
    public class SoundSettingsModel : ViewModelBase<SoundSettingsModel>
    {
        private string brokenFile;
        private string stillBrokenFile;
        private string fixedFile;

        public SoundSettingsModel()
        {
            this.SelectBrokenFile = new PlainCommand(() => ProcessFileSelection(fileName => BrokenFile = fileName));
            this.SelectFixedFile = new PlainCommand(() => ProcessFileSelection(fileName => FixedFile = fileName));
            this.SelectStillBrokenFile = new PlainCommand(() => ProcessFileSelection(fileName => StillBrokenFile = fileName));
        }

        public string BrokenFile
        {
            get { return brokenFile; }
            set { ChangeProperty(it => it.BrokenFile, value); }
        }

        public string StillBrokenFile
        {
            get { return stillBrokenFile; }
            set { ChangeProperty(it => it.StillBrokenFile, value); }
        }

        public string FixedFile
        {
            get { return fixedFile; }
            set { ChangeProperty(it => it.FixedFile, value); }
        }

        public string BrokenFileValidator()
        {
            return ValidateFile(BrokenFile);
        }

        public string StillBrokenFileValidator()
        {
            return ValidateFile(StillBrokenFile);
        }

        public string FixedFileValidator()
        {
            return ValidateFile(FixedFile);
        }

        public ICommand SelectBrokenFile { get; set; } 

        public ICommand SelectStillBrokenFile { get; set; } 

        public ICommand SelectFixedFile { get; set; }

        private string ValidateFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            if (!File.Exists(fileName))
            {
                return "File should exist";
            }

            string lowerInvariant = fileName.ToLowerInvariant();

            if (!(lowerInvariant.EndsWith(".mp3") || lowerInvariant.EndsWith(".wav")))
            {
                return "File should be an mp3 or wav";
            }

            return null;
        }

        private void ProcessFileSelection(Action<string> setValuAction)
        {
            // Configure open file dialog box
            var dlg = new OpenFileDialog
            {
                DefaultExt = ".wav",
                Filter = "Wave files (.wav)|*.wav|MP3 (.mp3)|*.mp3"
            };

            var result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                setValuAction(dlg.FileName);
            }
        }

        public void Initialize(SoundPluginConfiguration configuration)
        {
            if (configuration != null)
            {
                this.BrokenFile = configuration.Broken;
                this.FixedFile = configuration.Fixed;
                this.StillBrokenFile = configuration.StillBroken;
            }
        }
    }
}