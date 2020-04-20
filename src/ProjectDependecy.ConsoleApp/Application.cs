using Microsoft.Build.Construction;
using ProjectDependecy.ConsoleApp.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ProjectDependecy.ConsoleApp
{
    class Application
    {
        private readonly IProjectStructureService projectStructureService;
        private readonly IGeneratorService generatorService;

        public Application(IProjectStructureService projectStructureService, IGeneratorService generatorService)
        {
            this.projectStructureService = projectStructureService;
            this.generatorService = generatorService;
        }

        public void Start(string solutionFilePtah)
        {
            var solutionFile = SolutionFile.Parse(solutionFilePtah);
            var projects = solutionFile.ProjectsInOrder.Where(project => project.ProjectType == SolutionProjectType.KnownToBeMSBuildFormat);
            AddProjectDependecies(projects);
            BuildProjectDependencies();

            generatorService.Save(projectStructureService.GetStrucutre());
        }

        private void BuildProjectDependencies()
        {
            foreach (var item in projectStructureService.GetStrucutre())
            {
                generatorService.BuildProject(item.Key);

                foreach (var dep in item.Value)
                {
                    generatorService.BuildDependency(item.Key, dep);
                }
            }
        }

        private void AddProjectDependecies(IEnumerable<ProjectInSolution> projects)
        {
            foreach (var project in projects)
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(project.AbsolutePath);
                projectStructureService.AddProject(project.ProjectName);
                foreach (XmlElement projectReference in xmlDoc.GetElementsByTagName("ProjectReference"))
                {
                    projectStructureService.AddDependency(project.ProjectName, Path.GetFileNameWithoutExtension(projectReference.GetAttribute("Include")));
                }
            }
        }
    }
}
