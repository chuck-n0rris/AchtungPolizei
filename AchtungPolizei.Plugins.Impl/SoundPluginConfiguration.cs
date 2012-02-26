namespace AchtungPolizei.Plugins.Impl
{
    public class SoundPluginConfiguration : ConfigurationBase
    {
        public string Broken
        {
            get { return GetParameter("broken",""); }
            set { parameters["broken"] = value; }
        }

        public string StillBroken
        {
            get { return GetParameter("stillBroken",""); }
            set { parameters["stillBroken"] = value; }
        }

        public string Fixed
        {
            get { return GetParameter("fixed",""); }
            set { parameters["fixed"] = value; }
        }
    }
}