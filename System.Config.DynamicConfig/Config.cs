// ReSharper disable CheckNamespace

using System.Dynamic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace System
{
    public static class Config<TReturn>
    {
        private static readonly Lazy<DynamicConfig<TReturn>> LazyInstance = new(() => new DynamicConfig<TReturn>());

#pragma warning disable CA1000 // Do not declare static members on generic types
        public static dynamic Get { get; } = LazyInstance.Value;
#pragma warning restore CA1000 // Do not declare static members on generic types

        private class DynamicConfig<TValue> : DynamicObject
        {
            private static readonly object LockObj = new();
            private readonly IConfiguration _config;
            private readonly ILogger _logger;

            public DynamicConfig(IConfiguration config, ILogger logger)
            {
                _config = config;
                _logger = logger;
            }

            public DynamicConfig(IConfiguration config)
            {
                _config = config;
                var host = Host.CreateDefaultBuilder().Build();
                var services = host.Services;

                _logger = services.GetRequiredService<ILogger<DynamicConfig<TValue>>>();
            }

            public DynamicConfig()
            {
                var host = Host.CreateDefaultBuilder().Build();
                var services = host.Services;

                _config = services.GetRequiredService<IConfiguration>();
                _logger = services.GetRequiredService<ILogger<DynamicConfig<TValue>>>();
            }

            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                try
                {
                    lock (LockObj)
                    {
                        // Fetch the configuration value
                        result = _config.GetValue<TValue>(binder.Name);

                        // Check if the value exists
                        if (result == null)
                        {
                            _logger.LogWarning($"Could not fetch configuration value {binder.Name}");
                            result = default(TValue);
                            return false;
                        }

                        return true;
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Could not fetch configuration value {binder.Name}", ex);

                    result = default(TValue);
                    return false;
                }
            }
        }
    }
}