using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Lusitania.Simuladores.DataLayer.Simulation.POCO
{
    [Serializable]
    [XmlType("TOMADOR", Namespace = "")]
    public class Tomador
    {
        [XmlElement(ElementName = "NIF")]
        public string NIF { get; set; }

        [XmlElement(ElementName = "EMAIL")]
        public string Email { get; set; }

        [XmlElement(ElementName = "NOME")]
        public string Nome { get; set; }

        [XmlElement(ElementName = "TELEFONE")]
        public string Telefon { get; set; }

        [XmlElement(ElementName = "TELEMOVEL")]
        public string Telemovel { get; set; }


    }
}
