using NavegacaoAlura.Domain.Models.Entities;

namespace NavegacaoAlura.Infra
{
    public class Persistencia
    {
        public Persistencia()
        { }

        public void GerarCSV(List<Curso>? cursos)
        {
            using StreamWriter temp = new StreamWriter("Cursos.csv");
            foreach (var curso in cursos)
            {
                string linha = string.Empty;
                string professores = string.Empty;

                linha = $"{curso.Titulo};";

                foreach (var professor in curso.Professores)
                {
                    professores = $"{professores} {professor.Nome}";
                }

                linha = $"{linha}{professores};{curso.CargaHoraria};{curso.Descricao}";
                temp.WriteLine(linha);
            }
        }
    }
}
