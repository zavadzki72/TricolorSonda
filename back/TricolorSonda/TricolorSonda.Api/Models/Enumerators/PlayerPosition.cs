using System.ComponentModel.DataAnnotations;

namespace TricolorSonda.Api.Models.Enumerators
{
    public enum PlayerPosition
    {
        [Display(Name = "Goleiro")]
        Goalkeeper = 1,

        [Display(Name = "Lateral Direito")]
        RightBack = 2,

        [Display(Name = "Lateral Esquerdo")]
        LeftBack = 3,

        [Display(Name = "Zagueiro")]
        CenterBack = 4,

        [Display(Name = "Ala Direito")]
        RightWingBack = 5,

        [Display(Name = "Ala Esquerdo")]
        LeftWingBack = 6,

        [Display(Name = "Volante")]
        DefensiveMidfielder = 7,

        [Display(Name = "Meio-Campista Central")]
        CentralMidfielder = 8,

        [Display(Name = "Meia Ofensivo")]
        AttackingMidfielder = 9,

        [Display(Name = "Meia Armador")]
        Playmaker = 10,

        [Display(Name = "Ponta Direita")]
        RightWinger = 11,

        [Display(Name = "Ponta Esquerda")]
        LeftWinger = 12,

        [Display(Name = "Segundo Atacante")]
        SecondStriker = 13,

        [Display(Name = "Centroavante")]
        Striker = 14
    }
}
