using System;
using System.Collections.Generic;

namespace ItauIti.Challenge.PasswordValidate
{
    public interface IPasswordValidator
    {
        public IEnumerable<Func<string, bool>> Validations { get; }
        bool Validate(string password);
    }
}
