using System.ComponentModel.DataAnnotations;

namespace TricolorSonda.Api.Models.Enumerators
{
    public enum TransferType
    {
        [Display(Name = "Venda")]
        Sell = 1,

        [Display(Name = "Compra")]
        Buy = 2,

        [Display(Name = "Empréstimo")]
        Loan = 3,

        [Display(Name = "Transferência gratuita")]
        FreeTransfer = 4,

        [Display(Name = "Renovação de contrato")]
        ContractRenewal = 5,

        [Display(Name = "Retorno de empréstimo")]
        LoanReturn = 6
    }
}
