using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EventSourcedTooling.Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length < 2) throw new ArgumentException();
            
            var defaultInputPath = args[0];
            var defaultOutputPath = args[1];

            foreach (var file in Directory.EnumerateFiles(Path.Combine(defaultInputPath, "Events"), "*.txt"))
            {
                GenerateClass(file, defaultOutputPath, "IEvent");
            }

            foreach (var file in Directory.EnumerateFiles(Path.Combine(defaultInputPath, "Commands"), "*.txt"))
            {
                GenerateClass(file, defaultOutputPath, "ICommand");
            }
        }

        private static void GenerateClass(string inputFile, string defaultOutputPath, string markerInterface)
        {
            var lines = File.ReadAllLines(inputFile);

            var dictionary = new Dictionary<string, List<string>>();
            var tempClass = "";
            foreach (var line in lines)
            {
                if (Char.IsLetter(line.First()))
                {
                    tempClass = line;
                    dictionary.Add(tempClass, new List<string>());
                }
                else
                {
                    dictionary[tempClass].Add(line.Replace("\t", "").Replace(" ", ""));
                }
            }

            var builder = new StringBuilder();

            builder.AppendLine("using System.Collections.Generic;\n");
            builder.AppendLine("namespace EventSourcedTooling {");

            foreach (var item in dictionary)
            {
                var className = item.Key;
                var fields = item.Value;

                builder.AppendLine($"\tpublic struct {className} : {markerInterface} {{");

                AppendConstructor(builder, className, fields);
                AppendFields(fields, builder);

                builder.AppendLine("\t}");
            }

            builder.AppendLine("}");

            var outputFile = builder.ToString();

            File.WriteAllText(Path.Combine(defaultOutputPath, "Generated", dictionary.Keys.First() + ".cs"), outputFile);
        }

        private static void AppendFields(List<string> fields, StringBuilder builder)
        {
            foreach (var field in fields.Select(x => x.TrimStart(' ')))
            {
                if (field.StartsWith("List:"))
                {
                    var fieldName = field.Replace("List:", "").TrimEnd('s');
                    builder.AppendLine("\t\tpublic List<" + fieldName + "> " + fieldName + "s { get; set; }");
                }
                else
                {
                    builder.AppendLine("\t\tpublic string " + field + " { get; private set; }");
                }
            }
        }

        private static void AppendConstructor(StringBuilder builder, string className, List<string> fields)
        {
            builder.Append("public " + className + "(");
            builder.Append(String.Join(", ", fields.Select(CreateCtorParam)));
            builder.Append(")");
            builder.AppendLine("{");
            foreach (var field in fields)
            {
                if (field.StartsWith("List:"))
                {
                    var fieldName = field.Replace("List:", "").TrimEnd('s');
                    builder.AppendLine("this." + fieldName + "s = " + fieldName + "s;");
                }
                else
                {
                    builder.AppendLine("this." + field + " = " + field + ";");
                }
            }

            builder.AppendLine("}");
        }

        private static string CreateCtorParam(string field)
        {
            if (field.StartsWith("List:"))
            {
                var fieldName = field.Replace("List:", "").TrimEnd('s');
                return "List<" + fieldName + "> " + fieldName + "s";
            }
            else
            {
                return "string " + field;
            }
        }
    }
}
