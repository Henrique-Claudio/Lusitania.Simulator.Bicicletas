using System;
using System.Data;
using System.Configuration;
using System.Globalization;
using System.Data.OracleClient;
using Lusitania.Simuladores.Common.Auto;

namespace Lusitania.Simuladores.Common.Auto
{
    public enum Versao : int
    {
        Intranet = 0,
        Internet = 1
    }

    public enum TipoCliente : int
    {
        Particular = 1,
        Empresa = 2
    }

    public enum TipoUtilizacao : int
    {
        VidaPrivada = 1,
        Profissional = 2
    }

    public enum TipoSaberLogin : int
    {
        UserName = 1,
        DomainName = 2
    }


    public class Redes
    {
        public Redes()
        {
        }

        public static string SaberLogin(TipoSaberLogin idTipoRetorno)
        {
            string sRetorno = string.Empty;

            try
            {

                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings.Get("DebugUserName")))
                {
                    return ConfigurationManager.AppSettings.Get("DebugUserName");
                }

                if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    string[] Logon;

                    Logon = System.Web.HttpContext.Current.User.Identity.Name.Split('\\');

                    if (idTipoRetorno == TipoSaberLogin.UserName)
                    {
                        sRetorno = Logon[1];
                    }
                    else if (idTipoRetorno == TipoSaberLogin.DomainName)
                    {
                        sRetorno = Logon[0];

                    }
                }
            }
            catch (Exception)
            {
            }


            return sRetorno;
        }


        /// <summary>
        /// Indica que versão está a ser acedida, se a Internet ou a Intranet
        /// </summary>
        /// <returns></returns>
        public static Versao SaberVersao()
        {
            string sLogon = string.Empty;

            sLogon = SaberLogin(TipoSaberLogin.UserName);

            //Se não tiver login é porque não está autenticado na rede, logo visualiza a versão Internet
            if ((sLogon == "") || ConfigurationManager.AppSettings.Get("VERSAO_INTERNET") == "S")
            {
                return Versao.Internet;
            }
            else
            {
                return Versao.Intranet;
            }
        }

        public static bool GestorRede()
        {

            OracleConnection cn = new OracleConnection(LusOracleDB.ConnectionString);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cn;
            cmd.CommandText = "VALIDA_WEB_PKG.VALIDA_LOGIN_GESTOR";
            cmd.CommandType = CommandType.StoredProcedure;

            //Parametros IN
            cmd.Parameters.Add("a_login", OracleType.VarChar, 15).Value = Redes.SaberLogin(TipoSaberLogin.UserName);

            //Parametros OUT
            cmd.Parameters.Add("a_msg_erro", OracleType.VarChar, 100).Direction = ParameterDirection.Output;

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["a_msg_erro"].Value.ToString().Length > 0)
                {
                    return false;
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }

            return true;
        }


        public static string PesquisarMediador(string _Conta)
        {
            string mediador = null;

            OracleConnection cn = new OracleConnection(LusOracleDB.ConnectionString);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cn;
            cmd.CommandText = "VALIDA_WEB_PKG.Valida_Mediador";
            cmd.CommandType = CommandType.StoredProcedure;

            //Parametros IN
            cmd.Parameters.Add("a_conta", OracleType.VarChar, 15).Value = _Conta;

            //Parametros OUT
            cmd.Parameters.Add("a_nome_mediador", OracleType.VarChar, 50).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_msg_erro", OracleType.VarChar, 100).Direction = ParameterDirection.Output;

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["a_msg_erro"].Value.ToString().Length > 0)
                {
                    throw new Exception(cmd.Parameters["a_msg_erro"].Value.ToString());
                }

                mediador = cmd.Parameters["a_nome_mediador"].Value.ToString();
            }
            catch (Exception)
            {

            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }

            return mediador;
        }

        public static string PesquisarCobrador(string _Conta)
        {
            string cobrador = null;

            OracleConnection cn = new OracleConnection(LusOracleDB.ConnectionString);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cn;
            cmd.CommandText = "VALIDA_WEB_PKG.Valida_Cobrador";
            cmd.CommandType = CommandType.StoredProcedure;

            //Parametros IN
            cmd.Parameters.Add("a_conta", OracleType.VarChar, 15).Value = _Conta;
            cmd.Parameters.Add("a_login", OracleType.VarChar, 15).Value = Redes.SaberLogin(TipoSaberLogin.UserName);

            //Parametros OUT
            cmd.Parameters.Add("a_nome_cobrador", OracleType.VarChar, 50).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_msg_erro", OracleType.VarChar, 100).Direction = ParameterDirection.Output;

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["a_msg_erro"].Value.ToString().Length > 0)
                {
                    throw new Exception(cmd.Parameters["a_msg_erro"].Value.ToString());
                }

                cobrador = cmd.Parameters["a_nome_cobrador"].Value.ToString();
            }
            catch (Exception)
            {

            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }

            return cobrador;
        }

        public static string SaberContaInterna()
        {
            return OSBConnector.Shared.DataServices.GetUserAttributes(Redes.SaberLogin(TipoSaberLogin.UserName), true).Attributes.Attribute.Find(x => x.Key == "lusitania.util.conta").Value;
        }

        /*
        public static string SaberContaInterna_Old()
        {
            string conta = null;

            OracleConnection cn = new OracleConnection(LusOracleDB.ConnectionString);

            OracleCommand cmd = new OracleCommand();
            cmd.Connection = cn;
            cmd.CommandText = "VALIDA_WEB_PKG.CONTA_INTERNA_DEPENDENCIA";
            cmd.CommandType = CommandType.StoredProcedure;

            //Parametros IN
            cmd.Parameters.Add("a_login", OracleType.VarChar, 15).Value = Redes.SaberLogin(TipoSaberLogin.UserName);

            //Parametros OUT
            cmd.Parameters.Add("conta_interna", OracleType.VarChar, 50).Direction = ParameterDirection.Output;
            cmd.Parameters.Add("a_msg_erro", OracleType.VarChar, 100).Direction = ParameterDirection.Output;

            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();

                if (cmd.Parameters["a_msg_erro"].Value.ToString().Length > 0)
                {
                    throw new Exception(cmd.Parameters["a_msg_erro"].Value.ToString());
                }
                else
                {
                    conta = cmd.Parameters["conta_interna"].Value.ToString();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                cn.Close();
                cn.Dispose();
            }

            return conta;
        }
        */
    }

    public class Format
    {
        public Format()
        {
        }
        //
        // TODO: Add constructor logic here
        //






        /// <summary>
        /// Formata os montantes
        /// Ex: FormataMontante("14342,0",4)  -->  Resultado: 14.342,0000
        /// </summary>
        /// <param name="montante">Montante</param>
        /// <param name="decimais">Quantidade de decimais</param>
        /// <returns>Valor Formatado</returns>
        public static string FormataMontante(decimal montante, int decimais)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();

            nfi.NumberDecimalSeparator = ",";
            nfi.NumberDecimalDigits = decimais;
            nfi.NumberGroupSeparator = ".";

            return montante.ToString("N", nfi);
        }

        public static string FormataMontante(object montante, int decimais)
        {
            decimal dMontante = 0;

            try
            {
                dMontante = decimal.Parse(montante.ToString());
            }
            catch (Exception)
            {
                return "N.A.";
            }

            return FormataMontante(dMontante, decimais);

        }

        public static string FormataMontanteEuro(object montante)
        {
            return FormataMontanteEuro(montante, 2);
        }

        public static string FormataMontanteEuro(object montante, int decimais)
        {
            decimal dMontante = 0;

            try
            {
                dMontante = decimal.Parse(montante.ToString());
            }
            catch (Exception)
            {
                return "N.A.";
            }

            NumberFormatInfo nfi = new NumberFormatInfo();

            nfi.NumberDecimalSeparator = ",";
            nfi.NumberDecimalDigits = decimais;
            nfi.NumberGroupSeparator = ".";

            return dMontante.ToString("N", nfi) + " €";
        }

        public static string FormataMontanteEuroPremio(object montante)
        {
            decimal dMontante = 0;

            try
            {
                dMontante = decimal.Parse(montante.ToString());
            }
            catch (Exception)
            {
                return "N.A.";
            }

            if (dMontante == 0)
            {
                return "N.A.";
            }

            return FormataMontanteEuro(montante, 2);
        }

        public static string FormataData(DateTime data)
        {
            return string.Format("{0:dd-MM-yyyy}", data);
        }

        public static string FormataData(object data)
        {
            return string.Format("{0:dd-MM-yyyy}", data);
        }

        /// <summary>
        /// Devolve no formato MM-YYYY
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string FormataDataMesAno(object data)
        {
            return string.Format("{0:MM-yyyy}", data);
        }

        /// <summary>
        /// Devolve no formato DD/MM
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string FormataDataDiaMes(object data)
        {
            return string.Format("{0:dd-MM}", data).Replace("-", "/");
        }

        public static string FormataDataBarra(object data)
        {
            return string.Format("{0:dd-MM-yyyy}", data).Replace("-", "/");
        }

        public static string FormataDataBarra(DateTime data)
        {
            return string.Format("{0:dd-MM-yyyy}", data).Replace("-", "/");
        }

        public static DateTime ConverteAAAAMMDDParaData(string sData)
        {
            int ano;
            int mes;
            int dia;

            ano = int.Parse(sData.Substring(0, 4));
            mes = int.Parse(sData.Substring(4, 2));
            dia = int.Parse(sData.Substring(6, 2));

            return new DateTime(ano, mes, dia);
        }

        public static decimal ConverterString2Decimal(string strValor)
        {
            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ",";
            provider.NumberGroupSeparator = ".";
            provider.NumberGroupSizes = new int[] { 3 };

            decimal sValor = 0;
            try
            {
                sValor = Convert.ToDecimal(strValor, provider);
            }
            catch
            {
                sValor = 0;
            }

            return sValor;
        }

        //Formatar um numero para string em que o separador decimal é o "."
        //Para submeter para o Oracle quando um nº vai em texto.
        public static string FormataString2NumberStringOracle(decimal Valor)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();

            nfi.NumberDecimalSeparator = ".";
            nfi.NumberDecimalDigits = 2;
            nfi.NumberGroupSeparator = string.Empty;

            return Valor.ToString("N", nfi);
        }

        //Formatar um numero para string em que o separador decimal é o "."
        public static decimal String2Number(string strValor)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();

            nfi.NumberDecimalSeparator = ".";
            nfi.NumberDecimalDigits = 2;
            nfi.NumberGroupSeparator = string.Empty;


            decimal dValor = 0;
            try
            {
                dValor = Convert.ToDecimal(strValor, nfi);
            }
            catch
            {
                dValor = 0;
            }

            return dValor;
        }

        /// <summary>
        /// Trata se valor vier nulo, substitui por ""
        /// </summary>
        /// <param name="valor">String a ser verificada</param>
        /// <returns>Valor</returns>
        public static string TrataNulo(string valor)
        {
            if (valor == null)
            {
                return String.Empty;
            }
            else
            {
                return valor;
            }
        }

    }

    //public class Security
    //{

    //}
}
