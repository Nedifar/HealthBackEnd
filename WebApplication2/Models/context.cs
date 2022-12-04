using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using WebApplication2.ChatModels;

namespace WebApplication2.Models
{
    public class context : DbContext
    {
        public context(DbContextOptions<context> options) :
            base(options)
        {

        }

        public DbSet<Parent> Parents { get; set; }

        public DbSet<Citizenship> Citizenships{ get; set; }

        public DbSet<District> Districts { get; set; }

        public DbSet<PersonalDocument> Pasports { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<RegistrationAddress> RegistrationAddresses { get; set; }

        public DbSet<Street> Streets { get; set; }

        public DbSet<Organization> Organizations { get; set; }

        public DbSet<Camp> Camps { get; set; }

        public DbSet<Shift> Shifts { get; set; }

        public DbSet<Child> Children { get; set; }

        public DbSet<Locality> Localities { get; set; }

        public DbSet<FactAddress> FactAddresses { get; set; }

        public DbSet<CampPhoto> CampPhotos { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Dialog> Dialogs { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Admin> Admins { get; set; }

    }
}
