using Microsoft.EntityFrameworkCore;
using System;


namespace ContactManagerApi.Core
{
    public interface IDbContext :IDisposable
    {
        DbContext Instance { get; }
    }
}
