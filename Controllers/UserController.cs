using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Taste.DataAccess.Data.Repository.IRepositry;

namespace Treat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        public IActionResult Get()
        {
            var data= Json(new { data=_unitOfWork.ApplicationUser.GetAll()});
            return data;
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var _objFromDb = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == id);

            if(_objFromDb == null)
            {
                return Json(new {success=false,message="Error while Locking/Unlocking" });
            }
            if(_objFromDb.LockoutEnd != null && _objFromDb.LockoutEnd>DateTime.Now)
            {
                _objFromDb.LockoutEnd = DateTime.Now;

            }
            else
            {
                _objFromDb.LockoutEnd = DateTime.Now.AddYears(100);
            }
            
            _unitOfWork.Save();
            return Json(new { success = true, message = "Operation Successful." });
        }

    }
}