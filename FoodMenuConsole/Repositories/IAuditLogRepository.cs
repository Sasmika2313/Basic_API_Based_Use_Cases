using FoodMenuConsole.Models;

namespace FoodMenuConsole.Repositories
{
    public interface IAuditLogRepository
    {
        void Add(AuditLog auditLog);

        List<AuditLog> GetAll();
    }
}