using System;
using System.Collections.Generic;
using System.Text;

namespace RSync.Core.Enumerations
{
    public enum FileStatus
    {
        LocalFileNotFound = 0,

        ServerFileNotFound = 1,

        Uploading = 2,

        Uploaded = 3,

        Downloading = 4,

        Downloaded = 5,

        Removing = 6,

        Removed = 7,

        Synchronized = 8,

        Suspended = 9,
    }
}
