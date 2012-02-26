using System;
using Altalerta.Core;
using Altalerta.Core.Essential;
using Autofac;

namespace Altaltera.Cli
{
    class Program
    {
        static void Main()
        {
            try
            {
                Execute();
            }
            catch (Exception exception)
            {
                Console.WriteLine("Sorry... Something went wrong...");
                Console.WriteLine(exception.Message + exception.StackTrace);
            }
        }

        private static void Execute()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<DependencyConfiguration>();
            var container = builder.Build();

            using (var engine = container.Resolve<Engine>())
            {
                engine.Initialize(container);

                Console.WriteLine("Working... Press key to interrupt...");
                Console.ReadKey();

                container.Dispose();
            }
        }
    }
}