namespace Formulate.Core.Templates
{
    using System;

    /// <summary>
    /// An abstract implementation of <see cref="ITemplateDefinition" />.
    /// </summary>
    /// <remarks>Other template definitions can inherit from this.</remarks>
    public abstract class TemplateDefinition : ITemplateDefinition
    {
        /// <inheritdoc />
        public abstract string ViewName { get; }

        /// <inheritdoc />
        public abstract Guid KindId { get; }

        /// <inheritdoc />
        public abstract string Name { get; }

        /// <inheritdoc />
        public virtual bool IsLegacy => false;
    }
}
