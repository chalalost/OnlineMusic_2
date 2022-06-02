using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Music_2.BackApi.Services.Common;
using Music_2.Data.EF;
using Music_2.Data.Models.Singer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Music_2.BackApi.Services.Singer
{
    public class SingerService : ISingerService
    {
        private readonly OnlineMusicDbContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";

        public SingerService(OnlineMusicDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        public async Task<int> Create(SingerCreateRequest request)
        {
            var sing = new Data.Entities.Singer()
            {
                Name = request.Name,
                MetaTitle = request.MetaTitle,
                Desciption = request.Desciption,
                Meta = request.Meta,
                Code = request.Code,
                ViewCount = 0
            };
            if (request.Image != null)
            {
                sing.Image = await this.SaveFile(request.Image);
            }
            _context.Singers.Add(sing);
            await _context.SaveChangesAsync();
            return (int)sing.SingerID;
        }

        public async Task<List<SingerViewModel>> GetAll()
        {
            var sing = await _context.Singers
                .Select(x => new SingerViewModel()
                {
                    SingerID = x.SingerID,
                    Name = x.Name,
                    MetaTitle = x.MetaTitle,
                    Desciption = x.Desciption,
                    Image = x.Image,
                    Code = x.Code,
                    ViewCount = x.ViewCount
                }).ToListAsync();
            return sing;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }
    }
}
