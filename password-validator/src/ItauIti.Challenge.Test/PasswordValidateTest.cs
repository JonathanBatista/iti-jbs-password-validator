using ItauIti.Challenge.PasswordValidate.Configuration;
using Xunit;

namespace ItauIti.Challenge.Test
{
    public class PasswordValidateTest
    {
        private readonly ConfigurationPasswordValidator _config;
        public PasswordValidateTest()
        {
            _config = new ConfigurationPasswordValidator();
        }
        [Fact]
        public void ValidPasswordWithLowerCase()
        {
            _config.AddShouldHaveLowercaseValidation();
            var passwordValidate = _config.Initiaze();
            Assert.True(passwordValidate.Validate("PASSwORD"));
        }

        [Fact]
        public void InvalidPasswordWithLowerCase()
        {
            _config.AddShouldHaveLowercaseValidation();
            var passwordValidate = _config.Initiaze();
            Assert.False(passwordValidate.Validate("PASSWORD"));
        }

        [Fact]
        public void ValidPasswordWithUpperCase()
        {
            
            _config.AddShouldHaveUppercaseValidation();
            var passwordValidate = _config.Initiaze();
            Assert.True(passwordValidate.Validate("passWord"));
        }

        [Fact]
        public void InvalidPasswordWithUpperCase()
        {
            
            _config.AddShouldHaveUppercaseValidation();
            var passwordValidate = _config.Initiaze();
            Assert.False(passwordValidate.Validate("password"));
        }

        [Fact]
        public void ValidPasswordWithNumber()
        {
            
            _config.AddShouldHaveNumberValidation();
            var passwordValidate = _config.Initiaze();
            Assert.True(passwordValidate.Validate("passw0rd"));
        }

        [Fact]
        public void InvalidPasswordWithNumber()
        {
            
            _config.AddShouldHaveNumberValidation();
            var passwordValidate = _config.Initiaze();
            Assert.False(passwordValidate.Validate("password"));
        }

        [Fact]
        public void ValidPasswordWithSpecialCharacter()
        {
            
            _config.AddShouldHaveSpecialCharacterValidation();
            var passwordValidate = _config.Initiaze();
            Assert.True(passwordValidate.Validate("passw0rd!"));
        }

        [Fact]
        public void InvalidPasswordWithSpecialCharacter()
        {
            
            _config.AddShouldHaveSpecialCharacterValidation();
            var passwordValidate = _config.Initiaze();
            Assert.False(passwordValidate.Validate("password"));
        }

        [Fact]
        public void ValidPasswordWithMinimumLength()
        {
            
            _config.AddShouldHaveMinimumLengthValidation(8);
            var passwordValidate = _config.Initiaze();
            Assert.True(passwordValidate.Validate("passw0rd!"));
        }

        [Fact]
        public void InvalidPasswordWithMinimumLength()
        {
            
            _config.AddShouldHaveMinimumLengthValidation(8);
            var passwordValidate = _config.Initiaze();
            Assert.False(passwordValidate.Validate("pass"));
        }

        [Fact]
        public void ValidPasswordWithNonRepeatableChars()
        {
            
            _config.AddShouldNotRepeatCharactersValidation();
            var passwordValidate = _config.Initiaze();
            Assert.True(passwordValidate.Validate("pa$sw0rd!"));
        }

        [Fact]
        public void InvalidPasswordWithNonRepeatableChars()
        {
            
            _config.AddShouldNotRepeatCharactersValidation();
            var passwordValidate = _config.Initiaze();
            Assert.False(passwordValidate.Validate("password"));
        }

        [Fact]
        public void ValidCombineValidators()
        {
            

            _config.AddShouldHaveLowercaseValidation();
            _config.AddShouldHaveUppercaseValidation();
            _config.AddShouldHaveNumberValidation();
            _config.AddShouldHaveSpecialCharacterValidation();
            _config.AddShouldHaveMinimumLengthValidation(9);
            _config.AddShouldNotRepeatCharactersValidation();

            var passwordValidate = _config.Initiaze();
            Assert.True(passwordValidate.Validate("Pa$sw0rd!"));
        }

        [Fact]
        public void InvalidCombineValidators()
        {
            

            _config.AddShouldHaveLowercaseValidation();
            _config.AddShouldHaveUppercaseValidation();
            _config.AddShouldHaveNumberValidation();
            _config.AddShouldHaveSpecialCharacterValidation();
            _config.AddShouldHaveMinimumLengthValidation(9);
            _config.AddShouldNotRepeatCharactersValidation();

            var passwordValidate = _config.Initiaze();

            Assert.False(passwordValidate.Validate("Password"));
        }

        [Fact]
        public void CreateCustomValidator()
        {
            _config.AddCustomValidator((string input) => string.IsNullOrEmpty(input));
            Assert.False(_config.Initiaze().Validate("Password"));
        }
    }
}
