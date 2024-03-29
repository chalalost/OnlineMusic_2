﻿using Music_2.Data.Models;
using Music_2.Data.Models.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.ApiIntegration.Language
{
    public interface ILanguageApiClient
    {
        /// <summary>
        /// api lấy ds ngôn ngữ
        /// </summary>
        /// <returns></returns>
        Task<ApiResult<List<LanguageViewModel>>> GetAll();
    }
}
