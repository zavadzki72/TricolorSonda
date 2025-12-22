using TricolorSonda.Api.Models.Enumerators;

namespace TricolorSonda.Api.Models
{
    public class Transfers
    {
        public Transfers(TransferPlayer player, TransferType type, TransferStatus status, Club originClub, Club destinationClub, string observations, List<Source> sources, decimal? value, decimal? salaryValue)
        {
            Id = Guid.CreateVersion7();

            Player = player;
            Type = type;
            Status = status;
            OriginClub = originClub;
            DestinationClub = destinationClub;
            Value = value;
            SalaryValue = salaryValue;
            Observations = observations;
            Sources = sources;

            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public TransferPlayer Player { get; private set; }
        public TransferType Type { get; private set; }
        public TransferStatus Status { get; private set; }
        public Club OriginClub { get; private set; }
        public Club DestinationClub { get; private set; }
        public decimal? Value { get; private set; }
        public decimal? SalaryValue { get; private set; }
        public string Observations { get; private set; }
        public List<Source> Sources { get; private set; }
    }

    public class TransferPlayer
    {
        public TransferPlayer(string name, string nickname, Country nationality, string nationalityFlag, PlayerPosition principalPosition, List<PlayerPosition> possiblePositions)
        {
            Name = name;
            Nickname = nickname;
            Nationality = nationality;
            NationalityFlag = nationalityFlag;
            PrincipalPosition = principalPosition;
            PossiblePositions = possiblePositions;
        }

        public string Name { get; private set; }
        public string Nickname { get; private set; }
        public Country Nationality { get; private set; }
        public string NationalityFlag { get; private set; }
        public PlayerPosition PrincipalPosition { get; private set; }
        public List<PlayerPosition> PossiblePositions { get; private set; }
    }

    public class Club
    {
        public Club(string name, string image, string countryImage)
        {
            Name = name;
            Image = image;
            CountryImage = countryImage;
        }

        public string Name { get; set; }
        public string Image { get; set; }
        public string CountryImage { get; set; }
    }

    public class Source
    {
        public Source(string name, string comments, DateTime date, string link)
        {
            Name = name;
            Comments = comments;
            Date = date;
            Link = link;
        }

        public string Name { get; set; }
        public string Comments { get; set; }
        public DateTime Date { get; set; }
        public string Link { get; set; }
    }
}
