using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vsTutotrial;

namespace vsTutotrial.Data
{
    public class vsTutotrialContext : DbContext
    {
        public vsTutotrialContext (DbContextOptions<vsTutotrialContext> options)
            : base(options)
        {
        }

        public DbSet<vsTutotrial.Contact> Contact { get; set; } = default!;
    }
}
