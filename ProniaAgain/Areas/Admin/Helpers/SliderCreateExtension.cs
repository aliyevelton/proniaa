namespace ProniaAgain.Areas.Admin.Helpers
{
    public static class SliderCreateExtension
    {
        public static bool IsImage(this IFormFile file, string fileType)
        {
            return file.ContentType.Contains(fileType);
        }
        public static bool IsSize(this IFormFile file, int size)
        {
            return file.Length / 1024 < size;
        }
    }
}
