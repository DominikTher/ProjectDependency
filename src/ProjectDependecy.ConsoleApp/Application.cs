using Microsoft.Build.Construction;
using ProjectDependecy.ConsoleApp.Composite;
using ProjectDependecy.ConsoleApp.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Xml;

namespace ProjectDependecy.ConsoleApp
{
    class Application
    {
        private readonly IProjectStructureService projectStructureService;
        private readonly IGeneratorService generatorService;
        private IEnumerable<ProjectInSolution> rootProjects;
        private SolutionFile solutionFile;

        public Application(IProjectStructureService projectStructureService, IGeneratorService generatorService)
        {
            this.projectStructureService = projectStructureService;
            this.generatorService = generatorService;
        }

        public void Start(string solutionFilePtah)
        {
            solutionFile = SolutionFile.Parse(solutionFilePtah);

            // Demo
            var projectPath = GetPathForProject("{018DF148-8688-4384-8177-A214562C52B9}", string.Empty);

            var root = new SolutionFolder();
            rootProjects = GetProjectsInSolution(null);
            BuildTree(rootProjects, root);

            // Demo
            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.Converters.Add(new SolutionProjectCompositeConverter());
            var jsonString = JsonSerializer.Serialize(root, jsonSerializerOptions);
        }

        private string GetPathForProject(string guid, string projectName)
        {
            var p = solutionFile.ProjectsByGuid[guid];
            if (p.ParentProjectGuid == null)
                return p.ProjectName + projectName;
            else
                return GetPathForProject(p.ParentProjectGuid, p.ProjectName + projectName);
        }

        private void BuildTree(IEnumerable<ProjectInSolution> projectsInSolution, ISolutionProject parentComposite)
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

        private IEnumerable<ProjectInSolution> GetProjectsInSolution(string parentGuid)
        {
            return solutionFile.ProjectsInOrder.Where(project => project.ParentProjectGuid == parentGuid);
        }

        private void AddProjectDependecies(SolutionProject leaf, ProjectInSolution project)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(project.AbsolutePath);

            foreach (XmlElement projectReference in xmlDoc.GetElementsByTagName("ProjectReference"))
            {
                leaf.Dependencies.Add(Path.GetFileNameWithoutExtension(projectReference.GetAttribute("Include")));
            }
        }
    }
}
