using Microsoft.Build.Construction;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ProjectDependecy.ConsoleApp.Composite
{
    class SolutionProjectClient : ISolutionProjectClient
    {
        private SolutionFile solutionFile;

        public void SetSolutionFile(string solutionFilePtah)
        {
            solutionFile = SolutionFile.Parse(solutionFilePtah);
        }

        public void BuildTree(IEnumerable<ProjectInSolution> projectsInSolution, ISolutionProject parentComposite)
        {
            foreach (var projectChild in projectsInSolution)
            {
                if (projectChild.ProjectType == SolutionProjectType.SolutionFolder)
                {
                    var currentComposite = new SolutionFolder(projectChild.ProjectName);
                    parentComposite.Add(currentComposite);

                    BuildTree(GetProjectsInSolution(projectChild.ProjectGuid), currentComposite);
                }
                else
                {
                    var leaf = new SolutionProject(projectChild.ProjectName);
                    parentComposite.Add(leaf);
                    AddProjectDependecies(leaf, projectChild);
                }
            }
        }

        public IEnumerable<ProjectInSolution> GetProjectsInSolution(string parentGuid)
        {
            return solutionFile.ProjectsInOrder.Where(project => project.ParentProjectGuid == parentGuid);
        }

        private void AddProjectDependecies(SolutionProject leaf, ProjectInSolution project)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(project.AbsolutePath);

            foreach (XmlElement projectReference in xmlDoc.GetElementsByTagName("ProjectReference"))
            {
                var dependency = Path.GetFileNameWithoutExtension(projectReference.GetAttribute("Include"));
                leaf.Dependencies.Add(dependency);
            }
        }
    }
}
