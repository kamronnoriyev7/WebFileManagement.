using Microsoft.AspNetCore.Http;

namespace WebFileManagement.Service.Service;

public interface IStorageService
{
    void UploadFile(string filePath, Stream stream);
    void CreateDirectory(string directoryPath);
    List<string> GetAllFilesAndDirectories(string directoryPath);
    Stream DownloadFile(string filePath);
    Stream DownloadFileAsZip(string directoryPath);
    void DeleteFile(string filePath);
    void DeleteDirectory(string directoryPath);
}