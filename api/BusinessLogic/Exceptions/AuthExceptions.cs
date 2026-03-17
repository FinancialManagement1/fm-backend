
namespace BusinessLogic.Exceptions
{
    //401 
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException()
            : base("Invalid email or password.")
        {
        }
    }
    //409 
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException()
            : base("Email already exists")
        {
        }
    }
}
