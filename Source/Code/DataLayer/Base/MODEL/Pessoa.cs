using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lusitania.Simuladores.DataLayer.Base.MODEL
{
    [Serializable]
    public class Pessoa
    {
        public int  ORDEM { get; set; }

        public string TIPOPESSOA { get; set; }

        public string PARENTESCO { get; set; }

        public string PCODIGO { get; set; }

        public string PCONTRIB { get; set; }

        public string PBI { get; set; }

        public string CARTA_CONDUCAO { get; set; }

        public string PTITULO { get; set; }

        public string PNOME { get; set; }

        public string NACIONALIDADE { get; set; }

        public string ESTADO_CIVIL { get; set; }

        public string PPROFISS { get; set; }

        public string PTIPO { get; set; }

        public string TELEMOVEL { get; set; }

        public Endereco Contacto01 { get; set; }

        public Endereco Contacto02 { get; set; }

        public string NUMREGISTOS { get; set; }

        public string DT_NASCIMENTO { get; set; }

        public string DT_CARTA_CONDUCAO { get; set; }

        public string CLIENTE_MG { get; set; }

        public string CODIGO_EMPREGADO_MG { get; set; }

        public string CODIGO_ASSOCIADO_MG { get; set; }

        public string BALCAO_TITULAR { get; set; }


        public Pessoa() {
            this.Contacto01 = new Endereco();
            this.Contacto02 = new Endereco();
        }
    }
}
