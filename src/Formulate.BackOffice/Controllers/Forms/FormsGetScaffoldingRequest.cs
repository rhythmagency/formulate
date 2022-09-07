namespace Formulate.BackOffice.Controllers.Forms
{
    using System;
    using System.Runtime.Serialization;

    public sealed class FormsGetScaffoldingRequest : GetScaffoldingRequest
    {
        [DataMember(Name = "kindId")]
        public Guid? KindId { get; set; }
    }
}
