using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Data;
using SistemaFacturacion.Models;
using System.Linq.Expressions;

namespace SistemaFacturacion.Services;

public class FinanciamientoService
{
    private readonly Contexto _contexto;

    public FinanciamientoService(Contexto contexto)
    {
        _contexto = contexto;
    }

    // Crear o modificar un financiamiento
    public async Task<bool> Crear(Financiamiento financiamiento)
    {
        if (!await Existe(financiamiento.FinanciamientoId))
            return await Insertar(financiamiento);
        else
            return await Modificar(financiamiento);
    }

    // Verifica si el financiamiento existe por ID
    public async Task<bool> Existe(int id)
    {
        return await _contexto.Financiamientos
            .AnyAsync(f => f.FinanciamientoId == id);
    }

    public async Task<bool> ExisteNombre(string nombre)
    {
        var productoExistente = await _contexto.Financiamientos
            .AnyAsync(p => p.NombreCliente.ToLower() == nombre.ToLower());

        return productoExistente;
    }

    // Inserta un nuevo financiamiento
    public async Task<bool> Insertar(Financiamiento financiamiento)
    {
        _contexto.Financiamientos.Add(financiamiento);
        return await _contexto.SaveChangesAsync() > 0;
    }

    // Modifica un financiamiento existente
    public async Task<bool> Modificar(Financiamiento financiamiento)
    {
        _contexto.Update(financiamiento);
        var modifico = await _contexto.SaveChangesAsync() > 0;
        _contexto.Entry(financiamiento).State = EntityState.Detached;
        return modifico;
    }

    public async Task<bool> Eliminar(int financiamientoId)
    {
        var financiamiento = await _contexto.Financiamientos.FindAsync(financiamientoId);
        if (financiamiento != null)
        {
            _contexto.Financiamientos.Remove(financiamiento);
            await _contexto.SaveChangesAsync();
            return true;
        }
        return false;
    }

    public async Task<bool> EliminarDetalle(int detalleId)
    {
        var detalle = await _contexto.FacturaDetalle.FindAsync(detalleId);
        if (detalle != null)
        {
            _contexto.FacturaDetalle.Remove(detalle);
            await _contexto.SaveChangesAsync();
            return true;
        }
        return false;
    }




    // Obtiene un financiamiento por ID (con relaciones si existen)
    public async Task<Financiamiento?> ObtenerPorId(int financiamientoId)
    {
        return await _contexto.Financiamientos
			.Include(v => v.VentasDetalles) // Cargar los detalles de la factura
		    .FirstOrDefaultAsync(f => f.FinanciamientoId == financiamientoId);
    }

    // Realiza una búsqueda de financiamiento por ID, sin seguimiento de cambios
    public async Task<Financiamiento> Buscar(int id)
    {
        return await _contexto.Financiamientos
            .Include(f => f.VentasDetalles)
            .FirstOrDefaultAsync(f => f.FinanciamientoId == id);
    }


    // Lista los financiamientos que coinciden con el criterio dado
    public async Task<List<Financiamiento>> Listar(Expression<Func<Financiamiento, bool>> criterio)
    {
        return await _contexto.Financiamientos
            .Include(f => f.VentasDetalles)
            .AsNoTracking() // No realiza seguimiento de cambios en las entidades
            .Where(criterio)
            .ToListAsync();
    }
}
