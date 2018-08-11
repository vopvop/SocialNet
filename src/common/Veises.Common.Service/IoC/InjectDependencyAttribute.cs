using System;

namespace Veises.Common.Service.IoC
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class InjectDependencyAttribute : Attribute
    {
        public InjectDependencyAttribute() : this(DependencyScope.Scoped)
        {
        }

        public InjectDependencyAttribute(DependencyScope scope)
        {
            Scope = scope;
        }

        public DependencyScope Scope { get; }
    }
}