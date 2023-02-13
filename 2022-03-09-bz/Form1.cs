using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _2022_03_09_bz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        class Termek
        {
            public string nev;
            public int db;
            public int ar;
            public string torekeny;
        }
        List<Termek> lista_termek = new List<Termek>();
        int osszertek = 0;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnGeneral_Click(object sender, EventArgs e)
        {
            Random veletlen = new Random();
            tbDarab.Text = Convert.ToString(veletlen.Next(1, 500));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ListBox1-be írás
            string nev = tbNev.Text;
            string darab = tbDarab.Text;
            string ar = tbAr.Text;
            string torekeny;
            if (cbTorekeny.Checked == true)
            {
                torekeny = "Törékeny";
            }
            else
            {
                torekeny = "NEM törékeny";
            }
            string sor = nev + "," + darab + "," + ar + "," + torekeny;
            listBox1.Items.Add(sor);
            tbNev.Text = "";
            tbDarab.Text = "";
            tbAr.Text = "";
            cbTorekeny.Checked = false;

            // Termék listába rakás
            Termek t1 = new Termek();
            t1.nev = nev;
            t1.db = Convert.ToInt32(darab);
            t1.ar = Convert.ToInt32(ar);
            t1.torekeny = torekeny;
            lista_termek.Add(t1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // TextBoxba visszaírás ListBox1-ből
            int kivalasztott = listBox1.SelectedIndex;
            if (kivalasztott == -1)
            {
                MessageBox.Show("Nincs kijelölt elem!");
            }
            else
            {
                String[] adatsor = new String[3];
                adatsor = listBox1.SelectedItem.ToString().Split(',');
                tbNev.Text = adatsor[0];
                tbDarab.Text = adatsor[1];
                tbAr.Text = adatsor[2];
                string torekeny = adatsor[3];
                if (torekeny == "Törékeny")
                {
                    cbTorekeny.Checked = true;
                }
                else
                {
                    cbTorekeny.Checked = false;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Megrendeléshez hozzáadás
            int kivalasztott = listBox1.SelectedIndex;
            if (kivalasztott == -1)
            {
                MessageBox.Show("Nincs kiválasztott elem!");
            }
            else
            {
                string atvitelre = listBox1.SelectedItem.ToString();
                listBox2.Items.Add(atvitelre);
                String[] adatsor = new String[3];
                adatsor = listBox1.SelectedItem.ToString().Split(',');
                int ertek = Convert.ToInt32(adatsor[2]);
                osszertek = osszertek + ertek;
                lblOsszertek.Text = Convert.ToString(osszertek) + ",- Ft";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Fájlba kiírás
            StreamWriter kiir = new StreamWriter("fajlnev.txt");
            foreach (Termek t in lista_termek)
            {
                kiir.WriteLine(t.nev + "," + t.db + "," + t.ar + "," + t.torekeny);
            }
            kiir.Flush();
            kiir.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // Fájlból beolvasás 
            StreamReader beolvas = new StreamReader("fajlnev.txt");
            string str_beolvas = beolvas.ReadLine();
            if (rbUresLista.Checked == true)
            {
                listBox1.Items.Clear();
            }
            
            while (str_beolvas != null)
            {
                listBox1.Items.Add(str_beolvas);
                str_beolvas = beolvas.ReadLine();
            }

            beolvas.Close();
        }

        private void btnTorol_Click(object sender, EventArgs e)
        {
            int kivalasztott = listBox1.SelectedIndex;
            if (kivalasztott == -1)
            {
                MessageBox.Show("Nem törölhető elem!");
            }
            else
            {
                listBox1.Items.RemoveAt(kivalasztott);
            }
        }
    }
}
