using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KTM_ASP.Models
{
    public class Interiors
    {
        public int Id { get; set; }

        [Display(Name = "Tên Sản Phẩm")]
        public string? TenSanPham { get; set; }

        [Display(Name = "Mô Tả")]
        public string? MieuTa { get; set; }

        [Display(Name = "Số Lượng")]
        public int SoLuong { get; set; }

        [Display(Name = "Giá")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Gia { get; set; }

        [Display(Name = "Chất Liệu")]
        public string? ChatLieu { get; set; }

        [Display(Name = "Màu Sắc")]
        public string? MauSac { get; set; }

        [Display(Name = "Hình Ảnh")]
        public string? HinhAnh { get; set; }

        [NotMapped]
        [Display(Name = "Tải Ảnh Lên")]
        public IFormFile? AnhFile { get; set; }

        [Display(Name = "Tim")]
        public bool IsFavorite { get; set; }
    }
}
