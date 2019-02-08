namespace EmailService.Interfaces
{
    public interface IEmail1
    {
        IEmail2 To(params string[] toAddresses);
    }
}
