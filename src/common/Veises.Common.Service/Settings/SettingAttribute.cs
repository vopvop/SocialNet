using System;

namespace Veises.Common.Service.Settings
{
    /// <summary>
    ///     Setting definition.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public sealed class SettingAttribute : Attribute
    {
        /// <summary>
        ///     Mark class as configuration section settings representation.
        /// </summary>
        /// <param name="sectionName">Configuration section name.</param>
        public SettingAttribute(string sectionName)
        {
            SectionName = sectionName ?? throw new ArgumentNullException(nameof(sectionName));
        }

        /// <summary>
        ///     Configuration section name.
        /// </summary>
        public string SectionName { get; }
    }
}