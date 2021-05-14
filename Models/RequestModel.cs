namespace memes.Models
{
    public class RequestModel
    {
        public RequestModel(string username, string password, 
            string text0, string text1)
        {
            this.username = username;
            this.password = password;
            this.text0 = text0;
            this.text1 = text1;
        }
        public string username { get; set; }
        public string password { get; set; }
        public string text0 { get; set; }
        public string text1 { get; set; }
    }
}