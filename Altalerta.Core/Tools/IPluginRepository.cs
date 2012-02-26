using Altalerta.Core.Essential;
using Altalerta.Core.Plugins;
using Autofac;

namespace Altalerta.Core.Tools
{
    public interface IPluginRepository
    {
        void Discover(IContainer container);
        IPlugin Instantiate(PluginReference reference);
    }
}