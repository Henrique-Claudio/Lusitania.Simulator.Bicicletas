using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Lusitania.Simuladores.DataLayer.Simulation.POCO
{
    [Serializable]
    [XmlType("VALOR", Namespace = "")]
    public class Premio
    {
        [XmlAttribute(AttributeName = "fracionamento")]
        public string Fraccionamento { get; set; }

        [XmlElement(ElementName = "PREMIO_COMERCIAL")]
        public string PremioComercial { get; set; }

        [XmlElement(ElementName = "PREMIO_TOTAL")]
        public string PremioTotal { get; set; }

        [XmlElement(ElementName = "VALOR_RECIBO")]
        public string ValorRecibo { get; set; }

        [XmlElement(ElementName = "PERIODO_1_RECIBO")]
        public string PeriodoRecibo_1 { get; set; }

        [XmlElement(ElementName = "VALOR_REC_SEGUINTE")]
        public string ValorReciboSeguinte { get; set; }

        [XmlElement(ElementName = "PREMIO_DESCPLANO")]
        public string PremioPlano { get; set; }

        [XmlElement(ElementName = "DESC_PROTONEG")]
        public decimal Desconto { get; set; }
    }


    
}
