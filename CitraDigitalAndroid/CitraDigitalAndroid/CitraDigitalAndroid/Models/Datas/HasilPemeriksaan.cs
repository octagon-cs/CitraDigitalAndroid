using System;

namespace CitraDigitalAndroid
{
    public class HasilPemeriksaan :BaseNotify
    {
        private int _id;
        private bool _hasil;
        private DateTime? _comp;
        private string _keterangan;
        private string _tindak;

        public int Id {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        public int ItemPengajuanId { get; set; }
        public int ItemPemeriksaanId { get; set; }
        public bool Hasil
        {
            get { return _hasil; }
            set { SetProperty(ref _hasil, value); }
        }
        public string TindakLanjut
        {
            get { return _tindak; }
            set { SetProperty(ref _tindak, value); }
        }
        public string Keterangan
        {
            get { return _keterangan; }
            set { SetProperty(ref _keterangan, value); }
        }
        public DateTime? CompensationDeadline
        {
            get { return _comp; }
            set { SetProperty(ref _comp, value); }
        }
        public ItemPemeriksaan ItemPemeriksaan{ get; set; }


    }
}