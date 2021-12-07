using System.Collections.Generic;

namespace codeyes.msc.each.Models
{
    public class Analytics
    {
        public Analytics()
        {
            Marcacao = new Dictionary<string, string>();
            Comportamento = new Dictionary<string, string>();
            Conteudo = new Dictionary<string, string>();
            Design = new Dictionary<string, string>();
            LeitorTela = new Dictionary<string, string>();
        }
        public Dictionary<string, string> Marcacao { get; set; }
        public Dictionary<string, string> Comportamento { get; set; }
        public Dictionary<string, string> Conteudo { get; set; }
        public Dictionary<string, string> Design { get; set; }
        public Dictionary<string, string> LeitorTela { get; set; }
    }
}