// ReSharper disable CheckNamespace
using System.Dynamic;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace System
{
	public static class Config<TReturn>
	{
#pragma warning disable CA1000 // Do not declare static members on generic types
		public static dynamic Get { get; } = new DynamicConfig<TReturn>();
#pragma warning restore CA1000 // Do not declare static members on generic types

		public class DynamicConfig<TValue> : DynamicObject
		{
			private readonly IConfiguration _config;

			public DynamicConfig()
			{
				_config = Host.CreateDefaultBuilder().Build().Services
					.GetRequiredService<IConfiguration>();
			}

			public override bool TryGetMember(GetMemberBinder binder, out object result)
			{
				result = _config.GetValue<TValue>(binder.Name);
				return true;
			}
		}
	}
}