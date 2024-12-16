using Microsoft.EntityFrameworkCore;
using configurator_backend.Models.Catalogue.General;
using Pcie = configurator_backend.Models.Catalogue.Pcie;
using M2 = configurator_backend.Models.Catalogue.M2;
using IO = configurator_backend.Models.Catalogue.IO;

namespace Configurator.Data
{
    public class CatalogueContext : DbContext
    {
        public CatalogueContext(DbContextOptions<CatalogueContext> options)
            : base(options)
        {
        }

        public DbSet<Component> Components { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<Pcie.Bracket> PcieBrackets { get; set; }
        public DbSet<Pcie.Version> PcieVersions { get; set; }
        public DbSet<Pcie.Size> PcieSizes { get; set; }
        public DbSet<Pcie.Slot> PcieSlots { get; set; }
        public DbSet<Pcie.ExpansionCard> PcieExpansionCards { get; set; }
        
        public DbSet<M2.Key> M2Keys { get; set; }
        public DbSet<M2.FormFactor> M2FormFactors { get; set; }
        public DbSet<M2.Slot> M2Slots { get; set; }
        public DbSet<M2.ExpansionCard> M2ExpansionCards { get; set; }

        public DbSet<IO.Connector> IOConnectors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
