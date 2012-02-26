using System;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace Altalerta.Core
{
    public class AudioPlayer : IAudioPlayer, IDisposable
    {
        private WasapiOut device;
        private WaveChannel32 channel;
        
        public void Play(string fileName)
        {
            Stop();

            device = new WasapiOut(AudioClientShareMode.Shared, 100);
            channel = new WaveChannel32(new Mp3FileReader(fileName));

            device.Init(channel);
            device.Play();
        }

        public void Stop()
        {
            Dispose();
        }

        public void Dispose()
        {
            if (device != null)
            {
                device.Dispose();
            }

            if (channel != null)
            {
                channel.Dispose();
            }
        }
    }
}