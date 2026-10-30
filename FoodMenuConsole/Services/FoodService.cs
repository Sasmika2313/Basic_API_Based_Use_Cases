using FoodMenuConsole.Data;
using FoodMenuConsole.Models;
using FoodMenuConsole.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FoodMenuConsole.Services
{
    public class FoodService
    {
        private readonly IFoodRepository _repository;
        private readonly IAuditLogRepository _auditRepository;
        private readonly AppDbContext _context;

        public FoodService(
            IFoodRepository repository,
            IAuditLogRepository auditRepository,
            AppDbContext context)
        {
            _repository = repository;
            _auditRepository = auditRepository;
            _context = context;
        }

        public List<FoodItem> GetAll()
        {
            return _repository.GetAll();
        }

        public List<FoodItem> GetAvailable()
        {
            return _repository.GetAll()
                .Where(f => f.IsAvailable)
                .ToList();
        }

        public FoodItem? GetById(int id)
        {
            return _repository.GetById(id);
        }

        public List<FoodItem> GetByCategory(string category)
        {
            return _repository.GetByCategory(category);
        }

        public List<AuditLog> GetAuditLogs()
        {
            return _auditRepository.GetAll();
        }

        public bool Add(FoodItem item, out string message)
        {
            if (!Validate(item, out message))
            {
                return false;
            }

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                _repository.Add(item);

                AuditLog auditLog = new AuditLog
                {
                    Action = "CREATE",
                    EntityName = "FoodItem",
                    EntityId = item.Id,
                    Details = $"Added food item: {item.Name}"
                };

                _auditRepository.Add(auditLog);

                transaction.Commit();

                message = "Food item added successfully.";
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public bool Update(FoodItem item, out string message)
        {
            if (!Validate(item, out message))
            {
                return false;
            }

            FoodItem? existing = _repository.GetById(item.Id);

            if (existing == null)
            {
                message = "Food item not found.";
                return false;
            }

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                bool updated = _repository.Update(item);

                if (!updated)
                {
                    transaction.Rollback();
                    message = "Food item not found.";
                    return false;
                }

                AuditLog auditLog = new AuditLog
                {
                    Action = "UPDATE",
                    EntityName = "FoodItem",
                    EntityId = item.Id,
                    Details =
                        $"Updated food item '{existing.Name}' to '{item.Name}'. " +
                        $"Price: {existing.Price:F2} -> {item.Price:F2}. " +
                        $"Category: {existing.Category} -> {item.Category}. " +
                        $"Availability: {existing.IsAvailable} -> {item.IsAvailable}."
                };

                _auditRepository.Add(auditLog);

                transaction.Commit();

                message = "Food item updated successfully.";
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public bool Delete(int id, out string message)
        {
            FoodItem? existing = _repository.GetById(id);

            if (existing == null)
            {
                message = "Food item not found.";
                return false;
            }

            using var transaction = _context.Database.BeginTransaction();

            try
            {
                bool deleted = _repository.Delete(id);

                if (!deleted)
                {
                    transaction.Rollback();
                    message = "Food item not found.";
                    return false;
                }

                AuditLog auditLog = new AuditLog
                {
                    Action = "DELETE",
                    EntityName = "FoodItem",
                    EntityId = existing.Id,
                    Details =
                        $"Deleted food item: {existing.Name}. " +
                        $"Category: {existing.Category}. " +
                        $"Price: {existing.Price:F2}."
                };

                _auditRepository.Add(auditLog);

                transaction.Commit();

                message = "Food item deleted successfully.";
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private bool Validate(FoodItem item, out string message)
        {
            item.Name = item.Name.Trim();
            item.Category = item.Category.Trim();

            if (string.IsNullOrWhiteSpace(item.Name))
            {
                message = "Food name cannot be empty.";
                return false;
            }

            if (item.Name.Length > 100)
            {
                message = "Food name cannot exceed 100 characters.";
                return false;
            }

            string? category = FoodCategory.GetAll()
                .FirstOrDefault(c =>
                    c.Equals(
                        item.Category,
                        StringComparison.OrdinalIgnoreCase));

            if (category == null)
            {
                message = "Please select a valid category.";
                return false;
            }

            item.Category = category;

            if (item.Description?.Length > 500)
            {
                message = "Description cannot exceed 500 characters.";
                return false;
            }

            if (item.Price <= 0 || item.Price > 999999999.99m)
            {
                message = "Price must be greater than zero.";
                return false;
            }

            message = "";
            return true;
        }
    }
}