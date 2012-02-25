using System.Threading.Tasks;
using AchtungPolizei.Plugins.TextToSpeech;

namespace AchtungPolizei.Plugins.TextToSpeechTest
{
    class Program
    {
        static void Main()
        {
            var plugin = new TextToSpeechPlugin();
            var buildState = new BuildState();

            Task sayBuildFixed = plugin.Start(buildState, BuildStatus.Fixed);
            sayBuildFixed.RunSynchronously();

            Task sayBuildBroken = plugin.Start(buildState, BuildStatus.Broken);
            sayBuildBroken.RunSynchronously();

            Task sayBuildStillBroken = plugin.Start(buildState, BuildStatus.StillBroken);
            sayBuildStillBroken.RunSynchronously();
        }
    }
}
