using Demo.BL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Demo.BL.Helper
{
    public static class FileUpload
    {
        public static string UploadFile(string RootPath , IFormFile File)
        {
            try
            {
                //Get Dir
                var FilePath = Directory.GetCurrentDirectory() + RootPath;

                //Get FileName
                var FileName = Guid.NewGuid() + Path.GetFileName(File.FileName);

                // Merge Path With FileName
                var FinalPath = Path.Combine(FilePath, FileName);

                // Save File As A Stream
                using (var stream = new FileStream(FinalPath, FileMode.Create))
                {
                    File.CopyTo(stream);
                }

                return FileName;
            }
            catch (Exception ex)
            {
                return "Failed To Upload : " + ex.Message;
            }


        }
        public static string RemoveFile(string FolderName, string FileName)
        {
            try
            {
                var DeletedPath = Directory.GetCurrentDirectory() + FolderName + FileName;
                if (File.Exists(DeletedPath))
                {
                    File.Delete(DeletedPath);
                }

                var result = "Deleted Successfully";
                return result;
            }
            catch (Exception ex)
            {
                return "Failed To Upload : " + ex.Message;
            }


        }
    }
}
