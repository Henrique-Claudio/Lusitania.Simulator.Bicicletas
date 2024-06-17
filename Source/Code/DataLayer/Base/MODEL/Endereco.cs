using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lusitania.Simuladores.DataLayer.Base.MODEL
{
    [Serializable]
    public class Endereco
    {

        public string AO_CUIDADO { get; set; }

        public string TELEFONE { get; set; }

        public string TELEX_FAX { get; set; }

        public string MORADA_TIPO { get; set; }

        public string TITULO_ARTERIA { get; set; }

        public string LOCAL { get; set; }

        public string MORADA_DESC { get; set; }

        public string MORADA_NUMERO { get; set; }

        public string MORADA_ANDAR { get; set; }

        public string E_MAIL { get; set; }

        public string LOCALIDADE { get; set; }

        public string CODIGO_POSTAL { get; set; }

        public string SUFIXO_POSTAL { get; set; }

        public string PAIS { get; set; }

        public string CPALF { get; set; }
    }
}
