using AngleSharp;
using AngleSharp.Dom;
using codeyes.msc.each.Models;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;
using System.Threading;

namespace codeyes.msc.each.Mediator
{
    public class ScrapperService
    {
        private IBrowsingContext context { get; set; }
        private ChromeDriver driver { get; set; }
        private Analytics analytics { get; set; }

        public ScrapperService()
        {
            var config = Configuration.Default.WithDefaultLoader();
            context = BrowsingContext.New(config);
            ChromeOptions chrome_options = new ChromeOptions();
            chrome_options.Proxy = null;
            driver = new ChromeDriver(chrome_options);
            analytics = null;
        }

        public Report GetAnalyticsAsync(string uri)
        {
            //Tribunal de justiça do maranhão
            driver.Url = "https://pje.tjma.jus.br/pje/ConsultaPublica/listView.seam";

            driver.FindElement(By.Id("fPP:dnp:nomeParte")).SendKeys(uri.Replace("http://","").Replace("https://", ""));
            driver.FindElement(By.Name("fPP:searchProcessos")).Click();

            Thread.Sleep(5000);

            string webPage = driver.FindElement(By.Id("fPP:processosTable:j_id236"))
                  .FindElement(By.CssSelector("div[class='pull-right']"))
                  .FindElement(By.CssSelector("span[class='text-muted']")).Text;

            analytics = new Analytics();

            var htmlDocument = new HtmlAgilityPack.HtmlDocument();
            htmlDocument.LoadHtml(webPage);

            foreach (HtmlAgilityPack.HtmlNode item in htmlDocument.GetElementbyId("").ChildNodes)
            {
                analytics.Comportamento.Add("STF", "nota");
                analytics.Conteudo.Add("STJ", "nota");
                analytics.Design.Add("CJF", "nota");
            }

            // Diferencial do produto
            ConsolidaResuldadoUnico(analytics, webPage);

            driver.Quit();

            return Parse(analytics);
        }

        public Report Parse(Analytics analytics)
        {
            return new Mapper().Parse(analytics);
        }

        public void ConsolidaResuldadoUnico(Analytics analytics, string webPage)
        {
            string pattern = @"\d+";
            var _match = System.Text.RegularExpressions.Regex.Match(webPage, pattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            analytics.LeitorTela.Add("Consolida", _match.Value);
            analytics.Marcacao.Add("TJEM", webPage);
        }
    }
}