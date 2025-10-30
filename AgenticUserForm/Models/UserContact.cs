namespace AgenticUserForm.Models;

public class UserContact
{
    public string Ad { get; set; } = string.Empty;          // First Name
    public string Soyad { get; set; } = string.Empty;       // Last Name
    public string SirketAdi { get; set; } = string.Empty;   // Company Name
    public string Unvan { get; set; } = string.Empty;       // Title
    public string Eposta { get; set; } = string.Empty;      // Email Address (unique key)
    public string Telefon { get; set; } = string.Empty;     // Phone Number
}
