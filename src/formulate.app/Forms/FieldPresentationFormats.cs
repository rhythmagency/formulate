namespace formulate.app.Forms
{
    /// <summary>
    /// Values used to determine how the current form field should be formatted.
    /// </summary>
    public enum FieldPresentationFormats
    {
        /// <summary>
        /// The default option.
        /// </summary>
        Unspecified,

        /// <summary>
        /// Detemines this field should be formatted for email messages.
        /// </summary>
        Email,

        /// <summary>
        /// Detemines this field should be formatted for storage (e.g. database or file).
        /// </summary>
        Storage,

        /// <summary>
        /// Determines this field should be formatted for transmission.
        /// </summary>
        Transmission
    }
}
