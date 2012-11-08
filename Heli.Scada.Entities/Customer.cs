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
    public class CustomerModel
    { 
        [NotNullValidator(MessageTemplate="ID must not be null")]
        public int customerid { get; set; }
        [StringLengthValidator(1,50, MessageTemplate="Firstname must be bet 1 and 50 character!")]
        public string firstname { get; set; }
        [StringLengthValidator(1, 50, MessageTemplate = "Lastname must be bet 1 and 50 character!")]
        public string lastname { get; set; }
        [StringLengthValidator(1, 50, MessageTemplate="Username must be bet 1 and 50 character!"), NotNullValidator(MessageTemplate="Username is required!")]
        public string username { get; set; }
        [StringLengthValidator(5, 25, MessageTemplate="Password must be between 5 and 25 characters!"), NotNullValidator(MessageTemplate="Password is required!")]
        public string password { get; set; }
        [RegexValidator(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", MessageTemplate="Not a valid email adress")]
        public string email { get; set; }
        [NotNullValidator(MessageTemplate="engineerid is required")]
        public int engineerid { get; set; }
        private List<int> installation = new List<int>();

        public List<int> Installation
        {
            get { return installation; }
            set { installation = value; }
        }
    }
}
