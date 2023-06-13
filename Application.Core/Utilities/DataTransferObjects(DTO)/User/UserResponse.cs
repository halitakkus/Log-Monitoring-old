using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace Application.Core.Utilities.DataTransferObjects.User;

public class UserResponse
{
    public UserResponse(string name, string picture)
    {
        Ad = name;
        ProfilResmi = picture;
    }
    
    public int Id { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public string SicilNo { get; set; }
    public string Gorevi { get; set; }
    public string Telefon { get; set; }
    public string TelefonAyrinti { get; set; }
    public string CalismaBolgesi { get; set; }
    public string Yerleske { get; set; }
    public string Birim { get; set; }
    public string UstBirim { get; set; }
    public string Mail { get; set; }
    public string Enstitu { get; set; }
    public bool OnemliNumarami { get; set; }
    public string ProfilResmi { get; set; }
}