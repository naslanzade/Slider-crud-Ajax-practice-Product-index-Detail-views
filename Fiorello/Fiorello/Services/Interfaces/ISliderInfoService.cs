using Fiorello.Areas.Admin.ViewModels.Slider;
using Fiorello.Areas.Admin.ViewModels.SliderInfo;
using Fiorello.Models;

namespace Fiorello.Services.Interfaces
{
    public interface ISliderInfoService
    {
        Task<List<SliderInfoVM>> GetAllMappedDatas();
        Task<List<SliderInfo>> GetAllAsync();
        Task<SliderInfo> GetByIdAsync(int id);
        SliderInfoDetailVM GetMappedData(SliderInfo info);
        Task DeleteAsync(int id);
        Task CreateAsync(List<IFormFile> images, SliderInfoCreateVM newInfo);
        Task EditAsync(SliderInfoEditVM request, IFormFile newImage);
        



    }
}
