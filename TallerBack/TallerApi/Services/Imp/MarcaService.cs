using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using TallerApi.Data;
using TallerApi.Models;

namespace TallerApi.Services.Imp
{
    public class MarcaService : IMarcaService
    {
        private TallerMecanicoPruebaContext db;


        public MarcaService(TallerMecanicoPruebaContext tallerDb)
        {
            db = tallerDb;
        }
        public bool DeleteMarca(int id)
        {
            try
            {
                var marca = db.Marcas.Find(id);
                if (marca!=null)
                {
                    db.Remove((Marca)marca);
                    db.SaveChanges();
                    return true;
                }
                return false;

            }
            catch (System.Exception)
            {

                return false;
            }
        }


        public string GetMarca(int id)
        {
            try
            {
                var marca = from m in db.Marcas where m.Id == id select m;
                if (marca.IsNullOrEmpty())
                {
                    return ("No se ha encontrado la marca solicitada.");
                }
                Marca marcaResp = marca.First();
                return marcaResp.Nombre;
            }
            catch (System.Exception)
            {

                return ("No se ha encontrado la marca solicitada.");
            }

        }

        public List<string> GetMarcas()
        {
            try
            {
                List<string> response = new();
                var marcas = from m in db.Marcas select m;
                marcas.ToList().ForEach(m =>
                {
                    if (m.Nombre != null && m.Nombre != "") { response.Add(m.Nombre); }
                });
                return response;
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        public string SaveMarca(string marca)
        {
            try
            {
                Marca m = new();
                m.Nombre = marca;
                db.Add(m);
                db.SaveChanges();
                return (m.Nombre);
            }
            catch (Exception)
            {
                return ("No se ha podido insertar la marca");
            }
        }

        public string UpdateMarca(int id, string marca)
        {
            try
            {
                var m = db.Marcas.Find(id);
                if(m == null){
                    return ("No existe la marca a modificar.");
                }
                m.Nombre = marca;
                db.Update(m);
                db.SaveChanges();
                return m.Nombre;
            }
            catch (System.Exception)
            {
                return ("Ha ocurrido un problema con la actualizacion de la marca, intente nuevamente luego.");
            }
        }
    }
}