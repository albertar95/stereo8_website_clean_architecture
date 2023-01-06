using Microsoft.AspNetCore.Mvc;
using Domain;
using System;
using BackendUI.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata;
using System.Reflection;
using System.Security.Claims;
using Newtonsoft.Json;
using Application.DTO.Category;
using System.Text;
using Application.DTO.Type;
using Application.DTO.Brand;
using Application.DTO.Product;
using Application.Model;
using Application.DTO.File;
using Application.Helpers;
using Application.DTO.Order;
using Application.DTO.Ship;
using Application.DTO.Comment;

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
            //BaseAddress = configuration.GetSection("backendApiAddressDebug").Value;//for debug api
        }
        //category section
        public async Task<IActionResult> Categories()
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Category/GetCategoryList?includeProduct=true");
            if (result.IsSuccessfulResult())
                return View(JsonConvert.DeserializeObject<List<CategoryListDto>>(result.Content));
            else
                return RedirectToAction("StatusCodes", new { status = (int)result.ResultCode });
        }
        public async Task<IActionResult> AddCategory(CreateCategoryDto category)
        {
            var Content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Post, $"{BaseAddress}/Category/CreateCategory", Content);
            if(result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    TempData["CategorySuccess"] = "دسته بندی با موفقیت ایجاد گردید";
                else
                    TempData["CategoryError"] = "خطا در ایجاد دسته بندی.لطفا مجددا امتحان کنید";
            }else
                TempData["CategoryError"] = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید.";
            return RedirectToAction("Categories");
        }
        public async Task<IActionResult> Category(Guid NidCategory)
        {
            var model = new CategoryViewModel();
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Category/GetCategory/{NidCategory}?includeProduct=true");
            if (result.IsSuccessfulResult())
            {
                model.Category = JsonConvert.DeserializeObject<CategoryDto>(result.Content) ?? new CategoryDto();
                var result2 = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/File/GetFileList/{NidCategory}");
                if (result2.IsSuccessfulResult())
                    model.Files = JsonConvert.DeserializeObject<List<FileListDto>>(result2.Content) ?? new List<FileListDto>();
                return View(model);
            }
            else return RedirectToAction("StatusCodes", new { status = (int)result.ResultCode });
        }
        public async Task<IActionResult> EditType(Guid NidType)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Category/GetType/{NidType}");
            if (result.IsSuccessfulResult())
                return View(JsonConvert.DeserializeObject<TypeDto>(result.Content));
            else return RedirectToAction("StatusCodes", new { status = (int)result.ResultCode });
        }
        public async Task<IActionResult> EditBrand(Guid NidBrand)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Category/GetBrand/{NidBrand}");
            if (result.IsSuccessfulResult())
                return View(JsonConvert.DeserializeObject<BrandDto>(result.Content));
            else return RedirectToAction("StatusCodes", new { status = (int)result.ResultCode });
        }
        public async Task<IActionResult> EditCategory(UpdateCategoryDto category)
        {
            var Content = new StringContent(JsonConvert.SerializeObject(category),Encoding.UTF8,"application/json");
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Patch, $"{BaseAddress}/Category/UpdateCategory", Content);
            if(result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    TempData["CategoryPageSuccess"] = "دسته بندی با موفقیت ویرایش گردید";
                else
                    TempData["CategoryPageError"] = "خطا در ویرایش دسته بندی.لطفا مجددا امتحان کنید";
            }
            else
            {
                TempData["CategoryPageError"] = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید.";
            }
            return RedirectToAction("Category", new { NidCategory = category.Id });
        }
        public async Task<IActionResult> DeleteCategory(Guid NidCategory)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Delete, $"{BaseAddress}/Category/DeleteCategory/{NidCategory}");
            if (result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    TempData["CategorySuccess"] = "دسته بندی با موفقیت حذف گردید";
                else
                    TempData["CategoryError"] = "خطا در حذف دسته بندی.لطفا مجددا امتحان کنید";
            }
            else
            {
                TempData["CategoryError"] = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید.";
            }
            return RedirectToAction("Category", new { NidCategory = NidCategory });
        }
        public async Task<IActionResult> AddType(CreateTypeDto type)
        {
            var Content = new StringContent(JsonConvert.SerializeObject(type), Encoding.UTF8, "application/json");
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Post, $"{BaseAddress}/Category/CreateType", Content);
            if (result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    TempData["CategoryPageSuccess"] = "نوع با موفقیت ایجاد گردید";
                else
                    TempData["CategoryPageError"] = "خطا در ایجاد نوع.لطفا مجددا امتحان کنید";
            }
            else
                TempData["CategoryPageError"] = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید.";
            return RedirectToAction("Category", new { NidCategory = type.CategoryId });
        }
        public async Task<IActionResult> AddBrand(CreateBrandDto brand)
        {
            var Content = new StringContent(JsonConvert.SerializeObject(brand), Encoding.UTF8, "application/json");
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Post, $"{BaseAddress}/Category/CreateBrand", Content);
            if (result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    TempData["CategoryPageSuccess"] = "برند با موفقیت ایجاد گردید";
                else
                    TempData["CategoryPageError"] = "خطا در ایجاد برند.لطفا مجددا امتحان کنید";
            }
            else
                TempData["CategoryPageError"] = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید.";
            return RedirectToAction("Category", new { NidCategory = brand.CategoryId });
        }
        public async Task<IActionResult> DeleteType(Guid NidType,Guid NidCategory)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Delete, $"{BaseAddress}/Category/DeleteType/{NidType}");
            if (result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    TempData["CategoryPageSuccess"] = "نوع با موفقیت حذف گردید";
                else
                    TempData["CategoryPageError"] = "خطا در حذف نوع.لطفا مجددا امتحان کنید";
            }
            else
            {
                TempData["CategoryPageError"] = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید.";
            }
            return RedirectToAction("Category", new { NidCategory = NidCategory });
        }
        public async Task<IActionResult> SubmitEditType(UpdateTypeDto type)
        {
            var Content = new StringContent(JsonConvert.SerializeObject(type), Encoding.UTF8, "application/json");
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Patch, $"{BaseAddress}/Category/UpdateType", Content);
            if (result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    TempData["CategoryPageSuccess"] = "نوع با موفقیت ویرایش گردید";
                else
                    TempData["CategoryPageError"] = "خطا در ویرایش نوع.لطفا مجددا امتحان کنید";
            }
            else
            {
                TempData["CategoryPageError"] = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید.";
            }
            return RedirectToAction("Category", new { NidCategory = type.CategoryId });
        }
        public async Task<IActionResult> DeleteBrand(Guid NidBrand, Guid NidCategory)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Delete, $"{BaseAddress}/Category/DeleteBrand/{NidBrand}");
            if (result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    TempData["CategoryPageSuccess"] = "برند با موفقیت حذف گردید";
                else
                    TempData["CategoryPageError"] = "خطا در حذف برند.لطفا مجددا امتحان کنید";
            }
            else
            {
                TempData["CategoryPageError"] = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید.";
            }
            return RedirectToAction("Category", new { NidCategory = NidCategory });
        }
        public async Task<IActionResult> SubmitEditBrand(UpdateBrandDto brand)
        {
            var Content = new StringContent(JsonConvert.SerializeObject(brand), Encoding.UTF8, "application/json");
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Patch, $"{BaseAddress}/Category/UpdateBrand", Content);
            if (result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    TempData["CategoryPageSuccess"] = "برند با موفقیت ویرایش گردید";
                else
                    TempData["CategoryPageError"] = "خطا در ویرایش برند.لطفا مجددا امتحان کنید";
            }
            else
            {
                TempData["CategoryPageError"] = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید.";
            }
            return RedirectToAction("Category", new { NidCategory = brand.CategoryId });
        }
        public async Task<IActionResult> CloseCategory(Guid NidCategory)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Delete, $"{BaseAddress}/Category/DeleteCategory/{NidCategory}");
            if (result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    TempData["CategorySuccess"] = "دسته بندی با موفقیت غیرفعال گردید";
                else
                    TempData["CategoryError"] = "خطا در غیرفعال کردن دسته بندی.لطفا مجددا امتحان کنید";
            }
            else
            {
                TempData["CategoryError"] = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید.";
            }
            return RedirectToAction("Categories");
        }
        //product section
        public async Task<IActionResult> Products()
        {
            ProductViewModel model = new ProductViewModel();
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Product/GetProductList", null);
            if (result.IsSuccessfulResult())
                model.Products = JsonConvert.DeserializeObject<List<ProductListDto>>(result.Content);
            else
                model.Products = new List<ProductListDto>();
            var result2 = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Category/GetCategoryList");
            if (result2.IsSuccessfulResult())
                model.Categories = JsonConvert.DeserializeObject<List<CategoryListDto>>(result2.Content);
            else
                model.Categories = new List<CategoryListDto>();

            return View(model);
        }
        public async Task<IActionResult> AddProduct()
        {
            ProductViewModel model = new ProductViewModel();
            var result2 = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Category/GetCategoryList");
            if (result2.IsSuccessfulResult())
                model.Categories = JsonConvert.DeserializeObject<List<CategoryListDto>>(result2.Content);
            else
                model.Categories = new List<CategoryListDto>();
            return View(model);
        }
        public async Task<IActionResult> SubmitAddProduct(CreateProductDto product)
        {
            product.UserId = Guid.Parse("7E84B450-03E5-4969-BDF7-CB4EEB4FEBA1");
            var content = new StringContent(JsonConvert.SerializeObject(product),Encoding.UTF8,"application/json");
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Post,$"{BaseAddress}/Product/CreateProduct",content);
            if(result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    TempData["ProductSuccess"] = "محصول با موفقیت ایجاد گردید";
                else
                    TempData["ProductError"] = "خطا در ایجاد محصول.لطفا مجددا امتحان کنید";
            }
            else
            {
                TempData["ProductError"] = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید.";
            }
            return RedirectToAction("Products");
        }
        public async Task<IActionResult> EditProduct(Guid NidProduct)
        {
            ProductViewModel model = new ProductViewModel();
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Product/GetProduct/{NidProduct}");
            if (result.IsSuccessfulResult())
                model.Product = JsonConvert.DeserializeObject<ProductDto>(result.Content) ?? new ProductDto();
            else
                model.Product = new ProductDto();
            var result2 = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Category/GetCategoryList");
            if (result2.IsSuccessfulResult())
                model.Categories = JsonConvert.DeserializeObject<List<CategoryListDto>>(result2.Content) ?? new List<CategoryListDto>();
            else
                model.Categories = new List<CategoryListDto>();
            var result3 = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/File/GetFileList/{NidProduct}");
            if (result2.IsSuccessfulResult())
                model.Files = JsonConvert.DeserializeObject<List<FileListDto>>(result3.Content) ?? new List<FileListDto>();
            else
                model.Categories = new List<CategoryListDto>();
            //productViewModel.Files = _commonAction.GetFiles(NidProduct);
            return View(model);
        }
        public async Task<IActionResult> SubmitEditProduct(UpdateProductDto product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product),Encoding.UTF8,"application/json");
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Patch, $"{BaseAddress}/Product/UpdateProduct",content);
            if(result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    TempData["ProductSuccess"] = "محصول با موفقیت ویرایش گردید";
                else
                    TempData["ProductError"] = "خطا در ویرایش محصول.لطفا مجددا امتحان کنید";
            }
            else
            {
                TempData["ProductError"] = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید.";
            }
            return RedirectToAction("Products");
        }
        public async Task<IActionResult> ProductDetail(Guid NidProduct)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Product/GetProductDetail/{NidProduct}");
            if (result.IsSuccessfulResult())
                return Json(new { success = true, html = RenderViewToString.RenderViewAsync(this, "_ProductDetail", JsonConvert.DeserializeObject<DetailProductDto>(result.Content), true) });
            else
                return Json(new { success = false });
        }
        public async Task<IActionResult> DeleteProduct(Guid NidProduct)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Delete, $"{BaseAddress}/Product/DeleteProduct/{NidProduct}");
            if (result.IsSuccessfulResult())
            {
                if (JsonConvert.DeserializeObject<bool>(result.Content))
                    return Json(new { success = true, message = "محصول با موفقیت حذف گردید" });
                else
                    return Json(new { success = false, message = "خطا در حذف محصول.لطفا مجددا امتحان کنید" });
            }
            else
            {
                return Json(new { success = false, message = "خطایی در برنامه رخ داده است.لطفا با پشتیبان تماس بگیرید." });
            }
        }
        public async Task<IActionResult> ProductFilter(int Index, string FilterText)
        {
            List<ProductListDto> products = new List<ProductListDto>();
            bool success = true;
            var filters = new ProductFilters();
            filters.FilterType = Index;
            switch (Index)
            {
                case 1:
                    filters.CategoryId = Guid.Parse(FilterText.Split(',')[0]);
                    filters.TypeId = Guid.Parse(FilterText.Split(',')[1]);
                    filters.BrandId = Guid.Parse(FilterText.Split(',')[2]);
                    break;
                case 2:
                    filters.FromDate = DateTime.Parse(FilterText.Split(',')[0]).Date;
                    filters.ToDate = DateTime.Parse(FilterText.Split(',')[1]).Date;
                    break;
                case 3:
                    filters.FromPrice = decimal.Parse(FilterText.Split(',')[0]);
                    filters.ToPrice = decimal.Parse(FilterText.Split(',')[1]);
                    break;
                case 4:
                    filters.AvailableCount = int.Parse(FilterText);
                    break;
            }
            var content = new StringContent(JsonConvert.SerializeObject(filters),Encoding.UTF8,"application/json");
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Post, $"{BaseAddress}/Product/GetFilteredProduct",content);
            success = result.IsSuccessfulResult();
            products = JsonConvert.DeserializeObject<List<ProductListDto>>(result.Content) ?? new List<ProductListDto>();
            return Json(new { success = success, html = RenderViewToString.RenderViewAsync(this, "_FilteredProduct", products, true) });
        }
        //file section
        public async Task<IActionResult> UploadFile(IFormCollection data, IList<IFormFile> files)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
            List<string[]> pics = new List<string[]>();
            bool status = true;
            foreach (var file in data.Files)
            {
                if (file.Length > 0)
                {
                    string newFileName = "Image_" + DateTime.Now.ToShortDateString().Replace('/', '_') + "_" + DateTime.Now.ToString("HH:mm:ss").Replace(':', '_') + "_" + 
                    file.FileName.Split('.')[0].Replace(' ', '_') + ".webp";
                    string filebase64 = "";
                    using (var ms = new MemoryStream())
                    {
                        file.CopyTo(ms);
                        filebase64 = Convert.ToBase64String(ms.ToArray());
                    }
                    var newCreateFile = new CreateFileDto() { RelateType = byte.Parse(data["fileType"].ToString()) , RelateId = Guid.Parse(data["RelateId"].ToString()),FileName = newFileName, 
                    FilePath = Path.Combine(configuration.GetSection("ImagesPathRoot").Value, newFileName), Width = int.Parse(data["ImageWidth"].ToString()), 
                    FileUrl = $"{configuration.GetSection("ImageUrlRoot").Value}{newFileName}", Height = int.Parse(data["ImageHeight"].ToString()), TheFile = filebase64};
                    var content = new StringContent(JsonConvert.SerializeObject(newCreateFile), Encoding.UTF8, "application/json");
                    var addresult = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Post, $"{BaseAddress}/File/CreateFile", content);
                    if (!addresult.IsSuccessfulResult())
                        status = false;
                }
            }
            if(status)
            {
                var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/File/GetFileList/{Guid.Parse(data["RelateId"].ToString())}");
                if (result.IsSuccessfulResult())
                {
                    var gfiles = JsonConvert.DeserializeObject<List<FileListDto>>(result.Content) ?? new List<FileListDto>();
                    foreach (var gfile in gfiles.Where(p => p.RelateType == byte.Parse(data["fileType"].ToString())))
                    {
                        pics.Add(new string[3] { gfile.Id.ToString(), gfile.FileUrl, gfile.FileName });
                    }
                    string tmpPic = await RenderViewToString.RenderViewAsync(this, "_FileDemo", pics, true);
                    return Json(new { success = true, pics = tmpPic });
                }
                else
                    return Json(new { success = true });
            }else
                return Json(new { success = false });
        }
        public async Task<IActionResult> DeleteFile(Guid NidFile)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Delete, $"{BaseAddress}/File/DeleteFile/{NidFile}");
            if(result.IsSuccessfulResult())
            {
                if(JsonConvert.DeserializeObject<bool>(result.Content))
                    return Json(new { success = true });
                else
                    return Json(new { success = false });
            }else
                return Json(new { success = false });
        }
        //purchase section
        public async Task<IActionResult> Orders()
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Purchase/GetOrders");
            if (result.IsSuccessfulResult())
                return View(JsonConvert.DeserializeObject<List<OrderListDto>>(result.Content));
            else
                return RedirectToAction("StatusCodes", new { status = (int)result.ResultCode });
        }
        public async Task<IActionResult> ClosedOrders()
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Purchase/GetOrders?state=1");
            if (result.IsSuccessfulResult())
                return View(JsonConvert.DeserializeObject<List<OrderListDto>>(result.Content));
            else
                return RedirectToAction("StatusCodes", new { status = (int)result.ResultCode });
        }
        public async Task<IActionResult> OrderDetail(Guid NidOrder)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Purchase/GetOrder/{NidOrder}");
            if (result.IsSuccessfulResult())
                return View(JsonConvert.DeserializeObject<OrderDto>(result.Content));
            else
                return RedirectToAction("StatusCodes", new { status = (int)result.ResultCode });
        }
        public async Task<IActionResult> Ships(byte state = 1)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/Purchase/GetShips?state={state}");
            if (result.IsSuccessfulResult())
                return View(new Tuple<List<ShipListDto>, byte>(JsonConvert.DeserializeObject<List<ShipListDto>>(result.Content) ?? new List<ShipListDto>(), state));
            else
                return RedirectToAction("StatusCodes", new { status = (int)result.ResultCode });
        }
        public async Task<IActionResult> UpdateShip(Guid NidShip, byte State)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Patch, $"{BaseAddress}/Purchase/UpdateShip/{NidShip}?state={State}");
            if (result.IsSuccessfulResult())
                return Json(new { success = true, message = "مرسوله با موفقیت بروزرسانی شد" });
            else
                return Json(new { success = false, message = "خطا در بروزرسانی مرسوله.لطفا مجددا امتحان کنید" });
        }
        //general section
        public async Task<IActionResult> Comments()
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/General/GetComments");
            if (result.IsSuccessfulResult())
                return View(JsonConvert.DeserializeObject<List<CommentListDto>>(result.Content));
            else
                return RedirectToAction("StatusCodes", new { status = (int)result.ResultCode });
        }
        public async Task<IActionResult> DeleteComment(Guid nidComment)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Delete, $"{BaseAddress}/General/DeleteComment/{nidComment}");
            if (result.IsSuccessfulResult())
                return Json(new { success = true, message = "نظر با موفقیت حذف شد." });
            else
                return Json(new { success = false, message = "خطا در حذف نظر" });
        }
        public async Task<IActionResult> AcceptComment(Guid nidComment)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Patch, $"{BaseAddress}/General/UpdateComment/{nidComment}?state=2");
            if (result.IsSuccessfulResult())
                return Json(new { success = true, message = "نظر با موفقیت تایید شد." });
            else
                return Json(new { success = false, message = "خطا در تایید نظر" });
        }
        public async Task<IActionResult> AcceptedComments()
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/General/GetComments?state=2");
            if (result.IsSuccessfulResult())
                return View(JsonConvert.DeserializeObject<List<CommentListDto>>(result.Content));
            else
                return RedirectToAction("StatusCodes", new { status = (int)result.ResultCode });
        }
        public async Task<IActionResult> UnAcceptComment(Guid nidComment)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Patch, $"{BaseAddress}/General/UpdateComment/{nidComment}?state=0");
            if (result.IsSuccessfulResult())
                return Json(new { success = true, message = "نظر با موفقیت بازگردانده شد." });
            else
                return Json(new { success = false, message = "خطا در بازگرداندن نظر" });
        }
        public async Task<IActionResult> Settings()
        {
            SettingViewModel model = new SettingViewModel();
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/General/GetSettings");
            if (result.IsSuccessfulResult())
                model.Settings = JsonConvert.DeserializeObject<List<Domain.Setting>>(result.Content) ?? new List<Setting>();
            var fileresult = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/File/GetCommonFiles");
            if (result.IsSuccessfulResult())
                model.Files = JsonConvert.DeserializeObject<List<FileListDto>>(result.Content) ?? new List<FileListDto>();
            return View(model);
        }
        public async Task<IActionResult> AddSetting(Setting setting)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Post, $"{BaseAddress}/General/CreateSetting",new StringContent(JsonConvert.SerializeObject(setting),Encoding.UTF8,"application/json"));
            if (result.IsSuccessfulResult())
                TempData["SettingSuccess"] = "تنظیمات با موفقیت ایجاد گردید";
            else
                TempData["SettingError"] = "خطا در ایجاد تنظیمات.لطفا مجددا امتحان کنید";

            return RedirectToAction("Settings");
        }
        public async Task<IActionResult> EditSetting(Setting setting)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Patch, $"{BaseAddress}/General/UpdateSetting", new StringContent(JsonConvert.SerializeObject(setting), Encoding.UTF8, "application/json"));
            if (result.IsSuccessfulResult())
                TempData["SettingSuccess"] = "تنظیمات با موفقیت ویرایش گردید";
            else
                TempData["SettingError"] = "خطا در ویرایش تنظیمات.لطفا مجددا امتحان کنید";
            return RedirectToAction("Settings");
        }
        public async Task<IActionResult> DeleteSetting(Guid NidSetting)
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Delete, $"{BaseAddress}/General/DeleteSetting/{NidSetting}");
            if (result.IsSuccessfulResult())
                TempData["SettingSuccess"] = "تنظیمات با حذف ایجاد گردید";
            else
                TempData["SettingError"] = "خطا در حذف تنظیمات.لطفا مجددا امتحان کنید";
            return RedirectToAction("Settings");
        }
        public IActionResult StatusCodes(int status)
        {
            return View(status);
        }
        //less importants
        public async Task<IActionResult> Index()
        {
            var result = await ApiCall.Call(ApiCall.ConsumerType.BackendUI,ApiCall.HttpMethods.Get, $"{BaseAddress}/General/GetIndexPageValues");
            if (result.IsSuccessfulResult())
                return View(JsonConvert.DeserializeObject<string[]>(result.Content) ?? new string[5] { "", "", "", "", "" });
            else
                return RedirectToAction("StatusCodes", new { status = (int)result.ResultCode });
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