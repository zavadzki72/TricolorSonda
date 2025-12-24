using TricolorSonda.Api.Dtos;
using TricolorSonda.Api.Infra;

namespace TricolorSonda.Api.Services
{
    public class TransferService
    {
        private readonly MongoContext _context;

        public TransferService(MongoContext context)
        {
            _context = context;
        }

        public async Task<PaginatedTransferResponse> Create(CreateTransfer request)
        {
            // @TODO: Fazer isso antes de subir
            throw new NotImplementedException();
        }

        public async Task<PaginatedTransferResponse> GetPaginated(GetPaginatedTransfer request)
        {
            // @TODO: Fazer isso antes de subir
            throw new NotImplementedException();
        }
    }
}
