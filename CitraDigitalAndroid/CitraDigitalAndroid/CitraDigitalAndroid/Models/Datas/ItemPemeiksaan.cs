
namespace CitraDigitalAndroid
{
    public class ItemPemeriksaan
    {
        public int Id { get; set; }
        public string Kelengkapan { get; set; }
        public string Penjelasan { get; set; }
        public Pemeriksaan Pemeriksaan { get; set; }
        public JenisPemeriksaan JenisPemeriksaan { get;set; }
    }




    public enum JenisPemeriksaan
    {
        Kelengkapan, Dokumen
    }
}