using school.Model.Enum;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace school.Common.Tools
{
    public class FileHelper
    {

        public static string[] HelpFilePaths()
        {
            var xmlDocumentFiles = Directory.GetFiles(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"),
                "*.xml",
                SearchOption.TopDirectoryOnly);
            return xmlDocumentFiles;
        }
        /// <summary>
        /// 格式化字节数字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string FormatBytesStr(int bytes)
        {
            if (bytes > 1073741824)
            {
                return ((double)(bytes / 1073741824M)).ToString("f1") + "G";
            }
            if (bytes > 1048576)
            {
                return ((double)(bytes / 1048576M)).ToString("f1") + "M";
            }
            if (bytes > 1024)
            {
                return ((double)(bytes / 1024M)).ToString("f1") + "K";
            }
            return bytes.ToString() + "Bytes";
        }
        /// <summary>
        /// 验证文件类型
        /// </summary>
        private static bool CheckValidExt(string allType, string chkType)
        {
            var sArray = allType.Split('|');
            foreach (var item in sArray)
            {
                if (item.ToLower() == chkType.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 获取导出路径地址
        /// </summary>
        public static string GetFilePath(string fileName, HttpContext httpContext = null)
        {
            if (httpContext == null) httpContext = HttpContext.Current;
            var directory = "/export/";
            var path = httpContext.Server.MapPath(directory);
            if (!IsExistDirectory(path))
            {
                CreateDirectory(path);
            }
            return path + fileName;
        }
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        public static void CreateDirectory(string directoryPath)
        {
            //如果目录不存在则创建该目录
            if (!IsExistDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        /// <summary>
        /// 根据导出类型获取扩展名
        /// </summary>
        public static string GetExtension(FileType fileType)
        {
            var extensionName = "";
            switch (fileType)
            {
                case FileType.Jpeg:
                    extensionName = ".jpeg";
                    break;
                case FileType.Jpg:
                    extensionName = ".jpg";
                    break;
                case FileType.Bmp:
                    extensionName = ".bmp";
                    break;
                case FileType.Gif:
                    extensionName = ".gif";
                    break;
                case FileType.Png:
                    extensionName = ".png";
                    break;
                case FileType.Doc:
                    extensionName = ".doc";
                    break;
                case FileType.Docx:
                    extensionName = ".docx";
                    break;
                case FileType.MP4:
                    extensionName = ".mp4";
                    break;
                case FileType.PDF:
                    extensionName = ".pdf";
                    break;
                case FileType.EXCEL:
                    extensionName = ".xlsx";
                    break;
                case FileType.SAS7BAT:
                    extensionName = ".sas7bat";
                    break;
                case FileType.XPT:
                    extensionName = ".xpt";
                    break;
                case FileType.XML:
                    extensionName = ".xml";
                    break;
                case FileType.Other:
                    break;
            }
            return extensionName;
        }
    }
}
