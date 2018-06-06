using Microsoft.AspNet.Identity.EntityFramework;
using PortoGO.DB.Configuration;
using PortoGO.DB.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortoGO.DB
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Data.Entity.DbContext" />
    public class PortoGoContext : IdentityDbContext<User>
    {
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public DbSet<Hashtag> Hashtags { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Visit> Visits { get; set; }

        public DbSet<Road> Roads { get; set; }

        public DbSet<UserAuditTrail> UserAuditTrails { get; set; }

        public DbSet<GpsCoordinate> GpsCoordinates { get; set; }

        public DbSet<Route> Routes { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PortoGoContext"/> class.
        /// </summary>
        public PortoGoContext() : base("PortoGOConnectionString")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public static PortoGoContext Create()
        {
            return new PortoGoContext();
        }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // inicialização do Identity

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new LocationEntityConfiguration());
            modelBuilder.Configurations.Add(new HashtagEntityConfiguration());
            modelBuilder.Configurations.Add(new PointOfInterestEntityConfiguration());
            modelBuilder.Configurations.Add(new RoadEntityConfiguration());
            modelBuilder.Configurations.Add(new UserAuditTrailEntityConfiguration());
            modelBuilder.Configurations.Add(new GpsCoordinateEntityConfiguration());
            modelBuilder.Configurations.Add(new RouteEntityTypeConfiguration());
        }
    }
}
