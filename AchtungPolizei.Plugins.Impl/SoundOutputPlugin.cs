using System;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AchtungPolizei.Plugins.Impl
{
    public class SoundOutputPlugin : IOutputPlugin
    {
        private readonly Guid guid = Guid.Parse("282356A9-3E91-4405-B54B-072709E1DA09");
        private SoundPluginConfiguration configuration;

        public string FileName { get; private set; }

        public SoundOutputPlugin()
        {
            FileName = string.Empty;
        }

        public void Dispose()
        {
            
        }

        public Guid Id
        {
            get { return guid; }
        }

        public string Name
        {
            get { return "Sound output plugin"; }
        }

        public void SetConfiguration(ConfigurationBase configuration)
        {
            this.configuration = (SoundPluginConfiguration) configuration;
        }

        public IConfigirationControl GetConfigControl()
        {
            return new SoundSettingsView
                       {
                           DataContext = new SoundSettingsModel()
                       };
        }

        public Task Start(BuildState state, BuildStatus status)
        {
            var tcs = new TaskCompletionSource<byte>();

            try
            {
                var device = new WasapiOut(AudioClientShareMode.Shared, false, 100);
                var stream = new TrackableWaveChannel(BuildStream(FileName));

                device.Init(stream);
                device.Play();

                stream.Finished += (sender, args) =>
                {
                    tcs.SetResult(0);
                    
                    stream.Dispose();
                    device.Dispose();
                    stream = null;
                    device = null;
                };
            }
            catch (Exception e)
            {
                tcs.SetException(e);
            }

            return tcs.Task;
        }

        private WaveStream BuildStream(string fileName)
        {
            fileName = fileName.ToLowerInvariant();

            if (fileName.EndsWith(".mp3"))
            {
                return new Mp3FileReader(fileName);
            }
            else if (fileName.EndsWith(".wav"))
            {
                return new WaveFileReader(fileName);
            }
            else
            {
                throw new NotImplementedException("file format is not supported");
            }
        }
    }
}