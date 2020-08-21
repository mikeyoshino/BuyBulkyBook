using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BuyBulkyBook.DataAccess.Repository;
using BuyBulkyBook.DataAccess.Repository.IRepository;
using BuyBulkyBook.Models;
using BuyBulkyBook.Models.ViewModels;
using BuyBulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace BuyBulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public OrderDetailsVM orderVM { get; set; }
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            orderVM = new OrderDetailsVM()
            {
                OrderHeader = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id,
                                                        includeProperties: "ApplicationUser"),
                OrderDetails = _unitOfWork.OrderDetails.GetAll(o => o.OrderId == id, includeProperties: "Product")


            };

            return View(orderVM);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetOrderList(string status)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);

            IEnumerable<OrderHeader> orderHeaderList;
            if(User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee))
            {
                orderHeaderList = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser");
            }
            else
            {
                orderHeaderList = _unitOfWork.OrderHeader.GetAll(
                                    u => u.ApplicationUserId == claims.Value,
                                    includeProperties: "ApplicationUser");

            }

            switch (status)
            {
                case "pending":
                    orderHeaderList = orderHeaderList.Where(o => o.PaymentStatus == SD.PaymentStatusDelayedPayment 
                                                      || o.PaymentStatus == SD.PaymentStatusPending
                                                      || o.PaymentStatus == SD.StatusPending);
                    break;
                case "completed":
                    orderHeaderList = orderHeaderList.Where(o => o.OrderStatus == SD.StatusShipped);
                    break;
                case "rejected":
                    orderHeaderList = orderHeaderList.Where(o => o.OrderStatus == SD.StatusCancelled
                                                      || o.OrderStatus == SD.StatusRefunded
                                                      || o.OrderStatus == SD.PaymentStatusRejected);
                    break;
                case "inprocess":
                    orderHeaderList = orderHeaderList.Where(o => o.OrderStatus == SD.StatusApproved
                                                      || o.OrderStatus == SD.StatusInProcess
                                                      || o.OrderStatus == SD.StatusInProcess);
                    break;
                default:
                    break;


            }



            return Json(new { data = orderHeaderList });


        }
        #endregion
    }
}
