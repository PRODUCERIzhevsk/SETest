﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SETest.Data
{
    [Serializable]
    public class Invoice
    {
        public int IdSender { get; set; } 
        public string NameSender { get; set; }

        public int IdRecipient { get; set; }
        public string NameRecipient { get; set; }

        public Good item { get; set; }

        [XmlIgnore]
        public decimal DCount { get; set; }

        public string Count
        {
            get
            {
                return DCount.ToString();
            }
            set
            {
                decimal count = new decimal();
                decimal.TryParse(value, out count);
                DCount = count;
            }
        }

        [XmlIgnore]
        public decimal DPrice { get; set; }

        public string Price
        {
            get
            {
                return DPrice.ToString();
            }
            set
            {
                decimal price = new decimal();
                decimal.TryParse(value, out price);
                DPrice = price;
            }
        }

        public decimal Summ { get; set; }

        public static void SaveToXML(Invoice invoice, string fileName)
        {
            try
            {
                XmlSerializer xmlWriter = new XmlSerializer(typeof(Invoice));
                using (StreamWriter fileWriter = new StreamWriter(fileName, false, Encoding.UTF8))
                {
                    xmlWriter.Serialize(fileWriter, invoice);
                    fileWriter.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Invoice LoadFromXML(string fileName)
        {
            Invoice result;

            try
            {
                XmlSerializer xmlReader = new XmlSerializer(typeof(Invoice));
                using (TextReader fileReader = new StreamReader(fileName, Encoding.UTF8))
                {
                    result = xmlReader.Deserialize(fileReader) as Invoice;
                    fileReader.Close();
                    if (result == null) { throw new Exception(); }
                }
            }
            catch (Exception e)
            {
                result = null;
            }

            return result;
        }
    }
}
