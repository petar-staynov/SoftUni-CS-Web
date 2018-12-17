namespace TORSHIA_NEW.Models
{
    public class User
    {
        /*
           •	Has an Id – a GUID String or an Integer.
           •	Has an Username
           •	Has a Password
           •	Has an Email
           •	Has an Role – can be one of the following values (“User”, “Admin”)
         */


        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public UserRole Role { get; set; }
    }
}