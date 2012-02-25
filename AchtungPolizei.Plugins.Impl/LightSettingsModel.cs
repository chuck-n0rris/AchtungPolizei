using System.IO;
using System.Windows.Input;
using Microsoft.Win32;

namespace AchtungPolizei.Plugins.Impl
{
    public class LightSettingsModel : ViewModelBase<LightSettingsModel>
    {
        private string device;
        private string path;
        private string socket;

        public LightSettingsModel()
        {
            this.SelectFile = new PlainCommand(this.OnSelectFile);
        }

        public string Path
        {
            get { return path; }
            set { ChangeProperty(it => it.Path, value); }
        }

        public string Device
        {
            get { return device; }
            set { ChangeProperty(it => it.Device, value); }
        }

        public string Socket
        {
            get { return socket; }
            set { ChangeProperty(it => it.Device, value); }
        }

        public string PathValidator()
        {
            if (string.IsNullOrWhiteSpace(this.Path))
            {
                return "Please enter Path to executable";
            }

            if (Path.ToLowerInvariant().EndsWith(".exe"))
            {
                return "Please select PowerManager executable";
            }

            if (!File.Exists(Path))
            {
                return "Please select existing file";
            }

            return null;
        }

        public string DeviceValidator()
        {
            if (string.IsNullOrWhiteSpace(this.Device))
            {
                return "Please enter device name";
            }

            return null;
        }

        public string SocketValidator()
        {
            if (string.IsNullOrWhiteSpace(this.Socket))
            {
                return "Please enter socket name";
            }

            return null;
        }

        public ICommand SelectFile { get; set; }

        private void OnSelectFile()
        {
            // Configure open file dialog box
            var dlg = new OpenFileDialog
            {
                FileName = "pm.exe",
                DefaultExt = ".exe",
                Filter = "PowerManager executable (.exe)|*.exe"
            };
            
            var result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                this.Path = dlg.FileName;
            }
        }
    }
}