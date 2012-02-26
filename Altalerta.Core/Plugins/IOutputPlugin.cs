using System.Threading.Tasks;
using Altalerta.Core.Essential;

namespace Altalerta.Core.Plugins
{
    public interface IOutputPlugin : IPlugin
    {
        Task Notify(BuildState state, BuildInfo info);
    }
}