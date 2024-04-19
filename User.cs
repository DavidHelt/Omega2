namespace Omega
{
    public class User
    {
        // this class is used by most of other classes to reference to

        public string id;
        public string username;

        public User(string id, string username)
        {
            this.id = id;
            this.username = username;
        }
    }
}