using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace WebVansSite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
            : base()
        {
            Vans = new List<VanModel>();
        }

        public virtual UserProfileInfo UserProfileInfo { get; set; }

        public virtual ICollection<VanModel> Vans { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class UserProfileInfo
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string CPF { get; set; }

        public string CNPJ { get; set; }

        public string Telefone { get; set; }

        public bool UsuarioValidado { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("WebVansConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<UserProfileInfo> UserProfileInfo { get; set; }

        public DbSet<VanModel> Vans { get; set; }

        public DbSet<TipoVanModel> TipoVans { get; set; }

        public DbSet<TipoCargaModel> TipoCargas { get; set; }

        public DbSet<TipoLocomocaoModel> TipoLocomocaos { get; set; }

        public DbSet<EstadoModel> Estados { get; set; }

        public DbSet<CidadeModel> Cidades { get; set; }

        public DbSet<TipoServicoModel> TipoServicos { get; set; }

        public DbSet<SolicitacaoModel> Solicitacaos { get; set; }

        public DbSet<MarcaModel> Marcas { get; set; }

        public DbSet<ModeloModel> Modelos { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}