namespace AchtungPolizei.Plugins
{
    using System;

    public interface IPlugin
    {
        Guid Id { get; }

        string Name { get; }
    }
}