using ItauIti.Challenge.PasswordValidate.Configuration;
using System;
using System.Collections.Generic;

namespace ItauIti.Challenge.PasswordValidate
{
    public class PasswordValidator : IPasswordValidator
    {
        private readonly ICollection<Func<string, bool>> _validations;

        public IEnumerable<Func<string, bool>> Validations { get => _validations; }

        public PasswordValidator(ICollection<Func<string, bool>> validations)
        {
            _validations = validations;
        }

        public PasswordValidator(ConfigurationPasswordValidator configuration)
        {
            _validations = configuration.Validators;
        }

        public bool Validate(string password)
        {
            bool isValid = false;
            if (string.IsNullOrEmpty(password))
                return isValid;

            isValid = true;
            foreach (var validate in _validations)
                isValid &= validate.Invoke(password);

            return isValid;
        }
    }
}
