using EmployeeCleanArch.Domain.Common.Interfaces.EmployeeCleanArch.Domain.Common.Interfaces;

namespace EmployeeCleanArch.Domain.Common.Interfaces
{
    public interface IAuditableEntity<TId> : IEntity<TId>
    {
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
