namespace formulate.app.Forms
{

    // Namespaces.
    using core.Types;
    using System;
    using System.Collections.Generic;


    /// <summary>
    /// Stores information about a form handler.
    /// </summary>
    /// <typeparam name="T">The type of data stored by this form handler.</typeparam>
    public class FormHandler<T> : IFormHandler
        where T : IFormHandlerType,
        new()
    {

        #region Properties

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
            var instance = new T();
            return instance.DeserializeConfiguration(HandlerConfiguration);
        }


        /// <summary>
        /// Handles a form submission.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <param name="data">The form data.</param>
        /// <param name="files">The file data.</param>
        public void HandleForm(Form form, IEnumerable<FieldSubmission> data,
            IEnumerable<FileFieldSubmission> files)
        {
            var config = DeserializeConfiguration();
            var instance = new T();
            instance.HandleForm(form, data, files, config);
        }

        #endregion

    }

}