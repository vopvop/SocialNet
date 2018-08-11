namespace Veises.Common.Service
{
    internal sealed class SystemEnvironment : ISystemEnvironment
    {
        public SystemEnvironment(string[] commandLineArgs)
        {
            CommandLineArgs = commandLineArgs;
        }

        public string[] CommandLineArgs { get; }
    }
}