using Microsoft.EntityFrameworkCore;

namespace BankSystem.Data;

public class BankContext : DbContext
{
    public BankContext(DbContextOptions<BankContext> options) : base(options)
    {

    }

    // This class can be expanded to include DbSet properties for your entities
}