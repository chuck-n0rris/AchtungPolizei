using System.Threading.Tasks;

namespace Altalerta.Core
{
    public interface IOutputPlugin : IPlugin
    {
        Task Notify(BuildState state, BuildInfo info);
    }
}