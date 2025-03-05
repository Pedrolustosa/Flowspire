using Microsoft.EntityFrameworkCore;
using Flowspire.Domain.Entities;
using Flowspire.Infra.Data;
using Flowspire.Domain.Interfaces;
using Microsoft.Data.Sqlite;

namespace Flowspire.Infra.Repositories;
public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Category> AddAsync(Category category)
    {
        try
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao adicionar a categoria ao banco de dados.", ex);
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao adicionar categoria.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao adicionar categoria.", ex);
        }
    }

    public async Task<List<Category>> GetByUserIdAsync(string userId)
    {
        try
        {
            return await _context.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao recuperar categorias.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar categorias.", ex);
        }
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        try
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao recuperar categoria.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao recuperar categoria.", ex);
        }
    }

    public async Task UpdateAsync(Category category)
    {
        try
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("Erro ao atualizar a categoria no banco de dados.", ex);
        }
        catch (SqliteException ex)
        {
            throw new Exception("Erro de conexão ou operação no SQLite ao atualizar categoria.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("Erro inesperado ao atualizar categoria.", ex);
        }
    }
}