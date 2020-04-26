using Microsoft.Build.Construction;
using System.Collections.Generic;

namespace ProjectDependecy.ConsoleApp.Composite
{
    interface ISolutionProjectClient
    {
        void SetSolutionFile(string solutionFilePtah);
        void BuildTree(IEnumerable<ProjectInSolution> projectsInSolution, ISolutionProject parentComposite);
        public IEnumerable<ProjectInSolution> GetProjectsInSolution(string parentGuid);
    }
}