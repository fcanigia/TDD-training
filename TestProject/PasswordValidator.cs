using System.Linq;

namespace TestProject
{
    internal static class PasswordValidator
    {
        public static bool Validate(string passwordToValidate)
        {
            return passwordToValidate.IsLengthRight() && passwordToValidate.HasAtLeastOneUppercase() &&
                passwordToValidate.HasAtLeastOneLowercase() && passwordToValidate.HasAtLeastOneNumber();
        }

        public static bool IsLengthRight(this string str)
        {
            if (str.Length < 8)
            {
                return false;
            }

            return true;
        }

        public static bool HasAtLeastOneUppercase(this string str)
        {
            return str.Any(char.IsUpper);
        }

        public static bool HasAtLeastOneLowercase(this string str)
        {
            return str.Any(char.IsLower);
        }

        public static bool HasAtLeastOneNumber(this string str)
        {
            return str.Any(char.IsDigit);
        }

    }
}
