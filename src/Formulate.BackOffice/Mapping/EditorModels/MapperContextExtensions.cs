namespace Formulate.BackOffice.Mapping.EditorModels
{
    using Umbraco.Cms.Core.Mapping;

    internal static class MapperContextExtensions
    {
        public static void SetIsNew(this MapperContext mapperContext, bool value)
        {
            mapperContext.Items.Add("isNew", value);
        }

        public static bool IsNew(this MapperContext mapperContext)
        {
            if (mapperContext.Items.TryGetValue("isNew", out var value) && value is bool isNew)
            {
                return isNew;
            }

            return false;
        }
    }
}
