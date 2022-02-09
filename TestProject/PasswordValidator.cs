
namespace TestProject
{
    internal class PasswordValidator
    {
        public static bool Validate(string passwordToValidate)
        {
            if (passwordToValidate.Length < 8)
            {
                return false;
            }
            
            return true;
        }

    }
}
