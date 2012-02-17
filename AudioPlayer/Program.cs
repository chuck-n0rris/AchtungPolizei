using System;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace AudioPlayer
{
    class Program
    {
        static void Main()
        {
            using (var device = new WasapiOut(AudioClientShareMode.Shared, 100))
            using (var stream = new WaveChannel32(new Mp3FileReader("aircraft012.mp3")))
            {
                device.Init(stream);
                DoOnKeyStroke(() => device.Play(), "play");
                DoOnKeyStroke(() => device.Stop(), "stop");
                DoOnKeyStroke(() => {}, "exit");
            }
        }

        private static void DoOnKeyStroke(Action action, string actionName)
        {
            Console.WriteLine(string.Format("Press any key to {0}...", actionName));
            Console.ReadKey();
            action();
        }
    }
}
