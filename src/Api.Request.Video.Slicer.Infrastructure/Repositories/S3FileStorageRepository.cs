using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Api.Request.Video.Slicer.Infrastructure.Repository.Interfaces;

namespace Api.Request.Video.Slicer.Infrastructure.Repository
{
    public class S3FileStorageRepository : IFileStorageRepository
    {
        private readonly string _bucketName;
        private readonly RegionEndpoint _bucketRegion;
        private IAmazonS3 _s3Client;

        public S3FileStorageRepository()
        {
            _bucketName = "fiap-video-slicer";
            _bucketRegion = RegionEndpoint.GetBySystemName("sa-east-1");

            _s3Client = new AmazonS3Client(_bucketRegion);
        }

        public async Task<bool> DeleteFileAsync(string keyName)
        {
            try
            {
                DeleteObjectRequest deleteRequest = new DeleteObjectRequest
                {
                    BucketName = _bucketName,
                    Key = keyName
                };

                DeleteObjectResponse response = await _s3Client.DeleteObjectAsync(deleteRequest);

                return response.HttpStatusCode == System.Net.HttpStatusCode.NoContent;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error during deletion: " + e.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }
        }

        public async Task<bool> DoesFileExistAsync(string keyName)
        {
            try
            {
                GetObjectMetadataRequest request = new GetObjectMetadataRequest
                {
                    BucketName = _bucketName,
                    Key = keyName
                };

                await _s3Client.GetObjectMetadataAsync(request);
                return true;
            }
            catch (AmazonS3Exception e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Error checking file existence: " + e.Message);
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }
        }

        public async Task<byte[]> DownloadFileAsync(string keyName, string localFilePath)
        {
            try
            {
                GetObjectRequest getRequest = new GetObjectRequest
                {
                    BucketName = _bucketName,
                    Key = keyName
                };

                using (GetObjectResponse response = await _s3Client.GetObjectAsync(getRequest))
                using (Stream responseStream = response.ResponseStream)
                {
                    return ReadFully(responseStream);
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error during download: " + e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return null;
            }
        }

        private static byte[] ReadFully(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public async Task<bool> UploadFileAsync(byte[] data, string keyName)
        {
            using var inputStream = new MemoryStream(data, 0, data.Length);
            return await UploadFileAsync(inputStream, keyName);

        }

        public async Task<bool> UploadFileAsync(Stream stream, string keyName)
        {
            try
            {
                PutObjectRequest putRequest = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = keyName,
                    InputStream = stream,
                    ContentType = "video/mp4"
                };

                PutObjectResponse response = await _s3Client.PutObjectAsync(putRequest);

                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error during upload: " + e.Message);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }
        }
    }
}
