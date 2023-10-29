using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TicTacToeWPF
{
    internal class ApplicationContext : DbContext
    {

        public DbSet<sessions> Sessions { get; set; }

        public ApplicationContext() : base("DefaultConnection") { }

    }
}
