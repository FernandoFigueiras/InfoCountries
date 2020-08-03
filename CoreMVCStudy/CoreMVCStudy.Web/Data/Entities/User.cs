using Microsoft.AspNetCore.Identity;

namespace CoreMVCStudy.Web.Data.Entities
{
    public class User : IdentityUser//Temos de mudar o DataContext
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }
}
