using System;

namespace AchtungPolizei.Plugins
{
    public interface IInputPlugin : IPlugin
    {
        IConfigirationControl GetConfigControl();
    }
}