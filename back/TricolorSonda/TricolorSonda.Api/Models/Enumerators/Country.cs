using System.ComponentModel.DataAnnotations;

namespace TricolorSonda.Api.Models.Enumerators
{
    public enum Country
    {
        [Display(Name = "Brasil")]
        Brazil = 1,

        [Display(Name = "Argentina")]
        Argentina = 2,

        [Display(Name = "Uruguai")]
        Uruguay = 3,

        [Display(Name = "Chile")]
        Chile = 4,

        [Display(Name = "Colômbia")]
        Colombia = 5,

        [Display(Name = "Paraguai")]
        Paraguay = 6,

        [Display(Name = "Peru")]
        Peru = 7,

        [Display(Name = "Bolívia")]
        Bolivia = 8,

        [Display(Name = "Equador")]
        Ecuador = 9,

        [Display(Name = "Venezuela")]
        Venezuela = 10,

        // América do Norte e Central
        [Display(Name = "Estados Unidos")]
        UnitedStates = 11,

        [Display(Name = "México")]
        Mexico = 12,

        [Display(Name = "Canadá")]
        Canada = 13,

        [Display(Name = "Costa Rica")]
        CostaRica = 14,

        [Display(Name = "Panamá")]
        Panama = 15,

        // Europa
        [Display(Name = "Alemanha")]
        Germany = 16,

        [Display(Name = "França")]
        France = 17,

        [Display(Name = "Espanha")]
        Spain = 18,

        [Display(Name = "Portugal")]
        Portugal = 19,

        [Display(Name = "Itália")]
        Italy = 20,

        [Display(Name = "Inglaterra")]
        England = 21,

        [Display(Name = "Escócia")]
        Scotland = 22,

        [Display(Name = "País de Gales")]
        Wales = 23,

        [Display(Name = "Irlanda do Norte")]
        NorthernIreland = 24,

        [Display(Name = "Holanda")]
        Netherlands = 25,

        [Display(Name = "Bélgica")]
        Belgium = 26,

        [Display(Name = "Croácia")]
        Croatia = 27,

        [Display(Name = "Sérvia")]
        Serbia = 28,

        [Display(Name = "Suíça")]
        Switzerland = 29,

        [Display(Name = "Áustria")]
        Austria = 30,

        [Display(Name = "Polônia")]
        Poland = 31,

        [Display(Name = "Ucrânia")]
        Ukraine = 32,

        [Display(Name = "Turquia")]
        Turkey = 33,

        [Display(Name = "Rússia")]
        Russia = 34,

        // África
        [Display(Name = "Nigéria")]
        Nigeria = 35,

        [Display(Name = "Senegal")]
        Senegal = 36,

        [Display(Name = "Marrocos")]
        Morocco = 37,

        [Display(Name = "Gana")]
        Ghana = 38,

        [Display(Name = "Camarões")]
        Cameroon = 39,

        [Display(Name = "Costa do Marfim")]
        IvoryCoast = 40,

        [Display(Name = "Egito")]
        Egypt = 41,

        // Ásia
        [Display(Name = "Japão")]
        Japan = 42,

        [Display(Name = "Coreia do Sul")]
        SouthKorea = 43,

        [Display(Name = "Irã")]
        Iran = 44,

        [Display(Name = "Arábia Saudita")]
        SaudiArabia = 45,

        [Display(Name = "Catar")]
        Qatar = 46,

        // Oceania
        [Display(Name = "Austrália")]
        Australia = 47,

        [Display(Name = "Nova Zelândia")]
        NewZealand = 48
    }
}
