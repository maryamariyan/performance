// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Linq;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Extensions;
using MicroBenchmarks;

// Performance test for virtual call dispatch with three
// possible target classes mixed in varying proportions.

namespace GuardedDevirtualization
{   
    [BenchmarkCategory(Categories.Runtime, Categories.Virtual)]
    public class ThreeClassVirtual
    {
        public class B
        {
            public virtual int F() => 33;
        }
    
        public class D : B
        {
            public override int F() => 44;
        }
    
        public class E : B
        {
            public override int F() => 55;
        }
        
        [Benchmark(OperationsPerInvoke=TestInput.N)]
        [ArgumentsSource(nameof(GetInput))]
        public long Call(TestInput testInput)
        {
            long sum = 0;
            B[] input = testInput.Array;
            for (int i = 0; i < input.Length; i++)
            {
                sum += input[i].F();
            }
            return sum;
        }
        
        public static IEnumerable<TestInput> GetInput()
        {
            const int S = 3;
            const double delta = 1.0 / (double) S;
            
            for (int i = 0; i <= S; i++)
            {
                for (int j = 0; j <= S - i; j++)
                {
                    yield return new TestInput(i * delta, j * delta);
                }
            }
        }
    
        public class TestInput
        {
            public const int N = 1000;

            public B[] Array;
            private double _pB;
            private double _pD;
            
            public TestInput(double pB, double pD)
            {
                _pB = pB;
                _pD = pD;
                Array = ValuesGenerator.Array<double>(N).Select(p =>
                {
                    if (p <= _pB)
                        return new B();
                    if (p <= _pB + _pD)
                        return new D();
                    return new E();
                }).ToArray();
            }
    
            public override string ToString() => $"pB={_pB:F2} pD={_pD:F2}";
        }     
    }
}


