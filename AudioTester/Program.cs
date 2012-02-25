using System;
using System.Threading.Tasks;
using AchtungPolizei.Plugins;

namespace AudioTester
{
    class Program
    {
        static void Main()
        {
            var soundOutputPlugin = new SoundOutputPlugin("aircraft012.mp3");
            Task task = soundOutputPlugin.Start(new BuildState(), BuildStatus.Fixed);
            task.ContinueWith(it => Console.WriteLine("Hello"));
            Console.ReadLine();
        }
    }
}
