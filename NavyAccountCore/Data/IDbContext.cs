using Microsoft.EntityFrameworkCore;
using System;


namespace NavyAccountCore.Core.Data
{
    public interface IDbContext : IDisposable
    {
        DbContext Instance { get; }
    }
}
