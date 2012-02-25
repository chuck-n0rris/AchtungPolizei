namespace AchtungPolizei.Plugins
{
    using System;

    public interface IConfigirationControl
    {
        bool Validate();

        ConfigurationBase GetConfiguration();

        Type GetConfigurationType();
    }
}