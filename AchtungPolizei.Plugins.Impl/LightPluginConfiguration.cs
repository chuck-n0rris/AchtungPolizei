namespace AchtungPolizei.Plugins.Impl
{
    public class LightPluginConfiguration : ConfigurationBase
    {
        public string Path
        {
            get { return GetParameter("path",""); }
            set { parameters["path"] = value; }
        }

        public string Device
        {
            get { return GetParameter("device", "Device1"); }
            set { parameters["device"] = value; }
        }

        public string Socket
        {
            get { return GetParameter("socket", "Socket1"); }
            set { parameters["socket"] = value; }
        }

        public int Miliseconds
        {
            get { return GetParameter("miliseconds", 10000); }
            set { parameters["miliseconds"] = value.ToString(); }
        }
    }
}