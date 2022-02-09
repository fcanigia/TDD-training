using Xunit;

namespace TestProject
{
    public class PasswordValidator_Should
    {
        /* TODO:
         * - validar largo (al menos 8) X
         * - validar mayusculas (al menos una)
         * - validar minusculas (al menos una)
         * - validar numero (al menos uno)
         */

        [Fact]
        public void validate_at_least_8_characters_length()
        {
            Assert.True(PasswordValidator.Validate("12345678"));
        }

        [Fact]
        public void fail_when_length_less_than_8()
        {
            Assert.False(PasswordValidator.Validate("12345"));
        }

        [Fact]
        public void validate_at_least_1_uppercase()
        {
            Assert.False(PasswordValidator.Validate("asdfghqw"));
        }

        [Fact]
        public void validate_at_least_1_lowercase()
        {
            Assert.False(PasswordValidator.Validate("ASDFGHQW"));
        }

    }
}
