using System;
using HelloNet.Model;
using Microsoft.EntityFrameworkCore;

namespace HelloNet.DataAccesslayer
{
    public class NotesDBContext : DbContext
    {
        public NotesDBContext(DbContextOptions<NotesDBContext> options) : base(options)
        {
        }

        public DbSet<NotesResponse> NotesResponses { get; set; }
    }
}
