using System;
using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Core.Utils;
using GrpcSample.Shared;

namespace GrpcSample.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        private static async Task RunAsync()
        {
            var channel = new Channel("127.0.0.1", 5000, ChannelCredentials.Insecure);
            var invoker = new DefaultCallInvoker(channel);
            using (var call = invoker.AsyncDuplexStreamingCall(Descriptors.Method, null, new CallOptions{}))
            {
                var responseCompleted = call.ResponseStream
                    .ForEachAsync(async response =>
                    {
                        Console.WriteLine($"Got response: {response.Payload}");
                    });
                
                for (int i = 0; i < 100; i++)
                {
                    await call.RequestStream.WriteAsync(new CustomRequest {Payload = i });
                }

                await call.RequestStream.CompleteAsync();
                await responseCompleted;
            }

            Console.WriteLine("Press enter to stop...");
            Console.ReadLine();

            await channel.ShutdownAsync();
        }
    }
}