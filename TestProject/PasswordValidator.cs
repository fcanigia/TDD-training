using System.Linq;

namespace TestProject
{
    internal static class PasswordValidator
    {
        public static bool Validate(string passwordToValidate)
        {
            var len = passwordToValidate.IsLengthRight();

            var hasUppercase = passwordToValidate.HasAtLeastOneUppercase();

            return len && hasUppercase;
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
    }
}
