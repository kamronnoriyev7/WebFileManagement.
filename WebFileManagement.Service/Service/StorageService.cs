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
        var parentPath = Directory.GetParent(filePath);
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

    
}


