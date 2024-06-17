using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Lusitania.Simuladores.DataLayer.Simulation.POCO
{
    [Serializable]
    [XmlType("DADOS_CONTRATO", Namespace = "")]
    public class Contrato
    {
        [XmlElement(ElementName = "IDPLANO")]
        public string PlanID { get; set; }

        [XmlElement(ElementName = "DATA_INICIO")]
        public string DataInicio { get; set; }

        [XmlElement(ElementName = "VENCIMENTO")]
        public string Vencimento { get; set; }

        [XmlElement(ElementName = "PS_DATA_NASC")]
        public string PessoaSeguraDataNasc { get; set; }

        [XmlElement(ElementName = "TOMADOR")]
        public Tomador Tomador { get; set; }

        [XmlElement(ElementName = "SDD")]
        public string SDD { get; set; }

        [XmlElement(ElementName = "DESCONTOCOM")]
        public double DescontoComercial { get; set; }

        [XmlElement(ElementName = "COD_PROTOCOLO")]
        public string Protocolo { get; set; }

        [XmlElement(ElementName = "NUM_ASSOCIADO")]
        public string NumAssociado { get; set; }

    }
}