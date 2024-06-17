using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Lusitania.Simuladores.DataLayer.Base.MODEL;

namespace Lusitania.Simuladores.DataLayer.Simulation.POCO
{
    [Serializable]
    [XmlRoot(ElementName = "OUTPUTS", Namespace = "")]
    public class Outputs
    {

        [XmlArray(ElementName = "PRODUTOS")]
        public List<Produto> Produtos { get; set; }

        [XmlArray(ElementName = "ERROS")]
        public List<Erro> Erros { get; set; }
        
    }
}
