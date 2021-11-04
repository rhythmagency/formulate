using System;
using System.Collections.Generic;
using Formulate.Core.Persistence;
using System.Linq;
using Formulate.BackOffice.Persistence;
using Formulate.Core.ConfiguredForms;
using Formulate.Core.DataValues;
using Formulate.Core.Folders;
using Formulate.Core.Forms;
using Formulate.Core.Layouts;
using Formulate.Core.Validations;
using Microsoft.AspNetCore.Http.Features;

namespace Formulate.BackOffice.Trees
{
    public static class PersistedEntityExtensions
    {
        public static string TreeSafePathString(this IPersistedEntity entity)
        {
            return string.Join(",", entity.TreeSafePath());
        }

        public static string[] TreeSafePath(this IPersistedEntity entity)
        {
            var ids = new List<string> { "-1" };

            if (entity is null || entity.Path.Length < 2)
            {
                return ids.ToArray();
            }

            ids.AddRange(entity.Path.Skip(1).Select(x => x.ToString("N")).ToArray());

            return ids.ToArray();
        }

        public static string BackOfficeSafeId(this IPersistedEntity entity)
        {
            if (entity is null)
            {
                return string.Empty;
            }

            return entity.Id.ToString("N");
        }

        public static EntityTypes EntityType(this IPersistedEntity entity)
        {
            return entity switch
            {
                PersistedConfiguredForm => EntityTypes.ConfiguredForm,
                PersistedDataValues => EntityTypes.DataValues,
                PersistedFolder => EntityTypes.Folder,
                PersistedForm => EntityTypes.Form,
                PersistedLayout => EntityTypes.Layout,
                PersistedValidation => EntityTypes.Validation,
                _ => throw new NotSupportedException($"Entity type {entity.GetType()} is not supported")
            };
        }
    }
}
