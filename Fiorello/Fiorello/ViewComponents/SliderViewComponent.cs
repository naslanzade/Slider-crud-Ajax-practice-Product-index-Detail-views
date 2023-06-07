using Fiorello.Data;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Fiorello.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Fiorello.ViewComponents
{
    public class SliderViewComponent :ViewComponent
    {

        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;

        public SliderViewComponent(AppDbContext context,ISliderService sliderService)
        {
            _context = context;
            _sliderService = sliderService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAllByStatusAsync();
            SliderInfo sliderInfo = await _context.SlidersInfo.Where(m => !m.SoftDeleted).FirstOrDefaultAsync();
            SliderVM model = new()
            {
                SliderInfo = sliderInfo,
                Sliders = sliders

            };
            return await Task.FromResult((IViewComponentResult)View(model));
        }
    }
}
