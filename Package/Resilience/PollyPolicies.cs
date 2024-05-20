using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Wrap;
using System;
using System.Net.Http;

namespace Resilience
{
    public static class PollyPolicies
    {
        // GetRetryPolicy method
        // Returns an AsyncRetryPolicy<HttpResponseMessage> object
        // The policy will retry the request 3 times if the request is not successful
        // The policy will wait 2 seconds between each retry
        public static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            // Return a new AsyncRetryPolicy<HttpResponseMessage> object
            return Policy
                // Handle the result of the request
                // If the request is not successful
                // or if an HttpRequestException occurs
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .Or<HttpRequestException>()
                // Retry the request 3 times
                // Wait 2 seconds between each retry
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        // GetCircuitBreakerPolicy method
        // Returns an AsyncCircuitBreakerPolicy<HttpResponseMessage> object
        // The policy will break the circuit if the request is not successful 3 times in a row
        // The policy will wait 30 seconds before trying again
        public static AsyncCircuitBreakerPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return Policy
                // Handle the result of the request
                // If the request is not successful
                // or if an HttpRequestException occurs
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .Or<HttpRequestException>()
                // Break the circuit if the request is not successful 3 times in a row
                // Wait 30 seconds before trying again
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30),
                // When the circuit is broken
                // It writes "Circuit breaker tripped!" to the console
                // It enters this state after the request fails 3 times in a row
                    onBreak: (ex, breakDelay) => { Console.WriteLine("Circuit breaker tripped!"); },
                    // When the circuit is reset
                    // It writes "Circuit breaker reset!" to the console
                    // It enters this state after the break delay has passed
                    onReset: () => { Console.WriteLine("Circuit breaker reset!"); },
                    // When the circuit is half-open
                    // It writes "Circuit breaker half-open!" to the console
                    // It enters this state after the break delay has passed
                    onHalfOpen: () => { Console.WriteLine("Circuit breaker half-open!"); }
                );

        }

        public static AsyncPolicyWrap<HttpResponseMessage> GetRetryAndCircuitBreakerPolicy()
        {
            return Policy.WrapAsync(GetRetryPolicy(), GetCircuitBreakerPolicy());
        }

    }
}
