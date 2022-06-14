// ReSharper disable CheckNamespace

using System.Dynamic;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace System
{

	public static partial class Config<TReturn>
	{
		public static dynamic Get = new DynamicConfig<TReturn>();

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
