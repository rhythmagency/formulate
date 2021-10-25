namespace Formulate.Core.Layouts.Basic
{
    /// <summary>
    /// A basic layout.
    /// </summary>
    public sealed class BasicLayout : Layout<BasicLayoutConfiguration>
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicLayout"/> class.
        /// </summary>
        public BasicLayout(ILayoutSettings settings, BasicLayoutConfiguration configuration) : base(settings, configuration)
        {
        }
    }
}
