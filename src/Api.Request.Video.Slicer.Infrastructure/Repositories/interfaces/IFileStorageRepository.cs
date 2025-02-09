using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Api.Request.Video.Slicer.Infrastructure.Repository.Interfaces
{
    public interface IFileStorageRepository
    {
        Task<bool> UploadFileAsync(Stream stream, string keyName);

        Task<byte[]> DownloadFileAsync(string keyName, string localFilePath);

        Task<bool> DeleteFileAsync(string keyName);

        Task<bool> DoesFileExistAsync(string keyName);

    }
}
