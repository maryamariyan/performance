// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MicroBenchmarks;

namespace Microsoft.Extensions.Http.Performance
{
    [BenchmarkCategory(Categories.Libraries)]
    public class LoggingOverheadBenchmark
    {
        private const int Iterations = 100;

        public LoggingOverheadBenchmark()
        {
            Handler = new FakeClientHandler();
            LoggerProvider = new FakeLoggerProvider();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(b => b.AddProvider(LoggerProvider));
            serviceCollection.AddHttpClient("example", c =>
            {
                c.BaseAddress = new Uri("http://example.com/");
                c.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .ConfigurePrimaryHttpMessageHandler(() => Handler);

            var services = serviceCollection.BuildServiceProvider();
            Factory = services.GetRequiredService<IHttpClientFactory>();
        }

        private IHttpClientFactory Factory { get; }

        private HttpMessageHandler Handler { get; }

        private FakeLoggerProvider LoggerProvider { get; }

        [Benchmark(
            Description = "logging on", 
            OperationsPerInvoke = Iterations)]
        public async Task LoggingOn()
        {
            LoggerProvider.IsEnabled = true;

            for (var i = 0; i < Iterations; i++)
            {
                var client = Factory.CreateClient("example");

                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "api/Products"));
                response.EnsureSuccessStatusCode();
            }
        }

        [Benchmark(
            Description = "logging off",
            OperationsPerInvoke = Iterations)]
        public async Task LoggingOff()
        {
            LoggerProvider.IsEnabled = false;

            for (var i = 0; i < Iterations; i++)
            {
                var client = Factory.CreateClient("example");

                var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "api/Products"));
                response.EnsureSuccessStatusCode();
            }
        }
    }
}
