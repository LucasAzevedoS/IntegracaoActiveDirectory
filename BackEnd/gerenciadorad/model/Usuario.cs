namespace GerenciadoraAD.Model
{
    public class Usuario
    {
        public string? sAMAccountName { get; set; }
        public string? userPrincipalName { get; set; }
        public string? givenName { get; set; }
        public string? sn { get; set; }
        public string? cn { get; set; } 
        public string? mail { get; set; }
        public string? DisplayName { get; set; }
        public string? PhysicalDeliveryOfficeName { get; set; }
        public List<string>? MemberOf { get; set; }
        public string? Description { get; set; } 
        public string? TelephoneNumber { get; set; }
        public string? IpPhone { get; set; } 
        public string? StreetAddress { get; set; } 
        public string? l { get; set; }  
        public string? st { get; set; } 
        public string? PostalCode { get; set; } 
        public string? c { get; set; } 
        public string? ScriptPath { get; set; } 
        public List<string>? ProxyAddresses { get; set; } 
        public string? Vinculo { get; set; } 
        public string? Office { get; set; } 
        public string? Responsavel { get; set; } 
        public string? Nascimento { get; set; } 
    }
}
