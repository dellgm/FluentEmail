namespace EmailService.Interfaces
{
    public interface IEmail6
    {
        //IEmail7 Body(string body);

        /// <summary>
        /// Send body text, chose media type (html, xml...)
        /// </summary>
        /// <param name="body"></param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        IEmail7 Body(string body, string mediaType = "");
        void Send();
    }
}
