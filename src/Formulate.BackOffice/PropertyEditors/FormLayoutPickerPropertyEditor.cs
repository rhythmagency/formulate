namespace Formulate.BackOffice.PropertyEditors
{
    using Formulate.Core.PropertyEditors;
    using Umbraco.Cms.Core.PropertyEditors;

    [DataEditor(FormLayoutPickerPropertyValueConverter.PropertyEditorAlias,
    "Formulate Form Layout Picker",
    $"{Constants.Package.PluginPath}/property-editors/form-layout-picker/form-layout-picker.html",
    ValueType = ValueTypes.Json,
    Group = Umbraco.Cms.Core.Constants.PropertyEditors.Groups.Pickers)]
    public sealed class FormLayoutPickerPropertyEditor : DataEditor
    {
        public FormLayoutPickerPropertyEditor(IDataValueEditorFactory dataValueEditorFactory, EditorType type = EditorType.PropertyValue) : base(dataValueEditorFactory, type)
        {
        }
    }
}
