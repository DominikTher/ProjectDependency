using System;
using System.IO;

namespace ProjectDependecy.ConsoleApp.Services
{
    class HtmlChartService : IHtmlChartService
    {
        public void Save(string jsonString)
        {
            string text = File.ReadAllText(@".\Templates\template.html");
            text = text.Replace("#projectDependency", jsonString);
            File.WriteAllText($"dependencyDiagram-{DateTime.Now:dd-MM-yyyy-HH,mm,ss}.html", text);
        }
    }
}
