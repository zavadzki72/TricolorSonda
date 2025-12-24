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

        // ❌ Bad: método sync com Task no retorno, não usa await
        public async Task<PaginatedTransferResponse> Create(CreateTransfer request)
        {
            // ❌ Bad: variável não usada
            var unused = "teste";

            // ❌ Bad: SQL Injection
            var query = "SELECT * FROM transfers WHERE player_name = " + request.PlayerName;

            // ❌ Bad: Task.Run sem await (fire and forget)
            Task.Run(() => Console.WriteLine("Processando..."));

            // ❌ Bad: exceção genérica
            try
            {
                // ❌ Bad: null check ignorado
                var data = request.Value;
                var value = data.Value; // Pode dar NullReferenceException

                // ❌ Bad: hardcoded credentials
                var password = "admin123";

                // ❌ Bad: lógica de negócio no serviço de infra
                if (value > 1000000)
                {
                    // ❌ Bad: magic number
                    value = value * (decimal)0.95;
                }
            }
            catch (Exception ex)
            {
                // ❌ Bad: engolir exceção
            }

            // ❌ Bad: retorno nulo em vez de objeto vazio
            return null;
        }

        public async Task<PaginatedTransferResponse> GetPaginated(GetPaginatedTransfer request)
        {
            // ❌ Bad: TODO em produção
            // @TODO: Fazer isso antes de subir

            // ❌ Bad: Thread.Sleep em código async
            Thread.Sleep(1000);

            throw new NotImplementedException();
        }
    }
}