namespace AchtungPolizei.Plugins
{
    using System.Windows.Controls;

    public interface IInputPlugin : IPlugin
    {
        Control GetConfigControl();
    }
}