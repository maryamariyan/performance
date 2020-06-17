// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
/*
** This program was translated to C# and adapted for BenchmarkDotNet.
** New variants of several tests were added to compare class versus 
** struct and to compare jagged arrays vs multi-dimensional arrays.
*/

// Captures contents of NNET.DAT as a string
// to simplfy testing in CoreCLR.

internal class NeuralData
{
    public static string Input =
    "5  7  8 \n"
    + "26 \n"
    + "0  0  1  0  0 \n"
    + "0  1  0  1  0 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  1  1  1  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "0  1  0  0  0  0  0  1 \n"
    + "1  1  1  1  0 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  1  1  1  0 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  1  1  1  0 \n"
    + "0  1  0  0  0  0  1  0 \n"
    + "0  1  1  1  0 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  1 \n"
    + "0  1  1  1  0 \n"
    + "0  1  0  0  0  0  1  1 \n"
    + "1  1  1  1  0 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  1  1  1  0 \n"
    + "0  1  0  0  0  1  0  0 \n"
    + "1  1  1  1  1 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  1  1  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  1  1  1  1 \n"
    + "0  1  0  0  0  1  0  1 \n"
    + "1  1  1  1  1 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  1  1  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "0  1  0  0  0  1  1  0 \n"
    + "0  1  1  1  0 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  1  1 \n"
    + "1  0  0  0  1 \n"
    + "0  1  1  1  0 \n"
    + "0  1  0  0  0  1  1  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  1  1  1  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "0  1  0  0  1  0  0  0 \n"
    + "0  1  1  1  0 \n"
    + "0  0  1  0  0 \n"
    + "0  0  1  0  0 \n"
    + "0  0  1  0  0 \n"
    + "0  0  1  0  0 \n"
    + "0  0  1  0  0 \n"
    + "0  1  1  1  0 \n"
    + "0  1  0  0  1  0  0  1 \n"
    + "0  0  0  0  1 \n"
    + "0  0  0  0  1 \n"
    + "0  0  0  0  1 \n"
    + "0  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "0  1  1  1  0 \n"
    + "0  1  0  0  1  0  1  0 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  1  0 \n"
    + "1  0  1  0  0 \n"
    + "1  1  0  0  0 \n"
    + "1  0  1  0  0 \n"
    + "1  0  0  1  0 \n"
    + "1  0  0  0  1 \n"
    + "0  1  0  0  1  0  1  1 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  1  1  1  1 \n"
    + "0  1  0  0  1  1  0  0 \n"
    + "1  0  0  0  1 \n"
    + "1  1  0  1  1 \n"
    + "1  0  1  0  1 \n"
    + "1  0  1  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "0  1  0  0  1  1  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  1  0  0  1 \n"
    + "1  0  1  0  1 \n"
    + "1  0  1  0  1 \n"
    + "1  0  1  0  1 \n"
    + "1  0  0  1  1 \n"
    + "1  0  0  0  1 \n"
    + "0  1  0  0  1  1  1  0 \n"
    + "0  1  1  1  0 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "0  1  1  1  0 \n"
    + "0  1  0  0  1  1  1  1 \n"
    + "1  1  1  1  0 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  1  1  1  0 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "0  1  0  1  0  0  0  0 \n"
    + "0  1  1  1  0 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  1  0  1 \n"
    + "1  0  0  1  1 \n"
    + "0  1  1  1  1 \n"
    + "0  1  0  1  0  0  0  1 \n"
    + "1  1  1  1  0   \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  1  1  1  0 \n"
    + "1  0  1  0  0 \n"
    + "1  0  0  1  0 \n"
    + "1  0  0  0  1 \n"
    + "0  1  0  1  0  0  1  0 \n"
    + "0  1  1  1  1 \n"
    + "1  0  0  0  0 \n"
    + "1  0  0  0  0 \n"
    + "0  1  1  1  0 \n"
    + "0  0  0  0  1 \n"
    + "0  0  0  0  1 \n"
    + "1  1  1  1  0 \n"
    + "0  1  0  1  0  0  1  1 \n"
    + "1  1  1  1  1 \n"
    + "0  0  1  0  0 \n"
    + "0  0  1  0  0 \n"
    + "0  0  1  0  0 \n"
    + "0  0  1  0  0 \n"
    + "0  0  1  0  0 \n"
    + "0  0  1  0  0 \n"
    + "0  1  0  1  0  1  0  0 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "0  1  1  1  0 \n"
    + "0  1  0  1  0  1  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "0  1  0  1  0 \n"
    + "0  1  0  1  0 \n"
    + "0  1  0  1  0 \n"
    + "0  1  0  1  0 \n"
    + "0  0  1  0  0 \n"
    + "0  1  0  1  0  1  1  0 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  0  0  1 \n"
    + "1  0  1  0  1 \n"
    + "1  0  1  0  1 \n"
    + "1  0  1  0  1 \n"
    + "0  1  0  1  0 \n"
    + "0  1  0  1  0  1  1  1 \n"
    + "1  0  0  0  1 \n"
    + "0  1  0  1  0 \n"
    + "0  1  0  1  0 \n"
    + "0  0  1  0  0 \n"
    + "0  1  0  1  0 \n"
    + "0  1  0  1  0 \n"
    + "1  0  0  0  1 \n"
    + "0  1  0  1  1  0  0  0 \n"
    + "1  0  0  0  1 \n"
    + "0  1  0  1  0 \n"
    + "0  1  0  1  0 \n"
    + "0  0  1  0  0 \n"
    + "0  0  1  0  0 \n"
    + "0  0  1  0  0 \n"
    + "0  0  1  0  0 \n"
    + "0  1  0  1  1  0  0  1 \n"
    + "1  1  1  1  1 \n"
    + "0  0  0  1  0 \n"
    + "0  0  0  1  0 \n"
    + "0  0  1  0  0 \n"
    + "0  1  0  0  0 \n"
    + "0  1  0  0  0 \n"
    + "1  1  1  1  1 \n"
    + "0  1  0  1  1  0  1  0 \n"
    + " \n";
}
