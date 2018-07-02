using System;
using System.Collections.Generic;
using System.Reflection;

namespace Veises.Common.Service.Utils
{
    internal sealed class AssemblyProvider
    {
        private readonly IReadOnlyCollection<Assembly> _assemblies;

        public AssemblyProvider(IReadOnlyCollection<Assembly> assemblies)
        {
            _assemblies = assemblies ?? throw new ArgumentNullException(nameof(assemblies));
        }

        public IEnumerable<Assembly> GetAssemblies()
        {
            return _assemblies;
        }
    }
}