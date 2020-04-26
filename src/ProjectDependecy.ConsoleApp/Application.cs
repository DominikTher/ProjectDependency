using Microsoft.Build.Construction;
using ProjectDependecy.ConsoleApp.Composite;
using ProjectDependecy.ConsoleApp.Services;
using System.Collections.Generic;

namespace ProjectDependecy.ConsoleApp
{
    class Application
    {
        private IEnumerable<ProjectInSolution> rootProjects;
        private readonly IJsonService jsonService;
        private readonly ISolutionProjectClient solutionProjectClient;
        private readonly IHtmlChartService htmlChartService;

        public Application(IJsonService jsonService, ISolutionProjectClient solutionProjectClient, IHtmlChartService htmlChartService)
        {
            this.jsonService = jsonService;
            this.solutionProjectClient = solutionProjectClient;
            this.htmlChartService = htmlChartService;
        }

        public void Start(string solutionFilePtah)
        {
            solutionProjectClient.SetSolutionFile(solutionFilePtah);
            var root = new SolutionFolder();
            rootProjects = solutionProjectClient.GetProjectsInSolution(null);
            solutionProjectClient.BuildTree(rootProjects, root);

            var jsonString = jsonService.GetJsonString(root);
            jsonService.Save(jsonString);

            htmlChartService.Save(jsonString);
        }
    }
}
