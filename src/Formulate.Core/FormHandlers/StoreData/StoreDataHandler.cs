using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formulate.Core.FormHandlers.StoreData
{
    internal class StoreDataHandler : FormHandler
    {
        public StoreDataHandler(IFormHandlerSettings settings) : base(settings)
        {
            Icon = StoreDataDefinition.Constants.Icon;
        }

        override public string Icon { get; set; }

        public override void Handle(object submission)
        {
            //TODO: Implement.
            throw new NotImplementedException();
        }
    }
}
