namespace Formulate.Extensions.StoreData.Utilities
{
    using Formulate.Extensions.StoreData.Models;

    internal sealed class StoreFields : IStoreFields
    {
        public IReadOnlyCollection<StoreDataEntry> Execute(StoreFieldsInput input)
        {
            var entries = new List<StoreDataEntry>();

            var fields = input.Fields;
            var form = input.Form;

            // Normal fields.
            foreach (var kvp in fields)
            {
                var values = kvp.Value.GetValues();
                var formatted = string.Join(", ", values);
                var field = form.Fields.FirstOrDefault(x => x.Id == kvp.Key);

                if (field is null)
                {
                    continue;
                }

                entries.Add(new StoreDataEntry()
                {
                    FieldId = kvp.Key,
                    FieldName = field.Name,
                    Value = formatted
                });
            }

            return entries;
        }
    }
}