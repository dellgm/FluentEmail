namespace EmailService.Interfaces
{
    public interface IEmail7
    {
        IEmail7 Attachment(params string[] filePaths);
        IEmail7 Attachment(params GenFile[] memoryStreams);
        void Send();
    }
}
