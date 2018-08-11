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
                    $"Setting attribute is not defined for class {typeof(T).Escaped()}.");

            var configSection = _configuration.GetSection(settingAttribute.SectionName);

            if (configSection == null)
                return new T();

            var settingInstance = new T();

            configSection.Bind(settingInstance);

            return settingInstance;
        }
    }
}