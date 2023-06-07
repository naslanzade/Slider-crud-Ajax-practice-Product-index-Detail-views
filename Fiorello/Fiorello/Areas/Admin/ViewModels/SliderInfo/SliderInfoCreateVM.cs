using System.ComponentModel.DataAnnotations;

namespace Fiorello.Areas.Admin.ViewModels.SliderInfo
{
    public class SliderInfoCreateVM
    {
        [Required]
        public List<IFormFile>  Images { get; set; }

        [Required]
        [MaxLength(20)]
        public string? Title { get; set; }

        [Required]
        [MaxLength(70)]
        public string? Description { get; set; }
    }
}
