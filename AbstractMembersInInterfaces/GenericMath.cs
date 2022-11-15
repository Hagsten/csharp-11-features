using System.Numerics;
using Xunit;
using Xunit.Abstractions;

namespace Demo.StaticAbstractInterfaceMembers
{
    public class GenericMath
    {
        private readonly ITestOutputHelper _output;

        public GenericMath(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void DoCalc()
        {
            var average = new Average<int>();

            _output.WriteLine($"Instance average: {average.CalculateAverage(new[] { 1, 2, 3 })}");

            _output.WriteLine($"static int average: {Average<int>.CalculateAverageStatic(new[] { 1, 2, 3 })}");
            _output.WriteLine($"static double average: {Average<double>.CalculateAverageStatic(new[] { 1.3, 2.1, 3.23 })}");
            _output.WriteLine($"static decimal average: {Average<decimal>.CalculateAverageStatic(new[] { 1.3M, 2.1M, 3.23M })}");
        }
    }

    public interface IAverage<T> where T : INumber<T>
    {
        public T CalculateAverage(T[] inputs);

        static abstract T CalculateAverageStatic(T[] inputs);
    }

    public class Average<T> : IAverage<T> where T : INumber<T>
    {
        public T CalculateAverage(T[] inputs) => CalculateAverageStatic(inputs);

        public static T CalculateAverageStatic(T[] inputs) 
        {
            var length = T.Zero;
            var sum = T.Zero;
            
            foreach (var number in inputs)
            {
                length += T.One;
                sum += number;
            }

            return sum / length;
        }
    }

    public static class OldAverage
    {
        public static int CalculateAverage(int[] values)
        {
            return values.Sum(x => x) / values.Length;
        }

        public static double CalculateAverage(double[] values)
        {
            return values.Sum(x => x) / values.Length;
        }

        public static decimal CalculateAverage(decimal[] values)
        {
            return values.Sum(x => x) / values.Length;
        }

        public static float CalculateAverage(float[] values)
        {
            return values.Sum(x => x) / values.Length;
        }
    }
}