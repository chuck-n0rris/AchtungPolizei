namespace AchtungPolizei.Plugins
{
    public interface IConfigirationControl
    {
        bool Validate();

        ConfigurationBase GetConfiguration();
    }
}