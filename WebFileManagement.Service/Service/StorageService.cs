using Microsoft.AspNetCore.Http;
using WebFileManagement.StorageBroker.Service;

namespace WebFileManagement.Service.Service;

public class StorageService:  IStorageBrokerService, IStorageService
{
    private readonly IStorageBrokerService _storageBrokerService;

    public StorageService(IStorageBrokerService storageBrokerService)
    {
        _storageBrokerService = storageBrokerService;
    }


    public void UploadFile(string filePath, Stream stream)
    {
        _storageBrokerService.UploadFile(filePath, stream);
    }

    public void CreateDirectory(string directoryPath)
    {
       _storageBrokerService.CreateDirectory(directoryPath); 
    }

    public List<string> GetAllFilesAndDirectories(string directoryPath)
    {
        return _storageBrokerService.GetAllFilesAndDirectories(directoryPath);
    }

    public Stream DownloadFile(string filePath)
    {
        return _storageBrokerService.DownloadFile(filePath);
    }

    public Stream DownloadFileAsZip(string directoryPath)
    {
        return _storageBrokerService.DownloadFileAsZip(directoryPath);
    }

    public void DeleteFile(string filePath)
    {
      _storageBrokerService.DeleteFile(filePath);
    }

    public void DeleteDirectory(string directoryPath)
    {
       _storageBrokerService.DeleteDirectory(directoryPath);
    }

}


