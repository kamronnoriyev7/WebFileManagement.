namespace WebFileManagement.StorageBroker.Service;

public class LocalStorageBrokerService : IStorageBrokerService
{
    private string _dataPath;

    public LocalStorageBrokerService()
    {
        _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "data");
        if (!Directory.Exists(_dataPath))
        {
            Directory.CreateDirectory(_dataPath);
        }
    }

    public void UploadFile(string filePath, Stream stream)
    {
        filePath = Path.Combine(_dataPath, filePath);
        ValidateDirectory(filePath);
        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            fileStream.CopyTo(stream);  
        }
    }

    public void CreateDirectory(string directoryPath)
    {
        directoryPath = Path.Combine(_dataPath, directoryPath);
        ValidateDirectory(directoryPath);
        Directory.CreateDirectory(directoryPath);
    }
    

    public List<string> GetAllFilesAndDirectories(string directoryPath)
    {
        directoryPath = Path.Combine(_dataPath, directoryPath);
        var root = Directory.GetParent(directoryPath);
        if (!Directory.Exists(root?.FullName))
        {
            throw new Exception("folder does not exist");
        }
        var res = Directory.GetFileSystemEntries(directoryPath).ToList();
        return res;
    }
    private void ValidateDirectory(string directoryPath)
    {
        if (File.Exists(directoryPath))
        {
            throw new Exception("Folder already exists");    
        }
        var root = Directory.GetParent(directoryPath);
        if (!Directory.Exists(root?.FullName))
        {
            throw new Exception("folder does not exist");
        }
    }
}