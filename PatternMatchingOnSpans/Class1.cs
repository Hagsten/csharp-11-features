using Xunit;

namespace PatternMatchingOnSpans
{
    public class Class1
    {
        [Fact]
        public void Foo()
        {
            var str = "hejsan svejsan".AsSpan();

            var match = str switch
            {
                "hejsan" => "Svensjan",
                { Length: 3} => "Length 3",
                var s when s.StartsWith("hej") => "Hej på dig!",
                var s when s.EndsWith("san") => "Sansei"
            };
            
        }


    }
}