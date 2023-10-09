using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class AppSettings
    {
        private static readonly ConcurrentDictionary<Type, object> ConfigInstances = new();

        public static dynamic Get<TReturn>()
        {
            return ConfigInstances.GetOrAdd(typeof(TReturn), _ => new DynamicConfig<TReturn>());
        }

        public static dynamic Get<TReturn>(TReturn defaultValue)
        {
            return ConfigInstances.GetOrAdd(typeof(TReturn), _ => new DynamicConfig<TReturn>(defaultValue));
        }

        private class DynamicConfig<TValue> : DynamicObject
        {
            // ReSharper disable once StaticMemberInGenericType
            private static readonly object LockObj = new();
            private readonly IConfiguration _config;
            private readonly ILogger _logger;
            private readonly bool _hasDefaultValue;
            private readonly TValue _defaultValue;

            internal DynamicConfig(TValue defaultValue) : this()
            {
                _defaultValue = defaultValue;
                _hasDefaultValue = true;
            }

            internal DynamicConfig(IConfiguration config, ILogger logger)
            {
                _config = config;
                _logger = logger;
            }

            internal DynamicConfig(IConfiguration config)
            {
                _config = config;
                var host = Host.CreateDefaultBuilder().Build();
                var services = host.Services;

                _logger = services.GetRequiredService<ILogger<DynamicConfig<TValue>>>();
            }

            internal DynamicConfig()
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
                        // Check if the key exists in the configuration
                        var section = _config.GetSection(binder.Name);
                        if (!section.Exists())
                        {
                            if (!_hasDefaultValue)
                            {
                                throw new KeyNotFoundException($"Configuration key {binder.Name} does not exist.");
                            }

                            result = _defaultValue;
                            return true;

                        }

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