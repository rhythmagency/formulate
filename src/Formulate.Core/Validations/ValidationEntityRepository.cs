﻿using Formulate.Core.Persistence;

namespace Formulate.Core.Validations
{
    /// <summary>
    /// The default implementation of <see cref="IValidationEntityRepository"/>.
    /// </summary>
    internal sealed class ValidationEntityRepository : EntityRepository<PersistedValidation>, IValidationEntityRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationEntityRepository"/> class.
        /// </summary>
        /// <inheritdoc />
        public ValidationEntityRepository(IRepositoryUtilityFactory repositoryHelperFactory) : base(repositoryHelperFactory)
        {
        }
    }
}