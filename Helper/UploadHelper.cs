using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Pharmm.API.Helper;
using ImageMagick;
using Microsoft.AspNetCore.Http;
using Minio;

namespace Pharmm.API.Helper
{
    public class UploadHelper
    {
        private static readonly MinioClient minio = new($"{UrlStaticHelper.minIoIp}",
            "12345678",
            "12345678"
        );

        public static async Task<(bool, string)> UploadPersonImageToMINIOAsync(
            IFormFile formFile,
            string BucketName,
            string ContentType,
            long MaxSizeLimitInByte,
            int ThumbnailWidth,
            int ThumbnailHeight)
        {
            if (!isValidFileSize(formFile.Length, MaxSizeLimitInByte))
            {
                return (false, $"ukuran file maksimal : {ConvertByteToMb(MaxSizeLimitInByte).ToString()} ");
            }

            if (!IsValidFileContentType(formFile.ContentType, ContentType))
            {
                return (false, "tipe file tidak sesuai");
            }

            //check if bucket not found create new
            bool BucketOriginal = await minio.BucketExistsAsync($"{BucketName}-original");
            if (!BucketOriginal)
            {
                await minio.MakeBucketAsync($"{BucketName}-original");
            }

            bool BucketThumbnail = await minio.BucketExistsAsync($"{BucketName}-thumbnail");
            if (!BucketThumbnail)
            {
                await minio.MakeBucketAsync($"{BucketName}-thumbnail");
            }

            string fileExtension = GetFileExtension(formFile.FileName);

            //generate new file name
            string NewFileName = $"Person-{DateTime.Now.Ticks}-{Guid.NewGuid()}{fileExtension}";

            MemoryStream filestream = new MemoryStream();

            await formFile.CopyToAsync(filestream);

            await using (filestream)
            {
                filestream.Position = 0;

                Stream resizeImage = ResizeImage(filestream, ThumbnailWidth, ThumbnailHeight);

                //upload original image
                await minio.PutObjectAsync(
                    $"{BucketName}-original",
                    NewFileName,
                    filestream,
                    filestream.Length,
                    ContentType
                );

                //upload thumbnail image
                await minio.PutObjectAsync(
                    $"{BucketName}-thumbnail",
                    NewFileName,
                    resizeImage,
                    resizeImage.Length,
                    ContentType
                );
            }

            return (true, NewFileName);
        }


        public static async Task<(bool, string)> UploadPdfToMINIOAsync(
            IFormFile formFile,
            string BucketName,
            long MaxSizeLimitInByte,
            string ContentType = "application/pdf")
        {
            if (!isValidFileSize(formFile.Length, MaxSizeLimitInByte))
            {
                return (false, $"ukuran file maksimal : {ConvertByteToMb(MaxSizeLimitInByte).ToString()} Mb");
            }

            if (!IsValidFileContentType(formFile.ContentType, ContentType))
            {
                return (false, "tipe file tidak sesuai");
            }

            //check if bucket not found create new
            bool BucketOriginal = await minio.BucketExistsAsync($"{BucketName}-document");
            if (!BucketOriginal)
            {
                await minio.MakeBucketAsync($"{BucketName}-document");
            }

            string fileExtension = GetFileExtension(formFile.FileName);

            //generate new file name
            string NewFileName = $"{StringHelper.CamelCase(BucketName,"-")}-{DateTime.Now.Ticks}-{Guid.NewGuid()}{fileExtension}";

            MemoryStream filestream = new MemoryStream();

            await formFile.CopyToAsync(filestream);

            await using (filestream)
            {
                filestream.Position = 0;

                //upload original image
                await minio.PutObjectAsync(
                    $"{BucketName}-document",
                    NewFileName,
                    filestream,
                    filestream.Length,
                    ContentType
                );

            }

            return (true, NewFileName);
        }

        //untuk get url dokumen by path. ex : testing\testing.pdf => http://192.168.1.99\testing\testing.pdf?ASDSDQWASasdaswqeqw
        public static async Task<string> GetFileLinkByPath(string path, Dictionary<string, string> reqParams)
        {
            //var reqParams = new Dictionary<string, string>{
            //            { "response-content-type", "application/pdf" }
            //        };

            //path example = bucketName\fileName

            string[] paths = path.Split("\\");
            string bucketName = "";
            string objectPath = "";  //object path  / filename

            if (paths.Length > 0)
            {
                bucketName = paths[0];
                objectPath = path.Remove(path.IndexOf(bucketName), bucketName.Length + 1);
            }

            return await UploadHelper.GetFileLinkAsync(
                    bucketName,
                    objectPath,
                    reqParams);
        }

        public static async Task<string> GetFileLinkAsync(
            string BucketName, string ObjectName, Dictionary<string, string> RequestParameter)
        {
            string presignedUrl = await minio.PresignedGetObjectAsync(
                BucketName, ObjectName, 604800, RequestParameter
            );
            return presignedUrl;
        }

        public static async Task RemoveObjectFromBucketByPath(string path)
        {

            //path example = bucketName\fileName

            string[] paths = path.Split("\\");
            string bucketName = "";
            string objectPath = "";  //object path  / filename

            if (paths.Length > 0)
            {
                bucketName = paths[0];
                objectPath = path.Remove(path.IndexOf(bucketName), bucketName.Length + 1);
            }

            await minio.RemoveObjectAsync(bucketName, objectPath);
        }

        public static async Task RemoveObjectFromBucket(string BucketName,
            string ObjectName)
        {
            await minio.RemoveObjectAsync(BucketName, ObjectName);
        }

        private static Stream ResizeImage(MemoryStream filestream, int width, int height)
        {
            Stream stream;
            using (var image = new MagickImage(filestream))
            {
                image.Resize(width, height);
                stream = new MemoryStream(image.ToByteArray());
            }

            return stream;
        }

        private static decimal ConvertByteToMb(long ByteSize)
        {
            return (decimal)ByteSize / 1048576;
        }

        private static bool isValidFileSize(long FileSizeInByte, long MaxSizeInByte)
        {
            if (FileSizeInByte > MaxSizeInByte)
            {
                return false;
            }

            return true;
        }

        private static bool IsValidFileContentType(string FileContentType, string ValidcontentType)
        {
            if (ValidcontentType == FileContentType)
            {
                return true;
            }

            return false;
        }

        private static string GetFileExtension(string FileName)
        {
            return Path.GetExtension(FileName);
        }
    }
}