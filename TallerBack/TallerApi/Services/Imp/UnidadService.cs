using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TallerApi.Data;
using TallerApi.Models;

namespace TallerApi.Services.Imp
{
    public class UnidadService : IUnidadService
    {

        TallerMecanicoPruebaContext db;
        public UnidadService(TallerMecanicoPruebaContext TallerDb)
        {
            db = TallerDb;
        }



        public bool DeleteUnidad(int id)
        {
            try
            {
                var unidad = db.Unidades.Find(id);
                if (unidad == null || unidad.Descripcion == null) { return false; }
                db.Remove(unidad);
                db.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public string? GetUnidad(int id)
        {
            try
            {
                var unidad = db.Unidades.Find(id);
                if (unidad == null || unidad.Descripcion == null) { return null; }
                return unidad.Descripcion;
            }
            catch (System.Exception)
            {

                throw;
            }
        }

        public List<string> GetUnidades()
        {
            try
            {
                var unidades = from u in db.Unidades select u;
                List<string> response = new();
                unidades.ToList().ForEach(u => response.Add(u.Descripcion));
                return response;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public string SaveUnidad(string unidad)
        {
            try
            {
                Unidade u = new();
                u.Descripcion = unidad;
                db.Add(u);
                db.SaveChanges();
                return (u.Descripcion);
            }
            catch (System.Exception)
            {

                return ("Ha ocurrido un problema al insertar la nueva unidad de medida.");
            }
        }

        public string UpdateUnidad(int id, string unidad)
        {
            try
            {
                Unidade? u = db.Unidades.Find(id);
                if (u != null && u.Descripcion != null)
                {
                    u.Descripcion = unidad;
                    db.Update(u);
                    db.SaveChanges();
                    return (u.Descripcion);
                }
                return ("Ha ocurrido un problema al buscar la unidad de medida a actualizar.");
            }
            catch (System.Exception)
            {

                return ("Ha ocurrido un problema al actualizar la unidad de medida.");
            }
        }
    }
}