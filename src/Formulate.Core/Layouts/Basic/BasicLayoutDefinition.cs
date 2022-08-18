using System;
using Formulate.Core.Utilities;

namespace Formulate.Core.Layouts.Basic
{
    /// <summary>
    /// A layout definition for creating <see cref="BasicLayout"/>.
    /// </summary>
    public sealed class BasicLayoutDefinition : ILayoutDefinition
    {
        private readonly IJsonUtility _jsonUtility;

        /// <summary>
        /// Constants related to <see cref="BasicLayoutDefinition"/>.
        /// </summary>
        public static class Constants
        {
            /// <summary>
            /// The kind ID.
            /// </summary>
            public const string KindId = "B03310E9320744DCBE96BE0CF4F26C59";

            /// <summary>
            /// The name.
            /// </summary>
            public const string Name = "Basic Layout";

            /// <summary>
            /// The Angular JS directive.
            /// </summary>
            public const string Directive = "formulate-layout-basic";
        }

        /// <inheritdoc />
        public Guid KindId => Guid.Parse(Constants.KindId);

        /// <inheritdoc />
        public string Name => Constants.Name;

        /// <inheritdoc />
        public string Directive => Constants.Directive;
        
        /// <inheritdoc />
        public bool IsLegacy => false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonUtility"></param>
        public BasicLayoutDefinition(IJsonUtility jsonUtility)
        {
            _jsonUtility = jsonUtility;
        }

        /// <inheritdoc />
        public ILayout CreateLayout(ILayoutSettings settings)
        {
            var config = _jsonUtility.Deserialize<BasicLayoutConfiguration>(settings.Data);

            return new BasicLayout(settings, config);
        }

        /// <inheritdoc />
        public object GetBackOfficeConfiguration(ILayoutSettings settings)
        {
            var existingConfig = _jsonUtility.Deserialize<BasicLayoutConfiguration>(settings.Data);

            if (existingConfig is not null)
            {
                return existingConfig;
            }

            return new BasicLayoutConfiguration()
            {
                AutoPopulate = false
            };            
        }
    }
}
