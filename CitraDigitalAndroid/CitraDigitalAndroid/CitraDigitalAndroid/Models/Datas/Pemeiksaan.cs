using System.Collections.Generic;
namespace CitraDigitalAndroid
{
    public class Pemeriksaan
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ItemPemeriksaan> Items { get; set; }
    }
}