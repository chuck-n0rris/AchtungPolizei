namespace AchtungPolizei.Plugins
{
    using System.Threading.Tasks;

    public interface IOutputPlugin : IPlugin
    {
        Task Start(BuildState state, BuildStatus status);
    }
}