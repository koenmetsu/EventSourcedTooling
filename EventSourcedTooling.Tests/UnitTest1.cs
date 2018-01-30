using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Xunit;

namespace EventSourcedTooling.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        [Fact]
        public void TestCreation()
        {
            foreach (var file in Directory.EnumerateFiles(@"C:\Users\Koen\source\repos\EventSourcedTooling\EventSourcedTooling\Commands\", "*.txt"))
            {
                var lines = System.IO.File.ReadAllLines(file);

                var cmdName = lines[0];
                var fields = lines.Skip(1).Select(text => text.TrimStart(' ')).ToList();

                var builder = new StringBuilder();
                builder.AppendLine("namespace EventSourcedTooling {");
                builder.AppendLine($"\tpublic class {cmdName}{{");
                foreach (var field in fields)
                {
                    builder.AppendLine("\t\tpublic string " + field + " { get; set; }");
                }
                builder.AppendLine("\t}");
                builder.AppendLine("}");
                var clazz = builder.ToString();

                File.WriteAllText(@"C:\Users\Koen\source\repos\EventSourcedTooling\EventSourcedTooling\Generated\" + cmdName + ".cs", clazz);

            }

            var t = 1;
        }
    }
}
