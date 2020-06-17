﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MicroBenchmarks;

// Test code taken directly from GitHub issue #9692 (https://github.com/dotnet/coreclr/issues/9692)
// Laying the loop's early return path in-line can cost 30% on this micro-benchmark.

namespace Layout
{
    [BenchmarkCategory(Categories.Runtime)]
    public unsafe class SearchLoops
    {
        public int length = 100;

        private string test1;
        private string test2;

        public SearchLoops()
        {
            test1 = new string('A', length);
            test2 = new string('A', length);
        }

        [Benchmark]
        public bool LoopReturn() => LoopReturn(test1, test2);

        [Benchmark]
        public bool LoopGoto() => LoopGoto(test1, test2);

        // Variant with code written naturally -- need JIT to lay this out
        // with return path out of loop for best performance.
        public static bool LoopReturn(String strA, String strB)
        {
            int length = strA.Length;

            fixed (char* ap = strA) fixed (char* bp = strB)
            {
                char* a = ap;
                char* b = bp;

                while (length != 0)
                {
                    int charA = *a;
                    int charB = *b;

                    if (charA != charB)
                        return false;  // placement of prolog for this return is the issue

                    a++;
                    b++;
                    length--;
                }

                return true;
            }
        }

        // Variant with code written awkwardly but which acheives the desired
        // performance if JIT simply lays out code in source order.
        public static bool LoopGoto(String strA, String strB)
        {
            int length = strA.Length;

            fixed (char* ap = strA) fixed (char* bp = strB)
            {
                char* a = ap;
                char* b = bp;

                while (length != 0)
                {
                    int charA = *a;
                    int charB = *b;

                    if (charA != charB)
                        goto ReturnFalse;  // placement of prolog for this return is the issue

                    a++;
                    b++;
                    length--;
                }

                return true;

                ReturnFalse:
                return false;
            }
        }
    }
}
