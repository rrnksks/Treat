using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taste.DataAccess.Data.Repository.IRepositry;
using Taste.Utility;

namespace Treat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        //just checking
        [HttpGet]
        public IActionResult Get()
        {
            var data= Json(new { data=_unitOfWork.Category.GetAll()});
            return data;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var _objFromDb = _unitOfWork.Category.GetFirstOrDefault(u => u.Id == id);

            if(_objFromDb == null)
            {
                return Json(new {success=false,message="Error while deleting" });
            }
            _unitOfWork.Category.Remove(_objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Deleted Successfully" });
        }

    }
}