using Application.Persistance.Contracts;
using Infra.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistance.Repository
{
    public class FileRepository : BaseRepository, IFileRepository
    {
        private readonly AudioShopDbContext _context;

        public FileRepository(AudioShopDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.File>> GetCommonFiles(int RelateTypeStart = 16, int RelateTypeEnd = 29)
        {
            try
            {
                return await  _context.Files.Where(p => p.RelateType >= RelateTypeStart && p.RelateType <= RelateTypeEnd).ToListAsync() ?? new List<Domain.File>();
            }
            catch (Exception)
            {
                return new List<Domain.File>();
            }
        }

        public async Task<Domain.File> GetFile(Guid NidFile)
        {
            try
            {
                return await _context.Files.FirstOrDefaultAsync(p => p.Id == NidFile) ?? new Domain.File();
            }
            catch (Exception)
            {
                return new Domain.File();
            }
        }

        public async Task<IEnumerable<Domain.File>> GetFiles(Guid NidRelate)
        {
            try
            {
                return await _context.Files.Where(p => p.RelateId == NidRelate).ToListAsync() ?? new List<Domain.File>();
            }
            catch (Exception)
            {
                return new List<Domain.File>();
            }
        }
    }
}
