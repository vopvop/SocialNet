namespace Veises.Common.Service.Settings
{
    /// <summary>
    ///     Specified settings provider.
    /// </summary>
    /// <typeparam name="TSettings">Settings class type with default constructor.</typeparam>
    public interface ISetting<out TSettings>
        where TSettings : class, new()
    {
        /// <summary>
        ///     Get settings class from configuration provider.
        /// </summary>
        /// <returns>Settings class instance.</returns>
        TSettings GetSettings();
    }
}