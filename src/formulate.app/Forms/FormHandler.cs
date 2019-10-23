namespace formulate.app.Forms
{

    // Namespaces.
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;


    /// <summary>
    /// Stores information about a form handler.
    /// </summary>
    /// <typeparam name="T">The type of data stored by this form handler.</typeparam>
    public class FormHandler<T> : IFormHandler
        where T : IFormHandlerType,
        new()
    {

        #region Private Properties

        /// <summary>
        /// The form handler.
        /// </summary>
        private T Handler { get; set; }

        #endregion


        #region Public Properties

        /// <summary>
        /// The unique ID of the handler.
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        /// The alias of the handler.
        /// </summary>
        public string Alias { get; set; }


        /// <summary>
        /// The name of the handler.
        /// </summary>
        public string Name { get; set; }


        /// <summary>
        /// Is the handler enabled?
        /// </summary>
        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Enabled { get; set; }


        /// <summary>
        /// The configuration data stored by the handler.
        /// </summary>
        public string HandlerConfiguration { get; set; }


        /// <summary>
        /// The ID of the handler type.
        /// </summary>
        public Guid TypeId
        {
            get
            {
                var instance = new T();
                return instance.TypeId;
            }
            set
            {
            }
        }

        #endregion


        #region Methods

        /// <summary>
        /// Gets the directive to use for this form handler.
        /// </summary>
        /// <returns>The directive.</returns>
        public string GetDirective()
        {
            var instance = new T();
            return instance.Directive;
        }


        /// <summary>
        /// Gets the type label to use for this form handler.
        /// </summary>
        /// <returns>The type label.</returns>
        public string GetTypeLabel()
        {
            var instance = new T();
            return instance.TypeLabel;
        }


        /// <summary>
        /// Gets the icon to use for this form handler.
        /// </summary>
        /// <returns>The icon.</returns>
        public string GetIcon()
        {
            var instance = new T();
            return instance.Icon;
        }


        /// <summary>
        /// Returns the type of handler.
        /// </summary>
        /// <returns>
        /// The handler type.
        /// </returns>
        public Type GetHandlerType()
        {
            return typeof(T);
        }


        /// <summary>
        /// Deserializes the handler configuration into a .NET object instance.
        /// </summary>
        /// <returns>
        /// The deserialized handler configuration.
        /// </returns>
        public object DeserializeConfiguration()
        {
            if (Handler == null)
            {
                Handler = new T();
            }
            return Handler.DeserializeConfiguration(HandlerConfiguration);
        }


        /// <summary>
        /// Prepares to handle a form submission.
        /// </summary>
        /// <param name="context">
        /// The form submission context.
        /// </param>
        public void PrepareHandleForm(FormSubmissionContext context)
        {
            var config = DeserializeConfiguration();
            if (Handler == null)
            {
                Handler = new T();
            }
            Handler.PrepareHandleForm(context, config);
        }


        /// <summary>
        /// Handles a form submission.
        /// </summary>
        /// <param name="context">
        /// The form submission context.
        /// </param>
        public void HandleForm(FormSubmissionContext context)
        {
            var config = DeserializeConfiguration();
            if (Handler == null)
            {
                Handler = new T();
            }
            Handler.HandleForm(context, config);
        }

        /// <summary>
        /// Creates a new copy of this form handler. Similar to cloning, but some
        /// properties are not copied to avoid sharing instance data.
        /// </summary>
        /// <returns>
        /// The fresh copy of this form handler.
        /// </returns>
        public IFormHandler GetFreshCopy()
        {
            var copy = MemberwiseClone() as FormHandler<T>;
            copy.Handler = default(T);
            return copy;
        }

        #endregion

    }

}