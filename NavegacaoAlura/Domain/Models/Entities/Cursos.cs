using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavegacaoAlura.Domain.Models.Entities
{
    public class Curso
    {
        public Curso()
        {
            Titulo = string.Empty;
            Professores = new List<Professor>();
            CargaHoraria = string.Empty;
            Descricao = string.Empty;
        }

        public Curso(
            string titulo,
            ICollection<Professor> professores,
            string cargaHoraria,
            string descricao)
        {
            Titulo = titulo;
            Professores = professores;
            CargaHoraria = cargaHoraria;
            Descricao = descricao;
        }

        public string Titulo { get; set; }
        public ICollection<Professor> Professores { get; set; }
        public string CargaHoraria { get; set; }
        public string Descricao { get; set; }
    }
}
