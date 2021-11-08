using Microsoft.AspNetCore.Mvc;

namespace Formulate.BackOffice.Controllers
{
    public abstract partial class FormulateBackOfficeEntityApiController
    {
        [NonAction]
        public IActionResult Delete()
        {
            return new EmptyResult();
        }

        [NonAction]
        public IActionResult Get()
        {
            return new EmptyResult();
        }

        [NonAction]
        public IActionResult GetScaffolding()
        {
            return new EmptyResult();
        }

        [NonAction]
        public IActionResult GetCreateOptions()
        {
            return new EmptyResult();
        }

        [NonAction]
        public IActionResult Move()
        {
            return new EmptyResult();
        }

        [NonAction]
        public IActionResult Save()
        {
            return new EmptyResult();
        }
    }
}
