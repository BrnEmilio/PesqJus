using System;

namespace codeyes.msc.each.Models
{
    public class Report
    {
        public Report(Analytics analytics)
        {
            Analytics = analytics;
            QuantidadeProcesso = int.Parse((analytics.LeitorTela["Consolida"] == "" ? "0" : analytics.LeitorTela["Consolida"]));
            DateTime = DateTime.Now;
            DescricaoProcesso = analytics.Marcacao["TJEM"];
        }
        public DateTime DateTime { get; set; }
        public int QuantidadeProcesso { get; set; }
        public string DescricaoProcesso { get; set; }
        public Analytics Analytics { get; set; }
    }
}