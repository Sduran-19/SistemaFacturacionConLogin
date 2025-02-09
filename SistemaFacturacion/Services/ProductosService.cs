using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Data;
using SistemaFacturacion.Models;
using System.Linq.Expressions;

namespace SistemaFacturacion.Services;

public class ProductosService
{
    private readonly Contexto _contexto;

    public ProductosService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Crear(Productos producto)
    {
        if (!await Existe(producto.ProductoId))
            return await Insertar(producto);
        else
            return await Modificar(producto);
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.Productos
            .AnyAsync(c => c.ProductoId == id);
    }

    public async Task<bool> Insertar(Productos producto)
    {
        _contexto.Productos.Add(producto);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(Productos producto)
    {
        _contexto.Update(producto);
        var modifico = await _contexto.SaveChangesAsync() > 0;
        _contexto.Entry(producto).State = EntityState.Detached;
        return modifico;
    }

    public async Task<bool> Eliminar(int id)
    {
        var cantidad = await _contexto.Productos
            .Where(c => c.ProductoId == id)
            .ExecuteDeleteAsync();

        return cantidad > 0;
    }

    public async Task<Productos?> Buscar(int id)
    {
        return await _contexto.Productos
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ProductoId == id);
    }

    public async Task<bool> ExisteNombre(string nombre)
    {
        var productoExistente = await _contexto.Productos
            .AnyAsync(p => p.Nombre.ToLower() == nombre.ToLower());

        return productoExistente;
    }


    public async Task<List<Productos>> Listar(Expression<Func<Productos, bool>> criterio)
    {
        return await _contexto.Productos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
