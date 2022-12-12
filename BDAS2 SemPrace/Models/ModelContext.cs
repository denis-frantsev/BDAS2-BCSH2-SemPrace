using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using BDAS2_SemPrace.Models;
namespace BDAS2_SemPrace.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }
        private static User _user = new() { Role = Role.GHOST };
        public static User User { get { return _user; } set { _user = value; } }
        public virtual DbSet<Adresy> Adresy { get; set; }
        public virtual DbSet<Kategorie> Kategorie { get; set; }
        public virtual DbSet<NazvyPultu> NazvyPultu { get; set; }
        public virtual DbSet<Platby> Platby { get; set; }
        public virtual DbSet<Pokladny> Pokladny { get; set; }
        public virtual DbSet<Polozky> Polozky { get; set; }
        public virtual DbSet<PracovniMista> PracovniMista { get; set; }
        public virtual DbSet<Prodeje> Prodeje { get; set; }
        public virtual DbSet<Pulty> Pulty { get; set; }
        public virtual DbSet<Sklady> Sklady { get; set; }
        public virtual DbSet<SkladyZbozi> SkladyZbozi { get; set; }
        public virtual DbSet<Supermarkety> Supermarkety { get; set; }
        public virtual DbSet<Zakaznici> Zakaznici { get; set; }
        public virtual DbSet<Zamestnanci> Zamestnanci { get; set; }
        public virtual DbSet<Zbozi> Zbozi { get; set; }
        public virtual DbSet<Znacky> Znacky { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Obrazky> Obrazky { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseOracle();
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //                optionsBuilder.UseOracle("Data Source=(description=(address_list=(address = (protocol = TCP)(host = fei-sql1.upceucebny.cz)(port = 1521)))(connect_data=(service_name=IDAS.UPCEUCEBNY.CZ))\n);User ID=ST64102;Password=j8ex765gh;Persist Security Info=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ST64102")
                .UseCollation("USING_NLS_COMP");


            modelBuilder.Entity<Adresy>(entity =>
            {
                entity.HasKey(e => e.IdAdresa)
                    .HasName("ADRESY_PK");

                entity.ToTable("ADRESY");

                entity.Property(e => e.IdAdresa)
                    .HasPrecision(8)
                    .HasColumnName("ID_ADRESA");

                entity.Property(e => e.Mesto)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("MESTO");

                entity.Property(e => e.Psc)
                    .HasPrecision(5)
                    .HasColumnName("PSC");

                entity.Property(e => e.Ulice)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ULICE");
            });

            modelBuilder.Entity<Kategorie>(entity =>
            {
                entity.HasKey(e => e.IdKategorie)
                    .HasName("KATEGORIE_PK");

                entity.ToTable("KATEGORIE");

                entity.Property(e => e.IdKategorie)
                    .HasPrecision(4)
                    //.ValueGeneratedOnAdd()
                    .HasColumnName("ID_KATEGORIE");

                entity.Property(e => e.Nazev)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NAZEV");

                entity.Property(e => e.Popis)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("POPIS");
            });

            modelBuilder.Entity<NazvyPultu>(entity =>
            {
                entity.HasKey(e => e.IdPult)
                    .HasName("NAZVY_PULTU_PK");

                entity.ToTable("NAZVY_PULTU");

                entity.Property(e => e.IdPult)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("ID_PULT");

                entity.Property(e => e.Nazev)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("NAZEV");
            });

            modelBuilder.Entity<Platby>(entity =>
            {
                entity.HasKey(e => e.IdPlatba)
                    .HasName("TYPY_PLATBY_PK");

                entity.ToTable("PLATBY");

                entity.Property(e => e.IdPlatba)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("ID_PLATBA");

                entity.Property(e => e.Castka)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("CASTKA");

                entity.Property(e => e.CisloKarty)
                    .HasPrecision(16)
                    .HasColumnName("CISLO_KARTY");

                entity.Property(e => e.Datum)
                    .HasColumnType("DATE")
                    //.ValueGeneratedOnAdd()
                    .HasColumnName("DATUM");

                entity.Property(e => e.IdSupermarket)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("ID_SUPERMARKET");

                entity.Property(e => e.IdZakaznik)
                    .HasPrecision(6)
                    .HasColumnName("ID_ZAKAZNIK");

                entity.Property(e => e.Typ)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("TYP");

                entity.HasOne(d => d.IdSupermarketNavigation)
                    .WithMany(p => p.Platby)
                    .HasForeignKey(d => d.IdSupermarket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PLATBY_SUPERMARKETY_FK");

                entity.HasOne(d => d.IdZakaznikNavigation)
                    .WithMany(p => p.Platby)
                    .HasForeignKey(d => d.IdZakaznik)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("TYPY_PLATBY_ZAKAZNICI_FK");
            });

            modelBuilder.Entity<Pokladny>(entity =>
            {
                entity.HasKey(e => new { e.IdSupermarket, e.CisloPokladny })
                    .HasName("POKLADNY_PK");

                entity.ToTable("POKLADNY");

                entity.Property(e => e.IdSupermarket)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("ID_SUPERMARKET");

                entity.Property(e => e.CisloPokladny)
                    .HasPrecision(4)
                    .HasColumnName("CISLO_POKLADNY");

                entity.HasOne(d => d.IdSupermarketNavigation)
                    .WithMany(p => p.Pokladny)
                    .HasForeignKey(d => d.IdSupermarket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("POKLADNY_SUPERMARKETY_FK");
            });

            modelBuilder.Entity<Polozky>(entity =>
            {
                entity.HasKey(e => new { e.NazevZbozi, e.IdZbozi, e.CisloProdeje })
                    .HasName("POLOZKY_PK");

                entity.ToTable("POLOZKY");

                entity.Property(e => e.NazevZbozi)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAZEV_ZBOZI");

                entity.Property(e => e.IdZbozi)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("ID_ZBOZI");

                entity.Property(e => e.CisloProdeje)
                    .HasPrecision(8)
                    .HasColumnName("CISLO_PRODEJE");

                entity.Property(e => e.Mnozstvi)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("MNOZSTVI");

                entity.HasOne(d => d.CisloProdejeNavigation)
                    .WithMany(p => p.Polozky)
                    .HasForeignKey(d => d.CisloProdeje)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("POLOZKA_PRODEJ_FK");

                entity.HasOne(d => d.IdZboziNavigation)
                    .WithMany(p => p.Polozky)
                    .HasForeignKey(d => d.IdZbozi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("POLOZKY_ZBOZI_FK");
            });

            modelBuilder.Entity<PracovniMista>(entity =>
            {
                entity.HasKey(e => e.IdMisto)
                    .HasName("PRACOVNI_MISTA_PK");

                entity.ToTable("PRACOVNI_MISTA");

                entity.Property(e => e.IdMisto)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("ID_MISTO");

                entity.Property(e => e.MinPlat)
                    .HasPrecision(6)
                    .HasColumnName("MIN_PLAT");

                entity.Property(e => e.Nazev)
                    .IsRequired()
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("NAZEV");

                entity.Property(e => e.Popis)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("POPIS");
            });

            modelBuilder.Entity<Prodeje>(entity =>
            {
                entity.HasKey(e => e.CisloProdeje)
                    .HasName("PRODEJE_PK");

                entity.ToTable("PRODEJE");

                entity.Property(e => e.CisloProdeje)
                    .HasPrecision(8)
                    .HasColumnName("CISLO_PRODEJE");

                entity.Property(e => e.Datum)
                    .HasColumnType("DATE")
                    .HasColumnName("DATUM");

                entity.Property(e => e.IdPlatba)
                    .HasPrecision(8)
                    .HasColumnName("ID_PLATBA");

                entity.Property(e => e.Suma)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("SUMA");

                entity.HasOne(d => d.IdPlatbaNavigation)
                    .WithMany(p => p.Prodeje)
                    .HasForeignKey(d => d.IdPlatba)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PRODEJE_TYPY_PLATBY_FK");
            });

            modelBuilder.Entity<Pulty>(entity =>
            {
                entity.HasKey(e => new { e.CisloPultu, e.IdSupermarket })
                    .HasName("PULTY_PK");

                entity.ToTable("PULTY");

                entity.Property(e => e.CisloPultu)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("CISLO_PULTU");

                entity.Property(e => e.IdSupermarket)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("ID_SUPERMARKET");

                entity.Property(e => e.Nazev)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NAZEV");

                entity.HasOne(d => d.IdSupermarketNavigation)
                    .WithMany(p => p.Pulty)
                    .HasForeignKey(d => d.IdSupermarket)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PULTY_SUPERMARKETY_FK");

                entity.HasOne(d => d.NazevNavigation)
                    .WithMany(p => p.Pulty)
                    .HasForeignKey(d => d.Nazev)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PULTY_NAZVY_PULTU_FK");
            });

            modelBuilder.Entity<Sklady>(entity =>
            {
                entity.HasKey(e => e.IdSklad)
                    .HasName("SKLADY_PK");

                entity.ToTable("SKLADY");

                entity.Property(e => e.IdSklad)
                    .HasColumnType("NUMBER(8)")
                    //.ValueGeneratedOnAdd()
                    .HasColumnName("ID_SKLAD");

                entity.Property(e => e.IdAdresa)
                    .HasPrecision(8)
                    .HasColumnName("ID_ADRESA");

                entity.Property(e => e.Nazev)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("NAZEV");

                entity.HasOne(d => d.IdAdresaNavigation)
                    .WithMany(p => p.Sklady)
                    .HasForeignKey(d => d.IdAdresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SKLAD_ADRESA_FK");
            });

            modelBuilder.Entity<SkladyZbozi>(entity =>
            {
                entity.HasKey(e => new { e.SkladIdSklad, e.ZboziIdZbozi })
                    .HasName("SKLADY_ZBOZI_PK");

                entity.ToTable("SKLADY_ZBOZI");

                entity.Property(e => e.SkladIdSklad)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("SKLAD_ID_SKLAD");

                entity.Property(e => e.ZboziIdZbozi)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("ZBOZI_ID_ZBOZI");

                entity.Property(e => e.Pocet)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("POCET");

                entity.HasOne(d => d.SkladIdSkladNavigation)
                    .WithMany(p => p.SkladyZbozi)
                    .HasForeignKey(d => d.SkladIdSklad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SKLADY_ZBOZI_SKLADY_FK");

                entity.HasOne(d => d.ZboziIdZboziNavigation)
                    .WithMany(p => p.SkladyZbozi)
                    .HasForeignKey(d => d.ZboziIdZbozi)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SKLADY_ZBOZI_ZBOZI_FK");
            });

            modelBuilder.Entity<Supermarkety>(entity =>
            {
                entity.HasKey(e => e.IdSupermarket)
                    .HasName("SUPERMARKETY_PK");

                entity.ToTable("SUPERMARKETY");

                entity.HasIndex(e => e.Nazev, "SUPERMARKETY_UN")
                    .IsUnique();

                entity.Property(e => e.IdSupermarket)
                    .HasColumnType("NUMBER(8)")
                    //.ValueGeneratedOnAdd()
                    .HasColumnName("ID_SUPERMARKET");

                entity.Property(e => e.IdAdresa)
                    .HasPrecision(8)
                    .HasColumnName("ID_ADRESA");

                entity.Property(e => e.Nazev)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("NAZEV");

                entity.HasOne(d => d.IdAdresaNavigation)
                    .WithMany(p => p.Supermarkety)
                    .HasForeignKey(d => d.IdAdresa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SUPERMARKET_ADRESA_FK");
            });

            modelBuilder.Entity<Zakaznici>(entity =>
            {
                entity.HasKey(e => e.IdZakaznik)
                    .HasName("ZAKAZNICI_PK");

                entity.ToTable("ZAKAZNICI");

                entity.HasIndex(e => new { e.Email, e.TelefonniCislo }, "ZAKAZNICI_UK")
                    .IsUnique();

                entity.Property(e => e.IdZakaznik)
                    .HasPrecision(6)
                    .HasColumnName("ID_ZAKAZNIK");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Jmeno)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("JMENO");

                entity.Property(e => e.Prijmeni)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PRIJMENI");

                entity.Property(e => e.TelefonniCislo)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("TELEFONNI_CISLO");
            });

            modelBuilder.Entity<Zamestnanci>(entity =>
            {
                entity.HasKey(e => e.IdZamestnanec)
                    .HasName("ZAMESTNANCI_PK");

                entity.ToTable("ZAMESTNANCI");

                entity.HasIndex(e => new { e.Email, e.TelefonniCislo }, "ZAMESTNANCI_UK")
                    .IsUnique();

                entity.Property(e => e.IdZamestnanec)
                    .HasColumnType("NUMBER(8)")
                    //.ValueGeneratedOnAdd()
                    .HasColumnName("ID_ZAMESTNANEC");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.IdManazer)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("ID_MANAZER");

                entity.Property(e => e.IdMisto)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("ID_MISTO");

                entity.Property(e => e.IdSklad)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("ID_SKLAD");

                entity.Property(e => e.IdSupermarket)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("ID_SUPERMARKET");

                entity.Property(e => e.Jmeno)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("JMENO");

                entity.Property(e => e.Mzda)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("MZDA");

                entity.Property(e => e.Prijmeni)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("PRIJMENI");

                entity.Property(e => e.TelefonniCislo)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("TELEFONNI_CISLO");

                entity.HasOne(d => d.IdManazerNavigation)
                    .WithMany(p => p.InverseIdManazerNavigation)
                    .HasForeignKey(d => d.IdManazer)
                    .HasConstraintName("MANAZER_FK");

                entity.HasOne(d => d.IdMistoNavigation)
                    .WithMany(p => p.Zamestnanci)
                    .HasForeignKey(d => d.IdMisto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PRACOVNI_MISTO_FK");

                entity.HasOne(d => d.IdSkladNavigation)
                    .WithMany(p => p.Zamestnanci)
                    .HasForeignKey(d => d.IdSklad)
                    .HasConstraintName("SKLAD_FK");

                entity.HasOne(d => d.IdSupermarketNavigation)
                    .WithMany(p => p.Zamestnanci)
                    .HasForeignKey(d => d.IdSupermarket)
                    .HasConstraintName("SUPERMARKET_FK");
            });

            modelBuilder.Entity<Zbozi>(entity =>
            {
                entity.HasKey(e => e.IdZbozi)
                    .HasName("ZBOZI_PK");

                entity.ToTable("ZBOZI");

                entity.HasIndex(e => new { e.NazevZbozi, e.KodZbozi }, "ZBOZI_UN")
                    .IsUnique();

                entity.Property(e => e.IdZbozi)
                    .HasColumnType("NUMBER(8)")
                    //.ValueGeneratedOnAdd()
                    .HasColumnName("ID_ZBOZI");

                entity.Property(e => e.Cena)
                    .HasColumnType("NUMBER(8)")
                    .HasColumnName("CENA");

                entity.Property(e => e.IdKategorie)
                    .HasPrecision(4)
                    .HasColumnName("ID_KATEGORIE");

                entity.Property(e => e.IdZnacka)
                    .HasPrecision(4)
                    .HasColumnName("ID_ZNACKA");

                entity.Property(e => e.KodZbozi)
                    .HasPrecision(5)
                    //.ValueGeneratedOnAdd()
                    .HasColumnName("KOD_ZBOZI");

                entity.Property(e => e.NazevZbozi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAZEV_ZBOZI");

                entity.Property(e => e.Popis)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("POPIS");

                entity.Property(e => e.Obrazek)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("OBRAZEK");

                entity.HasOne(d => d.IdKategorieNavigation)
                    .WithMany(p => p.Zbozi)
                    .HasForeignKey(d => d.IdKategorie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ZBOZI_KATEGORIE_FK");

                entity.HasOne(d => d.IdZnackaNavigation)
                    .WithMany(p => p.Zbozi)
                    .HasForeignKey(d => d.IdZnacka)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ZBOZI_ZNACKA_FK");

                entity.HasMany(d => d.Pulty)
                    .WithMany(p => p.ZboziIdZbozi)
                    .UsingEntity<Dictionary<string, object>>(
                        "PultyZbozi",
                        l => l.HasOne<Pulty>().WithMany().HasForeignKey("PultCisloPultu", "PultIdSupermarket").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("PULTY_ZBOZI_PULTY_FK"),
                        r => r.HasOne<Zbozi>().WithMany().HasForeignKey("ZboziIdZbozi").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("PULTY_ZBOZI_ZBOZI_FK"),
                        j =>
                        {
                            j.HasKey("ZboziIdZbozi", "PultCisloPultu", "PultIdSupermarket").HasName("PULTY_ZBOZI_PK");

                            j.ToTable("PULTY_ZBOZI");

                            j.IndexerProperty<int>("ZboziIdZbozi").HasColumnType("NUMBER(8)").HasColumnName("ZBOZI_ID_ZBOZI");

                            j.IndexerProperty<int>("PultCisloPultu").HasColumnType("NUMBER(8)").HasColumnName("PULT_CISLO_PULTU");

                            j.IndexerProperty<int>("PultIdSupermarket").HasColumnType("NUMBER(8)").HasColumnName("PULT_ID_SUPERMARKET");
                        });
            });

            modelBuilder.Entity<Znacky>(entity =>
            {
                entity.HasKey(e => e.IdZnacka)
                    .HasName("ZNACKY_PK");

                entity.ToTable("ZNACKY");

                entity.Property(e => e.IdZnacka)
                    .HasPrecision(4)
                    //.ValueGeneratedOnAdd()
                    .HasColumnName("ID_ZNACKA");

                entity.Property(e => e.Nazev)
                    .IsRequired()
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("NAZEV");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("USERS_PK");

                entity.ToTable("USERS");

                entity.HasIndex(e => e.Email, "USERS_UK1")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .HasMaxLength(40)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("HESLO");

                entity.Property(e => e.IdObrazek)
                    .HasPrecision(6)
                    .HasColumnName("ID_OBRAZEK");

                entity.Property(e => e.Role)
                    .HasPrecision(2)
                    .HasColumnName("OPRAVNENI");

                entity.HasOne(d => d.IdObrazekNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.IdObrazek)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("USERS_FK");
            });

            modelBuilder.Entity<Obrazky>(entity =>
            {
                entity.HasKey(e => e.IdObrazek)
                    .HasName("OBRAZKY_PK");

                entity.ToTable("OBRAZKY");

                entity.Property(e => e.IdObrazek)
                    .HasPrecision(6)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_OBRAZEK");

                entity.Property(e => e.Data)
                    .IsRequired()
                    .HasColumnType("BLOB")
                    .HasColumnName("DATA");

                entity.Property(e => e.Popis)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("POPIS");
            });

            modelBuilder.HasSequence("S_ADRESY");

            modelBuilder.HasSequence("S_KATEGORIE").IncrementsBy(10);

            modelBuilder.HasSequence("S_PRODEJE");

            modelBuilder.HasSequence("S_SKLADY").IncrementsBy(10);

            modelBuilder.HasSequence("S_SUPERMARKETY").IncrementsBy(10);

            modelBuilder.HasSequence("S_ZAKAZNICI");

            modelBuilder.HasSequence("S_ZAMESTNANCI");

            modelBuilder.HasSequence("S_ZBOZI");

            modelBuilder.HasSequence("S_ZNACKY").IncrementsBy(10);

            modelBuilder.HasSequence("S_USERS");

            modelBuilder.HasSequence("S_OBRAZKY");

            OnModelCreatingPartial(modelBuilder);

        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public static bool HasAdminRights() => User.Role == Role.ADMIN;

        public bool IsUser(int? id) => User.Email == Zakaznici.Find(id).Email; 

    }
}
