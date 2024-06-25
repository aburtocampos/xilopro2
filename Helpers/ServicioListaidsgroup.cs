using xilopro2.Data;
using xilopro2.Data.Entities;

namespace xilopro2.Helpers
{
    public class ServicioListaidsgroup
    {
        private readonly DataContext _context;

        public ServicioListaidsgroup(DataContext context)
        {
            _context = context;
        }

        public void GuardarLista(List<int> lista, int id)
        {
            var entidadExistente = _context.ListaIdsGroups.FirstOrDefault(e => e.Id == id);

            if (entidadExistente != null)
            {
                // Si existe, actualiza los valores del registro existente
                 entidadExistente.Valores = lista;
                _context.ListaIdsGroups.Update(entidadExistente);
            }
            else
            {
                // Si no existe, crea una nueva entidad con el Id proporcionado y los valores
                var nuevaEntidad = new ListaIdsGroup
                {
                   // Id = id,
                    Valores = lista
                };
                _context.ListaIdsGroups.Add(nuevaEntidad);
            }

            _context.SaveChanges();
        }

        public List<int> ObtenerLista(int id)
        {
            var entidad = _context.ListaIdsGroups.Find(id);
            return entidad?.Valores ?? new List<int>();
        }
        public void EliminarLista(int id)
        {
            var entidad = _context.ListaIdsGroups.Find(id);
            if (entidad != null)
            {
                _context.ListaIdsGroups.Remove(entidad);
                _context.SaveChanges();
            }
        }

    }
}
