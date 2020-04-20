using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Xml;

namespace ProjectDependecy.ConsoleApp.Services
{
    class GeneratorService : IGeneratorService
    {
        private XmlDocument xmlDocument = new XmlDocument();
        private XmlNode root;

        public GeneratorService()
        {
            xmlDocument.Load(@"Templates\template.xml");
            root = xmlDocument.GetElementsByTagName("root")[0];
        }

        public void Save(Dictionary<string, List<string>> projectStructure)
        {
            SaveToXml();
            SaveToJson(projectStructure);
        }

        public void BuildProject(string name)
        {
            var mxCell = BuildMxCell(name, name, "rounded=0;whiteSpace=wrap;html=1;");
            mxCell.SetAttribute("vertex", "1");
            mxCell.SetAttribute("parent", "1");

            var mxGeometry = BuildMxGeometry();
            mxGeometry.SetAttribute("width", "200");
            mxGeometry.SetAttribute("height", "60");

            mxCell.AppendChild(mxGeometry);

            root.AppendChild(mxCell);
        }

        public void BuildDependency(string source, string target)
        {
            var mxCell = BuildMxCell(Guid.NewGuid().ToString(), "", "edgeStyle=orthogonalEdgeStyle;rounded=0;orthogonalLoop=1;jettySize=auto;html=1;");
            mxCell.SetAttribute("edge", "1");
            mxCell.SetAttribute("parent", "1");
            mxCell.SetAttribute("source", source);
            mxCell.SetAttribute("target", target);

            var mxGeometry = BuildMxGeometry();
            mxGeometry.SetAttribute("relative", "1");

            mxCell.AppendChild(mxGeometry);

            root.AppendChild(mxCell);
        }

        private XmlElement BuildMxCell(string id, string value, string style)
        {
            var mxCell = xmlDocument.CreateElement("mxCell");
            mxCell.SetAttribute("id", id);
            mxCell.SetAttribute("value", value);
            mxCell.SetAttribute("style", style);

            return mxCell;
        }

        private XmlElement BuildMxGeometry()
        {
            var mxGeometry = xmlDocument.CreateElement("mxGeometry");
            mxGeometry.SetAttribute("as", "geometry");

            return mxGeometry;
        }

        private static void SaveToJson(Dictionary<string, List<string>> projectStructure)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
            };

            var jsonString = JsonSerializer.Serialize(projectStructure, jsonSerializerOptions);
            File.WriteAllText($"dependencyDiagram-{DateTime.Now:dd-MM-yyyy-HH,mm,ss}.json", jsonString);
        }

        private void SaveToXml()
        {
            using var fileStream = new FileStream($"dependencyDiagram-{DateTime.Now:dd-MM-yyyy-HH,mm,ss}.xml", FileMode.Create);
            xmlDocument.Save(fileStream);
        }
    }
}
