using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineStore.DataAccess.Entities;

namespace OnlineStore.DataAccess.Configurations
{
    public class StockEventConfiguration : IEntityTypeConfiguration<StockEvent>
    {
        public void Configure(EntityTypeBuilder<StockEvent> builder)
        {
        }
    }
}
