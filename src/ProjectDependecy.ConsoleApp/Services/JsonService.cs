using ProjectDependecy.ConsoleApp.Composite;
using System;
using System.IO;
using System.Text.Json;

namespace ProjectDependecy.ConsoleApp.Services
{
    class JsonService : IJsonService
    {
        public string GetJsonString(SolutionFolder root)
        {
            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.WriteIndented = true;
            jsonSerializerOptions.Converters.Add(new SolutionProjectCompositeConverter());

            return JsonSerializer.Serialize(root, jsonSerializerOptions);
        }

        public void Save(string jsonString)
        {
            File.WriteAllText($"dependencyDiagram-{DateTime.Now:dd-MM-yyyy-HH,mm,ss}.json", jsonString);
        }
    }
}
