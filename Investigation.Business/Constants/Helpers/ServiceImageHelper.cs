using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using AngleSharp.Text;
using Microsoft.AspNetCore.Http;

namespace Investigation.Business.Constants.Helpers
{
    public static class ServiceImageHelper
    {
        public static void ImageValidation(IFormFile? image)
        {
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
            {
                throw new ArgumentException("Only JPG, PNG, and WEBP files are allowed.");
            }

            var allowedMimeTypes = new[] { "image/jpeg", "image/jpg", "image/png", "image/webp" };
            if (!allowedMimeTypes.Contains(image.ContentType.ToLowerInvariant()))
            {
                throw new ArgumentException("Invalid MIME type.");
            }

            if (!FileValidationHelper.IsValidImage(image))
            {
                throw new ArgumentException("Geçersiz resim dosyası. Desteklenen formatlar: JPG, JPEG, PNG, WEBP");
            }

            if (!ImageSecurityHelper.IsValidImageSignature(image))
            {
                throw new ArgumentException("File signature does not match image format.");
            }

            //if (ImageSecurityHelper.ContainsMaliciousPatterns(image))
            //{
            //    throw new ArgumentException("File contains suspicious content.");
            //}
           
            if (!ImageSecurityHelper.IsValidImageContent(image))
            {
                throw new ArgumentException("The file content is not a valid image.");
            }
        }
        public async static Task<string> AnnouncememntImageResize(IFormFile? image)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user/announcement/");
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Path is preparing: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);

            long fileSizeKB = image.Length / 1024;

            bool isNotPng = Path.GetExtension(image.FileName).ToLower() != ".png";
            if (fileSizeKB <= 130 && isNotPng)
            {
                using var originalImage = await Image.LoadAsync(image.OpenReadStream());

                originalImage.Metadata.ExifProfile = null;
                originalImage.Metadata.IccProfile = null;
                originalImage.Metadata.IptcProfile = null;
                originalImage.Metadata.XmpProfile = null;

                var encoder = new JpegEncoder
                {
                    Quality = 85,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                };

                await originalImage.SaveAsJpegAsync(filePath, encoder);
            }
            else
            {
                try
                {
                    using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);
                    using var loadedImage = await Image.LoadAsync(compressedStream);

                    loadedImage.Metadata.ExifProfile = null;
                    loadedImage.Metadata.IccProfile = null;
                    loadedImage.Metadata.IptcProfile = null;
                    loadedImage.Metadata.XmpProfile = null;

                    var encoder = new JpegEncoder
                    {
                        Quality = 85,
                        ColorType = JpegEncodingColor.YCbCrRatio420
                    };

                    await loadedImage.SaveAsJpegAsync(filePath, encoder);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred while adding the entity.", ex);
                }
            }
            //if (fileSizeKB <= 130 && isNotPng)
            //{
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await image.CopyToAsync(stream);
            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);

            //        using (var fileStream = new FileStream(filePath, FileMode.Create))
            //        {
            //            await compressedStream.CopyToAsync(fileStream);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("An unexpected error occurred while adding the entity.", ex);
            //    }
            //}
            return fileName;
        }
        public async static Task<string> BlogImageResize(IFormFile? image)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user/blog/");
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Path is preparing: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);

            long fileSizeKB = image.Length / 1024;

            bool isNotPng = Path.GetExtension(image.FileName).ToLower() != ".png";
            if (fileSizeKB <= 130 && isNotPng)
            {
                // Sadece metadata'yı temizle, boyutlandırma yapma
                using var originalImage = await Image.LoadAsync(image.OpenReadStream());

                originalImage.Metadata.ExifProfile = null;
                originalImage.Metadata.IccProfile = null;
                originalImage.Metadata.IptcProfile = null;
                originalImage.Metadata.XmpProfile = null;

                await originalImage.SaveAsJpegAsync(filePath, new JpegEncoder
                {
                    Quality = 85,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                });
            }
            else
            {
                // Dosya büyükse veya PNG ise sıkıştır
                using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);
                using var loadedImage = await Image.LoadAsync(compressedStream);

                loadedImage.Metadata.ExifProfile = null;
                loadedImage.Metadata.IccProfile = null;
                loadedImage.Metadata.IptcProfile = null;
                loadedImage.Metadata.XmpProfile = null;

                await loadedImage.SaveAsJpegAsync(filePath, new JpegEncoder
                {
                    Quality = 85,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                });
            }
            return fileName;
        }
        public async static Task<string> CompanyImageResize(IFormFile? image)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user/company/");
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Path is preparing: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);

            long fileSizeKB = image.Length / 1024;

            bool isNotPng = Path.GetExtension(image.FileName).ToLower() != ".png";
            if (fileSizeKB <= 130 && isNotPng)
            {
                using var originalImage = await Image.LoadAsync(image.OpenReadStream());

                originalImage.Metadata.ExifProfile = null;
                originalImage.Metadata.IccProfile = null;
                originalImage.Metadata.IptcProfile = null;
                originalImage.Metadata.XmpProfile = null;

                var encoder = new JpegEncoder
                {
                    Quality = 85,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                };

                await originalImage.SaveAsJpegAsync(filePath, encoder);
            }
            else
            {
                try
                {
                    using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);
                    using var loadedImage = await Image.LoadAsync(compressedStream);

                    loadedImage.Metadata.ExifProfile = null;
                    loadedImage.Metadata.IccProfile = null;
                    loadedImage.Metadata.IptcProfile = null;
                    loadedImage.Metadata.XmpProfile = null;

                    var encoder = new JpegEncoder
                    {
                        Quality = 85,
                        ColorType = JpegEncodingColor.YCbCrRatio420
                    };

                    await loadedImage.SaveAsJpegAsync(filePath, encoder);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred while adding the entity.", ex);
                }
            }
            return fileName;
        }
        public async static Task<string> CompanyTeamImageResize(IFormFile? image)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user/company/team/");
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Path is preparing: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);

            long fileSizeKB = image.Length / 1024;

            bool isNotPng = Path.GetExtension(image.FileName).ToLower() != ".png";
            if (fileSizeKB <= 130 && isNotPng)
            {
                using var originalImage = await Image.LoadAsync(image.OpenReadStream());

                originalImage.Metadata.ExifProfile = null;
                originalImage.Metadata.IccProfile = null;
                originalImage.Metadata.IptcProfile = null;
                originalImage.Metadata.XmpProfile = null;

                var encoder = new JpegEncoder
                {
                    Quality = 85,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                };

                await originalImage.SaveAsJpegAsync(filePath, encoder);
            }
            else
            {
                try
                {
                    using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);
                    using var loadedImage = await Image.LoadAsync(compressedStream);

                    loadedImage.Metadata.ExifProfile = null;
                    loadedImage.Metadata.IccProfile = null;
                    loadedImage.Metadata.IptcProfile = null;
                    loadedImage.Metadata.XmpProfile = null;

                    var encoder = new JpegEncoder
                    {
                        Quality = 85,
                        ColorType = JpegEncodingColor.YCbCrRatio420
                    };

                    await loadedImage.SaveAsJpegAsync(filePath, encoder);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred while adding the entity.", ex);
                }
            }
            return fileName;
        }
        public async static Task<string> InvestorImageResize(IFormFile? image)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user/investor/");
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Path is preparing: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);

            long fileSizeKB = image.Length / 1024;

            bool isNotPng = Path.GetExtension(image.FileName).ToLower() != ".png";
            if (fileSizeKB <= 130 && isNotPng)
            {
                using var originalImage = await Image.LoadAsync(image.OpenReadStream());

                originalImage.Metadata.ExifProfile = null;
                originalImage.Metadata.IccProfile = null;
                originalImage.Metadata.IptcProfile = null;
                originalImage.Metadata.XmpProfile = null;

                var encoder = new JpegEncoder
                {
                    Quality = 85,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                };

                await originalImage.SaveAsJpegAsync(filePath, encoder);
            }
            else
            {
                try
                {
                    using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);
                    using var loadedImage = await Image.LoadAsync(compressedStream);

                    loadedImage.Metadata.ExifProfile = null;
                    loadedImage.Metadata.IccProfile = null;
                    loadedImage.Metadata.IptcProfile = null;
                    loadedImage.Metadata.XmpProfile = null;

                    var encoder = new JpegEncoder
                    {
                        Quality = 85,
                        ColorType = JpegEncodingColor.YCbCrRatio420
                    };

                    await loadedImage.SaveAsJpegAsync(filePath, encoder);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred while adding the entity.", ex);
                }
            }
            return fileName;
        }
        public async static Task<string> PostImageResize(IFormFile? image)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user/post/");
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Path is preparing: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);

            long fileSizeKB = image.Length / 1024;

            bool isNotPng = Path.GetExtension(image.FileName).ToLower() != ".png";
            if (fileSizeKB <= 130 && isNotPng)
            {
                using var originalImage = await Image.LoadAsync(image.OpenReadStream());

                originalImage.Metadata.ExifProfile = null;
                originalImage.Metadata.IccProfile = null;
                originalImage.Metadata.IptcProfile = null;
                originalImage.Metadata.XmpProfile = null;

                var encoder = new JpegEncoder
                {
                    Quality = 85,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                };

                await originalImage.SaveAsJpegAsync(filePath, encoder);
            }
            else
            {
                try
                {
                    using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);
                    using var loadedImage = await Image.LoadAsync(compressedStream);

                    loadedImage.Metadata.ExifProfile = null;
                    loadedImage.Metadata.IccProfile = null;
                    loadedImage.Metadata.IptcProfile = null;
                    loadedImage.Metadata.XmpProfile = null;

                    var encoder = new JpegEncoder
                    {
                        Quality = 85,
                        ColorType = JpegEncodingColor.YCbCrRatio420
                    };

                    await loadedImage.SaveAsJpegAsync(filePath, encoder);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred while adding the entity.", ex);
                }
            }
            return fileName;
        }
        public async static Task<string> RecentlyInvestImageResize(IFormFile? image)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user/investor/recently/");
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Path is preparing: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);

            long fileSizeKB = image.Length / 1024;

            bool isNotPng = Path.GetExtension(image.FileName).ToLower() != ".png";
            if (fileSizeKB <= 130 && isNotPng)
            {
                using var originalImage = await Image.LoadAsync(image.OpenReadStream());

                originalImage.Metadata.ExifProfile = null;
                originalImage.Metadata.IccProfile = null;
                originalImage.Metadata.IptcProfile = null;
                originalImage.Metadata.XmpProfile = null;

                var encoder = new JpegEncoder
                {
                    Quality = 85,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                };

                await originalImage.SaveAsJpegAsync(filePath, encoder);
            }
            else
            {
                try
                {
                    using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);
                    using var loadedImage = await Image.LoadAsync(compressedStream);

                    loadedImage.Metadata.ExifProfile = null;
                    loadedImage.Metadata.IccProfile = null;
                    loadedImage.Metadata.IptcProfile = null;
                    loadedImage.Metadata.XmpProfile = null;

                    var encoder = new JpegEncoder
                    {
                        Quality = 85,
                        ColorType = JpegEncodingColor.YCbCrRatio420
                    };

                    await loadedImage.SaveAsJpegAsync(filePath, encoder);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred while adding the entity.", ex);
                }
            }
            return fileName;
        }
        public async static Task<string> UserProfileImageResize(IFormFile? image)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user/profileImage/");
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Path is preparing: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);

            long fileSizeKB = image.Length / 1024;

            bool isNotPng = Path.GetExtension(image.FileName).ToLower() != ".png";
            if (fileSizeKB <= 130 && isNotPng)
            {
                using var originalImage = await Image.LoadAsync(image.OpenReadStream());

                originalImage.Metadata.ExifProfile = null;
                originalImage.Metadata.IccProfile = null;
                originalImage.Metadata.IptcProfile = null;
                originalImage.Metadata.XmpProfile = null;

                var encoder = new JpegEncoder
                {
                    Quality = 85,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                };

                await originalImage.SaveAsJpegAsync(filePath, encoder);
            }
            else
            {
                try
                {
                    using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);
                    using var loadedImage = await Image.LoadAsync(compressedStream);

                    loadedImage.Metadata.ExifProfile = null;
                    loadedImage.Metadata.IccProfile = null;
                    loadedImage.Metadata.IptcProfile = null;
                    loadedImage.Metadata.XmpProfile = null;

                    var encoder = new JpegEncoder
                    {
                        Quality = 85,
                        ColorType = JpegEncodingColor.YCbCrRatio420
                    };

                    await loadedImage.SaveAsJpegAsync(filePath, encoder);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred while adding the entity.", ex);
                }
            }
            return fileName;
        }
        public async static Task<string> MultipleBlogImageResize(IFormFile? image)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user/blog/multiple/");
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Path is preparing: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);

            long fileSizeKB = image.Length / 1024;

            bool isNotPng = Path.GetExtension(image.FileName).ToLower() != ".png";
            if (fileSizeKB <= 130 && isNotPng)
            {
                using var originalImage = await Image.LoadAsync(image.OpenReadStream());

                originalImage.Metadata.ExifProfile = null;
                originalImage.Metadata.IccProfile = null;
                originalImage.Metadata.IptcProfile = null;
                originalImage.Metadata.XmpProfile = null;

                var encoder = new JpegEncoder
                {
                    Quality = 85,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                };

                await originalImage.SaveAsJpegAsync(filePath, encoder);
            }
            else
            {
                try
                {
                    using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);
                    using var loadedImage = await Image.LoadAsync(compressedStream);

                    loadedImage.Metadata.ExifProfile = null;
                    loadedImage.Metadata.IccProfile = null;
                    loadedImage.Metadata.IptcProfile = null;
                    loadedImage.Metadata.XmpProfile = null;

                    var encoder = new JpegEncoder
                    {
                        Quality = 85,
                        ColorType = JpegEncodingColor.YCbCrRatio420
                    };

                    await loadedImage.SaveAsJpegAsync(filePath, encoder);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred while adding the entity.", ex);
                }
            }
            //if (fileSizeKB <= 130 && isNotPng)
            //{
            //    using (var stream = new FileStream(filePath, FileMode.Create))
            //    {
            //        await image.CopyToAsync(stream);
            //    }
            //}
            //else
            //{
            //    try
            //    {
            //        using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);

            //        using (var fileStream = new FileStream(filePath, FileMode.Create))
            //        {
            //            await compressedStream.CopyToAsync(fileStream);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw new Exception("An unexpected error occurred while adding the entity.", ex);
            //    }
            //}
            return fileName;
        }
        public async static Task<string> MultipleCompanyImageResize(IFormFile? image)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user/company/multiple/");
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Path is preparing: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);

            long fileSizeKB = image.Length / 1024;

            bool isNotPng = Path.GetExtension(image.FileName).ToLower() != ".png";
            if (fileSizeKB <= 130 && isNotPng)
            {
                using var originalImage = await Image.LoadAsync(image.OpenReadStream());

                originalImage.Metadata.ExifProfile = null;
                originalImage.Metadata.IccProfile = null;
                originalImage.Metadata.IptcProfile = null;
                originalImage.Metadata.XmpProfile = null;

                var encoder = new JpegEncoder
                {
                    Quality = 85,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                };

                await originalImage.SaveAsJpegAsync(filePath, encoder);
            }
            else
            {
                try
                {
                    using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);
                    using var loadedImage = await Image.LoadAsync(compressedStream);

                    loadedImage.Metadata.ExifProfile = null;
                    loadedImage.Metadata.IccProfile = null;
                    loadedImage.Metadata.IptcProfile = null;
                    loadedImage.Metadata.XmpProfile = null;

                    var encoder = new JpegEncoder
                    {
                        Quality = 85,
                        ColorType = JpegEncodingColor.YCbCrRatio420
                    };

                    await loadedImage.SaveAsJpegAsync(filePath, encoder);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred while adding the entity.", ex);
                }
            }
            return fileName;
        }
        public async static Task<string> MultiplePostImageResize(IFormFile? image)
        {
            var directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img/user/post/multiple/");
            if (!Directory.Exists(directoryPath))
            {
                Console.WriteLine($"Path is preparing: {directoryPath}");
                Directory.CreateDirectory(directoryPath);
            }

            var fileName = Guid.NewGuid().ToString() + ".jpg";
            var filePath = Path.Combine(directoryPath, fileName);

            long fileSizeKB = image.Length / 1024;

            bool isNotPng = Path.GetExtension(image.FileName).ToLower() != ".png";
            if (fileSizeKB <= 130 && isNotPng)
            {
                using var originalImage = await Image.LoadAsync(image.OpenReadStream());

                originalImage.Metadata.ExifProfile = null;
                originalImage.Metadata.IccProfile = null;
                originalImage.Metadata.IptcProfile = null;
                originalImage.Metadata.XmpProfile = null;

                var encoder = new JpegEncoder
                {
                    Quality = 85,
                    ColorType = JpegEncodingColor.YCbCrRatio420
                };

                await originalImage.SaveAsJpegAsync(filePath, encoder);
            }
            else
            {
                try
                {
                    using var compressedStream = await FileResizeHelper.CompressImageAsync(image, 130);
                    using var loadedImage = await Image.LoadAsync(compressedStream);

                    loadedImage.Metadata.ExifProfile = null;
                    loadedImage.Metadata.IccProfile = null;
                    loadedImage.Metadata.IptcProfile = null;
                    loadedImage.Metadata.XmpProfile = null;

                    var encoder = new JpegEncoder
                    {
                        Quality = 85,
                        ColorType = JpegEncodingColor.YCbCrRatio420
                    };

                    await loadedImage.SaveAsJpegAsync(filePath, encoder);
                }
                catch (Exception ex)
                {
                    throw new Exception("An unexpected error occurred while adding the entity.", ex);
                }
            }
            return fileName;
        }
    }
}