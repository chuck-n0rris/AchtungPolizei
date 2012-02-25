using System;
using System.IO;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AchtungPolizei.Plugins
{
    /// ѕришлось писать наследника дл€ того чтобы пон€ть когда дошли до конца файла
    public class TrackableWaveChannel : WaveChannel32
    {
        public TrackableWaveChannel(WaveStream sourceStream, float volume, float pan)
            : base(sourceStream, volume, pan)
        {
        }

        public TrackableWaveChannel(WaveStream sourceStream)
            : base(sourceStream)
        {
        }

        public override int Read(byte[] destBuffer, int offset, int numBytes)
        {
            var result = base.Read(destBuffer, offset, numBytes);

            if (Position >= Length && Finished != null)
            {
                Finished(this, new EventArgs());
            }

            return result;
        }

        public event EventHandler Finished;
    }

    public class SoundOutputPlugin : IOutputPlugin
    {
        private readonly Guid guid = Guid.Parse("282356A9-3E91-4405-B54B-072709E1DA09");

        public string FileName { get; private set; }

        public SoundOutputPlugin(string fileName)
        {
            FileName = fileName;
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