using Polly;
using Polly.Retry;
using System.Data.Entity.Core;

namespace Inventory.Policies
{
    public class ServerPolicy
    {
        public AsyncRetryPolicy DbConnectionRetry { get; }
        public AsyncRetryPolicy RetryDbForever { get; }

        public ServerPolicy()
        {
            DbConnectionRetry = Policy.Handle<EntityException>().RetryAsync(
                5, 
                onRetry:(exception, retryCount)=>
                    {
                    Console.Error.WriteLine(String.Format("{0}: Attempted to connect to db {1} times and failed. Retrying..", exception, retryCount));
                    }
                );

            RetryDbForever = Policy.Handle<EntityException>()
                .WaitAndRetryForeverAsync(
                retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (exception, retryCount) =>
                    {
                    Console.Error.WriteLine(String.Format("{0}: Attempted to connect to db {1} times and failed. Retrying..", exception, retryCount));
                    }
                );
        }

    }
}
