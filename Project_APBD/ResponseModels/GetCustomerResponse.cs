namespace Project_APBD.ResponseModels;

public class GetCustomerResponse
{
    public int Id { get; set; }
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Pesel { get; set; }
    public string CompanyName { get; set; }
    public string KrsNumber { get; set; }
}