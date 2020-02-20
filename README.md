# Send messages and trigger handlers using Nimbus and Redis

This is a .Net core 3.0 console application test 
of Nimbus and Nimbus.Transports.Redis 4.0.19.

It sets up a minimal Nimbus using the Redis Transport.

On a clean empty Redis transport instance I have noticed the following problem.

Unless I wait for at least 3 seconds after starting the bus 
the first Event sent does not trigger the competing handler 
configured for that event.

Search for `// Uncomment for handler WhenCreatedEventArrives to trigger on empty Redis.`
Restore the delay commented out following this comment above and on my machine 
WhenCreatedEventArrives will be triggered on first run on an empty Redis.

This delay is not required for the command that is sent it always triggers its handler.

To run this it required an environment variable "ConnectionStrings:EventBus" to be
a connection string to a redis instance. I have been testing with the smallest
redis instance on Azure.

It does not matter if WhenCreatedEventArrives is `IHandleCompetingEvent<>` or a
`IHandleMulticastEvent<>` the first event doesn't trigger handler if there isn't 
a delay on an empty Redis.

## To run this example

Set environment variable ConnectionStrings:EventBus to a valid redis connection string.
I have been testing with the smallest azure redis instance.

execute `dotnet restore`

execute `dotnet run`
