using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace PeopleSearchDemo.Helper
{
    public static class FileHelper
    {
        // covert image to array
        public static byte[] ConvertStramToByeArray(Stream stream)
        {
            byte[] data;
            using (Stream inputStream = stream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
                return data;
            }
        }

        // get image fole from attachment
        public static Stream GetAttachmentStramFromHttpFiles(HttpFileCollectionBase attachments)
        {
            foreach (string file in attachments)
            {
                var fileContent = attachments[file];
                if (fileContent != null && fileContent.ContentLength > 0)
                {
                    return fileContent.InputStream;
                }
            }
            return null;

        }
    }
}