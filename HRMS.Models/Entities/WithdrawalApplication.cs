using System;

namespace HRMS.Models.Entities;

public class WithdrawalApplication
{
    public int Id { get; set; }
    public int StatusId { get; set; }
    public int CreatedPersonId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime LastChangeDate { get; set; }
    public DateTime StatusLastChangeDate { get; set; }
    public decimal Amount { get; set; }
    public int? SelectedEmployeeId { get; set; }

    public virtual ApplicationStatus Status { get; set; } = null!;
}
