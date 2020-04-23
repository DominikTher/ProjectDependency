using System;
using System.Collections.Generic;

namespace ProjectDependecy.ConsoleApp.Composite
{
    class SolutionProject : ISolutionProject
    {
        public List<string> Dependencies { get; private set; } = new List<string>();
        public string Name { get; private set; }

        public SolutionProject(string name)
        {
            Name = name;
        }

        public  void Add(ISolutionProject project)
        {
            throw new Exception("Project cannot contain another project!");
        }

        public  bool IsComposite() => false;

        public  bool IsRoot() => false;
    }
}
