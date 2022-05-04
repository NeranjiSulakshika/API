namespace Backend.Model
{
    public class UploadFiles
    {
        public IFormFile File { get; set; }
    }

    public class UploadFilesResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public class ExcelBulkUploadParameter
    {   
        public string Name { get; set; }
    }
}
