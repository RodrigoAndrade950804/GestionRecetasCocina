namespace GestionRecetasCocina.Services
{
    public class LogService
    {
        private readonly string _filePath;

        public LogService()
        {
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            _filePath = Path.Combine(folderPath, "Logs_Andrade.txt");
        }

        private async Task AppendAsync(string mensaje)
        {
            string log = $"{mensaje} el {DateTime.Now:dd/MM/yyyy HH:mm}\n";
            await File.AppendAllTextAsync(_filePath, log);
        }

        // Registrar creación
        public Task LogCrearAsync(string nombreReceta)
        {
            return AppendAsync($"[CREAR] Receta '{nombreReceta}' registrada");
        }

        // Registrar actualización
        public Task LogActualizarAsync(string nombreReceta)
        {
            return AppendAsync($"[ACTUALIZAR] Receta '{nombreReceta}' modificada");
        }

        // Registrar eliminación
        public Task LogEliminarAsync(string nombreReceta)
        {
            return AppendAsync($"[ELIMINAR] Receta '{nombreReceta}' eliminada");
        }

        // Leer historial de logs
        public async Task<string[]> ReadLogsAsync()
        {
            if (!File.Exists(_filePath))
                return Array.Empty<string>();
            return await File.ReadAllLinesAsync(_filePath);
        }
    }
}



