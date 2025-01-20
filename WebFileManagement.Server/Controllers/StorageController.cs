using Microsoft.AspNetCore.Mvc;
using WebFileManagement.Service.Service;

namespace WebFileManagment.Server.Controllers;

public class StorageController: ControllerBase
{ 
    private readonly IStorageService _storageService;

    public StorageController(IStorageService storageService)
    {
        _storageService = storageService;
    }


    [HttpPost("uploadFile")]
    public void UploadFile(IFormFile file, string directoryPath)
    {
        directoryPath += file.FileName;
        using (var stream = file.OpenReadStream())
        {
            _storageService.UploadFile(directoryPath, stream);
        }
    }

    [HttpPost("createFolder")]
    public void CreateFolder(string folderPath)
    {
        _storageService.CreateDirectory(folderPath);
    }

    [HttpGet("getAll")]
    public List<string> GetAll(string directoryPath)
    {
        var all = _storageService.GetAllFilesAndDirectories(directoryPath);
        return all;
    }
    
}












