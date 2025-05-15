using BlobImage.Service;
using Microsoft.AspNetCore.Mvc;

namespace BlobImage.Controllers
{
    public class UploadController : Controller
    {

        private readonly IBlobService _blob;

        public UploadController(IBlobService blob)
        {
            _blob = blob;
        }

        [HttpGet] public IActionResult Index() => View();
        [HttpPost, ValidateAntiForgeryToken]

        public async Task<IActionResult> Index(IFormFile image)
        {
            if (image is { Length: > 0 })
            {
                var url = await _blob.UploadAsync(image.OpenReadStream(), Path.GetRandomFileName() + Path.GetExtension(image.FileName), image.ContentType);
                ViewBag.Url = url;
                ViewBag.Msg = "Image uploaded successfully";
            }
            else 
            {
                ViewBag.Msg = "Please select a valid image";
            }
            return View();

        }


    }
}
