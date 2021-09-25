using Microsoft.EntityFrameworkCore;
using RouteBuilderSite.Models;

#nullable disable

namespace RouteBuilderSite.Services
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FMeasure> FMeasures { get; set; }
        public virtual DbSet<Geometry> Geometries { get; set; }
        public virtual DbSet<PreRawMeasurement> PreRawMeasurements { get; set; }
        public virtual DbSet<RawDevice> RawDevices { get; set; }
        public virtual DbSet<RawGrIndex> RawGrIndexes { get; set; }
        public virtual DbSet<RawMeasurement> RawMeasurements { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=192.168.50.128;Port=5432;Database=postgres;Username=postgres;Password=pdp565");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("adminpack")
                .HasAnnotation("Relational:Collation", "Russian_Russia.1251");

            modelBuilder.Entity<FMeasure>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("f_measures");

                entity.HasIndex(e => e.DateMeasure, "i_f_measures_dt_measure");

                entity.HasIndex(e => new { e.DeviceId, e.DateMeasure }, "pk_f_measures")
                    .IsUnique();

                entity.Property(e => e.DateMeasure).HasColumnName("date_measure");

                entity.Property(e => e.DeviceId).HasColumnName("device_id");

                entity.Property(e => e.GreenIndex).HasColumnName("green_index");
            });

            modelBuilder.Entity<Geometry>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("geometries");

                entity.Property(e => e.Name)
                    .HasColumnType("character varying")
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PreRawMeasurement>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("pre_raw_measurements");

                entity.Property(e => e.Aqi)
                    .HasPrecision(38, 2)
                    .HasColumnName("aqi");

                entity.Property(e => e.Co2)
                    .HasPrecision(38, 2)
                    .HasColumnName("co2");

                entity.Property(e => e.DateMeasure).HasColumnName("date_measure");

                entity.Property(e => e.DeviceId).HasColumnName("device_id");

                entity.Property(e => e.Formald)
                    .HasPrecision(38, 2)
                    .HasColumnName("formald");

                entity.Property(e => e.Humidity)
                    .HasPrecision(38, 2)
                    .HasColumnName("humidity");

                entity.Property(e => e.Los)
                    .HasPrecision(38, 2)
                    .HasColumnName("los");

                entity.Property(e => e.Pm1)
                    .HasPrecision(38, 2)
                    .HasColumnName("pm1");

                entity.Property(e => e.Pm10)
                    .HasPrecision(38, 2)
                    .HasColumnName("pm10");

                entity.Property(e => e.Pm25)
                    .HasPrecision(38, 2)
                    .HasColumnName("pm25");

                entity.Property(e => e.Pressure)
                    .HasPrecision(38, 2)
                    .HasColumnName("pressure");

                entity.Property(e => e.Temperature)
                    .HasPrecision(38, 2)
                    .HasColumnName("temperature");
            });

            modelBuilder.Entity<RawDevice>(entity =>
            {
                entity.HasKey(e => e.DeviceId)
                    .HasName("pk_rw_devices");

                entity.ToTable("raw_devices");

                entity.HasIndex(e => e.DeviceName, "uk_rw_devices")
                    .IsUnique();

                entity.Property(e => e.DeviceId)
                    .ValueGeneratedNever()
                    .HasColumnName("device_id");

                entity.Property(e => e.DeviceName)
                    .HasMaxLength(200)
                    .HasColumnName("device_name");
            });

            modelBuilder.Entity<RawGrIndex>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("raw_gr_indexes");

                entity.HasIndex(e => e.GreenIndexId, "pk_rw_gr_indexes")
                    .IsUnique();

                entity.Property(e => e.GreenIndexId).HasColumnName("green_index_id");

                entity.Property(e => e.GreenIndexName)
                    .HasMaxLength(100)
                    .HasColumnName("green_index_name");
            });

            modelBuilder.Entity<RawMeasurement>(entity =>
            {
                entity.HasKey(e => new { e.DeviceId, e.DateMeasure })
                    .HasName("pk_rw_measurements");

                entity.ToTable("raw_measurements");

                entity.HasIndex(e => e.DateMeasure, "i_rw_measurements_dt_meas");

                entity.Property(e => e.DeviceId).HasColumnName("device_id");

                entity.Property(e => e.DateMeasure).HasColumnName("date_measure");

                entity.Property(e => e.Aqi)
                    .HasPrecision(38, 2)
                    .HasColumnName("aqi");

                entity.Property(e => e.Co2)
                    .HasPrecision(38, 2)
                    .HasColumnName("co2");

                entity.Property(e => e.Formald)
                    .HasPrecision(38, 2)
                    .HasColumnName("formald");

                entity.Property(e => e.Humidity)
                    .HasPrecision(38, 2)
                    .HasColumnName("humidity");

                entity.Property(e => e.Los)
                    .HasPrecision(38, 2)
                    .HasColumnName("los");

                entity.Property(e => e.Pm1)
                    .HasPrecision(38, 2)
                    .HasColumnName("pm1");

                entity.Property(e => e.Pm10)
                    .HasPrecision(38, 2)
                    .HasColumnName("pm10");

                entity.Property(e => e.Pm25)
                    .HasPrecision(38, 2)
                    .HasColumnName("pm25");

                entity.Property(e => e.Pressure)
                    .HasPrecision(38, 2)
                    .HasColumnName("pressure");

                entity.Property(e => e.Temperature)
                    .HasPrecision(38, 2)
                    .HasColumnName("temperature");
            });

            modelBuilder.HasSequence("base_seq").StartsAt(2);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
