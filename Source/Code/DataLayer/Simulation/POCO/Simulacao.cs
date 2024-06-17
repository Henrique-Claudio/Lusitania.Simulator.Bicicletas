using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;


namespace Lusitania.Simuladores.DataLayer.Simulation.POCO
{
    [Serializable]
    [XmlRoot(ElementName = "SIMULACAO", Namespace = "")]
    public class Simulacao
    {
        [XmlElement(ElementName = "LOGIN")]
        public string Login { get; set; }

        [XmlElement(ElementName = "GRAVAR")]
        public string Gravar { get; set; }

        [XmlElement(ElementName = "COBRADOR")]
        public string Cobrador { get; set; }

        [XmlElement(ElementName = "MEDIADOR")]
        public string Mediador { get; set; }

        [XmlElement(ElementName = "DADOS_ELABORADOR")]
        public Elaborador Elaborador { get; set; }

        [XmlElement(ElementName = "DADOS_CONTRATO")]
        public Contrato Contrato { get; set; }

        [XmlElement(ElementName = "OUTPUTS")]
        public Outputs Outputs { get; set; }

    }
}
