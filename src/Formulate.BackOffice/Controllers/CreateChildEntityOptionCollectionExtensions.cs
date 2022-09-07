namespace Formulate.BackOffice.Controllers
{
    using System.Collections.Generic;

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

        public static void AddLayoutOption(this List<CreateChildEntityOption> items)
        {
            items.Add(
                new CreateChildEntityOption()
                {
                    EntityType = EntityTypes.Layout,
                    Name = "Layout",
                    Icon = Constants.Icons.Entities.Layout,
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
