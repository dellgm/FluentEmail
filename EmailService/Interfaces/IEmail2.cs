namespace EmailService.Interfaces
{
    public interface IEmail2
    {
        IEmail3 CC(params string[] ccAddresses);
        IEmail4 BCC(params string[] bccAddresses);
        IEmail6 Subject(string subject);
        void Send();
    }
}
