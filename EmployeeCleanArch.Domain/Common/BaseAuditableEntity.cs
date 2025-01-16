using EmployeeCleanArch.Domain.Common.Interfaces;

namespace EmployeeCleanArch.Domain.Common
{
    public abstract class BaseAuditableEntity<TId> : BaseEntity<TId>, IAuditableEntity<TId>
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
