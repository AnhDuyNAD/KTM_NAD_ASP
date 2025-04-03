using System.Diagnostics;
using KTM_ASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace KTM_ASP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var products = new List<Product>
            {
                new Product { Name = "ĐÈN NGƯỜI ÔM BÓNG ĐÈN", Price = 15000000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/1-5a1c9c12-8f2a-4ce3-9a8e-3d2872d3a2d6.jpg?v=1701320547190", Category = "Hiện đại" },
                new Product { Name = "GHẾ DA ARMCHAIR OAKWAY", Price = 6190000, OldPrice = 12380000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/frame-1000002981.jpg?v=1699895731600",Discount = "-10%", Category = "Hiện đại" },
                new Product { Name = "ĐÈN TRANG TRÍ LEFT HEAT", Price = 4950000, OldPrice = 5500000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/1-5fd67d62-06e6-4398-a5f4-9ae167e37dc6.jpg?v=1701320294470",Discount = "-50%", Category = "Hiện đại"},
                new Product { Name = "ĐÈN CHÙM ROSEMAS PHA LÊ", Price = 33050000, OldPrice = 36530000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/anh.jpg?v=1699895673857",Discount = "-10%", Category = "Hiện đại"},
                new Product { Name = "Đèn CHÙM THẢ ROYAL CRYSTAL", Price = 33050000, OldPrice = 36530000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/1-bd1b2a6b-d406-4e16-97d7-558f9acfeaeb.jpg?v=1701326274490", Category = "Cổ điển" },
                new Product { Name = "ĐÈN NGỦ ĐỂ BÀN", Price = 270000, OldPrice = 300000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/1-6651e4fa-f402-4bd9-9670-2bb4b79a9952.jpg?v=1701320018010",Discount = "-10%", Category = "Cổ điển" },
                new Product { Name = "SOFA VĂNG BỌC DA BÒ NHẬP KHẨU", Price = 15000000, OldPrice = 22000000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/1-d660324d-dd5d-4280-b849-ae16b2ccc1df.jpg?v=1701278408660",Discount = "-32%", Category = "Cổ điển" },
                new Product { Name = "MẪU GHẾ SOFA VĂNG VẢI CAO CẤP", Price = 6600000, OldPrice = 9000000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/1-5198a174-8af9-4f38-b1a4-565aff5dfbc9.jpg?v=1701277792313",Discount = "-27%", Category = "Cổ điển" },
                new Product { Name = "KỆ ĐỂ MÁY TÍNH - KỆ ĐỂ LAPTOP", Price = 40000, OldPrice = 0, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/1-64539de7-7cf2-4d3b-b06a-99914e8153bc.jpg?v=1712252785507", Category = "Đơn giản" },
                new Product { Name = "BỘ 4 LÓT LY GỐM TRÒN TICK", Price = 250000, OldPrice = 300000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/20.jpg?v=1712229205140",Discount = "-17%", Category = "Đơn giản" },
                new Product { Name = "ĐÈN NGỦ ĐỂ BÀN THÂN GỖ", Price = 150000, OldPrice = 250000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/15.jpg?v=1712228644447",Discount = "-40%", Category = "Đơn giản" },
                new Product { Name = "ĐÈN NGỦ LED ĐỂ BÀN", Price = 800000, OldPrice = 1300000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/9.jpg?v=1712228308703",Discount = "-38%", Category = "Đơn giản" },
                new Product { Name = "ĐỒNG HỒ TREO TƯỜNG CUCKOO", Price = 250000, OldPrice = 300000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/35.jpg?v=1712254885227",Discount = "-17%", Category = "Sang trọng" },
                new Product { Name = "GIÁ ĐỂ RƯỢU THẦN MÈO AI CẬP", Price = 300000, OldPrice = 0, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/30.jpg?v=1712254258810", Category = "Sang trọng" },
                new Product { Name = "BÚP BÊ TRANG TRÍ GNOME", Price = 80000, OldPrice = 0, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/24-b7ac8253-9c11-4f9a-aedb-2923e17d0710.jpg?v=1712253768743", Category = "Sang trọng" },
                new Product { Name = "KOZEEY SANG TRỌNG THỎ PHỤC SINH", Price = 200000, OldPrice = 250000, ImageUrl = "https://bizweb.dktcdn.net/thumb/large/100/501/740/products/19-f1a1f3f4-75c8-4d69-b278-a6b686da853b.jpg?v=1712253620637",Discount = "-20%", Category = "Sang trọng" }
            };

            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
