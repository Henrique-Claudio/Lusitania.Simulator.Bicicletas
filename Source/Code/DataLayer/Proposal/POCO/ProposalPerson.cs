using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Lusitania.Simuladores.DataLayer.Proposal.POCO
{
    [XmlType("PESSOA", Namespace = "")]
    public class ProposalPerson
    {

        [XmlAttribute(AttributeName = "TIPO")]
        public string TIPO { get; set; }

        [XmlAttribute(AttributeName = "SEQUENCIA")]
        public string SEQUENCIA { get; set; }



        [XmlElement(ElementName = "PCODIGO")]
        public string PCODIGO { get; set; }

        [XmlElement(ElementName = "TITULO_HONORIFICO")]
        public string TITULO_HONORIFICO { get; set; }

        [XmlElement(ElementName = "NOME")]
        public string NOME { get; set; }

        [XmlElement(ElementName = "PARENTESCO")]
        public string PARENTESCO { get; set; }

        [XmlElement(ElementName = "MORADA_TIPO")]
        public string MORADA_TIPO { get; set; }

        [XmlElement(ElementName = "TITULO_ARTERIA")]
        public string TITULO_ARTERIA { get; set; }

        [XmlElement(ElementName = "MORADA_DESC")]
        public string MORADA_DESC { get; set; }

        [XmlElement(ElementName = "MORADA_NUMERO")]
        public string MORADA_NUMERO { get; set; }

        [XmlElement(ElementName = "MORADA_ANDAR")]
        public string MORADA_ANDAR { get; set; }

        [XmlElement(ElementName = "LOCALIDADE")]
        public string LOCALIDADE { get; set; }

        [XmlElement(ElementName = "LOCAL")]
        public string LOCAL { get; set; }

        [XmlElement(ElementName = "CPALF")]
        public string CPALF { get; set; }

        [XmlElement(ElementName = "CODIGO_POSTAL")]
        public string CODIGO_POSTAL { get; set; }

        [XmlElement(ElementName = "SUFIXO_POSTAL")]
        public string SUFIXO_POSTAL { get; set; }

        [XmlElement(ElementName = "PAIS")]
        public string PAIS { get; set; }

        [XmlElement(ElementName = "SEXO")]
        public string SEXO { get; set; }

        [XmlElement(ElementName = "DATA_NASCIMENTO")]
        public string DATA_NASCIMENTO { get; set; }

        [XmlElement(ElementName = "ESTADO_CIVIL")]
        public string ESTADO_CIVIL { get; set; }

        [XmlElement(ElementName = "NUMERO_CONTRIBUINTE")]
        public string NUMERO_CONTRIBUINTE { get; set; }

        [XmlElement(ElementName = "BI")]
        public string BI { get; set; }

        [XmlElement(ElementName = "NACIONALIDADE")]
        public string NACIONALIDADE { get; set; }

        [XmlElement(ElementName = "PROFISSAO")]
        public string PROFISSAO { get; set; }

        [XmlElement(ElementName = "TELEFONE")]
        public string TELEFONE { get; set; }

        [XmlElement(ElementName = "TELEMOVEL")]
        public string TELEMOVEL { get; set; }

        [XmlElement(ElementName = "TELEX_FAX")]
        public string TELEX_FAX { get; set; }

        [XmlElement(ElementName = "E_MAIL")]
        public string E_MAIL { get; set; }

        [XmlElement(ElementName = "AO_CUIDADO")]
        public string AO_CUIDADO { get; set; }

        [XmlElement(ElementName = "MORADA_TIPO_2")]
        public string MORADA_TIPO_2 { get; set; }

        [XmlElement(ElementName = "TITULO_ARTERIA_2")]
        public string TITULO_ARTERIA_2 { get; set; }

        [XmlElement(ElementName = "MORADA_DESC_2")]
        public string MORADA_DESC_2 { get; set; }

        [XmlElement(ElementName = "MORADA_NUMERO_2")]
        public string MORADA_NUMERO_2 { get; set; }

        [XmlElement(ElementName = "MORADA_ANDAR_2")]
        public string MORADA_ANDAR_2 { get; set; }

        [XmlElement(ElementName = "LOCALIDADE_2")]
        public string LOCALIDADE_2 { get; set; }

        [XmlElement(ElementName = "LOCAL_2")]
        public string LOCAL_2 { get; set; }

        [XmlElement(ElementName = "CPALF_2")]
        public string CPALF_2 { get; set; }

        [XmlElement(ElementName = "CODIGO_POSTAL_2")]
        public string CODIGO_POSTAL_2 { get; set; }

        [XmlElement(ElementName = "SUFIXO_POSTAL_2")]
        public string SUFIXO_POSTAL_2 { get; set; }

        [XmlElement(ElementName = "PAIS_2")]
        public string PAIS_2 { get; set; }
    }
}
