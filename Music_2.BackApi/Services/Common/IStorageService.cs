using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Common
{
    public interface IStorageService
    {
        /// <summary>
        /// lấy url lưu file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string GetFileUrl(string fileName);
        /// <summary>
        /// lưu file
        /// </summary>
        /// <param name="mediaBinaryStream"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>

        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);
        /// <summary>
        /// xóa file
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>

        Task DeleteFileAsync(string fileName);
    }
}