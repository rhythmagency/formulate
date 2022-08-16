namespace Formulate.BackOffice.Controllers
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public sealed class CreateItemOption
    {
        [DataMember(Name = "icon")]
        public string Icon { get; set; }

        [DataMember(Name = "kindId")]
        public Guid? KindId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "category")]
        public string Category { get; set; }
    }
}