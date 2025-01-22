using System.IO.Compression;

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
        if (File.Exists(filePath))
        {
            throw new Exception("Folder already exists");    
        }
        using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            stream.CopyTo(fileStream); 
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
        res = res.Select(p => p.Remove(0, directoryPath.Length+1 )).ToList();
        return res;
    }

    public Stream DownloadFile(string filePath)
    {
        filePath = Path.Combine(_dataPath, filePath);
        if (!File.Exists(filePath))
        {
            throw new Exception("File not found");
        }

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return stream;
    }

    public Stream DownloadFileAsZip(string directoryPath)
    {
        if (Path.GetExtension(directoryPath) != string.Empty)
        {
            throw new Exception($"Directory is not directory ");
        }
        directoryPath = Path.Combine(_dataPath, directoryPath);
        if (!Directory.Exists(directoryPath))
        {
            throw new Exception("folder does not exist");
        }
        var zipPath = directoryPath + ".zip";
        ZipFile.CreateFromDirectory(directoryPath, zipPath);
        var stream = new FileStream(zipPath, FileMode.Open, FileAccess.Read);
        return stream;
    }

    public void DeleteFile(string filePath)
    {
       filePath = Path.Combine(_dataPath, filePath);
       if (!File.Exists(filePath))
       {
           throw new Exception("File not found");
       }
       File.Delete(filePath);
       
    }

    public void DeleteDirectory(string directoryPath)
    {
        directoryPath = Path.Combine(_dataPath, directoryPath);
        if (!Directory.Exists(directoryPath))
        {
            throw new Exception("folder does not exist");
        }
        Directory.Delete(directoryPath, true);
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