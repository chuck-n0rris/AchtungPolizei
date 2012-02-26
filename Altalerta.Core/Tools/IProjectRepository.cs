using System.Collections.Generic;
using Altalerta.Core.Essential;

namespace Altalerta.Core.Tools
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
    }
}