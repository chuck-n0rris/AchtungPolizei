using System.Collections.Generic;

namespace Altalerta.Core
{
    public interface IProjectRepository
    {
        IEnumerable<Project> GetAll();
    }
}