﻿using Microsoft.EntityFrameworkCore;

namespace MicroShop.Common.Data.Context;

public class EfBaseCommandDbContext : DbContext
{
    public EfBaseCommandDbContext(DbContextOptions options) : base(options)
    {
    }

    public EfBaseCommandDbContext()
    {
    }

    public override int SaveChanges()
    {
        return base.SaveChanges();
    }
}