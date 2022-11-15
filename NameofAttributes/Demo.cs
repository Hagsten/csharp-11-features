using System.Diagnostics;
using System.Runtime.CompilerServices;
using FluentAssertions.Common;
using Xunit;

namespace NameOfScope
{
    public class Demo
    {
        [Log(nameof(param1), nameof(param2), nameof(param3))]
        public void DoSomeStuff(string param1, int param2, double param3, [CallerArgumentExpression(nameof(param1))] string foo = "test")
        {}
    }

    public class LogAttribute : Attribute
    {
        public LogAttribute(params string[] argNames)
        {
            Debug.WriteLine(string.Join(", ", argNames));
        }
    }

    public class DemoRunner
    {
        [Fact]
        public void Runner()
        {
            var demo = new Demo();

            demo.DoSomeStuff("Param 1 value", 1337, 1337.0);

            var attr = demo.GetType().GetMethod("DoSomeStuff").GetCustomAttributes(true);
        }
    }
}