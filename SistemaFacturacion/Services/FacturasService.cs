using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Data;
using SistemaFacturacion.Models;
using System.Linq.Expressions;

namespace SistemaFacturacion.Services;

public class FacturasService
{
	private readonly Contexto _contexto;

	public FacturasService(Contexto contexto)
	{
		_contexto = contexto;
	}

    public async Task<bool> Crear(Ventas factura)
    {
        if (!await Existe(factura.VentasId))
            return await Insertar(factura);
        else
            return await Modificar(factura);
    }


    public async Task<bool> Existe(int id)
	{
		return await _contexto.Facturas
			.AnyAsync(c => c.VentasId == id);
	}

    public async Task<bool> ExisteNombre(string nombre)
    {
        var productoExistente = await _contexto.Facturas
            .AnyAsync(p => p.NombreCliente.ToLower() == nombre.ToLower());

        return productoExistente;
    }

    public async Task<bool> Insertar(Ventas factura)
    {
        _contexto.Facturas.Add(factura);
        return await _contexto.SaveChangesAsync() > 0;
    }


    public async Task<bool> Modificar(Ventas factura)
	{
		_contexto.Update(factura);
		var modifico = await _contexto.SaveChangesAsync() > 0;
		_contexto.Entry(factura).State = EntityState.Detached;
		return modifico;
	}

    public async Task<bool> Eliminar(int ventaId)
    {
        var ventas = await _contexto.Facturas.FindAsync(ventaId);
        if (ventas != null)
        {
            _contexto.Facturas.Remove(ventas);
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




    public async Task<Ventas> ObtenerPorId(int id)
	{
		return await _contexto.Facturas
			.Include(v => v.FacturaDetalle) // Cargar los detalles de la factura
			.FirstOrDefaultAsync(v => v.VentasId == id);
	}


	public async Task<Ventas?> Buscar(int id)
	{
		return await _contexto.Facturas
			.Include(f => f.FacturaDetalle)
			.FirstOrDefaultAsync(c => c.VentasId == id);
	}

	public async Task<List<Ventas>> Listar(Expression<Func<Ventas, bool>> criterio)
	{
		return await _contexto.Facturas
			.Include(f => f.FacturaDetalle)
			.AsNoTracking()
			.Where(criterio)
			.ToListAsync();
	}
}
