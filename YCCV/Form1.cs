using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

namespace YCCV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadSinhVienToControl();
        }
        private void loadfile()
        {
            dataGridView1.Rows.Clear();
            XmlDocument xdoc = new XmlDocument();
            xdoc.Load(@"D:\source\YCCV\YCCV\bin\Debug\XmlSinhVien_ADD_EDIT_DEL.xml");
            XPathNavigator nav = xdoc.CreateNavigator();
            XPathNodeIterator nodeiter = nav.Select(@"BIZREQUEST/DATAAREA/VOUCHERS/HEADER");
            int row = 0;
            while (nodeiter.MoveNext())
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[row].Cells[0].Value = nodeiter.Current.SelectSingleNode("@SinhvienPrkID").Value.ToString();
                dataGridView1.Rows[row].Cells[1].Value = nodeiter.Current.SelectSingleNode("@SinhvienID").Value.ToString();
                dataGridView1.Rows[row].Cells[2].Value = nodeiter.Current.SelectSingleNode("@SinVienName").Value.ToString();
                dataGridView1.Rows[row].Cells[3].Value = nodeiter.Current.SelectSingleNode("@SinvienEmail").Value.ToString();
                dataGridView1.Rows[row].Cells[4].Value = nodeiter.Current.SelectSingleNode("@SinvienPhone").Value.ToString();
                dataGridView1.Rows[row].Cells[5].Value = nodeiter.Current.SelectSingleNode("@SinhvienAddr").Value.ToString();
                row++;
            }
            dataGridView1.Refresh();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            SinhvienDBMng.Themsinhvien(getxmlDoc());
            LoadSinhVienToControl();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SinhvienDBMng.Suasinhvien(getxmlDoc());
            LoadSinhVienToControl();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SinhvienDBMng.Xoasinhvien(getxmlDoc());
            LoadSinhVienToControl();
        }
        private void LoadSinhVienToControl()
        {
            dataGridView1.Rows.Clear();
            XmlDocument XmlSinhVien_View = SinhvienDBMng.GetDanhsachSinvien();
            XPathNavigator nav = XmlSinhVien_View.CreateNavigator();
            XPathNodeIterator nodes = nav.Select("//HEADER");
            int i = 0;
            foreach (XPathNavigator v in nodes)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells["SinhvienPrKID"].Value = v.SelectSingleNode("@SinhvienPrkID").Value.ToString();
                dataGridView1.Rows[i].Cells["SinhvienID"].Value = v.SelectSingleNode("@SinhvienID").Value.ToString();
                dataGridView1.Rows[i].Cells["SinhvienName"].Value = v.SelectSingleNode("@SinhvienName").Value.ToString();
                dataGridView1.Rows[i].Cells["SinhvienAddr"].Value = v.SelectSingleNode("@SinhvienAddr").Value.ToString();
                ++i;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells["SinhvienPrkID"].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["SinhvienID"].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells["SinhvienName"].Value.ToString();
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells["SinhvienAdd"].Value.ToString();
        }
        private XmlDocument getxmlDoc()
        {
            XDocument xDoc = new XDocument(new XElement("HEADER",
                                             new XAttribute("SinhvienPrkID", textBox1.Text),
                                             new XAttribute("SinhvienID", textBox2.Text),
                                             new XAttribute("SinhvienName", textBox3.Text),
                                             new XAttribute("SinhvienAddr", textBox4.Text),
                                             new XAttribute("SinhvienEmail", textBox5.Text),
                                             new XAttribute("SinhvienPhone", textBox6.Text)));
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xDoc.ToString());
            return xmlDoc;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            loadfile();
        }
    }
}
