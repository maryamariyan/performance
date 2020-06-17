// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using BenchmarkDotNet.Attributes;
using MicroBenchmarks;

namespace System.Tests
{
    [BenchmarkCategory(Categories.Libraries)]
    public class Perf_Object
    {
        object obj = new object();
        
        [Benchmark]
        public object ctor() => new object();

        [Benchmark(Description = "GetType")]
        public Type GetType_() => obj.GetType();
    }
}
