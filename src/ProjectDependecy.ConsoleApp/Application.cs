using Microsoft.Build.Construction;
using ProjectDependecy.ConsoleApp.Services;
using System;
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
            var projects = solutionFile.ProjectsInOrder;

            var root = new Composite("root");
            rootProjects = GetProjectsInSolution(null);
            BuildTree(rootProjects, root);


            //root.Operation();

            var client = new Client();
            client.ClientCode(root);

            //BuildProjectDependencies();

            //generatorService.Save(projectStructureService.GetStrucutre());
        }

        private void BuildTree(IEnumerable<ProjectInSolution> projectsInSolution, Composite parentComposite)
        {
            foreach (var projectChild in projectsInSolution)
            {
                if (projectChild.ProjectType == SolutionProjectType.SolutionFolder)
                {
                    var currentComposite = new Composite(projectChild.ProjectName);
                    parentComposite.Add(currentComposite);

                    BuildTree(GetProjectsInSolution(projectChild.ProjectGuid), currentComposite);
                }
                else
                {
                    var leaf = new Leaf(projectChild.ProjectName);
                    parentComposite.Add(leaf);
                    AddProjectDependecies(leaf, projectChild);
                }
            }
        }

        private IEnumerable<ProjectInSolution> GetProjectsInSolution(string parentGuid)
        {
            return solutionFile.ProjectsInOrder.Where(project => project.ParentProjectGuid == parentGuid);
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

        private void AddProjectDependecies(Leaf leaf, ProjectInSolution project)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(project.AbsolutePath);

            foreach (XmlElement projectReference in xmlDoc.GetElementsByTagName("ProjectReference"))
            {
                leaf.AddDependency(Path.GetFileNameWithoutExtension(projectReference.GetAttribute("Include")));
            }
        }
    }
}
