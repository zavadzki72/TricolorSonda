using TricolorSonda.Api.Models.Enumerators;

namespace TricolorSonda.Api.Dtos
{
    public record PaginatedTransferResponse
    {
        public int Page { get; init; }
        public int TotalItems { get; init; }
        public int TotalPages { get; init; }
        public List<string> Data { get; init; } = [];
    }

    public record TrasnferResponse
    {
        public required DateTime CreatedAt { get; init; }
        public required DateTime UpdatedAt { get; init; }
        public required TransferPlayerResponse Player { get; init; }
        public required TransferType Type { get; init; }
        public required TransferStatus Status { get; init; }
        public required ClubResponse OriginClub { get; init; }
        public required ClubResponse DestinationClub { get; init; }
        public decimal? Value { get; init; }
        public decimal? SalaryValue { get; init; }
        public string? Observations { get; init; }
        public List<SourceResponse> Sources { get; init; } = [];
    }

    public record TransferPlayerResponse
    {
        public required string Name { get; init; }
        public required string Nickname { get; init; }
        public required Country Nationality { get; init; }
        public required string NationalityFlag { get; init; }
        public required PlayerPosition PrincipalPosition { get; init; }
        public List<PlayerPosition> PossiblePositions { get; init; } = [];
    }

    public class ClubResponse
    {
        public required string Name { get; init; }
        public required string Image { get; init; }
        public required string CountryImage { get; init; }
    }

    public class SourceResponse
    {
        public required string Name { get; init; }
        public required string Comments { get; init; }
        public required DateTime Date { get; init; }
        public required string Link { get; init; }
    }
}
