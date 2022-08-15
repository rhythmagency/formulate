namespace Formulate.BackOffice.Controllers
{
    using System.Collections.Generic;
    using Formulate.BackOffice.Persistence;
    using Formulate.BackOffice.Trees;
    using Umbraco.Cms.Core;

    internal static class CreateCreateChildEntityOptionCollectionExtensions
    {
        public static void AddFormOption(this List<CreateChildEntityOption> items)
        {
            items.Add(new CreateChildEntityOption()
            {
                EntityType = EntityTypes.Form,
                Name = "Form",
                Icon = FormulateFormsTreeController.Constants.FormNodeIcon
            });
        }

        public static void AddConfiguredFormOption(this List<CreateChildEntityOption> items)
        {
            items.Add(
                new CreateChildEntityOption()
                {
                    EntityType = EntityTypes.ConfiguredForm,
                    Name = "Configured Form",
                    Icon = FormulateFormsTreeController.Constants.ConfiguredFormNodeIcon,
                });
        }
        
        public static void AddDataValuesFolderOption(this List<CreateChildEntityOption> items)
        {
            items.AddFolderOption(FormulateDataValuesTreeController.Constants.FolderNodeIcon);
        }

        public static void AddFormFolderOption(this List<CreateChildEntityOption> items)
        {
            items.AddFolderOption(FormulateFormsTreeController.Constants.FolderNodeIcon);
        }

        public static void AddLayoutsFolderOption(this List<CreateChildEntityOption> items)
        {
            items.AddFolderOption(FormulateLayoutsTreeController.Constants.FolderNodeIcon);
        }

        public static void AddValidationsFolderOption(this List<CreateChildEntityOption> items)
        {
            items.AddFolderOption(FormulateValidationsTreeController.Constants.FolderNodeIcon);
        }

        public static void AddFolderOption(this List<CreateChildEntityOption> items, string icon = Constants.Icons.Folder)
        {
            items.Add(new CreateChildEntityOption()
            {
                EntityType = EntityTypes.Folder,
                Name = "Folder",
                Icon = icon
            });
        }
    }
}
