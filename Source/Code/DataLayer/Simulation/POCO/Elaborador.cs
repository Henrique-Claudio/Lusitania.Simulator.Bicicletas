using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Lusitania.Simuladores.DataLayer.Simulation.POCO
{
    [Serializable]
    [XmlType("DADOS_ELABORADOR", Namespace = "")]
    public class Elaborador
    {

        [XmlElement(ElementName = "NOME")]
        public string Nome { get; set; }

        [XmlElement(ElementName = "EMAIL")]
        public string Email { get; set; }

        [XmlElement(ElementName = "TELEFONE")]
        public string Telefon { get; set; }
    }
}
