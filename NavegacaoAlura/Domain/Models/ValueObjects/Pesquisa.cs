using NavegacaoAlura.Services.Navegador;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavegacaoAlura.Domain.Models.ValueObjects
{
    public class Pesquisa
    {
        private readonly GoogleChrome _googleChrome;
        public IWebElement ListaFiltros { get; set; }
        public ICollection<IWebElement> ItemLista { get; set; }
        public IWebElement? ListaPesquisaRetorno { get; set; }
        public IWebElement? ListaCursos { get; set; }
        public ReadOnlyCollection<IWebElement>? ItemListaCursos { get; set; }

        public Pesquisa(GoogleChrome googleChrome)
        {
            _googleChrome = googleChrome;

            ListaFiltros = _googleChrome.LocalizaElemento("busca--filtros--tipos", 3, 5, true);
            ItemLista = ListaFiltros.FindElements(By.TagName("li"));

            EfetuaClick();
            Thread.Sleep(3000);

            googleChrome.ClicaElementoPropriedade("input", "class", "busca-form-botao --desktop", 3, 5, true);
            Thread.Sleep(3000);
        }

        public void ObtemElementos()
        {
            ListaPesquisaRetorno = _googleChrome.LocalizaElemento("busca-resultados", 3, 5, true);
            ListaCursos = ListaPesquisaRetorno.FindElement(By.ClassName("paginacao-pagina"));
            ItemListaCursos = ListaCursos.FindElements(By.TagName("li"));
        }

        private void EfetuaClick()
        {
            foreach (var item in ItemLista)
            {
                var nomeItem = item.FindElement(By.ClassName("busca--filtro--nome-filtro"));
                if (nomeItem.Text.Trim().Equals("Cursos"))
                {
                    item.FindElement(By.TagName("Label")).Click();
                    break;
                }
            }
        }
    }
}
