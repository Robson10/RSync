using RSync.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RSync.Domain.Model
{
    public class File
    {
        [Key]
        public long FileId { get; set; }
        public int SetverId { get; set; }
        public string ServerFileName { get; set; }
        public string FileName { get; set; }
        public string ServerFilePath { get; set; }
        public string LocalFilePath { get; set; }
        public long Size { get; set; }
        
        [Column(TypeName ="tinyint")]
        public FileStatus FileStatus { get; set; }

        public File(int setverId, string serverFileName, string fileName, string serverFilePath, long size, FileStatus fileStatus)
        {
            SetverId = setverId;
            ServerFileName = serverFileName;
            FileName = fileName;
            ServerFilePath = serverFilePath;
            Size = size;
            FileStatus = fileStatus;
        }

        public File(int setverId, string serverFileName, string fileName, string serverFilePath, string localFilePath, long size, FileStatus fileStatus)
        {
            SetverId = setverId;
            ServerFileName = serverFileName;
            FileName = fileName;
            ServerFilePath = serverFilePath;
            LocalFilePath = localFilePath;
            Size = size;
            FileStatus = fileStatus;
        }
    }
}
