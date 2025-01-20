namespace WebFileManagement.StorageBroker.Service;

public class AzureStorageService: IStorageBrokerService
{
    public void UploadFile(string fileName, Stream fileStream)
    {
        throw new NotImplementedException();
    }

    public void CreateDirectory(string directoryPath)
    {
        throw new NotImplementedException();
    }

    public Stream DownloadFile(string fileName)
    {
        throw new NotImplementedException();
    }

    public void DownloadFile(string fileName, Stream fileStream)
    {
        throw new NotImplementedException();
    }

    public List<string> GetAllFilesAndDirectories(string directoryPath)
    {
        throw new NotImplementedException();
    }
}