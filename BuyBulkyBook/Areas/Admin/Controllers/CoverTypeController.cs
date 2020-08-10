using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyBulkyBook.DataAccess.Repository.IRepository;
using BuyBulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using BuyBulkyBook.Utility;
using Dapper;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {

            CoverType coverType = new CoverType();
            if (id == null)
            {
                //This for create
                return View(coverType);
            }

            //This for edit
            var paraemter = new DynamicParameters();
            paraemter.Add("@Id", id);
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, paraemter);

            if (coverType == null)
            {
                return NotFound();
            }

            return View(coverType);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            var paraemter = new DynamicParameters();
            paraemter.Add("@Name", coverType.Name);

            if (ModelState.IsValid)
            {
                if (coverType.Id == 0)
                {
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create, paraemter);
                }
                else
                {
                    paraemter.Add("@Id", coverType.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, paraemter);
                }
                _unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //passing ID vaule to store procedure.
            var paraemter = new DynamicParameters();
            paraemter.Add("@Id", id);

            //retrive one record from database via store procedure tpye of Cover Type.
            var objFromDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, paraemter);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "เกิดข้อผิดพลาดระหว่างการลบ" });
            }
            _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete, paraemter);
            _unitOfWork.Save();

            return Json(new { success = true, message = "ดำเนินการลบสำเร็จ" });

        }


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverType_GetAll, null);
            return Json(new { data = allObj });
        }
        #endregion
    }
}
