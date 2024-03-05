using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerApi.Services
{
    public interface IMarcaService
    {
        public List<string> GetMarcas();
        public string GetMarca(int id);
        
        public string SaveMarca(string marca);
        
        public string UpdateMarca(int id, string marca);
        public bool DeleteMarca(int id);
    }
}