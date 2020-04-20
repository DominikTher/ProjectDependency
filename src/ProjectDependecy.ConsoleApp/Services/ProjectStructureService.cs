using System.Collections.Generic;

namespace ProjectDependecy.ConsoleApp.Services
{
    class ProjectStructureService : IProjectStructureService
    {
        private Dictionary<string, List<string>> projects = new Dictionary<string, List<string>>();

        public void AddProject(string project)
        {
            projects.Add(project, new List<string>());
        }

        public void AddDependency(string project, string dependentSolutionPath)
        {
            projects[project].Add(dependentSolutionPath);
        }

        public Dictionary<string, List<string>> GetStrucutre()
        {
            return projects;
        }
    }
}
