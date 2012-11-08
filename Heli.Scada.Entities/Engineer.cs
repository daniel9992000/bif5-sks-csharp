using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;


namespace Heli.Scada.Entities
{
    public class EngineerModel
    {
        [NotNullValidator(MessageTemplate="EngineerId must not be null")]
        public int engineerid { get; set; }
        [StringLengthValidator(1, 50, MessageTemplate = "Firstname must be bet 1 and 50 character!")]
        public string firstname { get; set; }
        [StringLengthValidator(1, 50, MessageTemplate = "Lastname must be bet 1 and 50 character!")]
        public string lastname { get; set; }
        [StringLengthValidator(1, 50, MessageTemplate = "Username must be bet 1 and 50 character!"), NotNullValidator(MessageTemplate = "Username is required!")]
        public string username { get; set; }
        [StringLengthValidator(5, 25, MessageTemplate = "Password must be between 5 and 25 characters!"), NotNullValidator(MessageTemplate = "Password is required!")]
        public string password { get; set; }
        [RegexValidator(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", MessageTemplate = "Not a valid email adress")]
        public string email { get; set; }
    }
}
