using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Data;
namespace YCCV
{
    class SinhvienDBMng
    {
        public static void Themsinhvien(XmlDocument xmlDoc)
        {
            XPathNavigator sv = xmlDoc.CreateNavigator();
            XmlDocument xmlFile = new XmlDocument();
            xmlFile.Load(@"D:\source\YCCV\YCCV\bin\Debug\XmlSinhVien_ADD_EDIT_DEL.xml");
            XPathNavigator nav = xmlFile.CreateNavigator();
            nav.SelectSingleNode("//VOUCHERS").AppendChild(sv.SelectSingleNode("HEADER"));
            xmlFile.Save(@"D:\source\YCCV\YCCV\bin\Debug\XmlSinhVien_ADD_EDIT_DEL.xml");
            string xml = nav.SelectSingleNode("//VOUCHERS").InnerXml.ToString();
            string connetionString = null;
            connetionString = "Data Source=.;Initial Catalog=SampleDB_XML;Intergrated Security = true";
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {

                string oString = @"Update XMLDB set DATA = @xml";
                SqlCommand oCmd = new SqlCommand(oString, cnn);
                oCmd.Parameters.AddWithValue("@xml", xml);
                cnn.Open();
                oCmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
        public static void Suasinhvien(XmlDocument xmlDoc)
        {
            XPathNavigator sv = xmlDoc.CreateNavigator();
            string SinhvienID = sv.SelectSingleNode("//@SinhvienID").Value.ToString();
            XmlDocument xmlFile = new XmlDocument();
            xmlFile.Load(@"D:\source\YCCV\YCCV\bin\Debug\XmlSinhVien_ADD_EDIT_DEL.xml");
            XPathNavigator nav = xmlFile.CreateNavigator();
            nav.SelectSingleNode($"//HEADER[@SinhvienID={SinhvienID}]").ReplaceSelf(sv.SelectSingleNode("HEADER"));
            xmlFile.Save(@"D:\source\YCCV\YCCV\bin\Debug\XmlSinhVien_ADD_EDIT_DEL.xml");
            string xml = nav.SelectSingleNode("//VOUCHERS").InnerXml.ToString();
            string connetionString = null;
            connetionString = "Data Source=.;Initial Catalog=SampleDB_XML;Intergrated Security = true";
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {

                string oString = @"Update XMLDB set DATA = @xml";
                SqlCommand oCmd = new SqlCommand(oString, cnn);
                oCmd.Parameters.AddWithValue("@xml", xml);
                cnn.Open();
                oCmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
        public static void Xoasinhvien(XmlDocument xmlDoc)
        {
            XPathNavigator sv = xmlDoc.CreateNavigator();
            string SinhvienID = sv.SelectSingleNode("//@SinhvienID").Value.ToString();
            XmlDocument xmlFile = new XmlDocument();
            xmlFile.Load(@"D:\source\YCCV\YCCV\bin\Debug\XmlSinhVien_ADD_EDIT_DEL.xml");
            XPathNavigator nav = xmlFile.CreateNavigator();
            nav.SelectSingleNode($"//HEADER[@SinhvienID={SinhvienID}]").DeleteSelf();
            xmlFile.Save(@"D:\source\YCCV\YCCV\bin\Debug\XmlSinhVien_ADD_EDIT_DEL.xml");
            string xml = nav.SelectSingleNode("//VOUCHERS").InnerXml.ToString();
            string connetionString = null;
            connetionString = "Data Source=.;Initial Catalog=SampleDB_XML;Intergrated Security = true";
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {

                string oString = @"Update XMLDB set DATA = @xml";
                SqlCommand oCmd = new SqlCommand(oString, cnn);
                oCmd.Parameters.AddWithValue("@xml", xml);
                cnn.Open();
                oCmd.ExecuteNonQuery();
                cnn.Close();
            }
        }
        public static XmlDocument GetDanhsachSinvien()
        {
            XmlDocument xmlDoc = new XmlDocument();
            string connetionString = null;
            String result = null;
            connetionString = "Data Source=.;Initial Catalog=SampleDB_XML;Intergrated Security = true";
            using (SqlConnection cnn = new SqlConnection(connetionString))
            {
                string oString = @"select DATA from XmlDB";
                SqlCommand oCmd = new SqlCommand(oString, cnn);
                cnn.Open();
                using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    if (oReader.Read())
                        result = oReader["DATA"].ToString();
                    cnn.Close();
                }
            }

            xmlDoc.LoadXml($"<VOUCHERS>{@result}</VOUCHERS>");

            XPathNavigator nav = xmlDoc.CreateNavigator();
            XPathNodeIterator nodes = nav.Select("//@SinhvienPhone");
            while (nodes.MoveNext())
            {
                nodes.Current.DeleteSelf();
            }
            nodes = nav.Select("//@SinhvienEmail");
            while (nodes.MoveNext())
            {
                nodes.Current.DeleteSelf();
            }
            return xmlDoc;
        }
    

}
}
