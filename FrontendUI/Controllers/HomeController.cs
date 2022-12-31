using Domain;
using FrontendUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Text;
using Application.Helpers;
using Newtonsoft.Json;
using Application.DTO.Category;
using Application.DTO.Product;
using Application.DTO.File;
using Microsoft.AspNetCore.Authorization;
using Application.Model;

namespace FrontendUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly string BaseAddress = "";
        public HomeController(IConfiguration configuration)
        {
            BaseAddress = configuration.GetSection("frontendApiAddress").Value;
        }
        //first page section
        public async Task<IActionResult> Index()
        {
            IndexViewModel model = new IndexViewModel();
            List<FileListDto> files = new List<FileListDto>();
            List<Guid> relates = new List<Guid>();
            var categoryListResult = await ApiCall.Call(ApiCall.HttpMethods.Get,$"{BaseAddress}/UI/GetCategories");
            if(categoryListResult.IsSuccessfulResult())
                model.Categories = JsonConvert.DeserializeObject<List<CategoryListDto>>(categoryListResult.Content) ?? new List<CategoryListDto>();
            var specialProductListResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetSpecialProducts");
            if (specialProductListResult.IsSuccessfulResult())
                model.SpecialProducts = JsonConvert.DeserializeObject<List<ProductListDto>>(specialProductListResult.Content) ?? new List<ProductListDto>();
            var offProductListResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetOffProducts");
            if (offProductListResult.IsSuccessfulResult())
                model.OffProducts = JsonConvert.DeserializeObject<List<ProductListDto>>(offProductListResult.Content) ?? new List<ProductListDto>();
            var commonFileResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetCommonFiles?from=16&to=24");
            if (commonFileResult.IsSuccessfulResult())
            {
                foreach (var f in JsonConvert.DeserializeObject<List<FileListDto>>(commonFileResult.Content) ?? new List<FileListDto>())
                {
                    files.Add(f);
                }
            }
            relates.AddRange(model.Categories.Select(p => p.Id).ToList());
            relates.AddRange(model.SpecialProducts.Select(p => p.Id).ToList());
            relates.AddRange(model.OffProducts.Select(p => p.Id).ToList());
            foreach (var id in relates)
            {
                var fileResult = await ApiCall.Call(ApiCall.HttpMethods.Get,$"{BaseAddress}/UI/GetFiles/{id}");
                if(fileResult.IsSuccessfulResult())
                {
                    foreach (var f in JsonConvert.DeserializeObject<List<FileListDto>>(fileResult.Content) ?? new List<FileListDto>())
                    {
                        files.Add(f);
                    }
                }
            }
            model.Files = files;
            return View(model);
        }
        //category page section
        public async Task<IActionResult> Category(string Title, string BrandId = "", string TypeId = "")
        {
            CategoryViewModel model = new CategoryViewModel();
            var categoryResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetCategoryByTitle/{Title.Trim().Replace('_', ' ')}");
            if (categoryResult.IsSuccessfulResult())
                model.Category = JsonConvert.DeserializeObject<CategoryDto>(categoryResult.Content) ?? new CategoryDto();
            if (model.Category.Id != Guid.Empty)
            {
                List<FileListDto> files = new List<FileListDto>();
                List<Guid> relates = new List<Guid>();
                if (string.IsNullOrWhiteSpace(BrandId) && string.IsNullOrWhiteSpace(TypeId))
                {
                    var productsResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetProductsByCategoryId/{model.Category.Id}");
                    if (productsResult.IsSuccessfulResult())
                        model.Products = JsonConvert.DeserializeObject<List<ProductListDto>>(categoryResult.Content) ?? new List<ProductListDto>();
                }
                else
                {
                    List<Guid> brands = new List<Guid>();
                    List<Guid> types = new List<Guid>();
                    if (!string.IsNullOrWhiteSpace(BrandId))
                    {
                        model.BrandId = Guid.Parse(BrandId);
                        brands.Add(model.BrandId);
                    }
                    if (!string.IsNullOrWhiteSpace(TypeId))
                    {
                        model.TypeId = Guid.Parse(TypeId);
                        types.Add(model.TypeId);
                    }
                    var content = new StringContent(JsonConvert.SerializeObject(new UIProductFilters() { NidCategory = model.Category.Id, BrandIds = brands, TypeIds = types }), Encoding.UTF8, "application/json");
                    var productsResult = await ApiCall.Call(ApiCall.HttpMethods.Post, $"{BaseAddress}/UI/GetFilteredProducts",content);
                    if (productsResult.IsSuccessfulResult())
                        model.Products = JsonConvert.DeserializeObject<List<ProductListDto>>(productsResult.Content) ?? new List<ProductListDto>();
                }
                var productCountResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetProductCount/{model.Category.Id}");
                if (productCountResult.IsSuccessfulResult())
                    model.ProductCount = JsonConvert.DeserializeObject<int>(productCountResult.Content);
                relates.Add(model.Category.Id);
                relates.AddRange(model.Products.Select(p => p.Id).ToList());
                foreach (var id in relates)
                {
                    var fileResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetFiles/{id}");
                    if (fileResult.IsSuccessfulResult())
                    {
                        foreach (var f in JsonConvert.DeserializeObject<List<FileListDto>>(fileResult.Content) ?? new List<FileListDto>())
                        {
                            files.Add(f);
                        }
                    }
                }
                model.Files = files;
            }
            return View(model);
        }
        public async Task<IActionResult> CategoryFilter(string NidCategory, string MinPrice, string MaxPrice, string TypeId, string BrandId, int Sorttype)
        {
            CategoryViewModel model = new CategoryViewModel();
            var categoryResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetCategory/{NidCategory}");
            if (categoryResult.IsSuccessfulResult())
                model.Category = JsonConvert.DeserializeObject<CategoryDto>(categoryResult.Content) ?? new CategoryDto();
            if (model.Category.Id != Guid.Empty)
            {
                List<FileListDto> files = new List<FileListDto>();
                List<Guid> relates = new List<Guid>();
                List<Guid> types = new List<Guid>();
                List<Guid> brands = new List<Guid>();
                if (!string.IsNullOrWhiteSpace(TypeId))
                {
                    foreach (var typ in TypeId.Split(','))
                    {
                        types.Add(Guid.Parse(typ));
                    }
                }
                if (!string.IsNullOrWhiteSpace(BrandId))
                {
                    foreach (var brd in BrandId.Split(','))
                    {
                        brands.Add(Guid.Parse(brd));
                    }
                }
                var content = new StringContent(JsonConvert.SerializeObject(new UIProductFilters() { NidCategory = model.Category.Id, BrandIds = brands, TypeIds = types, SortType = Sorttype, MinPrice = decimal.Parse(MinPrice), MaxPrice = decimal.Parse(MaxPrice) }), Encoding.UTF8, "application/json");
                var productsResult = await ApiCall.Call(ApiCall.HttpMethods.Post, $"{BaseAddress}/UI/GetFilteredProducts",content);
                if (productsResult.IsSuccessfulResult())
                    model.Products = JsonConvert.DeserializeObject<List<ProductListDto>>(categoryResult.Content) ?? new List<ProductListDto>();
                var productCountResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetProductCount/{model.Category.Id}");
                if (productCountResult.IsSuccessfulResult())
                    model.ProductCount = JsonConvert.DeserializeObject<int>(categoryResult.Content);
                model.SortType = Sorttype;
                relates.Add(model.Category.Id);
                relates.AddRange(model.Products.Select(p => p.Id).ToList());
                foreach (var id in relates)
                {
                    var fileResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetFiles/{id}");
                    if (fileResult.IsSuccessfulResult())
                    {
                        foreach (var f in JsonConvert.DeserializeObject<List<FileListDto>>(fileResult.Content) ?? new List<FileListDto>())
                        {
                            files.Add(f);
                        }
                    }
                }
                model.Files = files;
                return Json(new { success = true, html = RenderViewToString.RenderViewAsync(this, "_CategoryFilter", model, true), countMessage = $"تعداد محصولات : {model.ProductCount}" });
            }
            else
                return Json(new { success = false, html = "" });
        }
        public async Task<IActionResult> CategoryPagination(string NidCategory, string MinPrice, string MaxPrice, string TypeId, string BrandId, int PageNumber, int Sorttype)
        {
            CategoryViewModel model = new CategoryViewModel();
            var categoryResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetCategory/{NidCategory}");
            if (categoryResult.IsSuccessfulResult())
                model.Category = JsonConvert.DeserializeObject<CategoryDto>(categoryResult.Content) ?? new CategoryDto();
            if (model.Category.Id != Guid.Empty)
            {
                List<FileListDto> files = new List<FileListDto>();
                List<Guid> relates = new List<Guid>();
                List<Guid> types = new List<Guid>();
                List<Guid> brands = new List<Guid>();
                if (!string.IsNullOrWhiteSpace(TypeId))
                {
                    foreach (var typ in TypeId.Split(','))
                    {
                        types.Add(Guid.Parse(typ));
                    }
                }
                if (!string.IsNullOrWhiteSpace(BrandId))
                {
                    foreach (var brd in BrandId.Split(','))
                    {
                        brands.Add(Guid.Parse(brd));
                    }
                }
                var content = new StringContent(JsonConvert.SerializeObject(new UIProductFilters() { NidCategory = model.Category.Id, BrandIds = brands, TypeIds = types, SortType = Sorttype, MinPrice = decimal.Parse(MinPrice), MaxPrice = decimal.Parse(MaxPrice), Pagesize = 10, Skip = PageNumber - 1 }), Encoding.UTF8, "application/json");
                var productsResult = await ApiCall.Call(ApiCall.HttpMethods.Post, $"{BaseAddress}/UI/GetFilteredProducts",content);
                if (productsResult.IsSuccessfulResult())
                    model.Products = JsonConvert.DeserializeObject<List<ProductListDto>>(categoryResult.Content) ?? new List<ProductListDto>();
                var productCountResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetProductCount/{model.Category.Id}");
                if (productCountResult.IsSuccessfulResult())
                    model.ProductCount = JsonConvert.DeserializeObject<int>(categoryResult.Content);
                model.PageNumber = PageNumber;
                model.SortType = Sorttype;
                relates.Add(model.Category.Id);
                relates.AddRange(model.Products.Select(p => p.Id).ToList());
                foreach (var id in relates)
                {
                    var fileResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetFiles/{id}");
                    if (fileResult.IsSuccessfulResult())
                    {
                        foreach (var f in JsonConvert.DeserializeObject<List<FileListDto>>(fileResult.Content) ?? new List<FileListDto>())
                        {
                            files.Add(f);
                        }
                    }
                }
                model.Files = files;
                return Json(new { success = true, html = RenderViewToString.RenderViewAsync(this, "_CategoryPagination", model, true), countMessage = $"تعداد محصولات : {model.ProductCount}" });
            }
            else
                return Json(new { success = false, html = "" });
        }
        //product page section
        public async Task<IActionResult> Product(string Title)
        {
            ProductViewModel model = new ProductViewModel();
            var productResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetCategoryByTitle/{Title.Trim().Replace('_', ' ')}");
            if (productResult.IsSuccessfulResult())
                model.Product = JsonConvert.DeserializeObject<DetailProductDto>(productResult.Content) ?? new DetailProductDto();
            if (model.Product.Id != Guid.Empty)
            {
                List<FileListDto> files = new List<FileListDto>();
                List<Guid> relates = new List<Guid>();
                var simProductResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetSimilarProducts/{model.Product.Id}");
                if (simProductResult.IsSuccessfulResult())
                    model.SimilarProducts = JsonConvert.DeserializeObject<List<ProductListDto>>(productResult.Content) ?? new List<ProductListDto>();
                relates.Add(model.Product.Id);
                relates.AddRange(model.SimilarProducts.Select(p => p.Id).ToList());
                foreach (var id in relates)
                {
                    var fileResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetFiles/{id}");
                    if (fileResult.IsSuccessfulResult())
                    {
                        foreach (var f in JsonConvert.DeserializeObject<List<FileListDto>>(fileResult.Content) ?? new List<FileListDto>())
                        {
                            files.Add(f);
                        }
                    }
                }
                model.Files = files;
            }
            return View(model);
        }
        //general section
        public async Task<IActionResult> Search(string Text)
        {
            List<ProductListDto> res = new List<ProductListDto>();
            var searchResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/SearchProduct/{Text}");
            if (searchResult.IsSuccessfulResult())
                res = JsonConvert.DeserializeObject<List<ProductListDto>>(searchResult.Content) ?? new List<ProductListDto>();
            if (res.ToList().Count != 0)
            {
                return Json(new { success = true, html = RenderViewToString.RenderViewAsync(this, "_SearchResult", res, true) });
            }
            else
            {
                return Json(new { success = false });
            }
        }
        public async Task<IActionResult> AboutUs()
        {
            List<FileListDto> files = new List<FileListDto>();
            var commonFileResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetCommonFiles?from=25&to=28");
            if (commonFileResult.IsSuccessfulResult())
            {
                foreach (var f in JsonConvert.DeserializeObject<List<FileListDto>>(commonFileResult.Content) ?? new List<FileListDto>())
                {
                    files.Add(f);
                }
            }
            return View(files);
        }
        public IActionResult ContactUs()
        {
            return View();
        }
        public async Task<IActionResult> Categories()
        {
            CategoriesViewModel model = new CategoriesViewModel();
            List<FileListDto> files = new List<FileListDto>();
            List<Guid> relates = new List<Guid>();
            var searchResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetCategories?includeProduct=true");
            if (searchResult.IsSuccessfulResult())
                model.Categories = JsonConvert.DeserializeObject<List<CategoryDto>>(searchResult.Content) ?? new List<CategoryDto>();
            foreach (var cat in model.Categories)
            {
                relates.AddRange(cat.Products.Select(p => p.Id).ToList());
            }
            foreach (var id in relates)
            {
                var fileResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress} /UI/GetFiles/{id}");
                if (fileResult.IsSuccessfulResult())
                {
                    foreach (var f in JsonConvert.DeserializeObject<List<FileListDto>>(fileResult.Content) ?? new List<FileListDto>())
                    {
                        files.Add(f);
                    }
                }
            }
            model.Files = files;
            return View(model);
        }
        public async Task<IActionResult> Bargain()
        {
            CategoryViewModel model = new CategoryViewModel();
            List<FileListDto> files = new List<FileListDto>();
            List<Guid> relates = new List<Guid>();
            var productsResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetBargainedProducts");
            if (productsResult.IsSuccessfulResult())
                model.Products = JsonConvert.DeserializeObject<List<ProductListDto>>(productsResult.Content) ?? new List<ProductListDto>();
            var productCountResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetBargainProductCount");
            if (productCountResult.IsSuccessfulResult())
                model.ProductCount = JsonConvert.DeserializeObject<int>(productCountResult.Content);
            var commonFileResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetCommonFiles?from=25&to=28");
            if (commonFileResult.IsSuccessfulResult())
            {
                foreach (var f in JsonConvert.DeserializeObject<List<FileListDto>>(commonFileResult.Content) ?? new List<FileListDto>())
                {
                    files.Add(f);
                }
            }
            relates.AddRange(model.Products.Select(p => p.Id).ToList());
                foreach (var id in relates)
                {
                    var fileResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetFiles/{id}");
                    if (fileResult.IsSuccessfulResult())
                    {
                        foreach (var f in JsonConvert.DeserializeObject<List<FileListDto>>(fileResult.Content) ?? new List<FileListDto>())
                        {
                            files.Add(f);
                        }
                    }
                }
            model.Files = files;
            return View(model);
        }
        public async Task<IActionResult> BargainPagination(int PageNumber)
        {
            CategoryViewModel model = new CategoryViewModel();
            List<FileListDto> files = new List<FileListDto>();
            List<Guid> relates = new List<Guid>();
            var productsResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetBargainedProducts?pageSize=10&skip={PageNumber - 1}");
            if (productsResult.IsSuccessfulResult())
                model.Products = JsonConvert.DeserializeObject<List<ProductListDto>>(productsResult.Content) ?? new List<ProductListDto>();
            var productCountResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetBargainProductCount");
            if (productCountResult.IsSuccessfulResult())
                model.ProductCount = JsonConvert.DeserializeObject<int>(productCountResult.Content);
            relates.AddRange(model.Products.Select(p => p.Id).ToList());
            foreach (var id in relates)
            {
                var fileResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetFiles/{id}");
                if (fileResult.IsSuccessfulResult())
                {
                    foreach (var f in JsonConvert.DeserializeObject<List<FileListDto>>(fileResult.Content) ?? new List<FileListDto>())
                    {
                        files.Add(f);
                    }
                }
            }
            model.Files = files;
            model.PageNumber = PageNumber;
            return Json(new { success = true, html = RenderViewToString.RenderViewAsync(this, "_BargainPagination", model, true), countMessage = $"تعداد محصولات : {model.ProductCount}" });
        }
        public async Task<IActionResult> Delivery()
        {
            List<FileListDto> files = new List<FileListDto>();
            var commonFileResult = await ApiCall.Call(ApiCall.HttpMethods.Get, $"{BaseAddress}/UI/GetCommonFiles?from=28&to=29");
            if (commonFileResult.IsSuccessfulResult())
            {
                foreach (var f in JsonConvert.DeserializeObject<List<FileListDto>>(commonFileResult.Content) ?? new List<FileListDto>())
                {
                    files.Add(f);
                }
            }
            return View(files);
        }
        public IActionResult StatusCodes(int status)
        {
            return View("HttpError", status);
        }
        //broker stuffs
        public IActionResult AddComment(Comment comment)
        {
            //comment.Id = Guid.NewGuid();
            //if (comment.UserId == Guid.Empty)
            //    comment.UserId = Guid.Parse("CE69B3F8-3A19-47B6-A5CC-BA09221857DA");
            //comment.CreateDate = DateTime.Now;
            //comment.State = 0;
            //if (_db.Add<Comment>(comment))
            //    TempData["CommentSuccess"] = $"نظر شما با موفقیت ثبت گردید و پس از تایید مدیر سایت به بخش نظرات اضافه خواهد شد.";
            //else
            //    TempData["CommentError"] = "خطا انجام عملیات.لطفا مجددا امتحان کنید";
            //return RedirectToAction("Product", new { Title = _db.GetProductById(comment.ProductId, false).ProductName.Replace(' ', '_') });
            return View();
        }
        public ActionResult AddToNewsletter(string Mail)
        {
            //Setting setting = new Setting() { NidSetting = Guid.NewGuid(), SettingAttribute = "NewsletterMail", SettingValue = Mail };
            //return Json(new { success = _db.Add<Setting>(setting) });
            return View();
        }
        public ActionResult AddContactForm(ContactForm form)
        {
            //Setting setting = new Setting() { NidSetting = Guid.NewGuid(), SettingAttribute = "ContactForm", SettingValue = string.Format("{0},{1},{2},{3}", form.name, form.subject, form.email, form.message) };
            //if (_db.Add<Setting>(setting))
            //    TempData["ContactSuccess"] = "دیدگاه شما با موفقیت ثبت گردید.از این که نظرات خود را با ما به اشتراک می گذارید متشکریم.";
            //else
            //    TempData["ContactError"] = "خطایی رخ داده است.لطفا مجددا امتحان کنید";
            return RedirectToAction("ContactUs");
        }
        //purchase section
        public IActionResult Carts()
        {
            //if (Request.Cookies.ContainsKey("Stereo8Login"))
            //{
            //    CartViewModel model = new CartViewModel();
            //    model.Carts = _db.GetCartsByUserId(Guid.Parse(UsersAuth.GetSpecificClaim(Request.Cookies["Stereo8Login"], 2)));
            //    List<Models.File> files = new List<Models.File>();
            //    foreach (var crt in model.Carts)
            //    {
            //        foreach (var f in _db.GetFiles(crt.ProductId))
            //        {
            //            files.Add(f);
            //        }
            //    }
            //    model.Files = files;
            //    return View(model);
            //}
            //else
                return RedirectToAction("Login");
        }
        public IActionResult Favorites()
        {
            //if (Request.Cookies.ContainsKey("Stereo8Login"))
            //{
            //    FavoriteViewModel model = new FavoriteViewModel();
            //    model.Favorites = _db.GetFavoritesByUserId(Guid.Parse(UsersAuth.GetSpecificClaim(Request.Cookies["Stereo8Login"], 2)));
            //    List<Models.File> files = new List<Models.File>();
            //    foreach (var crt in model.Favorites)
            //    {
            //        foreach (var f in _db.GetFiles(crt.ProductId))
            //        {
            //            files.Add(f);
            //        }
            //    }
            //    model.Files = files;
            //    return View(model);
            //}
            //else
                return RedirectToAction("Login");
        }
        public ActionResult AddProductToCart(Guid NidProduct, int Quantity, int price)//done
        {
            //var niduser = Guid.Parse(UsersAuth.GetSpecificClaim(Request.Cookies["Stereo8Login"], 2));
            //var products = _db.GetCartProductsByUserId(niduser);
            //int cartCount = _db.GetCartCountByUserId(niduser);
            //bool status = true;
            //var tmpProduct = _db.GetProductById(NidProduct, false);
            //if (tmpProduct != null)
            //{
            //    if (!products.Contains(NidProduct))
            //    {
            //        if (tmpProduct.AvailableCount >= Quantity)
            //        {
            //            var newCart = new Cart() { CreateDate = DateTime.Now, NidCart = Guid.NewGuid(), ProductId = NidProduct, UserId = niduser, Quantity = Quantity };
            //            if (_db.Add<Cart>(newCart))
            //            {
            //                cartCount = _db.GetCartCountByUserId(niduser);
            //            }
            //            else
            //                status = false;
            //        }
            //        else
            //        {
            //            var newCart = new Cart() { CreateDate = DateTime.Now, NidCart = Guid.NewGuid(), ProductId = NidProduct, UserId = niduser, Quantity = tmpProduct.AvailableCount };
            //            if (_db.Add<Cart>(newCart))
            //            {
            //                cartCount = _db.GetCartCountByUserId(niduser);
            //            }
            //            else
            //                status = false;
            //        }
            //    }
            //    else
            //    {
            //        var cart = _db.GetCartByProductId(niduser, NidProduct);
            //        if (cart.NidCart != Guid.Empty)
            //        {
            //            if (tmpProduct.AvailableCount >= (cart.Quantity + Quantity))
            //                cart.Quantity = cart.Quantity + Quantity;
            //            cart.LastModified = DateTime.Now;
            //            if (!_db.UpdateCart(cart))
            //                status = false;
            //        }
            //        else
            //            status = false;
            //    }
            //}
            //if (Request.Cookies.ContainsKey("CartCount"))
            //    Response.Cookies.Delete("CartCount");
            //HttpContext.Response.Cookies.Append("CartCount", cartCount.ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(14), HttpOnly = true, Path = "/" });
            //if (status)
            //    return Json(new { success = true, count = cartCount });
            //else
                return Json(new { success = false, message = "خطایی رخ داده است.لطفا مجددا امتحان کنید" });
        }
        public ActionResult RemoveProductFromCart(Guid NidCart)//done
        {
            //var niduser = Guid.Parse(UsersAuth.GetSpecificClaim(Request.Cookies["Stereo8Login"], 2));
            //if (_db.Remove<Cart>(_db.GetCartById(NidCart)))
            //{
            //    int cartCount = _db.GetCartCountByUserId(niduser);
            //    if (Request.Cookies.ContainsKey("CartCount"))
            //        Response.Cookies.Delete("CartCount");
            //    HttpContext.Response.Cookies.Append("CartCount", cartCount.ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(14), HttpOnly = true, Path = "/" });
            //    decimal aggregate = _db.GetCartAggregateByUserId(niduser);
            //    return Json(new { success = true, count = cartCount, aggregate = aggregate });
            //}
            //else
                return Json(new { success = false });
        }
        public ActionResult CartQuantityChanged(Guid NidCart, int Quantity)//done
        {
            //var niduser = Guid.Parse(UsersAuth.GetSpecificClaim(Request.Cookies["Stereo8Login"], 2));
            //var tmpCart = _db.GetCartById(NidCart);
            //if (tmpCart.NidCart != Guid.Empty)
            //{
            //    tmpCart.LastModified = DateTime.Now;
            //    tmpCart.Quantity = Quantity;
            //    if (_db.UpdateCart(tmpCart))
            //    {
            //        int cartCount = _db.GetCartCountByUserId(niduser);
            //        decimal aggregate = _db.GetCartAggregateByUserId(niduser);
            //        return Json(new { success = true, count = cartCount, aggregate = aggregate });
            //    }
            //    else
            //        return Json(new { success = false });
            //}
            //else
                return Json(new { success = false });

        }
        public ActionResult AddProductToFavorites(Guid NidProduct)//done
        {
            //var niduser = Guid.Parse(UsersAuth.GetSpecificClaim(Request.Cookies["Stereo8Login"], 2));
            //var products = _db.GetFavoriteProductsByUserId(niduser);
            //int favCount = _db.GetFavoriteCountByUserId(niduser);
            //if (!products.Contains(NidProduct))
            //{
            //    var newFav = new Favorite() { CreateDate = DateTime.Now, NidFav = Guid.NewGuid(), ProductId = NidProduct, UserId = niduser };
            //    if (_db.Add<Favorite>(newFav))
            //    {
            //        favCount = _db.GetFavoriteCountByUserId(niduser);
            //        if (Request.Cookies.ContainsKey("FavCount"))
            //            Response.Cookies.Delete("FavCount");
            //        HttpContext.Response.Cookies.Append("FavCount", favCount.ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(14), HttpOnly = true, Path = "/" });
            //        return Json(new { success = true, count = favCount });
            //    }
            //    else
            //        return Json(new { success = false });
            //}
            //else
            //return Json(new { success = true, count = favCount });
            return View();
        }
        public ActionResult RemoveProductFromFav(Guid NidFav)//done
        {
            //var niduser = Guid.Parse(UsersAuth.GetSpecificClaim(Request.Cookies["Stereo8Login"], 2));
            //if (_db.Remove<Favorite>(_db.GetFavoriteById(NidFav)))
            //{
            //    int favCount = _db.GetFavoriteCountByUserId(niduser);
            //    if (Request.Cookies.ContainsKey("FavCount"))
            //        Response.Cookies.Delete("FavCount");
            //    HttpContext.Response.Cookies.Append("FavCount", favCount.ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(14), HttpOnly = true, Path = "/" });
            //    return Json(new { success = true, count = favCount });
            //}
            //else
                return Json(new { success = false });
        }
        public ActionResult GetShipFee(int StateId, decimal TotalWeight, decimal CurrentAggregate)//done
        {
            //bool status = true;
            //var shipPrices = _db.GetShipPrices();
            //decimal shipPriceCal = 0;
            //if (StateId != 0)
            //{
            //    if (StateId == 1)
            //        shipPriceCal = shipPrices.FirstOrDefault(p => p.ToWeight == Math.Ceiling(TotalWeight)).InnerState;
            //    else if (StateId == 9 || StateId == 10 || StateId == 11 || StateId == 13 || StateId == 31)
            //        shipPriceCal = shipPrices.FirstOrDefault(p => p.ToWeight == Math.Ceiling(TotalWeight)).NeighborState;
            //    else
            //        shipPriceCal = shipPrices.FirstOrDefault(p => p.ToWeight == Math.Ceiling(TotalWeight)).OtherState;
            //}
            //if (status)
            //    return Json(new { success = true, shipprice = string.Format("{0:n0} تومان", shipPriceCal), newaggregate = string.Format("{0:n0} تومان", CurrentAggregate + shipPriceCal) });
            //else
                return Json(new { success = false, message = "خطایی رخ داده است.لطفا مجددا امتحان کنید" });
        }

        //user section
        public IActionResult Profile()
        {
            //if (Request.Cookies.ContainsKey("Stereo8Login"))
            //{
            //    var user = _db.GetUserById(Guid.Parse(UsersAuth.GetSpecificClaim(Request.Cookies["Stereo8Login"], 2)));
            //    return View(user);
            //}
            //else
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult SubmitLogin(string Username, string Password, bool Remember)
        {
            //var user = _db.LoginWithUsername(Username, Password);
            //if (user.NidUser == Guid.Empty)
            //{
            //    TempData["LoginError"] = "نام کاربری یا کلمه عبور اشتباه است";
            //    return RedirectToAction("Login");
            //}
            //else
            //{
            //    if (!user.IsDisabled)
            //    {
            //        user.LastLoginDate = DateTime.Now;
            //        _db.UpdateUser(user);
            //        if (Request.Cookies.ContainsKey("Stereo8Login"))
            //            Response.Cookies.Delete("Stereo8Login");
            //        if (Request.Cookies.ContainsKey("CartCount"))
            //            Response.Cookies.Delete("CartCount");
            //        if (Request.Cookies.ContainsKey("FavCount"))
            //            Response.Cookies.Delete("FavCount");
            //        if (Remember)
            //        {
            //            HttpContext.Response.Cookies.Append("Stereo8Login", UsersAuth.GenerateLoginCookieValue(user, _db.GetCartCountByUserId(user.NidUser), _db.GetFavoriteCountByUserId(user.NidUser)), new CookieOptions() { Expires = DateTime.Now.AddDays(14), HttpOnly = true, Path = "/" });
            //            HttpContext.Response.Cookies.Append("CartCount", _db.GetCartCountByUserId(user.NidUser).ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(14), HttpOnly = true, Path = "/" });
            //            HttpContext.Response.Cookies.Append("FavCount", _db.GetFavoriteCountByUserId(user.NidUser).ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(14), HttpOnly = true, Path = "/" });
            //        }
            //        else
            //        {
            //            HttpContext.Response.Cookies.Append("Stereo8Login", UsersAuth.GenerateLoginCookieValue(user, _db.GetCartCountByUserId(user.NidUser), _db.GetFavoriteCountByUserId(user.NidUser)), new CookieOptions() { Expires = DateTime.Now.AddDays(7), HttpOnly = true, Path = "/" });
            //            HttpContext.Response.Cookies.Append("CartCount", _db.GetCartCountByUserId(user.NidUser).ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(7), HttpOnly = true, Path = "/" });
            //            HttpContext.Response.Cookies.Append("FavCount", _db.GetFavoriteCountByUserId(user.NidUser).ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(7), HttpOnly = true, Path = "/" });
            //        }
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        TempData["LoginWarning"] = "کاربر غیرفعال می باشد";
            //        return RedirectToAction("Login");
            //    }
            //}
            return View();
        }
        public IActionResult Logout()
        {
            //HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //if (Request.Cookies.ContainsKey("Stereo8Login"))
            //    Response.Cookies.Delete("Stereo8Login");
            //if (Request.Cookies.ContainsKey("CartCount"))
            //    Response.Cookies.Delete("CartCount");
            //if (Request.Cookies.ContainsKey("FavCount"))
            //    Response.Cookies.Delete("FavCount");
            return RedirectToAction("Login");
        }
        public IActionResult Register(User user)
        {
            //if (!_db.CheckUsernameExist(user.Username))
            //{
            //    user.NidUser = Guid.NewGuid();
            //    user.Password = Commons.EncryptString(user.Password);
            //    user.CreateDate = DateTime.Now;
            //    user.IsAdmin = false;
            //    user.IsDisabled = true;
            //    if (_db.Add<User>(user))
            //    {
            //        MailRequest verify = new MailRequest();
            //        verify.Subject = $"فعال سازی حساب کاربری - {Commons.GetAppName()}";
            //        verify.ToEmail = user.Username;
            //        var mailHtml = RenderViewToString.RenderViewAsync(this, "_AccountVerificationEmail", string.Format("{0}://{1}/VerifyUserAccount?Hash={2}", Request.Scheme, Request.Host.Value, WebUtility.UrlEncode(Commons.EncryptString(user.NidUser.ToString()))));
            //        verify.Body = mailHtml.Result;
            //        _db.SendEmail(verify);
            //        return RedirectToAction("RegisterResult", new { IsSuccessful = true });
            //    }
            //    else
            //        return RedirectToAction("RegisterResult", new { IsSuccessful = false });
            //}
            //else
            //{
            //    TempData["UserExistError"] = "نام کاربری وارد شده قبلا ثبت شده است.لطفا ایمیل دیگری وارد کنید";
            //    return RedirectToAction("Login");
            //}
            return View();
        }
        public IActionResult VerifyUserAccount(string Hash)
        {
            //var NidUser = Guid.Parse(Commons.DecryptString(Hash));
            //var user = _db.GetUserById(NidUser, false);
            //user.IsDisabled = false;
            //if (_db.UpdateUser(user))
            //    TempData["UserActivateSuccess"] = $"کاربر با نام کاربری {user.Username} با موفقیت فعال گردید";
            //else
            //    TempData["UserActivateError"] = "خطا در فعال کردن کاربر.لطفا مجددا امتحان کنید";
            return RedirectToAction("Login");
        }
        public IActionResult RegisterResult(bool IsSuccessful)
        {
            return View(IsSuccessful);
        }
        public IActionResult UpdateUser(User user)
        {
            //var currentUser = _db.GetUserById(user.NidUser, false);
            //if (currentUser.NidUser != Guid.Empty)
            //{
            //    currentUser.FirstName = user.FirstName;
            //    currentUser.LastName = user.LastName;
            //    currentUser.Tel = user.Tel;
            //    if (_db.UpdateUser(currentUser))
            //        TempData["ProfileSuccess"] = "اطلاعات کاربر با موفقیت بروزرسانی گردید";
            //    else
            //        TempData["ProfileError"] = "خطا در بروزرسانی اطلاعات کاربر.لطفا مجدد امتحان نمایید.";
            //}
            //else
            //    TempData["ProfileError"] = "کاربر مورد نظر یافت نشد";
            return RedirectToAction("Profile");
        }
        public IActionResult UpdatePassword(Guid NidUser, string Password)
        {
            //var user = _db.GetUserById(NidUser, false);
            //if (user.NidUser != Guid.Empty)
            //{
            //    user.Password = Commons.EncryptString(Password);
            //    if (_db.UpdateUser(user))
            //        return Json(new { success = true, message = "کلمه عبور با موفقیت تغییر یافت." });
            //    else
            //        return Json(new { success = false, message = "خطا در بروزرسانی کلمه عبور.لطفا مجدد امتحان نمایید" });
            //}
            //else
            return Json(new { success = false, message = "کاربر مورد نظر یافت نشد." });
        }
        public IActionResult UpdateAddress(User user)
        {
            //var currentUser = _db.GetUserById(user.NidUser, false);
            //if (currentUser.NidUser != Guid.Empty)
            //{
            //    string regex = @"\b(?!(\d)\1{3})[13-9]{4}[1346-9][013-9]{5}\b";
            //    if (string.IsNullOrWhiteSpace(user.ZipCode.ToString()))
            //    {
            //        currentUser.Address = user.Address;
            //        if (_db.UpdateUser(currentUser))
            //            TempData["AddressSuccess"] = "آدرس با موفقیت بروزرسانی گردید";
            //        else
            //            TempData["AddressError"] = "خطا در بروزرسانی آدرس.لطفا مجدد امتحان نمایید.";
            //    }
            //    else
            //    {
            //        if (Regex.IsMatch(user.ZipCode.ToString(), regex, RegexOptions.IgnoreCase))
            //        {
            //            currentUser.Address = user.Address;
            //            currentUser.ZipCode = user.ZipCode;
            //            if (_db.UpdateUser(currentUser))
            //                TempData["AddressSuccess"] = "آدرس با موفقیت بروزرسانی گردید";
            //            else
            //                TempData["AddressError"] = "خطا در بروزرسانی آدرس.لطفا مجدد امتحان نمایید.";
            //        }
            //        else
            //        {
            //            TempData["AddressError"] = "کد پستی وارد شده معتبر نمی باشد.";
            //        }
            //    }
            //}
            //else
            //    TempData["AddressError"] = "کاربر مورد نظر یافت نشد";
            return RedirectToAction("Profile");
        }
        public IActionResult ForgetPassword(string Username)
        {
            //var user = _db.GetUserByUsername(Username, false);
            //if (user.NidUser != Guid.Empty)
            //{
            //    MailRequest verify = new MailRequest();
            //    verify.Subject = $"بازیابی کلمه عبور - {Commons.GetAppName()}";
            //    verify.ToEmail = user.Username;
            //    var mailHtml = RenderViewToString.RenderViewAsync(this, "_ResetPasswordEmail", string.Format("{0}://{1}/ChangePassword?Hash={2}", Request.Scheme, Request.Host.Value, WebUtility.UrlEncode(Commons.EncryptString(user.NidUser.ToString()))));
            //    verify.Body = mailHtml.Result;
            //    _db.SendEmail(verify);
            //    return Json(new { success = true, message = "فرم بازیابی کلمه عبور به ایمیل شما ارسال شد.برای تغییر کلمه عبور به ایمیل خود مراجعه نمایید" });
            //}
            //else
            //{
            //    return Json(new { success = false, message = "کاربری با این نام کاربری یافت نشد" });
            //}
            return View();
        }
        public IActionResult ChangePassword(string Hash)
        {
            //var NidUser = Guid.Parse(Commons.DecryptString(Hash));
            //var user = _db.GetUserById(NidUser, false);
            //return View(user);
            return View();
        }
        //purchases user oriented
        public IActionResult Checkout()
        {
            //if (Request.Cookies.ContainsKey("Stereo8Login"))
            //{
            //    CheckoutViewModel model = new CheckoutViewModel();
            //    var Niduser = Guid.Parse(UsersAuth.GetSpecificClaim(Request.Cookies["Stereo8Login"], 2));
            //    model.User = _db.GetUserById(Niduser);
            //    model.Carts = _db.GetCartsByUserId(Niduser);
            //    model.Order = _db.GetOrderByUserId(Niduser);
            //    model.ShipPrices = _db.GetShipPrices();
            //    return View(model);
            //}
            //else
            return RedirectToAction("Login");
        }
        public IActionResult CheckoutSubmit(Order order)
        {
            //var Niduser = Guid.Parse(UsersAuth.GetSpecificClaim(Request.Cookies["Stereo8Login"], 2));
            //bool status = true;
            //var carts = _db.GetCartsByUserId(Niduser, true);
            //var total = _db.GetCartAggregateByUserId(Niduser);
            //var shipPrices = _db.GetShipPrices();
            //decimal shipPriceCal = 0;
            //decimal TotalWeight = decimal.Parse(carts.Sum(p => p.Product.Weight).ToString());
            //if (order.StateId != 0)
            //{
            //    if (order.StateId == 1)
            //        shipPriceCal = shipPrices.FirstOrDefault(p => p.ToWeight == Math.Ceiling(TotalWeight)).InnerState;
            //    else if (order.StateId == 9 || order.StateId == 10 || order.StateId == 11 || order.StateId == 13 || order.StateId == 31)
            //        shipPriceCal = shipPrices.FirstOrDefault(p => p.ToWeight == Math.Ceiling(TotalWeight)).NeighborState;
            //    else
            //        shipPriceCal = shipPrices.FirstOrDefault(p => p.ToWeight == Math.Ceiling(TotalWeight)).OtherState;
            //}
            //if (order.NidOrder == Guid.Empty)
            //{
            //    order.CreateDate = DateTime.Now;
            //    order.NidOrder = Guid.NewGuid();
            //    order.UserId = Niduser;
            //    order.State = 0;
            //    order.TotalPrice = total + shipPriceCal;
            //    if (_db.Add<Order>(order))
            //    {
            //        foreach (var cart in carts)
            //        {
            //            if (!_db.Add<OrderDetail>(new OrderDetail() { NidDetail = Guid.NewGuid(), CreateDate = DateTime.Now, OrderId = order.NidOrder, ProductId = cart.ProductId, Quantity = cart.Quantity }))
            //                status = false;
            //        }
            //    }
            //    else
            //        status = false;
            //    if (!_db.Add<Ship>(new Ship() { NidShip = Guid.NewGuid(), OrderId = order.NidOrder, CreateDate = DateTime.Now, Address = order.Address, ShipPrice = shipPriceCal, State = 0, ZipCode = order.ZipCode ?? 1111111111, ShipVia = 0 }))
            //        status = false;
            //    if (status)
            //        return Json(new { success = true, redirect = string.Format("{0}://{1}/Payment?NidOrder={2}", Request.Scheme, Request.Host.Value, order.NidOrder) });
            //    else
            //        return Json(new { success = false, message = "خطا در سرور لطفا مجددا امتحان کنید" });
            //}
            //else
            //{
            //    var currentOrder = _db.GetOrderById(order.NidOrder, false);
            //    currentOrder.Address = order.Address;
            //    currentOrder.Email = order.Email;
            //    currentOrder.CityId = order.CityId;
            //    currentOrder.StateId = order.StateId;
            //    currentOrder.Description = order.Description;
            //    currentOrder.FirstName = order.FirstName;
            //    currentOrder.LastModified = DateTime.Now;
            //    currentOrder.LastName = order.LastName;
            //    currentOrder.Tel = order.Tel;
            //    currentOrder.ZipCode = order.ZipCode;
            //    currentOrder.TotalPrice = _db.GetCartAggregateByUserId(Niduser) + shipPriceCal;
            //    if (_db.UpdateOrder(currentOrder))
            //    {
            //        foreach (var ordd in _db.GetOrderDetailsByOrderId(order.NidOrder))
            //        {
            //            if (!_db.Remove<OrderDetail>(ordd))
            //                status = false;
            //        }
            //        foreach (var cart in carts)
            //        {
            //            if (!_db.Add<OrderDetail>(new OrderDetail() { NidDetail = Guid.NewGuid(), CreateDate = DateTime.Now, OrderId = order.NidOrder, ProductId = cart.ProductId, Quantity = cart.Quantity }))
            //                status = false;
            //        }
            //        var tmpShip = _db.GetShipByOrderId(order.NidOrder, false);
            //        if (tmpShip.NidShip == Guid.Empty)
            //        {
            //            if (!_db.Add<Ship>(new Ship() { NidShip = Guid.NewGuid(), OrderId = order.NidOrder, CreateDate = DateTime.Now, Address = order.Address, ShipPrice = shipPriceCal, State = 0, ZipCode = order.ZipCode ?? 1111111111, ShipVia = 0 }))
            //                status = false;
            //        }
            //        else
            //        {
            //            tmpShip.Address = order.Address;
            //            tmpShip.ZipCode = order.ZipCode ?? 1111111111;
            //            tmpShip.CreateDate = DateTime.Now;
            //            if (!_db.UpdateShip(tmpShip))
            //                status = false;
            //        }
            //    }
            //    else
            //        status = false;
            //    if (status)
            //        return Json(new { success = true, redirect = string.Format("{0}://{1}/Payment?NidOrder={2}", Request.Scheme, Request.Host.Value, currentOrder.NidOrder) });
            //    else
            //        return Json(new { success = false, message = "خطا در سرور لطفا مجددا امتحان کنید" });
            //}
            return View();
        }
        public async Task<ActionResult> Payment(Guid NidOrder)
        {
            //var tmporder = _db.GetOrderById(NidOrder);
            //try
            //{
            //    using (var client = new HttpClient())
            //    {
            //        string authority;
            //        RequestParameters parameters = new RequestParameters("eefcd17b-6b67-4261-b8e0-2bd14e059957", (tmporder.TotalPrice * 10).ToString(), "خرید از وب سایت استریو 8", "http://stereo8.ir/Verify?NidOrder=" + NidOrder, tmporder.Tel ?? "", tmporder.Email ?? "");

            //        var json = JsonConvert.SerializeObject(parameters);

            //        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            //        HttpResponseMessage response = await client.PostAsync("https://api.zarinpal.com/pg/v4/payment/request.json", content);

            //        string responseBody = await response.Content.ReadAsStringAsync();

            //        JObject jo = JObject.Parse(responseBody);
            //        string errorscode = jo["errors"].ToString();

            //        JObject jodata = JObject.Parse(responseBody);
            //        string dataauth = jodata["data"].ToString();


            //        if (dataauth != "[]")
            //        {
            //            authority = jodata["data"]["authority"].ToString();

            //            string gatewayUrl = "https://www.zarinpal.com/pg/StartPay/" + authority;

            //            return Redirect(gatewayUrl);

            //        }
            //        else
            //        {
            //            TempData["dargahRedirectError"] = "در انتقال به درگاه خطایی رخ داده است.لطفا مجدد امتحان کنید";
            //            return RedirectToAction("Checkout", "Home");
            //        }
            //    }
            //}

            //catch (Exception ex)
            //{
            //    TempData["dargahRedirectError"] = "در انتقال به درگاه خطایی رخ داده است.لطفا مجدد امتحان کنید";
            //    return RedirectToAction("Checkout", "Home");
            //}
            return View();
        }
        public async Task<ActionResult> Verify(Guid NidOrder)
        {
            //Order tmpOrder = _db.GetOrderById(NidOrder);
            //var Niduser = Guid.Parse(UsersAuth.GetSpecificClaim(Request.Cookies["Stereo8Login"], 2));
            //List<Cart> carts = _db.GetCartsByUserId(Niduser).ToList();
            //try
            //{
            //    string authority = "";
            //    VerifyParameters parameters = new VerifyParameters();
            //    if (HttpContext.Request.Query["Authority"] != "")
            //    {
            //        authority = HttpContext.Request.Query["Authority"];
            //    }

            //    parameters.authority = authority;

            //    parameters.amount = (tmpOrder.TotalPrice * 10).ToString();

            //    parameters.merchant_id = "eefcd17b-6b67-4261-b8e0-2bd14e059957";


            //    using (HttpClient client = new HttpClient())
            //    {

            //        var json = JsonConvert.SerializeObject(parameters);

            //        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            //        HttpResponseMessage response = await client.PostAsync("https://api.zarinpal.com/pg/v4/payment/verify.json", content);

            //        string responseBody = await response.Content.ReadAsStringAsync();

            //        JObject jodata = JObject.Parse(responseBody);

            //        string data = jodata["data"].ToString();

            //        JObject jo = JObject.Parse(responseBody);

            //        string errors = jo["errors"].ToString();

            //        if (data != "[]")
            //        {
            //            string refid = jodata["data"]["ref_id"].ToString();

            //            tmpOrder.RefId = long.Parse(refid);
            //            tmpOrder.State = byte.Parse(jodata["data"]["code"].ToString());
            //            if (_db.UpdateOrder(tmpOrder))
            //            {
            //                foreach (var cart in carts)
            //                {
            //                    _db.UpdateAvailableCount(cart.ProductId, cart.Quantity);
            //                    _db.Remove<Cart>(cart);
            //                }
            //                int cartCount = _db.GetCartCountByUserId(Niduser);
            //                if (Request.Cookies.ContainsKey("CartCount"))
            //                    Response.Cookies.Delete("CartCount");
            //                HttpContext.Response.Cookies.Append("CartCount", cartCount.ToString(), new CookieOptions() { Expires = DateTime.Now.AddDays(14), HttpOnly = true, Path = "/" });
            //                var tmpship = _db.GetShipByOrderId(tmpOrder.NidOrder);
            //                tmpship.State = 1;//paid
            //                _db.UpdateShip(tmpship);
            //            }
            //        }
            //        else if (errors != "[]")
            //        {

            //            string errorscode = jo["errors"]["code"].ToString();
            //            tmpOrder.State = int.Parse(errorscode);
            //            _db.UpdateOrder(tmpOrder);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tmpOrder.State = 102;//exception occured
            //    _db.UpdateOrder(tmpOrder);
            //}
            //return View(new CheckoutViewModel() { OrderDetails = _db.GetOrderDetailsByOrderId(tmpOrder.NidOrder), Order = tmpOrder });
            return View();
        }
    }
}