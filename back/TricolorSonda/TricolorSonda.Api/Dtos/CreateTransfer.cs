using TricolorSonda.Api.Models;
using TricolorSonda.Api.Models.Enumerators;

namespace TricolorSonda.Api.Dtos
{
    public record CreateTransfer
    {
        public required string PlayerName { get; init; }
        public required string PlayerNickname { get; init; }
        public required Country PlayerNationality { get; init; }
        public required string PlayerNationalityFlag { get; init; }
        public required PlayerPosition PlayerPrincipalPosition { get; init; }
        public required List<PlayerPosition> PlayerPossiblePositions { get; init; }

        public required string OriginClubName { get; init; }
        public required string OriginClubImage { get; init; }
        public required string OriginClubCountryImage { get; init; }

        public required string DestinationClubName { get; init; }
        public required string DestinationClubImage { get; init; }
        public required string DestinationClubCountryImage { get; init; }

        public string? SourceName { get; init; }
        public string? SourceComments { get; init; }
        public DateTime? SourceDate { get; init; }
        public string? SourceLink { get; init; }

        public required TransferType Type { get; init; }
        public required TransferStatus Status { get; init; }
        public decimal? Value { get; init; }
        public decimal? SalaryValue { get; init; }
        public string? Observations { get; init; }
    }
}
