using Microsoft.Build.Construction;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace ProjectDependecy.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //var solutionFilePtah = @"C:\Users\dominik.ther\source\repos\OnionTest\OnionTest.sln";
            var solutionFilePtah = @"C:\Users\dominik.ther\source\repos\mrdm-netdash-api\src\netdash\NetDash.sln";

            var solutionFile = SolutionFile.Parse(solutionFilePtah);
            var projects = solutionFile.ProjectsInOrder.Where(project => project.ProjectType == SolutionProjectType.KnownToBeMSBuildFormat);
            var strucuture = new Structure();

            foreach (var project in projects)
            {
                XmlDocument xmlDoc = new XmlDocument(); // Create an XML document object
                xmlDoc.Load(project.AbsolutePath); // Load the XML document from the specified file
                strucuture.AddProject(project.ProjectName);
                foreach (XmlElement projectReference in xmlDoc.GetElementsByTagName("ProjectReference"))
                {
                    strucuture.AddDependency(project.ProjectName, Path.GetFileNameWithoutExtension(projectReference.GetAttribute("Include")));
                }
            }

            

            var generator = new Generator();

            foreach (var item in strucuture.GetStrucutre())
            {
                generator.BuildProject(item.Key);

                foreach (var dep in item.Value)
                {
                    generator.BuildDependency(item.Key, dep);
                }
            }

            generator.Save();

            //var o = new JsonSerializerOptions
            //{
            //    WriteIndented = true,
            //};

            //var jsonString = JsonSerializer.Serialize(strucuture.GetStrucutre(), o);
            //Console.WriteLine(jsonString);
            //File.WriteAllText("projects.json", jsonString);
        }
    }

    class Generator
    {
        private XmlDocument xmlDocument;
        private XmlNode root;

        public Generator()
        {
            xmlDocument = new XmlDocument();
            xmlDocument.Load(@"C:\Users\dominik.ther\Desktop\template.xml");
            root = xmlDocument.GetElementsByTagName("root")[0];
        }

        public void Save()
        {
            xmlDocument.Save($"dependencyDiagram-{DateTime.Now.to}.xml");
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
    }

    class Structure
    {
        private Dictionary<string, List<string>> projects;

        public Structure()
        {
            projects = new Dictionary<string, List<string>>();
        }

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
