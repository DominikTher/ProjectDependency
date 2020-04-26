using System;
using System.IO;

namespace ProjectDependecy.ConsoleApp.Services
{
    class HtmlChartService : IHtmlChartService
    {
        public void Save(string jsonString, string solutionName)
        {
            string text = File.ReadAllText(@".\Templates\template.html");
            text = text.Replace("#projectDependency", jsonString);
            text = text.Replace("#solutionName", solutionName);
            File.WriteAllText($"dependencyDiagram-{DateTime.Now:dd-MM-yyyy-HH,mm,ss}.html", text);
        }
    }
}
