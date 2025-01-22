using Microsoft.AspNetCore.Mvc;
using WebFileManagement.Service.Service;
using Exception = System.Exception;

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
        directoryPath = directoryPath ?? string.Empty;
        directoryPath = Path.Combine(directoryPath, file.FileName);

        using (var stream = file.OpenReadStream())
        {
            _storageService.UploadFile(directoryPath, stream);
        }
    }
    [HttpPost("uploadFiles")]
    public void UploadFiles(List<IFormFile> files, string? directoryPath)
    {
        directoryPath = directoryPath ?? string.Empty;
        var mainPath = directoryPath;
        if (files == null || files.Count == 0)
        {
            throw new Exception("No files were uploaded.");
        }
        foreach (var file in files)
        {
            directoryPath = Path.Combine(mainPath, file.FileName);
            using (var stream = file.OpenReadStream())
            {
                _storageService.UploadFile(directoryPath, stream);
            }
        }
    }

    [HttpPost("createFolder")]
    public void CreateFolder(string folderPath)
    {
        _storageService.CreateDirectory(folderPath);
    }

    [HttpGet("getAll")]
    public List<string> GetAll(string? directoryPath)
    {
        directoryPath = directoryPath ?? string.Empty;
        var all = _storageService.GetAllFilesAndDirectories(directoryPath);
        return all;
    }

    [HttpGet("downloadFile")]
    public FileStreamResult DownloadFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new Exception("File is empty.");
        }
        var fileName = Path.GetFileName(filePath);
        var strem = _storageService.DownloadFile(filePath);
        FileStreamResult res = new FileStreamResult(strem, "application/octet-stream")
        {
           FileDownloadName = fileName
        };
         return res;
    }
   

    [HttpGet("downloadFolderAsZip")]
    public FileStreamResult DownloadFolderAsZip(string directoryPath)
    {
        if (string.IsNullOrEmpty(directoryPath))
        {
            throw new Exception("Directory is empty.");
        }
        var directoryName = Path.GetFileName(directoryPath);
        var stream = _storageService.DownloadFileAsZip(directoryPath);
        try
        {
            var res = new FileStreamResult(stream, "application/zip")
            {
                FileDownloadName = directoryName + ".zip"
            };
            var newRes = res;
            return newRes;
        }
        finally
        {
            _storageService.DeleteFile(directoryPath+".zip");
        }
    }

    [HttpDelete("deleteFile")]
    public void DeleteFile(string filePath)
    {
        _storageService.DeleteFile(filePath);
    }

    [HttpDelete("deleteDirectory")]
    public void DeleteDirectory(string directoryPath)
    {
        _storageService.DeleteDirectory(directoryPath);
    }
}












