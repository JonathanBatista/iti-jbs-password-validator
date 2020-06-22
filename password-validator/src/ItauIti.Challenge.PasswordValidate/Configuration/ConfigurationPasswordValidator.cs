using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ItauIti.Challenge.PasswordValidate.Configuration
{
    public class ConfigurationPasswordValidator
    {
        public ICollection<Func<string, bool>> Validators { get; private set; }

        public ConfigurationPasswordValidator()
        {
            Validators = new List<Func<string, bool>>();
        }

        public void AddCustomValidator(Func<string, bool> customValidator) => Validators.Add(customValidator);

        public void AddShouldHaveLowercaseValidation() => Validators.Add((string input) => Regex.IsMatch(input, "^(?=.*[a-z])"));

        public void AddShouldHaveMinimumLengthValidation(int minimumLength) => Validators.Add((string input) => Regex.IsMatch(input, "^(?=.{" + minimumLength + ",})"));

        public void AddShouldHaveNumberValidation() => Validators.Add((string input) => Regex.IsMatch(input, "^(?=.*[0-9])"));

        public void AddShouldHaveSpecialCharacterValidation() => Validators.Add((string input) => Regex.IsMatch(input, "^(?=.*[!@#$%^&*])"));

        public void AddShouldHaveUppercaseValidation() => Validators.Add((string input) => Regex.IsMatch(input, "^(?=.*[A-Z])"));

        public void AddShouldNotRepeatCharactersValidation() => Validators.Add((string input) => Regex.IsMatch(input, "^(?:([A-Za-z]|([0-9])|[!@#$%^&*])(?!.*\\1))*$"));

        public IPasswordValidator Initiaze() => (PasswordValidator) Activator.CreateInstance(typeof(PasswordValidator), Validators);
    }
}
