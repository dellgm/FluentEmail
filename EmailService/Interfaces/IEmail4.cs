namespace EmailService.Interfaces
{
    public interface IEmail4
    {
        IEmail5 CC(params string[] ccAddresses);
        IEmail6 Subject(string subject);
        void Send();
    }
}
