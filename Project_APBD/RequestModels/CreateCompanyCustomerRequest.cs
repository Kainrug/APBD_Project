﻿namespace Project_APBD.RequestModels;

public class CreateCompanyCustomerRequest
{
    public string Address { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string CompanyName { get; set; }
    public string KrsNumber { get; set; }
}