using ClientSync_Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientSync_Server
{
    public class ClientSyncDbContext : DbContext
    {
        public DbSet<FileHash> filehashes { get; set; }
    }
}