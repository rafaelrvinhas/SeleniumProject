using NavegacaoAlura.Domain.Models.Entities;
using NavegacaoAlura.Domain.Models.ValueObjects;
using NavegacaoAlura.Infra;
using NavegacaoAlura.Services.Interfaces;
using OpenQA.Selenium;

namespace NavegacaoAlura.Services.Navegador
{
    public class NavegadorAlura : INavegadorAlura
    {
        public GoogleChrome? GoogleChrome { get; set; }
        public List<Curso>? Cursos { get; set; }
        public List<Professor>? ListaProfessores { get; set; }

        public NavegadorAlura()
        {
            Cursos = new List<Curso>();
            ListaProfessores = new List<Professor>();
        }

        public void ExecutaNavegacao()
        {
            GoogleChrome = new GoogleChrome(true);
            GoogleChrome.Navegador.Navigate().GoToUrl("https://www.alura.com.br");
            GoogleChrome.EscreveElemento("header-barraBusca-form-campoBusca", "pyton", 3, 5, true);
            GoogleChrome.ClicaElementoPropriedade("button", "class", "header__nav--busca-submit", 3, 5, true);
            GoogleChrome.ClicaElementoPropriedade("span", "class", "show-filter-options", 3, 5, true);

            var pesquisa = new Pesquisa(GoogleChrome);
            pesquisa.ObtemElementos();

            InstanciarDominios(pesquisa);

            var persistencia = new Persistencia();
            persistencia.GerarCSV(Cursos);

            GoogleChrome.EncerraDriver();
        }

        private void InstanciarDominios(Pesquisa pesquisa)
        {

            for (int i = 0; i < pesquisa?.ItemListaCursos?.Count; i++)
            {
                ListaProfessores = new List<Professor>();

                Curso? curso = new Curso();

                try
                {
                    Thread.Sleep(3000);

                    pesquisa.ItemListaCursos[i].FindElement(By.TagName("a")).Click();
                    var titulo = GoogleChrome?.LocalizaElementoPropriedade("h1", "class", "curso-banner-course-title", 3, 5, true).Text;
                    var professoresLocal = GoogleChrome?.LocalizaElementosPropriedade("h3", "class", "instructor-title--name", 3, 5, true).ToArray();
                    
                    foreach (var prof in professoresLocal)
                    {
                        var professor = new Professor(prof.Text);
                        ListaProfessores.Add(professor);
                    }

                    var cargaHoraria = GoogleChrome?.LocalizaElementoPropriedade("p", "class", "courseInfo-card-wrapper-infos", 3, 5, true).Text;
                    var descricao = GoogleChrome?.LocalizaElementoPropriedade("p", "class", "course--banner-text-category", 3, 5, true).Text;

                    curso = new Curso(titulo, ListaProfessores, cargaHoraria, descricao);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Não foi possível localizar o elemento"))
                        curso.Professores = new List<Professor>();
                    else
                        i--;
                }

                Cursos?.Add(curso);
                Thread.Sleep(1000);

                GoogleChrome?.Navegador.Navigate().Back();
            }
        }
    }
}
