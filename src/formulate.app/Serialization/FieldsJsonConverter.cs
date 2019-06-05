namespace formulate.app.Serialization
{

    // Namepaces.
    using Forms;
    using Helpers;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using formulate.app.CollectionBuilders;

    using Umbraco.Core;
    using Umbraco.Core.Composing;

    using Current = Umbraco.Web.Composing.Current;

    /// <summary>
    /// Handles conversion of JSON to IFormField[].
    /// </summary>
    /// <remarks>
    /// This conversion is necessary to instantiate concrete instances
    /// of the IFormField interface. By avoiding embedding the full
    /// type name in the JSON, we can refactor names of classes without
    /// preventing deserialization later.
    /// </remarks>
    public class FieldsJsonConverter : JsonConverter
    {
        public FieldsJsonConverter()
        {
            // TODO: Find a way to resolve this without using Current.
            // Get field types.
            FieldTypes = Current.Factory.GetInstance<FormFieldTypeCollection>();
        }

        #region Public Methods

        /// <summary>
        /// Indicates whether or not this class can convert an object
        /// of the specified type.
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IFormField[]);
        }


        /// <summary>
        /// This class does not handle serialization.
        /// </summary>
        public override bool CanWrite => false;


        /// <summary>
        /// This class does handle deserialization.
        /// </summary>
        public override bool CanRead => true;


        /// <summary>
        /// Deserializes JSON into an array of IFormField.
        /// </summary>
        /// <returns>
        /// An array of IFormField.
        /// </returns>
        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            // Variables.
            var fields = new List<IFormField>();
            var jsonArray = JArray.Load(reader);


            // Deserialize each form field.
            foreach (var item in jsonArray)
            {

                // Create a form field instance by the field type ID.
                var jsonObject = item as JObject;
                var strTypeId = jsonObject["TypeId"].Value<string>();
                var typeId = Guid.Parse(strTypeId);
                var instance = InstantiateFieldByTypeId(typeId);

                // Populate the form field instance.
                if (instance != null)
                {
                    serializer.Populate(jsonObject.CreateReader(), instance);
                    fields.Add(instance);
                }
                else
                {
                    //TODO: Add logging to indicate that field type is unknown.
                }

            }


            // Return array of form fields.
            return fields.ToArray();

        }


        /// <summary>
        /// This does nothing (it must be implemented because it is
        /// abstract in the base class).
        /// </summary>
        public override void WriteJson(JsonWriter writer,
            object value, JsonSerializer serializer)
        {
            throw new InvalidOperationException(
                "This class is not intended to be used for serialization.");
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// Creates a new instance of a form field by the field's
        /// type ID.
        /// </summary>
        /// <param name="typeId">
        /// The form field type ID.
        /// </param>
        /// <returns>
        /// An instance of a form field.
        /// </returns>
        private IFormField InstantiateFieldByTypeId(Guid typeId)
        {
            var fieldType = FieldTypes?.FirstOrDefault(x => x.TypeId == typeId);

            if (fieldType == null)
            {
                return null;
            }

            return new FormField(fieldType);
        }

        #endregion

        private FormFieldTypeCollection FieldTypes { get; set; }
    }
}