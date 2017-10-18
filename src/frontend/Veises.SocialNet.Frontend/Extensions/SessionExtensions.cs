using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Veises.SocialNet.Frontend.Extensions
{
	internal static class SessionExtensions
	{
		public static void Set<T>(this ISession session, string key, T value)
		{
			var serialized = JsonConvert.SerializeObject(value);

			session.SetString(key, serialized);
		}

		public static T Get<T>(this ISession session, string key)
		{
			var value = session.GetString(key);

			return value == null
				? default(T)
				: JsonConvert.DeserializeObject<T>(value);
		}
	}
}