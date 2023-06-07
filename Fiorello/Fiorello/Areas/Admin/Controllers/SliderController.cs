using Fiorello.Areas.Admin.ViewModels.Slider;
using Fiorello.Data;
using Fiorello.Helpers;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Fiorello.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ISliderService _sliderService;

        public SliderController(AppDbContext context, IWebHostEnvironment webHostEnvironment,ISliderService sliderService)
        {
            _context = context;
            _env = webHostEnvironment;
            _sliderService = sliderService;
        }
        public async Task<IActionResult> Index()
        {            
            return View(await _sliderService.GetAllMappedDatas());
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(SliderCreateVM request)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    if (!request.Image.CheckFileType("image/"))
        //    {
        //        ModelState.AddModelError("image", "Please select only image file");
        //        return View();
        //    }

        //    if (request.Image.CheckFileSize(200))
        //    {
        //        ModelState.AddModelError("image", "Image size must be max 200KB");
        //        return View();
        //    }



        //    string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;

        //    await request.Image.SaveFileAsync(fileName, _env.WebRootPath, "img");


        //    Slider slider = new()
        //    {
        //        SliderImage = fileName
        //    };

        //    await _context.Sliders.AddAsync(slider);

        //    await _context.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


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
            await _sliderService.CreateAsync(request.Images);
            return RedirectToAction(nameof(Index));

        }



        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            Slider dbSlider = await _sliderService.GetByIdAsync((int)id);

            if (dbSlider is null) return NotFound();          

            return View(_sliderService.GetMappedData(dbSlider));


        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
         
            await _sliderService.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            Slider dbSlider = await _sliderService.GetByIdAsync((int) id);

            if(dbSlider is null) return NotFound();

            return View(new SliderEditVM
            {
                Image=dbSlider.SliderImage,
            });

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id,SliderEditVM request)
        {
            if (id is null) return BadRequest();

            Slider dbSlider = await _sliderService.GetByIdAsync((int)id);

            if (dbSlider is null) return NotFound();

          
            if (!request.NewImage.CheckFileType("image/"))
            {
                ModelState.AddModelError("NewImage", "Please select only image file");
                request.Image = dbSlider.SliderImage;
                return View(request);
            }

            if (request.NewImage.CheckFileSize(200))
            {
                ModelState.AddModelError("NewImage", "Image size must be max 200KB");
                request.Image=dbSlider.SliderImage;
                return View(request);
            }

            if (request.NewImage is null) return RedirectToAction(nameof(Index));


            await _sliderService.EditAsync(dbSlider, request.NewImage);


            return RedirectToAction(nameof(Index));




        }


        [HttpPost]
        public async Task<IActionResult> ChangeStatus(int ? id)
        {

            if (id is null) return BadRequest();

            Slider slider = await _sliderService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();
            
            return Ok(await _sliderService.ChangeStatusAsync(slider));


        }



    }
}
