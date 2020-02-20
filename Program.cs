using System;
using System.Reflection;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Nimbus.Configuration;
using Nimbus.Infrastructure;
using Nimbus.InfrastructureContracts;
using Nimbus.Logger.Serilog.Configuration;
using Nimbus.Serializers.Json.Configuration;
using Nimbus.Transports.Redis;
using Serilog;

namespace SendCommand
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = SetupConfiguration();
            var redisConnectionString = configuration.GetConnectionString("EventBus");
            SetupLog();            
            
            var bus = SetupBus(redisConnectionString);
            Log.Information("Welcome to SendCommand");

            // Uncomment for handler WhenCreatedEventArrives to trigger on empty Redis.
            // Thread.Sleep(3000); // 2000 is not long enough for first event sent to fire handler

            var command = new DeleteCommand(Guid.NewGuid(), "Test Command");
            bus.Send(command);
            Log.Information("After Send Command");
            
            var busEvent = new CreatedEvent(Guid.NewGuid(), "Test Event");
            bus.Publish(busEvent);
            Log.Information("After Publish Event");

            Thread.Sleep(5000);
            Log.Information("After Sleep 5000");

            Console.ReadKey();
            Log.Information("Exiting...");
            Log.CloseAndFlush();
        }

        private static IConfigurationRoot SetupConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();
            return builder;
        }

        private static void SetupLog()
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            Log.Logger = log;
        }

        private static IBus SetupBus(string connectionString)
        {
            var transport = new RedisTransportConfiguration().WithConnectionString(connectionString);
            var typeProvider = new AssemblyScanningTypeProvider(Assembly.GetExecutingAssembly());
            var bus = new BusBuilder().Configure()
                .WithNames("SendAndHandleMessagesApp", Environment.MachineName)
                .WithTransport(transport)
                .WithTypesFrom(typeProvider)
                .WithDefaultTimeout(TimeSpan.FromSeconds(1000))
                .WithSerilogLogger()
                .WithJsonSerializer()
                .Build();
            bus.Start();
            return bus;
        }
    }
}