﻿using Umbraco.Cms.Core.Composing;

namespace Formulate.Core.Types
{
    /// <inheritdoc />
    public sealed class ValidationTypeCollectionBuilder : LazyCollectionBuilderBase<ValidationTypeCollectionBuilder, ValidationTypeCollection, IValidationType>
    {
        /// <inheritdoc />
        protected override ValidationTypeCollectionBuilder This => this;
    }
}