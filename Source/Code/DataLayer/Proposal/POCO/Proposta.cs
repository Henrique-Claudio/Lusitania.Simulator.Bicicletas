using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Lusitania.Simuladores.DataLayer.Proposal.POCO
{
    [Serializable]
    [XmlRoot(ElementName = "PROPOSTA", Namespace = "")]
    public class Proposta
    {
        [XmlElement(ElementName = "LOGIN")]
        public string LOGIN { get; set; }

        [XmlElement(ElementName = "IDSIMULACAO")]
        public string IDSIMULACAO { get; set; }

        [XmlElement(ElementName = "COBRADOR")]
        public string COBRADOR { get; set; }

        [XmlElement(ElementName = "MEDIADOR")]
        public string MEDIADOR { get; set; }

        [XmlElement(ElementName = "FRACCIONAMENTO")]
        public string FRACCIONAMENTO { get; set; }

        [XmlElement(ElementName = "IBAN")]
        public string IBAN { get; set; }

        [XmlElement(ElementName = "BIC")]
        public string BIC { get; set; }

        [XmlElement(ElementName = "DEBITO_FALTA_PAG")]
        public string DEBITO_FALTA_PAG { get; set; }

        [XmlElement(ElementName = "QTD_SINISTROS")]
        public string QTD_SINISTROS { get; set; }

        [XmlElement(ElementName = "DOCS_POR_EMAIL")]
        public string DOCS_POR_EMAIL { get; set; }

        [XmlElement(ElementName = "EMAIL_PARA_DOCS")]
        public string EMAIL_PARA_DOCS { get; set; }

        [XmlElement(ElementName = "PS_E_TOMADOR")]
        public string PS_E_TOMADOR { get; set; }

        [XmlElement(ElementName = "OBSERVACOES")]
        public string OBSERVACOES { get; set; }

        [XmlElement(ElementName = "CONFIDENCIALIDADE_PS")]
        public string CONFIDENCIALIDADE_PS { get; set; }

        [XmlElement(ElementName = "HERDEIROS_LEGAIS")]
        public string HERDEIROS_LEGAIS { get; set; }

        [XmlElement(ElementName = "TIPO_BENEFICIARIO")]
        public string TIPO_BENEFICIARIO { get; set; }

        [XmlElement(ElementName = "CLAUSULA_GENERICA")]
        public string CLAUSULA_GENERICA { get; set; }

        [XmlArray(ElementName = "PESSOAS")]
        public List<ProposalPerson> PESSOAS { get; set; }

        [XmlElement(ElementName = "OBJETO_SEGURO")]
        public InsuranceObject InsuranceObject { get; set; }

        [XmlElement(ElementName = "OUTPUTS")]
        public Outputs Outputs { get; set; }

        public Proposta()
        {
            this.PESSOAS = new List<ProposalPerson>();
            this.Outputs = new Outputs();
        }

    }
}
