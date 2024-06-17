using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace Lusitania.Simuladores.WebSite.Helpers
{
    public class Proposal
    {
        #region Enums
        public enum TypeContact
        {
            Unknown = 0,
            Phone = 1,
            Mobile = 2,
            Fax = 3,
            Email = 4
        }

        public enum TypeDocument
        {
            Unknown = 0,
            TaxCard = 1,
            IdentityCard = 2,
            DrivingLicenceCard = 3,
            PassportCard = 4,
            SocialSecurityCard = 5,
            MembershipCard = 6,
            CreditCard = 7,
            BankCard = 8
        }

        #endregion

        public static string GetInputXml(string simulatorCode, string sessionId, string user, string quotationId,
                                         string paymenType, string frequency, string personList_content,
                                         string intermediate, string collector, string debitBAN, string mortgagee)
        {
            if (string.IsNullOrEmpty(quotationId))
                return string.Empty;

            object[] parameters = { sessionId, user, quotationId,
                                    paymenType, frequency, personList_content,
                                    intermediate, collector, simulatorCode,
                                    debitBAN, mortgagee};

            string proposal = string.Format(@"<?xml version='1.0' encoding='utf-8'?>
                                                <proposal>
                                                  <sessionId>{0}</sessionId>
                                                  <channel>web</channel>
                                                  <user>{1}</user>
                                                  <simulatorCode>{8}</simulatorCode>
                                                  <input>
                                                    <quotationId>{2}</quotationId>
                                                    <paymenType>{3}</paymenType>
                                                    <frequency>{4}</frequency>
                                                    <personList>{5}</personList>
                                                    <intermediate>{6}</intermediate>
                                                    <collector>{7}</collector>
                                                    <debitBankAccountNumber>{9}</debitBankAccountNumber>
                                                    <mortgagee>{10}</mortgagee>
                                                  </input>
                                                </proposal>", parameters);

            return proposal;
        }

        public static void AppendXml(string tagName, string xmlSource, string xmlAppend, out string xml)
        {
            xml = string.Empty;

            if (!string.IsNullOrEmpty(xmlSource))
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(xmlSource);

                XmlNode node = xDoc.SelectSingleNode(tagName);

                if (node != null)
                {
                    node.InnerXml = string.Concat(node.InnerXml, xmlAppend);
                }

                xml = xDoc.OuterXml;
            }
        }

        public static string GetPerson(string personType, string internalCode)
        {
            return string.Format(@"<person type='{0}' code='{1}' />", personType, internalCode);
        }

        public static string GetPerson(string personType, string internalCode, string firstName,
                                        string midleName, string surName, string title,
                                        string birthDate, string gender, string maritalStatus,
                                        string occupation, string nationality, string addressList_content,
                                        string contactList_content, string documentList_content, string questionList_content)
        {
            object[] parameters = { personType, internalCode, firstName,
                                    midleName, surName, title,
                                    birthDate, gender, maritalStatus,
                                    occupation, addressList_content, contactList_content,
                                    documentList_content, questionList_content, nationality };

            string person = string.Format(@"<person type='{0}' code='{1}'>
                                                <firstName>{2}</firstName>
                                                <midleName>{3}</midleName>
                                                <surName>{4}</surName>
                                                <fullName>{2} {4}</fullName>
                                                <title>{5}</title>
                                                <birthDate>{6}</birthDate>
                                                <gender>{7}</gender>
                                                <maritalStatus>{8}</maritalStatus>
                                                <occupation>{9}</occupation>
                                                <nationality>{14}</nationality>
                                                <addressList>{10}</addressList>
                                                <contactList>{11}</contactList>
                                                <documentList>{12}</documentList>
                                                <questionList>{13}</questionList>
                                              </person>", parameters);

            return person;
        }

        public static string GetAddress(string addressType, string streetType, string streetName,
                                        string streetNumber, string floor, string door,
                                        string postalCode, string local, string country,
                                        string isPrincipal, string isMailingAddress)
        {

            if (string.IsNullOrEmpty(streetName))
                return string.Empty;

            object[] parameters = { addressType, streetType, streetName,
                                    streetNumber, floor, door,
                                    postalCode, local, country,
                                    isPrincipal, isMailingAddress };

            string address = string.Format(@"<address type='{0}'>
                                <streetType>{1}</streetType>
                                <streetName>{2}</streetName>
                                <streetNumber>{3}</streetNumber>
                                <floor>{4}</floor>
                                <door>{5}</door>
                                <postalCode>{6}</postalCode>
                                <local>{7}</local>
                                <country>{8}</country>
                                <isPrincipal>{9}</isPrincipal>
                                <isMailingAddress>{10}</isMailingAddress>
                              </address>", parameters);

            return address;
        }

        public static string GetContact(TypeContact contactType, string data, string isPrincipal, string isMailingAddress)
        {
            if (string.IsNullOrEmpty(data))
                return string.Empty;

            string contact = string.Format(@"<contact type='{0}'>
                                                <data>{1}</data>
                                                <isPrincipal>{2}</isPrincipal>
                                                <isMailingAddress>{3}</isMailingAddress>
                                              </contact>", ((int)contactType).ToString(), data, isPrincipal, isMailingAddress);

            return contact;

        }

        public static string GetDocument(TypeDocument documentType, string number, string issueDate,
                                         string issuePlace, string expirationDate, string extraInfo)
        {
            if (string.IsNullOrEmpty(number))
                return string.Empty;

            object[] parameters = { ((int)documentType).ToString(), number, issueDate,
                                    issuePlace, expirationDate, extraInfo };

            string document = string.Format(@"<document type='{0}'>
                                                <number>{1}</number>
                                                <issueDate>{2}</issueDate>
                                                <issuePlace>{3}</issuePlace>
                                                <expirationDate>{4}</expirationDate>
                                                <extraInfo>{5}</extraInfo>
                                              </document>", parameters);

            return document;
        }

        public static string GetQuestion(string query, string response)
        {
            if (string.IsNullOrEmpty(query) || string.IsNullOrEmpty(response))
                return string.Empty;

            string question = string.Format(@"<question>
                                                <query>{0}</query>
                                                <response>{1}</response>
                                              </question>", query, response);

            return question;
        }

        public static string GetQuestion(bool haveBeenInsured, string company, bool hasDebts, string notes)
        {
            object[] parameters = new object[4];

            if(haveBeenInsured)
            {
                parameters[0] = "1";
                parameters[1] = company;
                parameters[2] = hasDebts ? "1" : "0";
            }
            else
            {
                parameters[0] = "0";
                parameters[1] = "";
                parameters[2] = "0";
            }

            parameters[3] = notes;

            string question = string.Format(@"<requiredQuestionnaire>
                                                <haveBeenInsured>{0}</haveBeenInsured>
                                                <company>{1}</company>
                                                <existPremiumsToCharge>{2}</existPremiumsToCharge>
                                              </requiredQuestionnaire>
                                              <observations>{3}</observations>",parameters);

            return question;

        }

    }
}
