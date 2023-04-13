namespace Application.Core.Entities.Concrete
{
    /// <summary>
    /// User entity.
    /// </summary>
    public class User
    {
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
        public string ProfilFotografi { get; set; }
    }
}
