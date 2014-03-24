using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace Benchmark
{
    public abstract class Bench
    {
        /// <summary>
        /// Executes the benchmark.
        /// </summary>
        /// <param name="operations">The amount of times to run each method</param>
        public BenchResult Execute(int operations = 1000)
        {
            if (operations <= 0) throw new ArgumentOutOfRangeException("operations", "Amount must be greater than 0.");

            var bench1 = new List<double>();
            var bench2 = new List<double>();

            for (int i = 0; i < operations; i++)
            {
                bench1.Add(TimeMethod(BenchMethodOne));
                bench2.Add(TimeMethod(BenchMethodTwo));
            }

            return new BenchResult(operations, bench1.Average(), bench2.Average());
        }

        private double TimeMethod(Action method)
        {
            var timer = Stopwatch.StartNew();
            method();
            timer.Stop();
            return (timer.ElapsedTicks * ((1000 * 1000 * 1000) / (double)Stopwatch.Frequency));
        }

        protected abstract void BenchMethodOne();
        protected abstract void BenchMethodTwo();
    }

    public sealed class BenchResult
    {
        private readonly int operations;
        private readonly double method1, method2;

        public BenchResult(int operations, double method1, double method2)
        {
            this.operations = operations;
            this.method1 = method1;
            this.method2 = method2;
        }
        /// <summary>
        /// Gets the amount of times each method was executed.
        /// </summary>
        public int Operations
        {
            get { return this.operations; }
        }
        /// <summary>
        /// Gets the average execution time in nanoseconds for method one.
        /// </summary>
        public double MethodOneAverage
        {
            get { return this.method1; }
        }
        /// <summary>
        /// Gets the average execution time in nanoseconds for method two.
        /// </summary>
        public double MethodTwoAverage
        {
            get { return this.method2; }
        }
    }
}