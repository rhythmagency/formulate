namespace formulate.app.Forms
{

    // Namespaces.
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A contract for creating all form fields.
    /// </summary>
    public interface IFormField
    {
        /// <summary>
        /// Gets or sets the type id.
        /// </summary>
        Guid TypeId { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the alias.
        /// </summary>
        string Alias { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        string Label { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        string Category { get; set; }

        /// <summary>
        /// Gets or sets the field configuration.
        /// </summary>
        string FieldConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the validations.
        /// </summary>
        Guid[] Validations { get; set; }

        /// <summary>
        /// Gets or sets the meta info.
        /// </summary>
        IFormFieldMetaInfo[] MetaInfo { get; set; }

        /// <summary>
        /// Gets a value indicating whether is transitory.
        /// </summary>
        bool IsTransitory { get; }

        /// <summary>
        /// Gets a value indicating whether is server side only.
        /// </summary>
        bool IsServerSideOnly { get; }

        /// <summary>
        /// Gets a value indicating whether is hidden.
        /// </summary>
        bool IsHidden { get; }

        /// <summary>
        /// Gets a value indicating whether is stored.
        /// </summary>
        bool IsStored { get; }

        /// <summary>
        /// Gets the icon to show in the editor UI.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/>.
        /// </returns>
        string GetIcon();

        /// <summary>
        /// Gets the AngularJS directive for this field type.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/>.
        /// </returns>
        string GetDirective();

        /// <summary>
        /// Gets the label to show in the edtior UI.
        /// </summary>
        /// <returns>
        /// A <see cref="string"/>.
        /// </returns>
        string GetTypeLabel();

        /// <summary>
        /// Gets field type.
        /// </summary>
        /// <returns>
        /// A <see cref="Type"/>.
        /// </returns>
        Type GetFieldType();

        /// <summary>
        /// Deserializes the serialized configuration.
        /// </summary>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        object DeserializeConfiguration();

        /// <summary>
        /// Formats the value.
        /// </summary>
        /// <param name="values">
        /// The values.
        /// </param>
        /// <param name="format">
        /// The format.
        /// </param>
        /// <returns>
        /// A <see cref="string"/>.
        /// </returns>
        string FormatValue(IEnumerable<string> values, FieldPresentationFormats format);

        /// <summary>
        /// Is the field value valid?
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool IsValid(IEnumerable<string> value);

        /// <summary>
        /// Returns a validation message that is shown for fields that have validation
        /// built in (i.e., rather than relying on the messages specific to validations
        /// that can be attached to any field).
        /// </summary>
        /// <returns>
        /// A <see cref="string"/>.
        /// </returns>
        string GetNativeFieldValidationMessage();
    }
}
