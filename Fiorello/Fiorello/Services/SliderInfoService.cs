using Fiorello.Areas.Admin.ViewModels.SliderInfo;
using Fiorello.Data;
using Fiorello.Helpers;
using Fiorello.Models;
using Fiorello.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Fiorello.Services
{
    public class SliderInfoService : ISliderInfoService
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderInfoService(AppDbContext context, IWebHostEnvironment env)
        {

            _context = context;
            _env = env;
        }

        public async Task CreateAsync(List<IFormFile> images, SliderInfoCreateVM newInfo)
        {
            foreach (var item in images)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                await item.SaveFileAsync(fileName, _env.WebRootPath, "img");


                SliderInfo sliderInfo = new()
                {
                    SignImage = fileName,
                    Title = newInfo.Title,
                    Description = newInfo.Description,
                };

                await _context.SlidersInfo.AddAsync(sliderInfo);

            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            SliderInfo sliderInfo = await GetByIdAsync(id);

            _context.SlidersInfo.Remove(sliderInfo);

            await _context.SaveChangesAsync();

            string path = Path.Combine(_env.WebRootPath, "img", sliderInfo.SignImage);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(SliderInfoEditVM request, IFormFile newImage)
        {
            string oldPath = Path.Combine(_env.WebRootPath, "img", newImage.FileName);

            if (File.Exists(oldPath))
            {
                File.Delete(oldPath);
            }

            string fileName = Guid.NewGuid().ToString() + "_" + newImage.FileName;

            await newImage.SaveFileAsync(fileName, _env.WebRootPath, "img");

            request.Image = fileName;

            SliderInfo sliderInfo = new()
            {
                Id = request.Id,
                Title = request.Title,
                Description = request.Description,
                SignImage = request.Image
            };

            _context.Update(sliderInfo);

            await _context.SaveChangesAsync();
        }

        public async Task<List<SliderInfo>> GetAllAsync()
        {
            return await _context.SlidersInfo.ToListAsync();
        }

        public async Task<List<SliderInfoVM>> GetAllMappedDatas()
        {
            List<SliderInfoVM> list = new();

            List<SliderInfo> infos = await GetAllAsync();

            foreach (SliderInfo info in infos)
            {
                SliderInfoVM model = new()
                {
                    Id = info.Id,
                    Title = info.Title,
                    Description = info.Description,
                    SignImage = info.SignImage,
                };

                list.Add(model);
            }
            

            return list;
        }

        public async Task<SliderInfo> GetByIdAsync(int id)
        {
            return await _context.SlidersInfo.FirstOrDefaultAsync(m => m.Id == id);
        }

        public SliderInfoDetailVM GetMappedData(SliderInfo info)
        {
            SliderInfoDetailVM model = new()
            {
                Title = info.Title,
                Description = info.Description,
                SignImage = info.SignImage,
                CreatedDate=info.CreatedDate.ToString("MM.dd.yyyy"),

            };
            return model;
        }
    }
}
