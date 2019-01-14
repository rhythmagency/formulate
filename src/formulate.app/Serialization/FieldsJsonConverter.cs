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

            // Get field type.
            var types = ReflectionHelper
                .InstantiateInterfaceImplementations<IFormFieldType>();
            var type = types.FirstOrDefault(x => x.TypeId == typeId);
            if (type != null)
            {

                // Create instance of form field.
                var fieldType = type.GetType();
                var genericType = typeof(FormField<>);
                var fullType = genericType.MakeGenericType(fieldType);
                var instance = Activator.CreateInstance(fullType);
                var casted = instance as IFormField;
                return casted;

            }


            // Could not instantiate a form field.
            return null;

        }

        #endregion

    }

}