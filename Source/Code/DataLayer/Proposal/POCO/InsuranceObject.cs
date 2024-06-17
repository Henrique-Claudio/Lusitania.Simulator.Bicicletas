using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Lusitania.Simuladores.DataLayer.Proposal.POCO
{    
    [XmlRoot(ElementName = "OBJETO_SEGURO", Namespace = "")]
    public class InsuranceObject
    {
        private string _ANO_AQUISICAO;
        [XmlElement(ElementName = "ANO_AQUISICAO")]
        public XmlNode ANO_AQUISICAO
        {
            get
            {
                return (new XmlDocument()).CreateCDataSection(_ANO_AQUISICAO);
            }
            set { _ANO_AQUISICAO = value.InnerText; }
        }

        private string _MARCA;
        [XmlElement(ElementName = "MARCA")]
        public XmlNode MARCA
        {
            get
            {
                return (new XmlDocument()).CreateCDataSection(_MARCA);
            }
            set { _MARCA = value.InnerText; }
        }

        private string _MODELO;
        [XmlElement(ElementName = "MODELO")]
        public XmlNode MODELO
        {
            get
            {
                return (new XmlDocument()).CreateCDataSection(_MODELO);
            }
            set { _MODELO = value.InnerText; }
        }

        private string _VERSAO;
        [XmlElement(ElementName = "VERSAO")]
        public XmlNode VERSAO
        {
            get
            {
                return (new XmlDocument()).CreateCDataSection(_VERSAO);
            }
            set { _VERSAO = value.InnerText; }
        }

        private string _NUM_QUADRO;
        [XmlElement(ElementName = "NUM_QUADRO")]
        public XmlNode NUM_QUADRO
        {
            get
            {
                return (new XmlDocument()).CreateCDataSection(_NUM_QUADRO);
            }
            set { _NUM_QUADRO = value.InnerText; }
        }

        private string _NUM_SERIE;
        [XmlElement(ElementName = "NUM_SERIE")]
        public XmlNode NUM_SERIE
        {
            get
            {
                return (new XmlDocument()).CreateCDataSection(_NUM_SERIE);
            }
            set { _NUM_SERIE = value.InnerText; }
        }

        private string _NUM_OUTRO;
        [XmlElement(ElementName = "NUM_OUTRO")]
        public XmlNode NUM_OUTRO
        {
            get
            {
                return (new XmlDocument()).CreateCDataSection(_NUM_OUTRO);
            }
            set { _NUM_OUTRO = value.InnerText; }
        }

        public InsuranceObject() { }

        public InsuranceObject(string anoAquisicao, string marca, string modelo, string versao, string nrQuadro) {
            _ANO_AQUISICAO = anoAquisicao;
            _MARCA = marca;
            _MODELO = modelo;
            _VERSAO = versao;
            _NUM_QUADRO = nrQuadro;
        }
    }
}
