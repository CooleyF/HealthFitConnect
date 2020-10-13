using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HealthNetwork.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set;  }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string AvatarURL { get; set; }
        [NotMapped]
        public HttpPostedFileBase Avatar { get; set; }
        [NotMapped]
        public string FullName
        {
            get
            {
                return $"{LastName}, {FirstName}";
            }
        }
        [NotMapped]
        public string FullNameWithEmail
        {
            get
            {
                return $"{LastName}, {FirstName}  -  {Email}";
            }
        }
     
     
        //Navigation
        public virtual ICollection<BlogPost> BlogPosts { get; set; }
        public virtual ICollection<BlogComment> BlogComments { get; set; }

        public ApplicationUser()
        {
            this.BlogComments = new HashSet<BlogComment>();
            this.BlogPosts = new HashSet<BlogPost>();
        }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }


    }
}