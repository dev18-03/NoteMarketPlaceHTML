using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace NoteMarketPlace.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        public string ConfirmPassword { get; set; }
    }

    public class UserMetadata
    { 
        //[Display(Name = "First Name")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "First name required")]
        public string FirstName { get; set; }

        //[Display(Name = "Last Name")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Last name required")]
        public string LastName { get; set; }

        //[Display(Name = "Email ID")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Email ID required")]
        //[DataType(DataType.EmailAddress)]
        public string EmailID { get; set; }


        //[Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        //[DataType(DataType.Password)]
        //[MinLength(6,ErrorMessage = "minimum 6 charccter req")]
        public string Password { get; set; }

        //[Display(Name = "COnfirm Password")]
        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Confirm Password and PAssword do not match")]
        public string ConfirmPassword { get; set; }


    }
}