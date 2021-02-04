using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfClient.Models
{
    public class Context : DbContext
    {
        public Context() : base("DefaultConnection")
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}
