using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Oracle.DataAccess.Client;
using Oracle.DataAccess;


namespace Lusitania.Simuladores.Common.Auto
{

    /// <summary>
    /// Summary description for PlanoFamilia
    /// </summary>
    public class PlanoFamilia
    {

        #region ** Propriedades Privadas **

        private int _idSimulacao;
        private int _NumContratos;
        private int _NumRamos;
        private string _Apolice;
        private string _Recibo;
        private string _Nif;
        private string _TomadorNome;
        private string _TomadorTelefone;
        private DateTime _TomadorDataCarta;
        private string _TomadorProfissao;
        private string _TomadorSexo;
        private string _TomadorEstadoCivil;
        private DateTime _TomadorDataNascimento;
        private string _PCodigo;

        private string _Titulo;
        private string _MoradaTipo;
        private string _MoradaDesc;
        private string _MoradaNum;
        private string _MoradaAndar;
        private string _Localidade;
        private string _CodPostal;
        private string _SufixoPostal;
        private string _Pais;
        private string _BI;
        private string _Nacionalidade;
        private string _Fax;
        private string _Email;
        private string _CartaNum;

        //Segundo Contacto
        private string _ContactoNome;
        private string _ContactoMoradaTipo;
        private string _ContactoMoradaDesc;
        private string _ContactoMoradaNum;
        private string _ContactoMoradaAndar;
        private string _ContactoLocalidade;
        private string _ContactoCodPostal;
        private string _ContactoSufixoPostal;
        private string _ContactoPais;
        private string _ContactoFax;
        private string _ContactoTelefone;

        #endregion

        #region ** Propriedades Públicas **

        public int idSimulacao
        {
            get { return _idSimulacao; }
        }

        public int NumContratos
        {
            get { return _NumContratos; }
        }

        public int NumRamos
        {
            get { return _NumRamos; }
        }

        public string Apolice
        {
            get { return _Apolice; }
        }

        public string Recibo
        {
            get { return _Recibo; }
        }

        public string Nif
        {
            get { return _Nif; }
        }

        public string TomadorNome
        {
            get { return _TomadorNome; }
        }

        public string TomadorTelefone
        {
            get { return _TomadorTelefone; }
        }

        public DateTime TomadorDataCarta
        {
            get { return _TomadorDataCarta; }
        }

        public string TomadorProfissao
        {
            get { return _TomadorProfissao; }
        }

        public string TomadorSexo
        {
            get { return _TomadorSexo; }
        }

        public string TomadorEstadoCivil
        {
            get { return _TomadorEstadoCivil; }
        }

        public DateTime TomadorDataNascimento
        {
            get { return _TomadorDataNascimento; }
        }

        /// <summary>
        /// Código Pessoa
        /// </summary>
        public string PCodigo
        {
            get { return _PCodigo; }
        }


        public string Titulo
        {
            get { return _Titulo; }
        }

        public string MoradaTipo
        {
            get { return _MoradaTipo; }
        }

        public string MoradaDesc
        {
            get { return _MoradaDesc; }
        }

        public string MoradaNum
        {
            get { return _MoradaNum; }
        }

        public string MoradaAndar
        {
            get { return _MoradaAndar; }
        }

        public string Localidade
        {
            get { return _Localidade; }
        }

        public string CodPostal
        {
            get { return _CodPostal; }
        }

        public string SufixoPostal
        {
            get { return _SufixoPostal; }
        }

        public string Pais
        {
            get { return _Pais; }
        }

        public string BI
        {
            get { return _BI; }
        }

        public string Nacionalidade
        {
            get { return _Nacionalidade; }
        }

        public string Fax
        {
            get { return _Fax; }
        }

        public string Email
        {
            get { return _Email; }
        }

        public string CartaNum
        {
            get { return _CartaNum; }
        }


        /// <summary>
        /// Nome Segundo Contacto
        /// </summary>
        public string ContactoNome
        {
            get { return _ContactoNome; }
        }

        public string ContactoMoradaTipo
        {
            get { return _ContactoMoradaTipo; }
        }

        public string ContactoMoradaDesc
        {
            get { return _ContactoMoradaDesc; }
        }

        public string ContactoMoradaNum
        {
            get { return _ContactoMoradaNum; }
        }

        public string ContactoMoradaAndar
        {
            get { return _ContactoMoradaAndar; }
        }

        public string ContactoLocalidade
        {
            get { return _ContactoLocalidade; }
        }

        public string ContactoCodPostal
        {
            get { return _ContactoCodPostal; }
        }

        public string ContactoSufixoPostal
        {
            get { return _ContactoSufixoPostal; }
        }

        public string ContactoPais
        {
            get { return _ContactoPais; }
        }

        public string ContactoFax
        {
            get { return _ContactoFax; }
        }

        public string ContactoTelefone
        {
            get { return _ContactoTelefone; }
        }


        #endregion


        /// <summary>
        /// Dados de um Plano Família
        /// </summary>
        /// <param name="IDPlanoFamilia"></param>
        /// <param name="NumSimulacao"></param>
        public PlanoFamilia(int IDPlanoFamilia,
                            int NumSimulacao)
        {
            _idSimulacao = 0;
            _TomadorDataCarta = DateTime.MinValue;
            _TomadorDataNascimento = DateTime.MinValue;


            OracleConnection cn = new OracleConnection(LusOracleDB.ConnectionString);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cn;
            cmd.CommandText = "WEB_SIM_MOTORE_GERAL_PKG.Saber_IDSim_Plano_Familia";
            cmd.CommandType = CommandType.StoredProcedure;

            //Parametros IN
            cmd.Parameters.Add("a_IDPlanoFamilia", OracleDbType.Int32).Value = IDPlanoFamilia;
            cmd.Parameters.Add("a_NumSimulacao", OracleDbType.Int32).Value = NumSimulacao;

            //Parametros OUT
            cmd.Parameters.Add("a_IDSimulacao", OracleDbType.Int32).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_Apolice", OracleDbType.Varchar2, 7).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_Recibo", OracleDbType.Varchar2, 6).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_NIF", OracleDbType.Varchar2, 9).Direction = ParameterDirection.Output;

            cmd.Parameters.Add("a_TNome", OracleDbType.Varchar2, 50).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_TTELEFONE", OracleDbType.Varchar2, 15).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_TDATACARTA", OracleDbType.Date).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_TPROFISSAO", OracleDbType.Varchar2, 6).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_TSEXO", OracleDbType.Varchar2, 1).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_TESTADOCIVIL", OracleDbType.Varchar2, 1).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_TDATANASCIMENTO", OracleDbType.Date).Direction = ParameterDirection.Output;

            cmd.Parameters.Add("a_NumContratos", OracleDbType.Int32).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_NumRamos", OracleDbType.Int32).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_PCodigo", OracleDbType.Int32).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_cursor", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

            try
            {
                cn.Open();
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                _idSimulacao = int.Parse(cmd.Parameters["a_IDSimulacao"].Value.ToString());
                _Apolice = cmd.Parameters["a_Apolice"].Value.ToString();
                _Recibo = cmd.Parameters["a_Recibo"].Value.ToString();
                _Nif = cmd.Parameters["a_NIF"].Value.ToString();

                _TomadorNome = cmd.Parameters["a_TNome"].Value.ToString();
                _TomadorTelefone = cmd.Parameters["a_TTELEFONE"].Value.ToString();

                if (cmd.Parameters["a_TDATACARTA"].Value != DBNull.Value)
                {
                    _TomadorDataCarta = Convert.ToDateTime(cmd.Parameters["a_TDATACARTA"].Value.ToString());
                }

                _TomadorProfissao = cmd.Parameters["a_TPROFISSAO"].Value.ToString();
                _TomadorSexo = cmd.Parameters["a_TSEXO"].Value.ToString();
                _TomadorEstadoCivil = cmd.Parameters["a_TESTADOCIVIL"].Value.ToString();

                if (cmd.Parameters["a_TDATANASCIMENTO"].Value != DBNull.Value)
                {
                    _TomadorDataNascimento = Convert.ToDateTime(cmd.Parameters["a_TDATANASCIMENTO"].Value.ToString());
                }

                _NumContratos = int.Parse(cmd.Parameters["a_NumContratos"].Value.ToString());
                _NumRamos = int.Parse(cmd.Parameters["a_NumRamos"].Value.ToString());

                if (dr.Read())
                {
                    _PCodigo = dr["PCodigo"].ToString();

                    _Titulo = dr["TTitulo"].ToString();
                    _MoradaTipo = dr["TMoradaTipo"].ToString();
                    _MoradaDesc = dr["TMoradaDesc"].ToString();
                    _MoradaNum = dr["TMoradaNum"].ToString();
                    _MoradaAndar = dr["TMoradaAndar"].ToString();
                    _Localidade = dr["TLocalidade"].ToString();
                    _CodPostal = dr["TCodPostal"].ToString();
                    _SufixoPostal = dr["TSufixoPostal"].ToString();
                    _Pais = dr["TPais"].ToString();
                    _BI = dr["TBI"].ToString();
                    _Nacionalidade = dr["TNacionalidade"].ToString();
                    _Fax = dr["TFax"].ToString();
                    _Email = dr["TEmail"].ToString();
                    _CartaNum = dr["TCartaNum"].ToString();

                    //Segundo Contacto
                    _ContactoNome = dr["LNome"].ToString();
                    _ContactoMoradaTipo = dr["LMoradaTipo"].ToString();
                    _ContactoMoradaDesc = dr["LMoradaDesc"].ToString();
                    _ContactoMoradaNum = dr["LMoradaNum"].ToString();
                    _ContactoMoradaAndar = dr["LMoradaAndar"].ToString();
                    _ContactoLocalidade = dr["LLocalidade"].ToString();
                    _ContactoCodPostal = dr["LCodPostal"].ToString();
                    _ContactoSufixoPostal = dr["LSufixoPostal"].ToString();
                    _ContactoPais = dr["LPais"].ToString();
                    _ContactoFax = dr["LFax"].ToString();
                    _ContactoTelefone = dr["LTelefone"].ToString();
                }

                if (_PCodigo == "0")
                {
                    _PCodigo = "";
                }

            }
            catch (Exception)
            {
                string strMsgErro = "Não foi possível identificar a simulação";

                throw new Exception(strMsgErro);
            }
            finally
            {
                cmd = null;
                cn.Close();
                cn.Dispose();
                cn = null;
            }
        }


    }
}
