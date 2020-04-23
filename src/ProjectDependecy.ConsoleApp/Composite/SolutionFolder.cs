using System.Collections.Generic;

namespace ProjectDependecy.ConsoleApp.Composite
{
    class SolutionFolder : ISolutionProject
    {
        public List<ISolutionProject> Projects { get; private set; } = new List<ISolutionProject>();
        public string Name { get; private set; }

        private const string RootName = "Root";

        public SolutionFolder() : this(RootName)
        {
        }

        public SolutionFolder(string name)
        {
            Name = name;
        }

        public void Add(ISolutionProject project)
        {
            Projects.Add(project);
        }

        public bool IsComposite() => true;

        public bool IsRoot() => Name == RootName;
    }
}
