using System;
using System.Linq;
using Formulate.Core.Forms;
using Formulate.Core.Persistence;
using Formulate.Core.Utilities;

namespace Formulate.Core.Layouts.Basic
{
    /// <summary>
    /// A layout definition for creating <see cref="BasicLayout"/>.
    /// </summary>
    public sealed class BasicLayoutDefinition : ILayoutDefinition
    {
        private readonly IFormEntityRepository _formEntityRepository;
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
        public BasicLayoutDefinition(IFormEntityRepository formEntityRepository, IJsonUtility jsonUtility)
        {
            _formEntityRepository = formEntityRepository;
            _jsonUtility = jsonUtility;
        }

        /// <inheritdoc />
        public ILayout CreateLayout(PersistedLayout entity)
        {
            var config = _jsonUtility.Deserialize<BasicLayoutConfiguration>(entity.Data);

            if (config is null)
            {
                return default;
            }

            if (config.AutoPopulate == false)
            {
                return new BasicLayout(entity, config);
            }

            var formId = entity.ParentId();

            if (formId.HasValue == false)
            {
                return default;
            }

            var form = _formEntityRepository.Get(formId.Value);

            if (form is null)
            {
                return default;
            }

            var autoPopulatedConfig = new BasicLayoutConfiguration()
            {
                AutoPopulate = true,
                Rows = new[] 
                {
                    new BasicLayoutRow()
                    {
                        Cells = new [] 
                        { 
                            new BasicLayoutCell()
                            {
                                ColumnSpan = 12,
                                Fields = form.Fields.Select(x=> new BasicLayoutField()
                                {
                                    Id = x.Id,
                                }).ToArray(),
                            }
                        },
                        IsStep = false
                    }
                }
            };


            return new BasicLayout(entity, autoPopulatedConfig);
        }

        /// <inheritdoc />
        public object GetBackOfficeConfiguration(PersistedLayout entity)
        {
            var existingConfig = _jsonUtility.Deserialize<BasicLayoutConfiguration>(entity.Data);

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
