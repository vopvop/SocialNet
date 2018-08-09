using System;

namespace Veises.Common.Service.IoC
{
    public sealed class InjectDependencyAttribute : Attribute
    {
        public DependencyScope Scope { get; }

        public InjectDependencyAttribute() : this(DependencyScope.Scoped)
        {
            
        }

        public InjectDependencyAttribute(DependencyScope scope)
        {
            Scope = scope;
        }
    }
}