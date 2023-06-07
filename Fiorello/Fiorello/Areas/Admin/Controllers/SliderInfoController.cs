using Fiorello.Areas.Admin.ViewModels.Category;
using Fiorello.Areas.Admin.ViewModels.Slider;
using Fiorello.Areas.Admin.ViewModels.SliderInfo;
using Fiorello.Data;
using Fiorello.Helpers;
using Fiorello.Models;
using Fiorello.Services;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderInfoController : Controller
    {

        private readonly ISliderInfoService _sliderInfoService;
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderInfoController(ISliderInfoService sliderInfoService,AppDbContext context,IWebHostEnvironment env)
        {
            
            _sliderInfoService = sliderInfoService;
            _context = context;
            _env = env;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _sliderInfoService.GetAllMappedDatas());
        }


        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            SliderInfo dbSliderInfo = await _sliderInfoService.GetByIdAsync((int)id);
            if (dbSliderInfo is null) return NotFound();

            return View(_sliderInfoService.GetMappedData(dbSliderInfo));
        }


        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderInfoCreateVM request)
        {           
            foreach (var item in request.Images)
            {

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("image", "Please select only image file");
                    return View();
                }

                if (item.CheckFileSize(200))
                {
                    ModelState.AddModelError("image", "Image size must be max 200KB");
                    return View();
                }
            }

            await _sliderInfoService.CreateAsync(request.Images, request);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int ? id)
        {
            if (id is null) return BadRequest();

            SliderInfo dbSliderInfo= await _sliderInfoService.GetByIdAsync((int)id);

            if (dbSliderInfo is null) return NotFound();

            return View(new SliderInfoEditVM
            {
                Image=dbSliderInfo.SignImage,
                Title=dbSliderInfo.Title,
                Description=dbSliderInfo.Description,

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ? id, SliderInfoEditVM request)
        {
            if (id is null) return BadRequest();

            SliderInfo existSliderInfo = await _context.SlidersInfo.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (existSliderInfo is null) return NotFound();

            if (existSliderInfo.Title.Trim() == request.Title)
            {
                return RedirectToAction(nameof(Index));
            }
            if(existSliderInfo.Description.Trim()==request.Description)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Please select only image file");
                request.Image = existSliderInfo.SignImage;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200KB");
                request.Image = existSliderInfo.SignImage;
                return View(request);
            }

            await _sliderInfoService.EditAsync(request, request.NewImage);


            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
          
            await _sliderInfoService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }






    }
}
