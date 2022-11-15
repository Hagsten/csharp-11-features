namespace Demo.StringInterpolation
{
    //It allows any valid code between { }, including newlines
    public class Example
    {
        static void Main(string[] args)
        {
            DemoInterpolation();
            DemoRawStringLiterals();
        }

        public static void DemoInterpolation()
        {
            var time = DateTime.Now;

            Console.WriteLine($"It is now {time switch
            {
                { Hour: <= 7 } => "Sleepy time",
                { Hour: <= 17 } => "Work time",
                { Hour: <= 23 } => "Evening time",
                _ => "Some other time"
            }}");

            Console.ReadLine();

            Console.WriteLine($"Values below 10 are {
                string.Join(", ", Enumerable.Range(1, 20)
                                            .Where(x => x < 10))
            }");

            Console.ReadLine();
        }

        public static void DemoRawStringLiterals()
        {
            var name = "Andreas";
            var surname = "Hagsten";

            var str = """He said "I'll be there at 7", but he wasn't""";

            Console.WriteLine(str);

            //Indentation
            var jsonString =
                $$""""
                {
                    "Name": {{name}},
                    "Surname": {{surname}}
                }
                """";

            Console.WriteLine(jsonString);

            Console.ReadLine();
        }
    }
}