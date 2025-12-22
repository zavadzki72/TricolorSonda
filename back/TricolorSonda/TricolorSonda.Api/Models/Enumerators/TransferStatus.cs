using System.ComponentModel.DataAnnotations;

namespace TricolorSonda.Api.Models.Enumerators
{
    public enum TransferStatus
    {
        [Display(Name = "Rumor")]
        Rumor = 1,

        [Display(Name = "Interesse")]
        Interest = 2,

        [Display(Name = "Em negociação")]
        InNegotiation = 3,

        [Display(Name = "Proposta aceita")]
        OfferAccepted = 4,

        [Display(Name = "Acordo verbal")]
        VerbalAgreement = 5,

        [Display(Name = "Contrato assinado")]
        ContractSigned = 6,

        [Display(Name = "Transferência concluída")]
        Completed = 7,

        [Display(Name = "Melou")]
        Cancelled = 8,
    }
}
