using System;
using System.IO;
using System.Linq;
using System.Text;

namespace EventSourcedTooling.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var defaultInputPath = @"C:\Users\Koen\source\repos\EventSourcedTooling\EventSourcedTooling\Commands\";
            var defaultOutputPath = @"C:\Users\Koen\source\repos\EventSourcedTooling\EventSourcedTooling\Generated\";

            foreach (var file in Directory.EnumerateFiles(defaultInputPath, "*.txt"))
            {
                GenerateClass(file, defaultOutputPath);
            }
        }

        private static void GenerateClass(string file, string defaultOutputPath)
        {
            var lines = File.ReadAllLines(file);

            var className = lines[0];
            var fields = lines.Skip(1).Select(text => text.TrimStart(' ')).ToList();

            var builder = new StringBuilder();
            builder.AppendLine("using System.Collections.Generic;\n");
            builder.AppendLine("namespace EventSourcedTooling {");
            builder.AppendLine($"\tpublic class {className}{{");
            builder.Append("public " + className + "(");
            builder.Append(String.Join(", ", fields.Select(field => "string " + field)));
            builder.Append(")");
            builder.AppendLine("{");
            foreach (var field in fields)
            {
                builder.AppendLine("this." + field + " = " + field + ";");
            }
            builder.AppendLine("}");
            foreach (var field in fields.Select(x => x.TrimStart(' ')))
            {
                if (field.StartsWith("List:"))
                {
                    var fieldName = field.Replace("List:", "").TrimEnd('s');
                    builder.AppendLine("\t\tpublic List<" + fieldName + "> " + fieldName + "s { get; set; }");
                }
                else
                {
                    builder.AppendLine("\t\tpublic string " + field + " { get; set; }");
                }
            }

            builder.AppendLine("\t}");
            builder.AppendLine("}");
            var clazz = builder.ToString();

            File.WriteAllText(defaultOutputPath + className + ".cs", clazz);
        }
    }
}
