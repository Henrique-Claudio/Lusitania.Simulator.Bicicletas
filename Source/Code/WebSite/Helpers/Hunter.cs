using System;
using System.Collections.Generic;
using System.Web;
using System.Xml;

namespace Lusitania.Simuladores.WebSite.Helpers
{
    public class Hunter
    {
        
    }

    [Serializable]
    public class DeclaredGun
    {
        private string _make;
        public string Make
        {
            get { return _make; }
            private set
            { 
                if(string.IsNullOrEmpty(value))
                    throw new Exception(string.Format(Resources.Hunter.MandatoryMsg,Resources.Hunter.Make));

                _make = value.ToUpper();
            }
        }

        private string _model;
        public string Model
        {
            get { return _model; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(string.Format(Resources.Hunter.MandatoryMsg, Resources.Hunter.Model));

                _model = value.ToUpper();
            }
        }

        private string _serialNumber;
        public string SerialNumber
        {
            get { return _serialNumber; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(string.Format(Resources.Hunter.MandatoryMsg, Resources.Hunter.SerialNumber));

                _serialNumber = value.ToUpper();
            }
        }

        public DeclaredGun(string make, string model, string serialNumber)
        {
            this.Make = make;
            this.Model = model;
            this.SerialNumber = serialNumber;
        }

        public XmlDocument GetXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(string.Format(@"<gun>
                                            <make>{0}</make>
                                            <model>{1}</model>
                                            <serialNumber>{2}</serialNumber>
                                         </gun>"
                                        , Make
                                        , Model
                                        , SerialNumber));

            return xDoc;
        }
    }

    [Serializable]
    public class DeclaredDog
    {
        private string _breed;
        public string Breed
        {
            get { return _breed; }
            private set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception(string.Format(Resources.Hunter.MandatoryMsg, Resources.Hunter.Breed));

                _breed = value.ToUpper();
            }
        }

        private string _name;
        public string Name
        { 
            get{ return _name; }
            private set
            {
                if(!string.IsNullOrEmpty(value))
                    _name = value.ToUpper();

            }
        }

        public DateTime? BirthDate{ get; private set; }
        public string Gender { get; private set; }
        
        private string _eid;
        public string EID
        { 
            get { return _eid; }
            private set
            {
                if(!string.IsNullOrEmpty(value))
                    _eid = value.ToUpper();
            }
        }

        public DeclaredDog(string breed)
        {
            this.Breed = breed;
        }

        public DeclaredDog(string breed, string name, string birthDate, string gender, string eid)
        {
            this.Breed = breed;
            this.Name = name;
            this.Gender = gender;
            this.EID = eid;
            this.BirthDate = TryConvert.ToDateTime(birthDate);
        }

        public XmlDocument GetXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(string.Format(@"<dog>
                                            <breed>{0}</breed>
                                            <name>{1}</name>
                                            <birthDate>{2}</birthDate>
                                            <gender>{3}</gender>
                                            <eid>{4}</eid>
                                         </dog>"
                                        , this.Breed
                                        , this.Name
                                        , this.BirthDate.HasValue ? this.BirthDate.Value.ToShortDateString() : string.Empty
                                        , this.Gender
                                        , this.EID));

            return xDoc;
        }
    }
}
