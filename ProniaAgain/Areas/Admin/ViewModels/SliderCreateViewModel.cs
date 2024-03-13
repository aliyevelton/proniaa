using System.ComponentModel.DataAnnotations;

namespace ProniaAgain.Areas.Admin.ViewModels
{
    public class SliderCreateViewModel
    {
        public int Id { get; set; }
        public string? Offer { get; set; }
        [Required(ErrorMessage = "Bos qoyma")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public IFormFile Image { get; set; }
    }
}
