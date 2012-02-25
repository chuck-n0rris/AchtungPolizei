namespace AchtungPolizei.Plugins.Impl
{
    public class LightPluginConfiguration : ConfigurationBase
    {
        public string Path
        {
            get { return parameters["path"]; }
            set { parameters["path"] = value; }
        }

        public string Device
        {
            get { return parameters["device"]; }
            set { parameters["device"] = value; }
        }

        public string Socket
        {
            get { return parameters["socket"]; }
            set { parameters["socket"] = value; }
        }
    }
}