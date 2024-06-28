using Project_APBD.Enums;

namespace Project_APBD.RequestModels;

public class CreatePaymentRequest
{
    public int ContractId { get; set; }
    
    public decimal Amount { get; set; }

    public PaymentType PaymentType { get; set; }
}