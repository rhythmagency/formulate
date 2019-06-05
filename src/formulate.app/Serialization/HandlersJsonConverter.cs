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
    using Current = Umbraco.Web.Composing.Current;

    /// <summary>
    /// Handles conversion of JSON to IFormHandler[].
    /// </summary>
    /// <remarks>
    /// This conversion is necessary to instantiate concrete instances
    /// of the IFormHandler interface. By avoiding embedding the full
    /// type name in the JSON, we can refactor names of classes without
    /// preventing deserialization later.
    /// </remarks>
    public class HandlersJsonConverter : JsonConverter
    {
        public HandlersJsonConverter()
        {
            // TODO: Find a way to resolve this without using Current.
            // Get form handler types.
            FormHandlerTypes = Current.Factory.GetInstance<FormHandlerTypeCollection>();
        }

        #region Public Methods

        /// <summary>
        /// Indicates whether or not this class can convert an object
        /// of the specified type.
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(IFormHandler[]);
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
        /// Deserializes JSON into an array of IFormHandler.
        /// </summary>
        /// <returns>
        /// An array of IFormHandler.
        /// </returns>
        public override object ReadJson(JsonReader reader,
            Type objectType, object existingValue,
            JsonSerializer serializer)
        {

            // Variables.
            var handlers = new List<IFormHandler>();
            var jsonArray = JArray.Load(reader);


            // Deserialize each form handler.
            foreach (var item in jsonArray)
            {

                // Create a form handler instance by the handler type ID.
                var jsonObject = item as JObject;
                var strTypeId = jsonObject["TypeId"].Value<string>();
                var typeId = Guid.Parse(strTypeId);
                var instance = InstantiateHandlerByTypeId(typeId);


                // Populate the form handler instance.
                serializer.Populate(jsonObject.CreateReader(), instance);
                handlers.Add(instance);

            }


            // Return array of form handlers.
            return handlers.ToArray();

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
        /// Creates a new instance of a form handler by the handler's
        /// type ID.
        /// </summary>
        /// <param name="typeId">
        /// The form handler type ID.
        /// </param>
        /// <returns>
        /// An instance of a form handler.
        /// </returns>
        private IFormHandler InstantiateHandlerByTypeId(Guid typeId)
        {
            var handler = FormHandlerTypes?.FirstOrDefault(x => x.TypeId == typeId);
            return new FormHandler(handler);
        }

        #endregion
        private FormHandlerTypeCollection FormHandlerTypes { get; set; }
    }
}