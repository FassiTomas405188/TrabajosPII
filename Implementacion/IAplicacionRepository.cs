using Practica02.Models;

namespace Practica02.Implementacion
{
    public interface IAplicacionRepository
    {
        List<Articulo> GetAll();
        Articulo GetById(int id);
        bool Save(Articulo oArticulo);
        bool Delete(int id);
    }
}
