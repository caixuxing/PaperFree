using Microsoft.AspNetCore.Http;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System.IO;
using Trasen.PaperFree.Domain.Shared.CustomException;
using static System.Net.Mime.MediaTypeNames;

namespace Trasen.PaperFree.Domain.Shared.FileConversion
{
    public record FileConversionClass
    {
        /// <summary>
        /// 文件转换成Base64字符串
        /// </summary>
        /// <param name="fileName">文件绝对路径</param>
        /// <returns></returns>
        public static String FileToBase64(string fileName)
        {
            string strRet = "";

            try
            {
                FileStream fs = new FileStream(fileName, FileMode.Open);
                byte[] bt = new byte[fs.Length];
                fs.Read(bt, 0, bt.Length);
                strRet = Convert.ToBase64String(bt);
                fs.Close();
            }
            catch (Exception ex)
            {
                strRet = null;
            }

            return strRet;
        }

        /// <summary>
        /// Base64字符串转换成文件
        /// </summary>
        /// <param name="strInput">base64字符串</param>
        /// <param name="fileName">保存文件的绝对路径</param>
        /// <returns></returns>
        public static bool Base64ToFileAndSave(string strInput, string fileName)
        {
            bool bTrue = false;
            try
            {
                byte[] buffer = Convert.FromBase64String(strInput);
                FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate);
                fs.Write(buffer, 0, buffer.Length);
                fs.Close();
                bTrue = true;
            }
            catch (Exception ex)
            {
            }
            return bTrue;
        }
        /// <summary>
        /// 文件转byte[]
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async static Task<byte[]> ReadFileToByteAsync(string filePath)
        {
            if (!File.Exists(filePath))
                throw new BusinessException(Response.MessageType.Warn, "文件未找到。", filePath);

            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true);
            using var memoryStream = new MemoryStream();
            byte[] buffer = new byte[4096];
            int bytesRead;

            while ((bytesRead = await fileStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
            {
                await memoryStream.WriteAsync(buffer, 0, bytesRead);
            }
      
            return memoryStream.ToArray();
            //await FileReader.ReadFileBytesAsync(path);
        }

        /// <summary>
        /// 流转PDF保存文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public async static Task SaveFile(IFormFile fileIfrom, string filePath, string type)
        {

            byte[] file = await ConvertIFormFileToByteArray(fileIfrom);
            if (type.ToLower() == ".pdf")
            {
                using FileStream fs = new FileStream(filePath, FileMode.Create);
                await fs.WriteAsync(file, 0, file.Length);
                return;
            }
            using (MemoryStream pdfStream = new MemoryStream())
            {
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                using MemoryStream imageStream = new MemoryStream(file) ;
                    XImage image = XImage.FromStream(() => imageStream);
                    gfx.DrawImage(image, 0, 0);
                document.Save(pdfStream, false);
                document.Dispose();
                using FileStream fileStream = new FileStream($"{filePath}.PDF", FileMode.Create, FileAccess.Write);
                    await pdfStream.CopyToAsync(fileStream);
            }

        }
        public async static Task<byte[]> ConvertIFormFileToByteArray(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}