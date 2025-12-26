namespace API.DB;

public partial class VendingMachine
{
    public int SerialNumber { get; set; }

    public string NameLocation { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? RfidCashCollection { get; set; }

    public string? Notes { get; set; }

    public string Location { get; set; } = null!;

    public int WorkModeId { get; set; }

    public string? RfidLoading { get; set; }

    public string Model { get; set; } = null!;

    public string? KitOnlineId { get; set; }

    public int CreatorCompanyId { get; set; }

    public int? CriticalThresholdTemplateId { get; set; }

    public int ServicePriorityId { get; set; }

    public string ManagerGuid { get; set; } = null!;

    public int StatusId { get; set; }

    public int? NotificationTemplateId { get; set; }

    public string? WorkingHours { get; set; }

    public string EngineerGuid { get; set; } = null!;

    public string Id { get; set; } = null!;

    public DateOnly? CreationDate { get; set; }

    public DateTime? InstallDate { get; set; }

    public string Place { get; set; } = null!;

    public int OperatorId { get; set; }

    public string TechnicianGuid { get; set; } = null!;

    public DateTime? LastMaintainceDate { get; set; }

    public short? MaintainceInterval { get; set; }

    public string? RfidService { get; set; }

    public string? Coordinates { get; set; }

    public decimal TotalIncome { get; set; }

    public string Timezone { get; set; } = null!;

    public DateTime? InventoryDate { get; set; }

    public string? ManufactureCountry { get; set; }

    public DateTime? NextMaintainceDate { get; set; }

    public uint? TimeResource { get; set; }

    public ushort MaintenanceTime { get; set; }

    public virtual Company CreatorCompany { get; set; } = null!;

    public virtual CriticalThresholdTemplate? CriticalThresholdTemplate { get; set; }

    public virtual User Engineer { get; set; } = null!;

    public virtual ICollection<Maintenance> Maintenances { get; set; } = new List<Maintenance>();

    public virtual User Manager { get; set; } = null!;

    public virtual NotificationTemplate? NotificationTemplate { get; set; }

    public virtual Operator Operator { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<Sale> Sales { get; set; } = new List<Sale>();

    public virtual ServicePriority ServicePriority { get; set; } = null!;

    public virtual StatusVendingMachine Status { get; set; } = null!;

    public virtual User Technician { get; set; } = null!;

    public virtual User User { get; set; } = null!;

    public virtual WorkMode WorkMode { get; set; } = null!;

    public virtual ICollection<PaymentType> PaymentTypes { get; set; } = new List<PaymentType>();
}
