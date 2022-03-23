using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Tuan4_NguyenTruongTho.Models;

namespace Tuan4_NguyenTruongTho.Controllers
{
    public class NguoiDungController : Controller
    {
        // GET: NguoiDung
        MyDataDataContext data = new MyDataDataContext();
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(System.Web.Mvc.FormCollection collection, KhachHang kh)
        {
            try
            {
                var hoten = collection["hoten"];
                var tendangnhap = collection["tendangnhap"];
                var matkhau = collection["matkhau"];
                var MatKhauXacNhan = collection["MatKhauXacNhan"];
                var email = collection["email"];
                var diachi = collection["diachi"];
                var dienthoai = collection["dienthoai"];
                var ngaysinh = String.Format("{0:MM/dd/yyyy}", collection["ngaysinh"]);
                if (String.IsNullOrEmpty(MatKhauXacNhan))
                {
                    ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";
                }
                else
                {
                    if (!matkhau.Equals(MatKhauXacNhan))
                    {
                        ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khẩu xác nhận phải giống nhau";
                    }
                    else
                    {
                        kh.hoten = hoten;
                        kh.tendangnhap = tendangnhap;
                        kh.matkhau = matkhau;
                        kh.email = email;
                        kh.diachi = diachi;
                        kh.dienthoai = dienthoai;
                        DateTime temp;
                        bool flag = DateTime.TryParse(ngaysinh, out temp);
                        if (flag == true)
                            kh.ngaysinh = temp;
                        data.KhachHangs.InsertOnSubmit(kh);
                        data.SubmitChanges();
                        return RedirectToAction("DangNhap");
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);

            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(System.Web.Mvc.FormCollection collection)
        {
            var tendangnhap = collection["tendangnhap"];
            var matkhau = collection["matkhau"];
            KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.tendangnhap == tendangnhap && n.matkhau == matkhau);
            if (kh != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["TaiKhoan"] = kh;
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            return RedirectToAction("Index", "Home");
        }




    } 
}
