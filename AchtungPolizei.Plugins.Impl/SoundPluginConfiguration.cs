namespace AchtungPolizei.Plugins.Impl
{
    public class SoundPluginConfiguration : ConfigurationBase
    {
        public string Broken
        {
            get { return parameters["broken"]; }
            set { parameters["broken"] = value; }
        }

        public string StillBroken
        {
            get { return parameters["stillBroken"]; }
            set { parameters["stillBroken"] = value; }
        }

        public string Fixed
        {
            get { return parameters["fixed"]; }
            set { parameters["fixed"] = value; }
        }
    }
}