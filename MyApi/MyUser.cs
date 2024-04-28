using Microsoft.AspNetCore.Identity;

namespace MyApi
{
    public class MyUser : IdentityUser<int>
    {
        public bool hasSportmanship { get; set; }
    }
}
