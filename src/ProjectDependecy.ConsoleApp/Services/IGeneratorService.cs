using System.Collections.Generic;

namespace ProjectDependecy.ConsoleApp.Services
{
    interface IGeneratorService
    {
        void BuildDependency(string source, string target);
        void BuildProject(string id, string name);
        void Save(Dictionary<string, List<string>> projectStructure);
    }
}