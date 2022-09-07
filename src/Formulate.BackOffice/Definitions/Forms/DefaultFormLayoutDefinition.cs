namespace Formulate.BackOffice.Definitions.Forms
{
    using System;

    public sealed class DefaultFormLayoutDefinition : FormDefinition
    {
        public override Guid KindId => new Guid("61244083BD75417BAF77DFA8F8DC2673");

        public override string Name => "Form";

        public override string Description => "Creates a form without a layout. A layout can be added after a form is saved.";

        public override int SortOrder => int.MaxValue;
    }
}
