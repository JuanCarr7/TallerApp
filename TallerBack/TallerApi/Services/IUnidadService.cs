using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace TallerApi.Services
{
    public interface IUnidadService
    {
        public List<string> GetUnidades();
        public string? GetUnidad(int id);
        public string SaveUnidad(string unidad);
        public bool DeleteUnidad(int id);
        public string UpdateUnidad(int id, string unidad);
    }
}