using DotnetCoding.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetCoding.Services
{
    public class RedisKeyExpirationListener :IHostedService
    {
        private readonly IConnectionMultiplexer _redisConnection;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private ISubscriber _subscriber;
 /*       private SemaphoreSlim _processingSemaphore = new SemaphoreSlim(1, 1); // Allow 1 thread at a time*/
        public RedisKeyExpirationListener(IConnectionMultiplexer redisConnection,IServiceScopeFactory scopeFactory)
        {
            _redisConnection = redisConnection;
            _serviceScopeFactory = scopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriber = _redisConnection.GetSubscriber();
            _subscriber.Subscribe("__keyevent@0__:expired", async (channel, key) =>
            {
                if (key.ToString().StartsWith("reservation:"))
                {
                   /* await _processingSemaphore.WaitAsync(); // Wait for the semaphore
                    try
                    {*/
                        using (var scope = _serviceScopeFactory.CreateScope())
                        {
                            var reservationService = scope.ServiceProvider.GetRequiredService<IReservationService>();

                            int reservationId = int.Parse(key.ToString().Split(':')[1]);
                            await reservationService.CancelReservationAsync(reservationId);
                        }
                    /*}
                    finally
                    {
                        _processingSemaphore.Release(); // Release the semaphore for the next operation
                    }*/
                }
            });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _subscriber.UnsubscribeAll();
            return Task.CompletedTask;
        }
    }
}
