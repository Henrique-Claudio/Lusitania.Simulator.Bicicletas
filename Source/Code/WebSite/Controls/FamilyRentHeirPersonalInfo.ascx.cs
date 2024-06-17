using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Lusitania.Simuladores.DataLayer.FamilyRentDSTableAdapters;

public partial class FamilyRentHeirPersonalInfo : UserControl
{
    #region Properties

    public decimal? SimulationID
    {
        get
        {
            try
            {
                return Int32.Parse(mSimulationIDField.Value);
            }
            catch
            {
                return null;
            }
        }
        set
        {
            mSimulationIDField.Value = value.ToString();
        }
    }

    public string ValidationGroup
    {
        get
        {
            return mProposalUserDetails.ValidationGroup;
        }
        set
        {
            mProposalUserDetails.ValidationGroup = value;
        }
    }

    public string PersonName
    {
        get { return mProposalUserDetails.PersonName; }
    }

    public string Nif
    {
        get { return mProposalUserDetails.Nif; }
    }

    #endregion

    #region Methods

    public bool TrySaveToHolder(out string personId, out string errorMessage, out string personName)
    {
        (new QueriesTableAdapterFix()).GRAVAR_HERDEIRO_TITULAR(
            SimulationID,
            mProposalUserDetails.ClientNumber,
            mProposalUserDetails.PersonTitleID,
            mProposalUserDetails.PersonName,
            mProposalUserDetails.AddressTypeID,
            mProposalUserDetails.AddressStreetName,
            mProposalUserDetails.AddressStreetNumber,
            mProposalUserDetails.AddressFloorNumber,
            mProposalUserDetails.Locality,
            mProposalUserDetails.ZipCode4,
            mProposalUserDetails.ZipCode3,
            mProposalUserDetails.CountryID,
            mProposalUserDetails.GenderID,
            mProposalUserDetails.MaritalStatusID,
            mProposalUserDetails.Nif,
            mProposalUserDetails.IdCardNumber,
            mProposalUserDetails.NationalityID,
            mProposalUserDetails.DateOfBirth,
            mProposalUserDetails.ProfessionID,
            mProposalUserDetails.PhoneNumber,
            mProposalUserDetails.FaxNumber,
            mProposalUserDetails.EMail,
            mProposalUserDetails.DriverLicense,
            mProposalUserDetails.DriverLicenseDate,
            mProposalUserDetails.AttentionOf,
            mProposalUserDetails.ExtraAddressTypeID,
            mProposalUserDetails.ExtraAddressStreetName,
            mProposalUserDetails.ExtraAddressStreetNumber,
            mProposalUserDetails.ExtraAddressFloorNumber,
            mProposalUserDetails.ExtraLocality,
            mProposalUserDetails.ExtraZipCode4,
            mProposalUserDetails.ExtraZipCode3,
            mProposalUserDetails.ExtraCountryID,
            mProposalUserDetails.ExtraPhoneNumber,
            mProposalUserDetails.ExtraFaxNumber,
            out personId,
            out errorMessage
        );

        personName = mProposalUserDetails.PersonName;

        return !String.IsNullOrEmpty(personId) && String.IsNullOrEmpty(errorMessage);
    }

    public bool TrySaveToSpouse(out string personId, out string errorMessage, out string personName)
    {
        (new QueriesTableAdapterFix()).GRAVAR_HERDEIRO_CONJUGE(
            SimulationID,
            mProposalUserDetails.ClientNumber,
            mProposalUserDetails.PersonTitleID,
            mProposalUserDetails.PersonName,
            mProposalUserDetails.AddressTypeID,
            mProposalUserDetails.AddressStreetName,
            mProposalUserDetails.AddressStreetNumber,
            mProposalUserDetails.AddressFloorNumber,
            mProposalUserDetails.Locality,
            mProposalUserDetails.ZipCode4,
            mProposalUserDetails.ZipCode3,
            mProposalUserDetails.CountryID,
            mProposalUserDetails.GenderID,
            mProposalUserDetails.MaritalStatusID,
            mProposalUserDetails.Nif,
            mProposalUserDetails.IdCardNumber,
            mProposalUserDetails.NationalityID,
            mProposalUserDetails.DateOfBirth,
            mProposalUserDetails.ProfessionID,
            mProposalUserDetails.PhoneNumber,
            mProposalUserDetails.FaxNumber,
            mProposalUserDetails.EMail,
            mProposalUserDetails.DriverLicense,
            mProposalUserDetails.DriverLicenseDate,
            mProposalUserDetails.AttentionOf,
            mProposalUserDetails.ExtraAddressTypeID,
            mProposalUserDetails.ExtraAddressStreetName,
            mProposalUserDetails.ExtraAddressStreetNumber,
            mProposalUserDetails.ExtraAddressFloorNumber,
            mProposalUserDetails.ExtraLocality,
            mProposalUserDetails.ExtraZipCode4,
            mProposalUserDetails.ExtraZipCode3,
            mProposalUserDetails.ExtraCountryID,
            mProposalUserDetails.ExtraPhoneNumber,
            mProposalUserDetails.ExtraFaxNumber,
            out personId,
            out errorMessage
        );

        personName = mProposalUserDetails.PersonName;

        return !String.IsNullOrEmpty(personId) && String.IsNullOrEmpty(errorMessage);
    }

    #endregion
}