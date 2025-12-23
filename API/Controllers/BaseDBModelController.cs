using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public abstract class BaseDBModelController<T> : ControllerBase where T : class
    {
        public BaseDBModelController() { }


        [HttpGet]
        public ActionResult<List<T>> GetAll() 
        {
            var props = DBExample.GetDB().GetType().GetProperties();
            return Ok();
        }
    }
}
