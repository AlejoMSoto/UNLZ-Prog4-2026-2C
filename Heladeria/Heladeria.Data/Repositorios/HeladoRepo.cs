using Heladeria.Data.Contexto;
using Heladeria.Data.Modelos;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Heladeria.Data.Repositorios
{
    public interface IHeladoRepositorio
    {
        List<Helado> ObtenerHelados();
        Helado? ObtenerHeladoPorId(int id);
        void AgregarHelado(Helado helado);
        void ActualizarHelado(Helado helado);
        bool BorrarHeladoPorId(int id);
    }

    public class HeladoRepo : IHeladoRepositorio
    {
        private HeladoContexto _db;

        public HeladoRepo(HeladoContexto context)
        {
            _db = context;
        }

        public void AgregarHelado(Helado helado)
        {
            _db.Helados.Add(helado);
            _db.SaveChanges();
        }

        public List<Helado> ObtenerHelados() => _db
            .Helados
            .ToList();


        public Helado ObtenerHeladoPorId(int id)
        {
            return _db.Helados.Find(id);
        }

        public void ActualizarHelado(Helado helado)
        {
            _db.Helados.Update(helado);
            _db.SaveChanges();
        }

        public bool BorrarHeladoPorId(int id)
        {
            var helado = _db.Helados.Find(id);
            if (helado is null) return false;
            helado.Borrado = true;
            _db.Update(helado);
            _db.SaveChanges();
            return true;
        }

    }
}
