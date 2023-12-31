﻿using Microsoft.EntityFrameworkCore;

namespace Skillfactory.Module32.DAL;

public class BlogRepository : IBlogRepository
{
    private readonly BlogContext _context;

    public BlogRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task AddUser(User user)
    {
        var entry = _context.Entry(user);
        if (entry.State == EntityState.Detached)
            await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();
    }

    public async Task<User[]> GetUsers()
    {
        return await _context.Users.ToArrayAsync();
    }
}
