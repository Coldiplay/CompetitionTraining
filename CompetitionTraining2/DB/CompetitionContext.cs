using Microsoft.EntityFrameworkCore;

namespace CompetitionTraining2.DB;

public partial class CompetitionContext : DbContext
{
    public CompetitionContext()
    {
    }

    public CompetitionContext(DbContextOptions<CompetitionContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<CriticalThresholdTemplate> CriticalThresholdTemplates { get; set; }

    public virtual DbSet<Maintenance> Maintenances { get; set; }

    public virtual DbSet<NotificationTemplate> NotificationTemplates { get; set; }

    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

    public virtual DbSet<PaymentType> PaymentTypes { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<ServicePriority> ServicePriorities { get; set; }

    public virtual DbSet<StatusVendingMachine> StatusVendingMachines { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<VendingMachine> VendingMachines { get; set; }

    public virtual DbSet<WorkMode> WorkModes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("user=student;password=student;server=192.168.200.13;database=Competition", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.3.39-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("company");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<CriticalThresholdTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("critical_threshold_template");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Maintenance>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("maintenance");

            entity.HasIndex(e => e.TechnicianGuid, "maintenance_user_FK");

            entity.HasIndex(e => e.VendingMachineId, "maintenance_vending_machine_FK");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("date");
            entity.Property(e => e.IssuesFound)
                .HasMaxLength(150)
                .HasColumnName("issues_found");
            entity.Property(e => e.TechnicianGuid)
                .HasMaxLength(36)
                .HasColumnName("technician_guid");
            entity.Property(e => e.VendingMachineId)
                .HasMaxLength(36)
                .HasColumnName("vending_machine_id");
            entity.Property(e => e.WorkDescription)
                .HasMaxLength(150)
                .HasColumnName("work_description");

            entity.HasOne(d => d.Technician).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.TechnicianGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("maintenance_user_FK");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Maintenances)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("maintenance_vending_machine_FK");
        });

        modelBuilder.Entity<NotificationTemplate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("notification_template");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<PaymentMethod>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payment_method");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<PaymentType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("payment_type");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.VendingMachineId, "product_vending_machine_FK");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .HasColumnName("description");
            entity.Property(e => e.MinStock)
                .HasColumnType("smallint(6)")
                .HasColumnName("min_stock");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.QuantityAvailable)
                .HasColumnType("smallint(5) unsigned")
                .HasColumnName("quantity_available");
            entity.Property(e => e.SalesTrend).HasColumnName("sales_trend");
            entity.Property(e => e.VendingMachineId)
                .HasMaxLength(36)
                .HasColumnName("vending_machine_id");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Products)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("product_vending_machine_FK");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sale");

            entity.HasIndex(e => e.PaymentMethodId, "sale_payment_method_FK");

            entity.HasIndex(e => e.ProductId, "sale_product_FK");

            entity.HasIndex(e => e.VendingMachineId, "sale_vending_machine_FK");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .HasColumnName("id");
            entity.Property(e => e.PaymentMethodId)
                .HasColumnType("int(11)")
                .HasColumnName("payment_method_id");
            entity.Property(e => e.ProductId)
                .HasMaxLength(36)
                .HasColumnName("product_id");
            entity.Property(e => e.Quantity)
                .HasColumnType("smallint(6)")
                .HasColumnName("quantity");
            entity.Property(e => e.Timestamp)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("timestamp");
            entity.Property(e => e.TotalCost).HasColumnName("total_cost");
            entity.Property(e => e.VendingMachineId)
                .HasMaxLength(36)
                .HasColumnName("vending_machine_id");

            entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Sales)
                .HasForeignKey(d => d.PaymentMethodId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_payment_method_FK");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_product_FK");

            entity.HasOne(d => d.VendingMachine).WithMany(p => p.Sales)
                .HasForeignKey(d => d.VendingMachineId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("sale_vending_machine_FK");
        });

        modelBuilder.Entity<ServicePriority>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("service_priority");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<StatusVendingMachine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("status_vending_machine");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.RoleId, "user_role_FK");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .HasColumnName("id");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Fio)
                .HasMaxLength(120)
                .HasColumnName("fio");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.IsEngineer).HasColumnName("is_engineer");
            entity.Property(e => e.IsManager).HasColumnName("is_manager");
            entity.Property(e => e.IsOperator).HasColumnName("is_operator");
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .HasColumnName("phone");
            entity.Property(e => e.RoleId)
                .HasColumnType("int(11)")
                .HasColumnName("role_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_role_FK");
        });

        modelBuilder.Entity<VendingMachine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("vending_machine");

            entity.HasIndex(e => e.CreatorCompanyId, "vending_machine_company_FK");

            entity.HasIndex(e => e.CriticalThresholdTemplateId, "vending_machine_critical_threshold_template_FK");

            entity.HasIndex(e => e.NotificationTemplateId, "vending_machine_notification_template_FK");

            entity.HasIndex(e => e.ServicePriorityId, "vending_machine_service_priority_FK");

            entity.HasIndex(e => e.StatusId, "vending_machine_status_vending_machine_FK");

            entity.HasIndex(e => e.UserId, "vending_machine_user_FK");

            entity.HasIndex(e => e.ManagerGuid, "vending_machine_user_FK_1");

            entity.HasIndex(e => e.EngineerGuid, "vending_machine_user_FK_2");

            entity.HasIndex(e => e.OperatorId, "vending_machine_user_FK_3");

            entity.HasIndex(e => e.TechnicianGuid, "vending_machine_user_FK_4");

            entity.HasIndex(e => e.WorkModeId, "vending_machine_work_mode_FK");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .HasColumnName("id");
            entity.Property(e => e.Coordinates)
                .HasMaxLength(100)
                .HasColumnName("coordinates");
            entity.Property(e => e.CreationDate).HasColumnName("creation_date");
            entity.Property(e => e.CreatorCompanyId)
                .HasColumnType("int(11)")
                .HasColumnName("creator_company_id");
            entity.Property(e => e.CriticalThresholdTemplateId)
                .HasColumnType("int(11)")
                .HasColumnName("critical_threshold_template_id");
            entity.Property(e => e.EngineerGuid)
                .HasMaxLength(36)
                .HasColumnName("engineer_guid");
            entity.Property(e => e.InstallDate)
                .HasColumnType("datetime")
                .HasColumnName("install_date");
            entity.Property(e => e.InventoryDate)
                .HasColumnType("datetime")
                .HasColumnName("inventory_date");
            entity.Property(e => e.KitOnlineId)
                .HasMaxLength(10)
                .HasColumnName("kit_online_id");
            entity.Property(e => e.LastMaintainceDate)
                .HasColumnType("datetime")
                .HasColumnName("last_maintaince_date");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");
            entity.Property(e => e.MaintainceInterval)
                .HasColumnType("smallint(6)")
                .HasColumnName("maintaince_interval");
            entity.Property(e => e.ManagerGuid)
                .HasMaxLength(36)
                .HasColumnName("manager_guid");
            entity.Property(e => e.ManufactureCountry)
                .HasMaxLength(100)
                .HasColumnName("manufacture_country");
            entity.Property(e => e.Model)
                .HasMaxLength(100)
                .HasColumnName("model");
            entity.Property(e => e.NameLocation)
                .HasMaxLength(100)
                .HasColumnName("name_location");
            entity.Property(e => e.NextMaintainceDate)
                .HasColumnType("datetime")
                .HasColumnName("next_maintaince_date");
            entity.Property(e => e.Notes)
                .HasMaxLength(255)
                .HasColumnName("notes");
            entity.Property(e => e.NotificationTemplateId)
                .HasColumnType("int(11)")
                .HasColumnName("notification_template_id");
            entity.Property(e => e.OperatorId)
                .HasMaxLength(36)
                .HasColumnName("operator_id");
            entity.Property(e => e.Place)
                .HasMaxLength(100)
                .HasColumnName("place");
            entity.Property(e => e.RfidCashCollection)
                .HasMaxLength(12)
                .HasColumnName("rfid_cash_collection");
            entity.Property(e => e.RfidLoading)
                .HasMaxLength(12)
                .HasColumnName("rfid_loading");
            entity.Property(e => e.RfidService)
                .HasMaxLength(13)
                .HasColumnName("rfid_service");
            entity.Property(e => e.SerialNumber)
                .HasColumnType("int(11)")
                .HasColumnName("serial_number");
            entity.Property(e => e.ServicePriorityId)
                .HasColumnType("int(11)")
                .HasColumnName("service_priority_id");
            entity.Property(e => e.StatusId)
                .HasColumnType("int(11)")
                .HasColumnName("status_id");
            entity.Property(e => e.TechnicianGuid)
                .HasMaxLength(36)
                .HasColumnName("technician_guid");
            entity.Property(e => e.Timezone)
                .HasMaxLength(6)
                .HasColumnName("timezone");
            entity.Property(e => e.TotalIncome).HasColumnName("total_income");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .HasColumnName("user_id");
            entity.Property(e => e.WorkModeId)
                .HasColumnType("int(11)")
                .HasColumnName("work_mode_id");
            entity.Property(e => e.WorkingHours)
                .HasMaxLength(13)
                .HasColumnName("working_hours");

            entity.HasOne(d => d.CreatorCompany).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.CreatorCompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vending_machine_company_FK");

            entity.HasOne(d => d.CriticalThresholdTemplate).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.CriticalThresholdTemplateId)
                .HasConstraintName("vending_machine_critical_threshold_template_FK");

            entity.HasOne(d => d.Engineer).WithMany(p => p.VendingMachineEngineers)
                .HasForeignKey(d => d.EngineerGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vending_machine_user_FK_2");

            entity.HasOne(d => d.Manager).WithMany(p => p.VendingMachineManagers)
                .HasForeignKey(d => d.ManagerGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vending_machine_user_FK_1");

            entity.HasOne(d => d.NotificationTemplate).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.NotificationTemplateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vending_machine_notification_template_FK");

            entity.HasOne(d => d.Operator).WithMany(p => p.VendingMachineOperators)
                .HasForeignKey(d => d.OperatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vending_machine_user_FK_3");

            entity.HasOne(d => d.ServicePriority).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.ServicePriorityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vending_machine_service_priority_FK");

            entity.HasOne(d => d.Status).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vending_machine_status_vending_machine_FK");

            entity.HasOne(d => d.Technician).WithMany(p => p.VendingMachineTechnicians)
                .HasForeignKey(d => d.TechnicianGuid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vending_machine_user_FK_4");

            entity.HasOne(d => d.User).WithMany(p => p.VendingMachineUsers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vending_machine_user_FK");

            entity.HasOne(d => d.WorkMode).WithMany(p => p.VendingMachines)
                .HasForeignKey(d => d.WorkModeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("vending_machine_work_mode_FK");

            entity.HasMany(d => d.PaymentTypes).WithMany(p => p.VendingMachines)
                .UsingEntity<Dictionary<string, object>>(
                    "VendingMachinePaymentType",
                    r => r.HasOne<PaymentType>().WithMany()
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("vending_machine_payment_type_payment_type_FK"),
                    l => l.HasOne<VendingMachine>().WithMany()
                        .HasForeignKey("VendingMachineId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("vending_machine_payment_type_vending_machine_FK"),
                    j =>
                    {
                        j.HasKey("VendingMachineId", "PaymentTypeId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j.ToTable("vending_machine_payment_type");
                        j.HasIndex(new[] { "PaymentTypeId" }, "vending_machine_payment_type_payment_type_FK");
                        j.IndexerProperty<string>("VendingMachineId")
                            .HasMaxLength(36)
                            .HasColumnName("vending_machine_id");
                        j.IndexerProperty<int>("PaymentTypeId")
                            .HasColumnType("int(11)")
                            .HasColumnName("payment_type_id");
                    });
        });

        modelBuilder.Entity<WorkMode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("work_mode");

            entity.Property(e => e.Id)
                .HasColumnType("int(11)")
                .HasColumnName("id");
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .HasColumnName("title");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
