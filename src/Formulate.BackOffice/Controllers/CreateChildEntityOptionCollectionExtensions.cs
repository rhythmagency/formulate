namespace Formulate.BackOffice.Controllers
{
    using System.Collections.Generic;
    using Formulate.BackOffice.Persistence;

    internal static class CreateChildEntityOptionCollectionExtensions
    {
        public static void AddFormOption(this List<CreateChildEntityOption> items)
        {
            items.Add(new CreateChildEntityOption()
            {
                EntityType = EntityTypes.Form,
                Name = "Form",
                Icon = Constants.Icons.Entities.Form
            });
        }

        public static void AddConfiguredFormOption(this List<CreateChildEntityOption> items)
        {
            items.Add(
                new CreateChildEntityOption()
                {
                    EntityType = EntityTypes.ConfiguredForm,
                    Name = "Configured Form",
                    Icon = Constants.Icons.Entities.ConfiguredForm,
                });
        }
        
        public static void AddFolderOption(this List<CreateChildEntityOption> items, string icon = Constants.Icons.Folders.Default)
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
