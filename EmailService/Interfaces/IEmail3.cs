namespace EmailService.Interfaces
{
    public interface IEmail3
    {
        IEmail5 BCC(params string[] bccAddresses);
        IEmail6 Subject(string subject);
        void Send();
    }
}
