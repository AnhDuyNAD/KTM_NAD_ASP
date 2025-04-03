using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace KTM_ASP.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        // Danh sách lưu tài khoản tạm thời (có thể thay bằng CSDL)
        private static List<Customer> Customers = new List<Customer>
        {
            new Customer { Username = "user1", Password = "password1", Email = "user1@example.com", Role = "customer" },
            new Customer { Username = "user2", Password = "password2", Email = "user2@example.com", Role = "customer" }
        };

        public AccountController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Kiểm tra nếu là admin
            if (username == "admin" && password == "123456")
            {
                SetSession(username, "admin");
                return RedirectToAction("Index", "Home");
            }

            // Kiểm tra tài khoản khách hàng
            var customer = Customers.FirstOrDefault(c => c.Username == username && c.Password == password);
            if (customer != null)
            {
                SetSession(customer.Username, customer.Role);
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Tài khoản hoặc mật khẩu không đúng.";
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public IActionResult Register(string username, string password, string email)
        {
            if (Customers.Exists(c => c.Username == username))
            {
                ViewBag.ErrorMessage = "Tài khoản đã tồn tại.";
                return View();
            }

            var newCustomer = new Customer
            {
                Username = username,
                Password = password,
                Email = email,
                Role = "customer" // Mặc định khách hàng mới là "customer"
            };

            Customers.Add(newCustomer);
            SetSession(newCustomer.Username, newCustomer.Role);

            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Logout
        public IActionResult Logout()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        // Phương thức tiện ích để lưu thông tin vào Session
        private void SetSession(string username, string role)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            session.SetString("Username", username);
            session.SetString("Role", role);
        }
    }

    // Model Customer (có thể thay bằng Entity trong CSDL)
    public class Customer
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Thêm trường Role để phân quyền
    }
}
