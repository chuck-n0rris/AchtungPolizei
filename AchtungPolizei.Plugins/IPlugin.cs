namespace AchtungPolizei.Plugins
{
    using System;

    public interface IPlugin : IDisposable
    {
        Guid Id { get; }

        string Name { get; }
    }
}