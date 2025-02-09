using Microsoft.EntityFrameworkCore;
using SistemaFacturacion.Data;
using SistemaFacturacion.Models;
using System.Linq.Expressions;

namespace SistemaFacturacion.Services;

public class CuentasXcobrarService
{
    private readonly Contexto _contexto;

    public CuentasXcobrarService(Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<bool> Crear(CuentasXcobrar cuenta)
    {
        if (!await Existe(cuenta.CuentasXcobrarId))
            return await Insertar(cuenta);
        else
            return await Modificar(cuenta);
    }

    public async Task<bool> Existe(int id)
    {
        return await _contexto.CuentasXcobrar.AnyAsync(c => c.CuentasXcobrarId == id);
    }
    public async Task<bool> ExisteNombre(string nombre)
    {
        var productoExistente = await _contexto.CuentasXcobrar
            .AnyAsync(p => p.NombreCliente.ToLower() == nombre.ToLower());

        return productoExistente;
    }


    public async Task<bool> Insertar(CuentasXcobrar cuenta)
    {
        _contexto.CuentasXcobrar.Add(cuenta);
        return await _contexto.SaveChangesAsync() > 0;
    }

    public async Task<bool> Modificar(CuentasXcobrar cuenta)
    {
        _contexto.Update(cuenta);
        var modifico = await _contexto.SaveChangesAsync() > 0;
        _contexto.Entry(cuenta).State = EntityState.Detached;
        return modifico;
    }

    public async Task<bool> Eliminar(int cuentaId)
    {
        var cuenta = await _contexto.CuentasXcobrar.FindAsync(cuentaId);
        if (cuenta != null)
        {
            _contexto.CuentasXcobrar.Remove(cuenta);
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

    public async Task<CuentasXcobrar?> ObtenerPorId(int cuentaId)
    {
        return await _contexto.CuentasXcobrar
            .Include(c => c.VentasDetalles)
            .FirstOrDefaultAsync(c => c.CuentasXcobrarId == cuentaId);
    }

    public async Task<CuentasXcobrar> Buscar(int id)
    {
        return await _contexto.CuentasXcobrar
            .Include(c => c.VentasDetalles)
            .FirstOrDefaultAsync(c => c.CuentasXcobrarId == id);
    }

    public async Task<List<CuentasXcobrar>> Listar(Expression<Func<CuentasXcobrar, bool>> criterio)
    {
        return await _contexto.CuentasXcobrar
            .Include(c => c.VentasDetalles)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
