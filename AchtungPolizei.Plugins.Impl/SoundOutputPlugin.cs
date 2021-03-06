using System;
using System.IO;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AchtungPolizei.Plugins.Impl
{
    public class SoundOutputPlugin : IOutputPlugin
    {
        private readonly Guid guid = Guid.Parse("282356A9-3E91-4405-B54B-072709E1DA09");
        private SoundPluginConfiguration configuration;

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
            var soundSettingsModel = new SoundSettingsModel();
            soundSettingsModel.Initialize(this.configuration);

            return new SoundSettingsView
            {
                DataContext = soundSettingsModel
            };
        }

        public IConfigirationControl GetConfigControl(ConfigurationBase config)
        {
            var soundSettingsModel = new SoundSettingsModel();
            soundSettingsModel.Initialize((SoundPluginConfiguration)config);

            return new SoundSettingsView
            {
                DataContext = soundSettingsModel
            };
        }

        public Task Start(BuildState state, BuildStatus status)
        {
            var tcs = new TaskCompletionSource<byte>();

            TrackableWaveChannel stream = null;
            WasapiOut device = null;

            try
            {
                var fileName = "";
                
                switch (status)
                {
                    case BuildStatus.Broken:
                        fileName = configuration.Broken;
                        break;
                    case BuildStatus.StillBroken:
                        fileName = configuration.StillBroken;
                        break;
                    case BuildStatus.Fixed:
                        fileName = configuration.Fixed;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("status");
                }

                if (File.Exists(fileName))
                {
                    device = new WasapiOut(AudioClientShareMode.Shared, 100);
                    stream = new TrackableWaveChannel(BuildStream(fileName));

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
                else
                {
                    var task = tcs.Task;
                    tcs.SetResult(0);
                    return task;
                }
            }
            catch (Exception e)
            {
                tcs.SetException(e);

                if (stream != null)
                {
                    stream.Dispose();
                    stream = null;
                }

                if (device != null)
                {
                    device.Dispose();
                    device = null;
                }
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