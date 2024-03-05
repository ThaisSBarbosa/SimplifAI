using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimplifAI.Utils
{
    internal static class ImageHelper
    {
        public static async Task<byte[]> ConvertAndCompressImage(Stream stream)
        {
            byte[] imageData;

            using (MemoryStream ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                imageData = ms.ToArray();
            }

            var imagemMenor = await CompressImage(imageData);

            using (var ms = new MemoryStream())
            {
                imagemMenor.CopyTo(ms);
                return ms.ToArray();
            }            
        }

        public static async Task<Stream> CompressImage(byte[] originalStream)
        {
            try
            {
                // Carregar a imagem original
                using (var originalBitmap = SKBitmap.Decode(originalStream))
                {
                    var originalBitmapRotated = Rotate(originalStream);
                    var width = originalBitmapRotated.Width;
                    var height = originalBitmapRotated.Height;

                    var resizedBitmap = originalBitmapRotated.Resize(new SKImageInfo(width, height), SKFilterQuality.High);

                    // Converter o bitmap redimensionado de volta para um stream
                    var compressedStream = new MemoryStream();
                    resizedBitmap.Encode(compressedStream, SKEncodedImageFormat.Jpeg, 100);

                    // Reiniciar a posição do stream para o início
                    compressedStream.Position = 0;

                    return compressedStream;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static SKBitmap Rotate(byte[] photo)
        {
            using (var bitmap = SKBitmap.Decode(photo))
            {


                var rotated = new SKBitmap(bitmap.Height, bitmap.Width);

                using (var surface = new SKCanvas(rotated))
                {
                    surface.Translate(rotated.Width, 0);
                    surface.RotateDegrees(90);
                    surface.DrawBitmap(bitmap, 0, 0);
                }

                return rotated;
            }
        }

    }
}
