using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavegacaoAlura.Domain.Models.Entities
{
    public class Professor
    {
        public Professor(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }
    }
}
