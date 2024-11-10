using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavegacaoAlura.Objeto
{
    public class Curso
    {
        public string Titulo { get; set; }
        public List<Professor> Professores { get; set; }
        public string CargaHoraria { get; set; }
        public string Descricao { get; set; }
    }
}
