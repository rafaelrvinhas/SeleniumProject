using NavegacaoAlura.Objeto;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Selenium.Navegadores;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

GoogleChrome googleChrome = new GoogleChrome(true);

googleChrome.Navegador.Navigate().GoToUrl("https://www.alura.com.br");

googleChrome.EscreveElemento("header-barraBusca-form-campoBusca", "pyton", 3, 5, true);

googleChrome.ClicaElementoPropriedade("button", "class", "header__nav--busca-submit", 3, 5, true);

googleChrome.ClicaElementoPropriedade("span", "class", "show-filter-options", 3, 5, true);

var listaFiltros = googleChrome.LocalizaElemento("busca--filtros--tipos", 3, 5, true);

var itemLista = listaFiltros.FindElements(By.TagName("li"));

foreach (var item in itemLista)
{
    var nomeItem = item.FindElement(By.ClassName("busca--filtro--nome-filtro"));

    if (nomeItem.Text.Trim().Equals("Cursos"))
    {
        item.FindElement(By.TagName("Label")).Click();
        break;
    }

}

Thread.Sleep(3000);

googleChrome.ClicaElementoPropriedade("input", "class", "busca-form-botao --desktop", 3, 5, true);

Thread.Sleep(3000);

var listaPesquisaRetorno = googleChrome.LocalizaElemento("busca-resultados", 3, 5, true);

var listaCursos = listaPesquisaRetorno.FindElement(By.ClassName("paginacao-pagina"));

var itemListaCursos = listaCursos.FindElements(By.TagName("li"));

List<Curso> cursos = new();

List<Professor> listaProfessores = null;

for (int i = 0; i < itemListaCursos.Count; i++)
{
    listaProfessores = new List<Professor>();

    Curso curso = new Curso();

    try
    {
        Thread.Sleep(3000);

        itemListaCursos[i].FindElement(By.TagName("a")).Click();

        Professor professores = new Professor();

        curso.Titulo = googleChrome.LocalizaElementoPropriedade("h1", "class", "curso-banner-course-title", 3, 5, true).Text;

        var professoresLocal = googleChrome.LocalizaElementosPropriedade("h3", "class", "instructor-title--name", 3, 5, true).ToArray();

        foreach (var prof in professoresLocal)
        {
            professores.NomeProfessor = prof.Text;
            listaProfessores.Add(professores);
        }

        curso.Professores = listaProfessores;
        curso.CargaHoraria = googleChrome.LocalizaElementoPropriedade("p", "class", "courseInfo-card-wrapper-infos", 3, 5, true).Text;
        curso.Descricao = googleChrome.LocalizaElementoPropriedade("p", "class", "course--banner-text-category", 3, 5, true).Text;


    }
    catch (Exception ex)
    {
        if (ex.Message.Contains("Não foi possível localizar o elemento"))
            curso.Professores = new List<Professor>();
        else
            i--;
    }

    cursos.Add(curso);

    Thread.Sleep(1000);

    googleChrome.Navegador.Navigate().Back();
}

using (StreamWriter temp = new StreamWriter("Cursos.csv"))
{
     
    foreach (var str in cursos)
    {
        string linha = string.Empty;
        string professores = string.Empty;

        linha = $"{str.Titulo};";

        foreach (var item in str.Professores)
        {
            professores = $"{professores} {item.NomeProfessor}";
        }

        linha = $"{linha}{professores};{str.CargaHoraria};{str.Descricao}";

        temp.WriteLine(linha);
    }
}

Thread.Sleep(10000);

googleChrome.EncerraDriver();