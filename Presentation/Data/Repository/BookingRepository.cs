using Microsoft.EntityFrameworkCore;
using Presentation.Entities;
using Presentation.Models;
using System.Linq.Expressions;


namespace Presentation.Data.Repository;


public class BookingRepository(DataContext context) : BaseRepository<BookingEntity>(context), IBookingRepository
{
    public override async Task<RepositoryResult<IEnumerable<BookingEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await _table.
                Include(b => b.BookingOwner)
                .ThenInclude(b => b!.Address)
                .ToListAsync();
            return new RepositoryResult<IEnumerable<BookingEntity>> { Success = true, Result = entities };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<BookingEntity>>
            {
                Success = false,
                Error = ex.Message,
            };
        }
    }

    public override async Task<RepositoryResult<BookingEntity?>> GetAsync(Expression<Func<BookingEntity, bool>> expression)
    {
        try
        {
            var entity = await _table
                .Include(b => b.BookingOwner)
                .ThenInclude(b => b!.Address)
                .FirstOrDefaultAsync(expression) ?? throw new Exception("Not Found.");
            return new RepositoryResult<BookingEntity?> { Success = true, Result = entity };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<BookingEntity?>
            {
                Success = false,
                Error = ex.Message,
            };
        }
    }
    public override async Task<RepositoryResult> AddAsync(BookingEntity entity)
    {
        try
        {
            _context.Entry(entity).State = EntityState.Added;
            _context.Entry(entity.BookingOwner).State = EntityState.Added;
            _context.Entry(entity.BookingOwner.Address).State = EntityState.Added;

            await _context.SaveChangesAsync();
            return new RepositoryResult { Success = true };
        }
        catch (Exception ex)
        {
            return new RepositoryResult
            {
                Success = false,
                Error = ex.InnerException?.Message ?? ex.Message,
            };
        }
    }

}
