using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Net;

/// <summary>
/// Summary description for Settings
/// </summary>
public static class Settings
{
    public static bool DEV
    {
        get { return ConfigurationManager.AppSettings.Get("Ambiente") == "DEV"; }
    }

    public static bool QUAL
    {
        get { return ConfigurationManager.AppSettings.Get("Ambiente") == "QUAL"; }
    }

    public static bool PROD
    {
        get { return ConfigurationManager.AppSettings.Get("Ambiente") == "PROD"; }
    }

    public static int FamilyPlan_NumberOfPlansToTriggerWarning
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings.Get("FamilyPlan.NumberOfPlansToTriggerWarning")); }
    }

    public static int UserDetails_YearsFromBirthDateToDriverLicenseDate
    {
        get { return Convert.ToInt32(ConfigurationManager.AppSettings.Get("UserDetails.YearsFromBirthDateToDriverLicenseDate")); }
    }

    public static int DefaultCacheDurationInSeconds
    {
        get { return Int32.Parse(ConfigurationManager.AppSettings.Get("DefaultCacheDurationInSeconds")); }
    }

    public static string HomeSimulator_ResidentiaProductID
    {
        get { return ConfigurationManager.AppSettings.Get("HomeSimulator_ResidentiaProductID"); }
    }

    public static string HomeSimulator_AuroProductID
    {
        get { return ConfigurationManager.AppSettings.Get("HomeSimulator_AuroProductID"); }
    }

    public static string HomeSimulator_CasaXsProductID
    {
        get { return ConfigurationManager.AppSettings.Get("HomeSimulator_CasaXsProductID"); }
    }

    public static string HomeSimulator_CasaIdealProductID
    {
        get { return ConfigurationManager.AppSettings.Get("HomeSimulator_CasaIdealProductID"); }
    }

    public static string UserSuggestions_DestinationEmail
    {
        get { return ConfigurationManager.AppSettings.Get("UserSuggestions_DestinationEmail"); }
    }

    public static string FaturaReciboAtivo
    {
        get { return ConfigurationManager.AppSettings.Get("FaturaReciboAtivo"); }
    }
    
    public static Uri OutputES_Url
    {
        get
        {
            return new Uri(ConfigurationManager.AppSettings.Get("OutputES.OutputServiceUrl"));
        }
    }
        
    public static string AutoSimulator_FamilyPlanNumberCode
    {
        get { return ConfigurationManager.AppSettings.Get("AutoSimulator_FamilyPlanNumberCode"); }
    }

    public static string AutoSimulator_FamilyPlanVersionCode
    {
        get { return ConfigurationManager.AppSettings.Get("AutoSimulator_FamilyPlanVersionCode"); }
    }

    public static string AutoSimulator_RefererCode_BackOffice
    {
        get { return ConfigurationManager.AppSettings.Get("AutoSimulator_RefererCode_BackOffice"); }
    }

    public static string AutoSimulator_RefererCode_FamilyPlan
    {
        get { return ConfigurationManager.AppSettings.Get("AutoSimulator_RefererCode_FamilyPlan"); }
    }

    public static string AutoSimulator_UrlEncodePassword
    {
        get { return ConfigurationManager.AppSettings.Get("AutoSimulator_UrlEncodePassword"); }
    }

    public static string DefaultMoneyEmptyString
    {
        get { return ConfigurationManager.AppSettings.Get("DefaultMoneyEmptyString"); }
    }

    public static int TravelSimulator_MinDurationInDays
    {
        get { return Int32.Parse(ConfigurationManager.AppSettings.Get("TravelSimulator_MinDurationInDays")); }
    }

    public static int TravelSimulator_MaxDurationInDays
    {
        get { return Int32.Parse(ConfigurationManager.AppSettings.Get("TravelSimulator_MaxDurationInDays")); }
    }

    public static int HouseMaid_SalaryTypeID_FullTime
    {
        get { return Int32.Parse(ConfigurationManager.AppSettings.Get("HouseMaid_SalaryTypeID_FullTime")); }
    }

    public static string TravelSimulator_DeathCoverageCode
    {
        get { return ConfigurationManager.AppSettings.Get("TravelSimulator_DeathCoverageCode"); }
    }

    public static int HouseMaid_SalaryTypeID_PartTime
    {
        get { return Int32.Parse(ConfigurationManager.AppSettings.Get("HouseMaid_SalaryTypeID_PartTime")); }
    }

    public static string TravelSimulator_LuggageCoverageCode
    {
        get { return ConfigurationManager.AppSettings.Get("TravelSimulator_LuggageCoverageCode"); }
    }

    public static string TravelSimulator_TravelAssistanceCoverageCode
    {
        get { return ConfigurationManager.AppSettings.Get("TravelSimulator_TravelAssistanceCoverageCode"); }
    }

    public static string DefaultMoneyFormat
    {
        get { return ConfigurationManager.AppSettings.Get("DefaultMoneyFormat"); }
    }

    public static bool UseSessionForViewState
    {
        get { return Boolean.Parse(ConfigurationManager.AppSettings.Get("UseSessionForViewState")); }
    }

    public static string SendQuotation_From_EMail
    {
        get { return ConfigurationManager.AppSettings.Get("SendQuotation_From_EMail"); }
    }

    public static CultureInfo CurrencyFormatCulture
    {
        get { return CultureInfo.GetCultureInfo(ConfigurationManager.AppSettings.Get("CurrencyFormatCulture")); }
    }

    public static string DebugUserName
    {
        get { return ConfigurationManager.AppSettings.Get("DebugUserName"); }
    }

    public static string DefaultCountryCode
    {
        get { return ConfigurationManager.AppSettings.Get("DefaultCountryCode"); }
    }

    public static string UrlEncodePassword
    {
        get { return ConfigurationManager.AppSettings.Get("UrlEncodePassword"); }
    }

    public static string UrlEncodeParameterName
    {
        get { return ConfigurationManager.AppSettings.Get("UrlEncodeParameterName"); }
    }

    public static string DefaultNationalityCode
    {
        get { return ConfigurationManager.AppSettings.Get("DefaultNationalityCode"); }
    }
}
