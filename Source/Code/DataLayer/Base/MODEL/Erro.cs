using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Lusitania.Simuladores.DataLayer.Base.MODEL
{
    [Serializable]
    [XmlType("ERRO", Namespace = "")]
    public class Erro
    {
        [XmlText]
        public string MsgErro { get; set; }
    }
}
