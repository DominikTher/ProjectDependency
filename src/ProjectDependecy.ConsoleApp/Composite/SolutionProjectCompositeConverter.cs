using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjectDependecy.ConsoleApp.Composite
{
    class SolutionProjectCompositeConverter : JsonConverter<SolutionFolder>
    {
        public override SolutionFolder Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, SolutionFolder value, JsonSerializerOptions options)
        {
            if (value.IsComposite())
            {
                var projects = value.Projects;

                if (!value.IsRoot())
                    writer.WritePropertyName(value.Name);

                writer.WriteStartObject();

                var solutionProjects = projects.Where(c => !c.IsComposite());
                foreach (var project in solutionProjects)
                {
                    writer.WritePropertyName(project.Name);
                    writer.WriteStartArray();
                    foreach (var e in ((SolutionProject)project).Dependencies)
                    {
                        writer.WriteStringValue(e);
                    }
                    writer.WriteEndArray();
                }

                var solutionFolders = projects.Where(c => c.IsComposite());
                foreach (var solutionFolder in solutionFolders)
                {
                    Write(writer, (SolutionFolder)solutionFolder, options);
                }

                writer.WriteEndObject();
            }
        }
    }
}
