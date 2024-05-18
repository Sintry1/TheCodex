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
        public static AsyncRetryPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .Or<HttpRequestException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public static AsyncCircuitBreakerPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return Policy
                .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(30),
                    onBreak: (ex, breakDelay) => { Console.WriteLine("Circuit breaker tripped!"); },
                    onReset: () => { Console.WriteLine("Circuit breaker reset!"); },
                    onHalfOpen: () => { Console.WriteLine("Circuit breaker half-open!"); }
                );

        }

        public static AsyncPolicyWrap<HttpResponseMessage> GetRetryAndCircuitBreakerPolicy()
        {
            return Policy.WrapAsync(GetRetryPolicy(), GetCircuitBreakerPolicy());
        }

    }
}
