

namespace CitraDigitalAndroid
{
    public enum UserType
    {
        None, Administrator, Manager, Approval1, HSE, Company, Gate
    }

    public enum StatusPengajuan
    {
        Baru, Proccess, Complete  , Cancel=-1
    }

    public enum AttackStatus
    {
        Baru, Perpanjangan, Complete
    }

    public enum StatusPersetujuan
    {
        Proccess, Approved, Complete, Reject, Fixed, Cancel = -1
    }

    public enum ExpireStatus
    {
        None, WillExpire, Expire
    }

}