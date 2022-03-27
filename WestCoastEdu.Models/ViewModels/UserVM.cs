using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WestCoastEdu.Models.ViewModels
{
    public class UserVM
    {
        public ApplicationUser User{ get; set; }
        [ValidateNever]
        public IEnumerable<ApplicationUser> UserList { get; set; }

    }
}
