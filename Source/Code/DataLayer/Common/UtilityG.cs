using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Lusitania.Simuladores.DataLayer.Common.Model;

namespace Lusitania.Simuladores.DataLayer.Common
{
    public enum Products
    {
        LIGHT,
        SUPER
    }

    public enum Fractionations
    { 
        ANUAL,
        SEMESTRAL,
        TRIMESTRAL,
        MENSAL
    }

    public enum GenericLists
    {
        SEXO,
        ESTADO_CIVIL,
        TITLO_ONORIFICO,
        NACIONALIDADE,
        PROFISSAO,
        PAIS,
        FRACCIONAMENTO
    }

    public class UtilityG
    {
        public const string RMS = "130100";
        public const int SIMULATOR_ID = 51;


        public static string GetValue(Products product) {
            string _val = string.Empty;

            switch (product)
            {
                case Products.LIGHT:
                    _val = "00006";
                    break;
                case Products.SUPER:
                    _val = "00007";
                    break;
                default:
                    break;
            }

            return _val;
        }

        public static string GetValue(Fractionations fraccionamento)
        {
            string _val = string.Empty;

            switch (fraccionamento)
            {
                case Fractionations.ANUAL:
                    _val = "Anual";
                    break;
                case Fractionations.SEMESTRAL:
                    _val = "Semestral";
                    break;
                case Fractionations.TRIMESTRAL:
                    _val = "Trimestral";
                    break;
                case Fractionations.MENSAL:
                    _val = "Mensal";
                    break;
                default:
                    break;
            }

            return _val;
        }

        public static string GetValue(GenericLists tipo)
        {
            string _val = string.Empty;

            switch (tipo)
            {
                case GenericLists.SEXO:
                    _val = "WEB_GERAL_PKG.Saber_Sexo";
                    break;
                case GenericLists.ESTADO_CIVIL:
                    _val = "WEB_GERAL_PKG.Saber_Estado_Civil";
                    break;
                case GenericLists.TITLO_ONORIFICO:
                    _val = "WEB_GERAL_PKG.Saber_Titulo_Nome";
                    break;
                case GenericLists.NACIONALIDADE:
                    _val = "WEB_GERAL_PKG.Saber_Nacionalidades";
                    break;
                case GenericLists.PROFISSAO:
                    _val = "WEB_GERAL_PKG.Saber_Profissoes";
                    break;
                case GenericLists.PAIS:
                    _val = "WEB_GERAL_PKG.SABER_PAISES";
                    break;
                case GenericLists.FRACCIONAMENTO:
                    _val = "WEB_PPROD_RC_PKG.LISTA_VALORES_FRACIONAMENTO";
                    break;  
                default:
                    break;
            }

            return _val;
        }

        public static string GetFormatDoubleValue(string capital)
        {
            double c = 0;
            if (double.TryParse(capital, out c))
                return string.Concat(c.ToString("N2"), " €");
            else
                return capital;
        }


        public static void BindList(ListControl ctr, string source, bool hasEmpty)
        {
            BindList(ctr, string.Empty, source, hasEmpty);
        }
        public static void BindList(ListControl ctr, string tipo, string source, bool hasEmpty)
        {
            ctr.DataSource = GenericListsDS.Load(tipo, source);
            ctr.DataTextField = "Descritivo";
            ctr.DataValueField = "Codigo";
            ctr.DataBind();
            if (hasEmpty)
            {
                ctr.Items.Insert(0, new ListItem("  ", String.Empty));
            }
        }
        public static void BindList(ListControl ctr, List<GenericListsDS> dataSource, bool hasEmpty, string EmptyText)
        {
            ctr.DataSource = dataSource;
            ctr.DataTextField = "Descritivo";
            ctr.DataValueField = "Codigo"; 
            ctr.DataBind();
            if (hasEmpty)
            {
                ctr.Items.Insert(0, new ListItem(EmptyText, String.Empty));
            }
           
        }
    }
}
