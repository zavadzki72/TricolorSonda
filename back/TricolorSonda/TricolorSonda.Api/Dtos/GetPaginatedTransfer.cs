namespace TricolorSonda.Api.Dtos
{
    public record GetPaginatedTransfer
    {
        public int Size { get; init; } = 10;
        public int Page { get; init; } = 1;
    }
}
