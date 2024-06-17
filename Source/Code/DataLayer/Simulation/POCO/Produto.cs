using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml.Serialization;

namespace Lusitania.Simuladores.DataLayer.Simulation.POCO
{
    [Serializable]
    [XmlType("PRODUTO", Namespace = "")]
    public class Produto
    {
        [XmlAttribute(AttributeName = "cod")]
        public string ProductID { get; set; }

        [XmlAttribute(AttributeName = "idsimulacao")]
        public string SimulalationID { get; set; }

        [XmlArray(ElementName = "VALORES")]
        public List<Premio> premios { get; set; }
    }
}
