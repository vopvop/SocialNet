using System;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Veises.Common.Extensions;

namespace Veises.Common.Service.Settings
{
    /// <inheritdoc />
    internal sealed class Setting<T> : ISetting<T> where T : class, new()
    {
        private readonly IConfiguration _configuration;

        public Setting(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <inheritdoc />
        public T GetSettings()
        {
            var settingAttribute = typeof(T).GetCustomAttribute<SettingAttribute>();

            if (settingAttribute == null)
                throw new InvalidOperationException(
                    $"Class of type {typeof(T).Escaped()} is not marked with {typeof(SettingAttribute).Escaped()} attribute.");

            return _configuration.GetSection(settingAttribute.SectionName).Get<T>();
        }
    }
}