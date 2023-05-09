﻿using Blog.Entitiy.Enums;
using Blog.Entitiy.ViewModels_DTOs.Images;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace Blog.Service.Helpers.Images
{
    public class ImageHelper : IImageHelper
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string wwwroot;
        private const string imgFolder = "images";
        private const string articleImagesFolder = "article-images";
        private const string UserImagesFolder = "user-images";

        public ImageHelper(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            wwwroot = webHostEnvironment.WebRootPath;
        }
        private string ReplaceInvalidChars(string fileName)
        {
            return fileName.Replace("İ", "I")
                 .Replace("ı", "i")
                 .Replace("Ğ", "G")
                 .Replace("ğ", "g")
                 .Replace("Ü", "U")
                 .Replace("ü", "u")
                 .Replace("ş", "s")
                 .Replace("Ş", "S")
                 .Replace("Ö", "O")
                 .Replace("ö", "o")
                 .Replace("Ç", "C")
                 .Replace("ç", "c")
                 .Replace("é", "")
                 .Replace("!", "")
                 .Replace("'", "")
                 .Replace("^", "")
                 .Replace("+", "")
                 .Replace("%", "")
                 .Replace("/", "")
                 .Replace("(", "")
                 .Replace(")", "")
                 .Replace("=", "")
                 .Replace("?", "")
                 .Replace("_", "")
                 .Replace("*", "")
                 .Replace("æ", "")
                 .Replace("ß", "")
                 .Replace("@", "")
                 .Replace("€", "")
                 .Replace("<", "")
                 .Replace(">", "")
                 .Replace("#", "")
                 .Replace("$", "")
                 .Replace("½", "")
                 .Replace("{", "")
                 .Replace("[", "")
                 .Replace("]", "")
                 .Replace("}", "")
                 .Replace(@"\", "")
                 .Replace("|", "")
                 .Replace("~", "")
                 .Replace("¨", "")
                 .Replace(",", "")
                 .Replace(";", "")
                 .Replace("`", "")
                 .Replace(".", "")
                 .Replace(":", "")
                 .Replace(" ", "");
        }


        public async Task<ImageUploadedViewModel> Upload(string name, IFormFile imageFile, ImageType imageType, string folderName = null)
        {
           folderName ??= imageType == ImageType.User ? UserImagesFolder : articleImagesFolder;
            if (!Directory.Exists($"{wwwroot}/{imgFolder}/{folderName}")) ;
                Directory.CreateDirectory($"{wwwroot}/{imgFolder}/{folderName}");
            string oldFileName = Path.GetFileNameWithoutExtension(imageFile.FileName);
            string fileExtension = Path.GetExtension(imageFile.FileName);

            name = ReplaceInvalidChars(name);
            DateTime dateTime = DateTime.Now;
            string newFileName = $"{name}_{dateTime.Millisecond}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/{imgFolder}/{folderName}", newFileName);
            await using var stream =new FileStream(path, FileMode.Create,FileAccess.Write, FileShare.None, 1024 * 1024, useAsync:false);
            await imageFile.CopyToAsync(stream);
            await stream.FlushAsync();

            string message = imageType == ImageType.User 
                ? $"{newFileName} isimli kullanıcı resmi eklenmiştir." 
                : $"{newFileName} isimli makale resmi eklenmiştir.";
            return new ImageUploadedViewModel
            {
                FullName = $"{folderName}/{newFileName}"
            };
        }
        public void Delete(string imageName)
        {
            var fileToDelete = Path.Combine($"{wwwroot}/{imgFolder}/{imageName}");
            if (File.Exists(fileToDelete))
                File.Delete(fileToDelete);
        }

    }
}
