using GestionRecetasCocina.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GestionRecetasCocina.Repositories
{
    public class RecetaRepository
    {
        private readonly SQLiteAsyncConnection _database;

        // Constructor principal
        public RecetaRepository(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Receta>().Wait();
        }

        // Constructor por defecto
        public RecetaRepository() : this(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "recetas.db"))
        {
        }

        // Obtener todas las recetas
        public Task<List<Receta>> GetRecetasAsync()
        {
            return _database.Table<Receta>().ToListAsync();
        }

        // Obtener receta por ID
        public Task<Receta> GetRecetaByIdAsync(int id)
        {
            return _database.Table<Receta>()
                            .Where(r => r.Id == id)
                            .FirstOrDefaultAsync();
        }

        // Agregar nueva receta
        public Task<int> AddRecetaAsync(Receta receta)
        {
            return _database.InsertAsync(receta);
        }

        // Actualizar receta existente
        public Task<int> UpdateRecetaAsync(Receta receta)
        {
            return _database.UpdateAsync(receta);
        }

        // Eliminar receta por objeto
        public Task<int> DeleteRecetaAsync(Receta receta)
        {
            return _database.DeleteAsync(receta);
        }

        // Eliminar receta por ID
        public async Task<int> DeleteRecetaByIdAsync(int id)
        {
            var receta = await GetRecetaByIdAsync(id);
            if (receta != null)
                return await DeleteRecetaAsync(receta);
            return 0;
        }
    }
}

