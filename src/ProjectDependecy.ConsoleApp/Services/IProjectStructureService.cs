using System.Collections.Generic;

namespace ProjectDependecy.ConsoleApp.Services
{
    interface IProjectStructureService
    {
        void AddProject(string project);
        void AddDependency(string project, string dependentSolutionPath);
        Dictionary<string, List<string>> GetStrucutre();
    }
}
