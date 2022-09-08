namespace Formulate.BackOffice.Definitions.Forms
{
    using Formulate.Core.Forms;
    using Formulate.Core.Layouts;
    using Formulate.Core.Layouts.Basic;
    using Formulate.Core.Utilities;
    using System;

    public sealed class FormWithBasicLayoutDefinition : FormDefinition
    {

        public override Guid KindId => new Guid("3A11BD580AC643D78F632F88E7C132AF");

        public override string Name => "Form with Basic Layout";

        public override string Description => "Creates a form with a preconfigured form layout. This can be changed later on.";
    }
}
