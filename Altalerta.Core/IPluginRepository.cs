using Autofac;

namespace Altalerta.Core
{
    public interface IPluginRepository
    {
        void Discover(IContainer container);
        IPlugin Instantiate(PluginReference reference);
    }
}