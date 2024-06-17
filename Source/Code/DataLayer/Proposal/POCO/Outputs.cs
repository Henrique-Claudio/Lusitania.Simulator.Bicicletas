using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Lusitania.Simuladores.DataLayer.Base.MODEL;

namespace Lusitania.Simuladores.DataLayer.Proposal.POCO
{
    [XmlRoot(ElementName = "OUTPUTS", Namespace = "")]
    public class Outputs
    {  

        [XmlArray(ElementName = "ERROS")]
        public List<Erro> Erros { get; set; }

        [XmlElement(ElementName = "REF_PROPOSTA")]
        public string REF_PROPOSTA { get; set; }

        public Outputs()
        {
            this.Erros = new List<Erro>();
        }

    }
}
