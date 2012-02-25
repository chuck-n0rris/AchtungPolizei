namespace AchtungPolizei.Plugins.Impl
{
    public class LightPluginConfiguration : ConfigurationBase
    {
        public string Path
        {
            get { return Parameters["path"]; }
            set { Parameters["path"] = value; }
        }

        public string Device
        {
            get { return Parameters["device"]; }
            set { Parameters["device"] = value; }
        }

        public string Socket
        {
            get { return Parameters["socket"]; }
            set { Parameters["socket"] = value; }
        }
    }
}