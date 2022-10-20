namespace TestProject02.Models
{
    public class FileValidationModel
    {
        public string Message { get; set; }
        public bool Valid { get; set; }
        public List<IFormFile> List { get; set; }
    }
}
