namespace OOPShop.Models
{
    public class AuthUser
    {
        public User? User { get; set; }
        
        public bool IsAuthenticated()
        {
            if (User is null) 
                return false;
            return true;
        }

        public bool IsNotAuthenticated()
        {
            return !IsAuthenticated();
        }
    }
}
