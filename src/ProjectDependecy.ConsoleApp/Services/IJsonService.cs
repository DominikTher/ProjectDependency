using ProjectDependecy.ConsoleApp.Composite;

namespace ProjectDependecy.ConsoleApp.Services
{
    interface IJsonService
    {
        string GetJsonString(SolutionFolder root);
        void Save(string jsonString);
    }
}
