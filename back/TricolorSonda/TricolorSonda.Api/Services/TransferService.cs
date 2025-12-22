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
            throw new NotImplementedException();
        }

        public async Task<PaginatedTransferResponse> GetPaginated(GetPaginatedTransfer request)
        {
            throw new NotImplementedException();
        }
    }
}
