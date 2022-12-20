using Microsoft.AspNetCore.Mvc;
using Domain;
using System;
using BackendUI.Helpers;
using BackendUI.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;
using System.Reflection;
using System.Security.Claims;
using Newtonsoft.Json;
using Application.DTO.Category;

namespace BackendUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string BaseAddress = "";
        public HomeController(IWebHostEnvironment environment, IConfiguration configuration)
        {
            _hostingEnvironment = environment;
            BaseAddress = configuration.GetSection("backendApiAddress").Value;
        }
        //category section
        public async Task<IActionResult> Categories()
        {
            var result = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/Category/GetCategoryList");
            if (result.ResultCode == System.Net.HttpStatusCode.OK)
                return View(JsonConvert.DeserializeObject<List<CategoryListDto>>(result.Content));
            else
                return RedirectToAction("StatusCodes", result.ResultCode);
        }
        public IActionResult AddCategory(Category category)
        {
            //category.NidCategory = Guid.NewGuid();
            //category.CreateDate = DateTime.Now;
            //category.State = 0;
            //if (_categoryAction.Add<Category>(category))
            //    TempData["CategorySuccess"] = "دسته بندی با موفقیت ایجاد گردید";
            //else
            //    TempData["CategoryError"] = "خطا در ایجاد دسته بندی.لطفا مجددا امتحان کنید";
            return RedirectToAction("Categories");
        }
        public IActionResult Category(Guid NidCategory)
        {
            //var category = _categoryAction.GetCategory(NidCategory);
            return View();
        }
        public IActionResult EditType(Guid NidType)
        {
            //var type = _categoryAction.GetType(NidType);
            return View();
        }
        public IActionResult EditBrand(Guid NidBrand)
        {
            //var brand = _categoryAction.GetBrand(NidBrand);
            return View();
        }
        public IActionResult EditCategory(Category category)
        {
            //category.LastModified = DateTime.Now;
            //if (_categoryAction.UpdateCategory(category))
            //    TempData["CategoryPageSuccess"] = "دسته بندی با موفقیت ویرایش گردید";
            //else
            //    TempData["CategoryPageError"] = "خطا در ویرایش دسته بندی.لطفا مجددا امتحان کنید";
            //return RedirectToAction("Category", new { NidCategory = category.NidCategory });
            return View();
        }
        public IActionResult DeleteCategory(Guid NidCategory)
        {
            //var category = _categoryAction.GetCategory(NidCategory, false, false, false);
            //category.State = 2;
            //category.LastModified = DateTime.Now;
            //if (_categoryAction.UpdateCategory(category))
            //    TempData["CategorySuccess"] = "دسته بندی با موفقیت حذف گردید";
            //else
            //    TempData["CategoryError"] = "خطا در حذف دسته بندی.لطفا مجددا امتحان کنید";
            return RedirectToAction("Categories");
        }
        public IActionResult CloseCategory(Guid NidCategory)
        {
            //var category = _categoryAction.GetCategory(NidCategory, false, false, false);
            //category.State = 1;
            //category.LastModified = DateTime.Now;
            //if (_categoryAction.UpdateCategory(category))
            //    TempData["CategorySuccess"] = "دسته بندی با موفقیت غیرفعال گردید";
            //else
            //    TempData["CategoryError"] = "خطا در غیرفعال کردن دسته بندی.لطفا مجددا امتحان کنید";
            return RedirectToAction("Categories");
        }
        public IActionResult AddType(Domain.Type type)
        {
            //type.NidType = Guid.NewGuid();
            //type.State = 0;
            //type.CreateDate = DateTime.Now;
            //if (_categoryAction.Add<Models.Type>(type))
            //    TempData["CategoryPageSuccess"] = "نوع با موفقیت ایجاد گردید";
            //else
            //    TempData["CategoryPageError"] = "خطا در ایجاد نوع.لطفا مجددا امتحان کنید";
            return RedirectToAction("Category", new { NidCategory = type.CategoryId });
        }
        public IActionResult AddBrand(Brand brand)
        {
            //brand.NidBrand = Guid.NewGuid();
            //brand.State = 0;
            //brand.CreateDate = DateTime.Now;
            //if (_categoryAction.Add<Brand>(brand))
            //    TempData["CategoryPageSuccess"] = "برند با موفقیت ایجاد گردید";
            //else
            //    TempData["CategoryPageError"] = "خطا در ایجاد برند.لطفا مجددا امتحان کنید";
            return RedirectToAction("Category", new { NidCategory = brand.CategoryId });
        }
        public IActionResult DeleteType(Guid NidType)
        {
            //var type = _categoryAction.GetType(NidType);
            //type.State = 2;
            //type.LastModified = DateTime.Now;
            //if (_categoryAction.UpdateType(type))
            //    TempData["CategoryPageSuccess"] = "نوع با موفقیت حذف گردید";
            //else
            //    TempData["CategoryPageError"] = "خطا در حذف نوع.لطفا مجددا امتحان کنید";
            //return RedirectToAction("Category", new { NidCategory = type.CategoryId });
            return View();
        }
        public IActionResult SubmitEditType(Domain.Type type)
        {
            //type.LastModified = DateTime.Now;
            //if (_categoryAction.UpdateType(type))
            //    TempData["CategoryPageSuccess"] = "نوع با موفقیت ویرایش گردید";
            //else
            //    TempData["CategoryPageError"] = "خطا در ویرایش نوع.لطفا مجددا امتحان کنید";
            return RedirectToAction("Category", new { NidCategory = type.CategoryId });
        }
        public IActionResult DeleteBrand(Guid NidBrand)
        {
            //var brand = _categoryAction.GetBrand(NidBrand);
            //brand.State = 2;
            //brand.LastModified = DateTime.Now;
            //if (_categoryAction.UpdateBrand(brand))
            //    TempData["CategoryPageSuccess"] = "برند با موفقیت حذف گردید";
            //else
            //    TempData["CategoryPageError"] = "خطا در حذف برند.لطفا مجددا امتحان کنید";
            //return RedirectToAction("Category", new { NidCategory = brand.CategoryId });
            return View();
        }
        public IActionResult SubmitEditBrand(Brand brand)
        {
            //brand.LastModified = DateTime.Now;
            //if (_categoryAction.UpdateBrand(brand))
            //    TempData["CategoryPageSuccess"] = "برند با موفقیت ویرایش گردید";
            //else
            //    TempData["CategoryPageError"] = "خطا در ویرایش برند.لطفا مجددا امتحان کنید";
            return RedirectToAction("Category", new { NidCategory = brand.CategoryId });
        }
        //product section
        public IActionResult Products()
        {
            //ProductViewModel model = new ProductViewModel();
            //model.Products = _productAction.GetProducts();
            //model.Categories = _categoryAction.GetCategories();
            return View();
        }
        public IActionResult AddProduct()
        {
            //ProductViewModel model = new ProductViewModel();
            //model.Categories = _categoryAction.GetCategories(true, true, false);
            return View();
        }
        public IActionResult SubmitAddProduct(Product product)
        {
            //product.CreateDate = DateTime.Now;
            //product.NidProduct = Guid.NewGuid();
            //product.State = 0;
            //product.UserId = Guid.Parse(User.Claims.FirstOrDefault(p => p.Type == "NidUser").Value);
            //if (_productAction.Add<Product>(product))
            //    TempData["ProductSuccess"] = "محصول با موفقیت ایجاد گردید";
            //else
            //    TempData["ProductError"] = "خطا در ایجاد محصول.لطفا مجددا امتحان کنید";
            return RedirectToAction("Products");
        }
        public IActionResult EditProduct(Guid NidProduct)
        {
            //ProductViewModel productViewModel = new ProductViewModel();
            //productViewModel.Product = _productAction.GetProduct(NidProduct);
            //productViewModel.Categories = _categoryAction.GetCategories(true, true, false);
            //productViewModel.Files = _commonAction.GetFiles(NidProduct);
            return View();
        }
        public IActionResult SubmitEditProduct(Product product)
        {
            //product.LastModified = DateTime.Now;
            //if (_productAction.UpdateProduct(product))
            //    TempData["ProductSuccess"] = "محصول با موفقیت ویرایش گردید";
            //else
            //    TempData["ProductError"] = "خطا در ویرایش محصول.لطفا مجددا امتحان کنید";
            return RedirectToAction("Products");
        }
        public IActionResult ProductDetail(Guid NidProduct)
        {
            //var product = _productAction.GetProduct(NidProduct, true);
            //if (product.NidProduct != Guid.Empty)
            //    return Json(new { success = true, html = Helpers.RenderViewToString.RenderViewAsync(this, "_ProductDetail", product, true) });
            //else
                return Json(new { success = false });
        }
        public IActionResult DeleteProduct(Guid NidProduct)
        {
            //var product = _productAction.GetProduct(NidProduct);
            //if (product.NidProduct != Guid.Empty)
            //{
            //    product.State = 2;
            //    product.LastModified = DateTime.Now;
            //    if (_productAction.UpdateProduct(product))
            //        return Json(new { success = true, message = "محصول با موفقیت حذف گردید" });
            //    else
            //        return Json(new { success = false, message = "خطا در حذف محصول.لطفا مجددا امتحان کنید" });
            //}
            //else
            //{
            //    return Json(new { success = false, message = "محصول مورد نظر یافت نشد" });
            //}
            return Json("");
        }
        public IActionResult ProductFilter(int Index, string FilterText)
        {
            List<Product> products = new List<Product>();
            bool success = true;
            //switch (Index)
            //{
            //    case 1:
            //        products = _productAction.GetProductsByCategory(Guid.Parse(FilterText.Split(',')[0]), Guid.Parse(FilterText.Split(',')[1]), Guid.Parse(FilterText.Split(',')[2])).ToList();
            //        break;
            //    case 2:
            //        products = _productAction.GetProductsByCreateDate(DateTime.Parse(FilterText.Split(',')[0]).Date, DateTime.Parse(FilterText.Split(',')[1]).Date).ToList();
            //        break;
            //    case 3:
            //        products = _productAction.GetProductsByPriceRange(decimal.Parse(FilterText.Split(',')[0]), decimal.Parse(FilterText.Split(',')[1])).ToList();
            //        break;
            //    case 4:
            //        products = _productAction.GetProductsByAvailablity(int.Parse(FilterText)).ToList();
            //        break;
            //}
            return Json(new { success = success, html = RenderViewToString.RenderViewAsync(this, "_FilteredProduct", products, true) });
        }
        public IActionResult Orders()
        {
            //var orders = _productAction.GetOrders();
            return View();
        }
        public IActionResult ClosedOrders()
        {
            //var orders = _productAction.GetOrders(1);
            return View();
        }
        public IActionResult OrderDetail(Guid NidOrder)
        {
            //var order = _productAction.GetOrder(NidOrder);
            return View();
        }
        public IActionResult Ships(byte state = 1)
        {
            //var ships = _productAction.GetShips(state);
            //return View(new Tuple<IEnumerable<Ship>, byte>(ships, state));
            return View();
        }
        public IActionResult UpdateShip(Guid NidShip, byte State)
        {
            //var ship = _productAction.GetShip(NidShip);
            //ship.State = State;
            //if (_productAction.UpdateShip(ship))
            //    return Json(new { success = true, message = "مرسوله با موفقیت بروزرسانی شد" });
            //else
            //    return Json(new { success = false, message = "خطا در بروزرسانی مرسوله.لطفا مجددا امتحان کنید" });
            return Json("");
        }
        public IActionResult Comments()
        {
            //var comments = _productAction.GetComments();
            return View();
        }
        public IActionResult DeleteComment(Guid nidComment)
        {
            //var comment = _productAction.GetComment(nidComment);
            //if (comment.NidComment != Guid.Empty)
            //{
            //    comment.State = 2;
            //    if (_productAction.UpdateComment(comment))
            //        return Json(new { success = true, message = "نظر با موفقیت حذف شد." });
            //    else
            //        return Json(new { success = false, message = "خطا در حذف نظر" });
            //}
            //else
            //    return Json(new { success = false, message = "خطا در حذف نظر" });
            return Json("");
        }
        public IActionResult AcceptComment(Guid nidComment)
        {
            //var comment = _productAction.GetComment(nidComment);
            //if (comment.NidComment != Guid.Empty)
            //{
            //    comment.State = 1;
            //    if (_productAction.UpdateComment(comment))
            //        return Json(new { success = true, message = "نظر با موفقیت تایید شد." });
            //    else
            //        return Json(new { success = false, message = "خطا در تایید نظر" });
            //}
            //else
            //    return Json(new { success = false, message = "خطا در تایید نظر" });
            return Json("");
        }
        public IActionResult AcceptedComments()
        {
            //var comments = _productAction.GetComments(1);
            return View();
        }
        public IActionResult UnAcceptComment(Guid nidComment)
        {
            //var comment = _productAction.GetComment(nidComment);
            //if (comment.NidComment != Guid.Empty)
            //{
            //    comment.State = 0;
            //    if (_productAction.UpdateComment(comment))
            //        return Json(new { success = true, message = "نظر با موفقیت بازگردانده شد." });
            //    else
            //        return Json(new { success = false, message = "خطا در بازگرداندن نظر" });
            //}
            //else
                return Json(new { success = false, message = "خطا در بازگرداندن نظر" });
        }
        //general section
        public async Task<IActionResult> UploadFile(IFormCollection data, IList<IFormFile> files)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            //List<string[]> pics = new List<string[]>();
            //foreach (var file in data.Files)
            //{
            //    if (file.Length > 0)
            //    {
            //        string newFileName = "Image_" + DateTime.Now.ToShortDateString().Replace('/', '_') + "_" + DateTime.Now.ToString("HH:mm:ss").Replace(':', '_') + "_" + file.FileName.Split('.')[0].Replace(' ', '_') + ".webp";
            //        string filePath = Path.Combine(configuration.GetSection("ImagesPathRoot").Value, newFileName);
            //        if (Commons.SaveReducedImage(int.Parse(data["ImageWidth"].ToString()), int.Parse(data["ImageHeight"].ToString()), filePath, file))
            //        {
            //            var newFile = new Models.File() { NidFile = Guid.NewGuid(), CreateDate = DateTime.Now, RelateType = byte.Parse(data["fileType"].ToString()), FileName = newFileName, FileExtension = "webp", FilePath = filePath, FileSize = (int)(file.Length / 1024), FileUrl = $"{configuration.GetSection("ImageUrlRoot").Value}{newFileName}", Priority = 0, RelateId = Guid.Parse(data["RelateId"].ToString()) };
            //            _commonAction.Add<Models.File>(newFile);
            //        }
            //    }
            //}
            //var gfiles = _commonAction.GetFiles(Guid.Parse(data["RelateId"].ToString()));
            //foreach (var gfile in gfiles.Where(p => p.RelateType == byte.Parse(data["fileType"].ToString())))
            //{
            //    pics.Add(new string[3] { gfile.NidFile.ToString(), gfile.FileUrl, gfile.FileName });
            //}
            //string tmpPic = await RenderViewToString.RenderViewAsync(this, "_FileDemo", pics, true);
            //return Json(new { success = true, pics = tmpPic });
            return Json("");
        }
        public IActionResult DeleteFile(Guid NidFile)
        {
            //var file = _commonAction.GetFile(NidFile);
            //if (file.NidFile != Guid.Empty)
            //{
            //    System.IO.File.Delete(file.FilePath);
            //    if (_commonAction.Remove<Models.File>(file))
            //        return Json(new { success = true });
            //    else
            //        return Json(new { success = false });
            //}
            //else
                return Json(new { success = false });
        }
        public IActionResult Settings()
        {
            //SettingViewModel model = new SettingViewModel();
            //model.Settings = _commonAction.GetSettings();
            //model.Files = _commonAction.GetCommonFiles();
            return View();
        }
        public IActionResult AddSetting(Setting setting)
        {
            //setting.NidSetting = Guid.NewGuid();
            //setting.State = 0;
            //if (_commonAction.Add<Setting>(setting))
            //    TempData["SettingSuccess"] = "تنظیمات با موفقیت ایجاد گردید";
            //else
            //    TempData["SettingError"] = "خطا در ایجاد تنظیمات.لطفا مجددا امتحان کنید";

            return RedirectToAction("Settings");
        }
        public IActionResult EditSetting(Setting setting)
        {
            //if (_commonAction.Update<Setting>(setting))
            //    TempData["SettingSuccess"] = "تنظیمات با موفقیت ویرایش گردید";
            //else
            //    TempData["SettingError"] = "خطا در ویرایش تنظیمات.لطفا مجددا امتحان کنید";

            return RedirectToAction("Settings");
        }
        public IActionResult DeleteSetting(Guid NidSetting)
        {
            //var setting = _commonAction.GetSetting(NidSetting);
            //if (setting.NidSetting != Guid.Empty)
            //{
            //    setting.State = 2;
            //    if (_commonAction.Update<Setting>(setting))
            //        TempData["SettingSuccess"] = "تنظیمات با حذف ایجاد گردید";
            //    else
            //        TempData["SettingError"] = "خطا در حذف تنظیمات.لطفا مجددا امتحان کنید";
            //}
            //else
            //    TempData["SettingError"] = "تنظیمات یافت نشد";
            return RedirectToAction("Settings");
        }
        public IActionResult StatusCodes(int status)
        {
            return View("HttpError", status);
        }
        //less importants
        public IActionResult Index()
        {
            //var res = _commonAction.GetIndexPageValues();
            return View();
        }
        public IActionResult Charts()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        //user section
        public IActionResult Users()
        {
            UsersViewModel viewModel = new UsersViewModel();
            //viewModel.AdminUsers = _userAction.GetUsers();
            //viewModel.RegularUsers = _userAction.GetUsers(true, false);
            return View(viewModel);
        }
        public IActionResult AddUser()
        {
            return View();
        }
        public IActionResult SubmitAddUser(User user)
        {
            //user.Password = Helpers.Commons.EncryptString(user.Password);
            //user.IsAdmin = true;
            //user.CreateDate = DateTime.Now;
            //user.NidUser = Guid.NewGuid();
            //if (_userAction.Add<User>(user))
            //    TempData["UserSuccess"] = $"کاربر با موفقیت ایجاد گردید";
            //else
            //    TempData["UserError"] = "خطا در ایجاد کاربر.لطفا مجددا امتحان کنید";
            return RedirectToAction("Users");
        }
        public IActionResult EditUser(Guid nidUser)
        {
            //var User = _userAction.GetUser(nidUser);
            return View(User);
        }
        public IActionResult SubmitEditUser(User user)
        {
            //if (_userAction.UpdateUser(user))
            //    TempData["UserSuccess"] = "کاربر با موفقیت ویرایش گردید";
            //else
            //    TempData["UserError"] = "خطا در ویرایش کاربر.لطفا مجددا امتحان کنید";
            return RedirectToAction("Users");
        }
        public IActionResult UserDetail(Guid nidUser)
        {
            //var User = _userAction.GetUser(nidUser);
            return View(User);
        }
        public IActionResult DeleteUser(Guid nidUser)
        {
            //var User = _userAction.GetUser(nidUser);
            //if (User.NidUser != Guid.Empty)
            //{
            //    _userAction.Remove<User>(User);
            //    TempData["UserSuccess"] = "کاربر با موفقیت حذف گردید";
            //}
            //else
            //    TempData["UserError"] = "خطا در حذف کاربر.لطفا مجددا امتحان کنید";
            return RedirectToAction("Users");
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        public async Task<IActionResult> SubmitLogin(string Username, string Password)
        {
            //    var user = _userAction.LoginWithUsername(Username, Password);
            //    if (user.NidUser == Guid.Empty)
            //    {
            //        TempData["LoginError"] = "نام کاربری یا کلمه عبور اشتباه است";
            //        return RedirectToAction("Login");
            //    }
            //    else
            //    {
            //        if (!user.IsDisabled)
            //        {
            //            List<Claim> claims = new List<Claim>
            //            {
            //                new Claim("Username",user.Username),
            //                new Claim("NidUser",user.NidUser.ToString()),
            //                new Claim("Role", "Admin"),
            //             };
            //            var claimsIdentity = new ClaimsIdentity(
            //claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //            var authProperties = new AuthenticationProperties { IsPersistent = true };

            //            await HttpContext.SignInAsync(
            //                CookieAuthenticationDefaults.AuthenticationScheme,
            //                new ClaimsPrincipal(claimsIdentity),
            //                authProperties);
            //            return RedirectToAction("Index");
            //        }
            //        else
            //        {
            //            TempData["LoginWarning"] = "کاربر غیرفعال می باشد";
            //            return RedirectToAction("Login");
            //        }
            //    }

            return RedirectToAction("Index");
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}