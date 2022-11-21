using System.Collections;
using System.Numerics;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Demo.ListPatterns
{
    public class Examples
    {
        private readonly ITestOutputHelper _output;

        public Examples(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(new[] { 1, 2 }, "1 and 2")]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, "3 in the middle")]
        [InlineData(new[] { 2, 3, 1 }, "don't care, don't care, ends with 1")]
        [InlineData(new[] { 2, 3, 4 }, "No match")]
        [InlineData(new[] { 2, 3, 4, 5, 6, 7, 8, 9, 10 }, "No match")]
        public void ConstantAndDiscardPatterns(int[] input, string output)
        {
            var result = input switch
            {
                [1, 2] => "1 and 2",
                [_, _, 3, _, _] => "3 in the middle",
                [_, _, 1] => "don't care, don't care, ends with 1",
                _ => "No match"
            };

            result.Should().Be(output);
        }

        [Theory]
        [InlineData(new[] { 1, 2 }, "1 and 2")]
        [InlineData(new[] { 1, 2, 3, 4, 5 }, "1 then some, then 5")]
        [InlineData(new[] { 2, 3, 4 }, "2 then some, then 4")]
        [InlineData(new[] { 2, 3, 4, 5, 10 }, "2 then some, then 10")]
        public void VarPatterns(int[] input, string output)
        {
            var result = input switch
            {
                [var a, var b] => $"{a} and {b}",
                [var a, .., var z] => $"{a} then some, then {z}",
                _ => "No match"
            };

            result.Should().Be(output);
        }

        [Fact]
        public void SlicePatterns()
        {
            var array = new[] { 1, 2, 3, 4, 5 };

            //Start with 1, then some others
            (array is [1, ..]).Should().BeTrue();

            //Start with 1, then some, then ends with 5
            (array is [1, .., 5]).Should().BeTrue();

            //Ends with 5
            (array is [.., 5]).Should().BeTrue();

            _output.WriteLine(array switch
            {
                [1, .. var tail] => string.Join(", ", tail),
                _ => ""
            });
        }

        [Fact]
        public void RelationalPatterns()
        {
            var array = new[] { 1, 2, 3, 4, 5 };

            //Last element should be less than 6
            (array is [.., < 6]).Should().BeTrue();

            //First element larger than 0 and last element less than 6
            (array is [> 0, .., < 6]).Should().BeTrue();

            //3rd element between 2 and 4 (3)
            (array is [.., > 2 and < 4, _, _]).Should().BeTrue();
        }

        [Fact]
        public void ComplexTypePatterns()
        {
            var person1 = new Person(25, "Sven", "Svensson");
            var person2 = new Person(38, "Andreas", "Hagsten");
            var person3 = new Person(57, "Adam", "Adamsson");

            var persons = new[] { person1, person2, person3 };

            _output.WriteLine(persons switch
            {
                [{ Age: >= 18 }, .., { Age: <= 66 }] => "Yey",
                _ => "Ney"
            });
        }

        [Fact]
        public void JaggedArrayPatterns()
        {
            var jagged = new[]
            {
                new[] { 1, 2, 3 },
                new[] { 4, 5, 6 },
                new[] { 7, 8, 9 }
            };

            if (jagged is [_, [_, .. var fiveAndSix], _])
            {
                _output.WriteLine(string.Join(", ", fiveAndSix));
            }
        }
    }

    record Person(int Age, string FirstName, string LastName);
}
































public class MyOwnTypeForListPatternMatching
{
    public int Count { get; set; }

    public int this[int index]
    {
        get => throw new AbandonedMutexException();
        set => throw new AbandonedMutexException();
    }
}