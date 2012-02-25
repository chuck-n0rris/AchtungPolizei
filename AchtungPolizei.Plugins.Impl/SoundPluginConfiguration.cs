namespace AchtungPolizei.Plugins.Impl
{
    public class SoundPluginConfiguration : ConfigurationBase
    {
        public string Broken
        {
            get { return Parameters["broken"]; }
            set { Parameters["broken"] = value; }
        }

        public string StillBroken
        {
            get { return Parameters["stillBroken"]; }
            set { Parameters["stillBroken"] = value; }
        }

        public string Fixed
        {
            get { return Parameters["fixed"]; }
            set { Parameters["fixed"] = value; }
        }
    }
}