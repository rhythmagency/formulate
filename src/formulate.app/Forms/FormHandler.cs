namespace formulate.app.Forms
{

    // Namespaces.
    using CollectionBuilders;
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using System.Linq;
    using Umbraco.Core;
    using Current = Umbraco.Web.Composing.Current;

    /// <summary>
    /// Stores information about a form handler.
    /// </summary>
    public sealed class FormHandler : IFormHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FormHandler"/> class.
        /// </summary>
        /// <param name="handler">
        /// The handler.
        /// </param>
        public FormHandler(IFormHandlerType handler)
        {
            Handler = handler;
        }

        #region Public Properties

        /// <summary>
        /// Gets or sets the unique ID of the handler.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the alias of the handler.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Gets or sets the name of the handler.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is the handler enabled?
        /// </summary>
        [DefaultValue(true)]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Populate)]
        public bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets the configuration data stored by the handler.
        /// </summary>
        public string HandlerConfiguration { get; set; }

        /// <summary>
        /// Gets or sets the ID of the handler type.
        /// </summary>
        public Guid TypeId
        {
            get
            {
                return Handler.TypeId;
            }
            set
            {
            }
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets or sets the form handler.
        /// </summary>
        private IFormHandlerType Handler { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the directive to use for this form handler.
        /// </summary>
        /// <returns>The directive.</returns>
        public string GetDirective()
        {
            return Handler.Directive;
        }

        /// <summary>
        /// Gets the type label to use for this form handler.
        /// </summary>
        /// <returns>The type label.</returns>
        public string GetTypeLabel()
        {
            return Handler.TypeLabel;
        }

        /// <summary>
        /// Gets the icon to use for this form handler.
        /// </summary>
        /// <returns>The icon.</returns>
        public string GetIcon()
        {
            return Handler.Icon;
        }

        /// <summary>
        /// Returns the type of handler.
        /// </summary>
        /// <returns>
        /// The handler type.
        /// </returns>
        public Type GetHandlerType()
        {
            return Handler.GetType();
        }

        /// <summary>
        /// Deserializes the handler configuration into a .NET object instance.
        /// </summary>
        /// <returns>
        /// The deserialized handler configuration.
        /// </returns>
        public object DeserializeConfiguration()
        {
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
            var copy = MemberwiseClone() as FormHandler;
            var FormHandlerTypes = Current.Factory.GetInstance<FormHandlerTypeCollection>();
            copy.Handler = FormHandlerTypes.FirstOrDefault(x => x.TypeId == Handler.TypeId);
            return copy;
        }

        #endregion
    }
}