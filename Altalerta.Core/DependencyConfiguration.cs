using System;
using System.Linq;
using Altalerta.Core.Essential;
using Altalerta.Core.Plugins;
using Altalerta.Core.Plugins.Impl;
using Altalerta.Core.Tools.Impl;
using Autofac;

namespace Altalerta.Core
{
    public class DependencyConfiguration : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<JsonProjectRepository>().AsImplementedInterfaces();
            builder.RegisterType<PluginRepository>().AsImplementedInterfaces();
            builder.RegisterType<HudsonPoller>().AsImplementedInterfaces();
            builder.RegisterType<SoundPlayer>().AsImplementedInterfaces();
            builder.RegisterType<Flasher>().AsImplementedInterfaces();
            builder.RegisterType<AudioPlayer>().AsImplementedInterfaces();
            builder.RegisterType<Engine>().AsSelf();

            AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof (IPlugin).IsAssignableFrom(x) && x.IsClass && !x.IsAbstract)
                .ToList()
                .ForEach(x => builder.RegisterType(x).AsImplementedInterfaces().AsSelf());
        }
    }
}