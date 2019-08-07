using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace SmartFleetManagementSystem.Helper
{
    public static class FileHelper
    {
        public static void SaveFile(byte[] content, string path)
        {
            string filePath = GetFileFullPath(path);
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            //Save file
            using (FileStream str = File.Create(filePath))
            {
                str.Write(content, 0, content.Length);
            }
        }

        public static string GetFileFullPath(string path)
        {
            string relName = "";
            if (path.IndexOf(":\\") > -1 || path.IndexOf(":/") > -1|| path.StartsWith("~"))
            {
                relName = path;
            }
            else if (path.StartsWith("/"))
            {
                relName = string.Concat("~", path);
            } 
            else
            {
                relName = string.Concat("~/", path);
            }

            string filePath = relName.StartsWith("~") ? HostingEnvironment.MapPath(relName) : relName;

            return filePath;
        }

        public static string CreateFolderIfNeeded(string path)
        {
            string result = "1";
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception ex)
                {
                    /*TODO: You must process this exception.*/
                    
                    result = ex.ToString();
                }
            }
            return result;
        }
        public static bool CreateFolder(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            else
            {
                result = false;
            }
            return result;
        }
    }
}