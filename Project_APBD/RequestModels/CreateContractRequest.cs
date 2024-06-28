namespace Project_APBD.RequestModels;

public class CreateContractRequest
{
   
    public int SoftwareId { get; set; }
        
    
    public int CustomerId { get; set; }
        
    
    public DateTime StartDate { get; set; }
        
    
    public DateTime EndDate { get; set; }
        
   
    public decimal Price { get; set; }
    
    public int SupportYears { get; set; }
    
    public bool IsSigned { get; set; }
}