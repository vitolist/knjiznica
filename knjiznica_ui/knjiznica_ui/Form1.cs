using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace knjiznica_ui
{

    public partial class Form1 : Form
    {
        Knjiznica knjiznica = new Knjiznica();

        public Form1()
        {
            InitializeComponent();

            knjiznica.dodajKnjigu("Hamlet");
            knjiznica.dodajKnjigu("Hamlet");
            knjiznica.dodajKnjigu("Lovac");
            knjiznica.dodajKnjigu("Vuk");
            knjiznica.dodajKnjigu("Crvenkapica");
            knjiznica.dodajKnjigu("Crvenkapica");
            knjiznica.dodajKnjigu("Crvenkapica");
            knjiznica.dodajKnjigu("Crvenkapica");
            knjiznica.dodajKnjigu("Snjeguljica");
            knjiznica.dodajKnjigu("Hamlet");

            knjiznica.ispisKnjiga();
            Console.WriteLine();

            knjiznica.dodajKorisnika("Ivan");
            knjiznica.dodajKorisnika("Marko");
            knjiznica.dodajKorisnika("Ivan");
            knjiznica.dodajKorisnika("Vito");
            knjiznica.dodajKorisnika("Mirko");
            knjiznica.dodajKorisnika("Bruno");

            knjiznica.posudiKnjigu("2", "3");
            knjiznica.posudiKnjigu("2", "4");
            knjiznica.posudiKnjigu("2", "3");
            knjiznica.posudiKnjigu("2", "3");
            knjiznica.posudiKnjigu("2", "3");
            knjiznica.posudiKnjigu("2", "3");

            knjiznica.ispisKorisnika();
            Console.WriteLine();

        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string ime = imeTextBox.Text;
            //string prezime = prezimeTextBox.Text;
            string prezime = "test";

            if(ime != "" && prezime != "")
            {
                knjiznica.dodajKorisnika(ime);
                korisniciList.Items.Clear();
                foreach(Osoba o in knjiznica.vratiKorisnike())
                {
                    korisniciList.Items.Add(o.getIme());
                }
            }
        }
    }

    public class Knjiga
    {
        private static int id_counter = 0;
        private string id;
        private string naziv;
        public bool posudeno = false;


        public Knjiga(string naziv)
        {
            this.id = id_counter.ToString();
            this.naziv = naziv;

            id_counter++;
        }

        public string getNaziv()
        {
            return this.naziv;
        }

        public string getId()
        {
            return this.id;
        }
    }

    public class Osoba
    {
        public List<Knjiga> knjige = new List<Knjiga>();
        private static int id_counter = 0;
        private string ime;
        private string id;

        public Osoba(string ime)
        {
            this.id = id_counter.ToString();
            this.ime = ime;

            id_counter++;
        }

        public void ispisKnjiga(string padding = "")
        {
            foreach (Knjiga k in this.knjige)
            {
                Console.WriteLine(padding + k.getId() + " -> " + k.getNaziv());
            }
        }


        public string getID()
        {
            return this.id;
        }

        public string getIme()
        {
            return this.ime;
        }

    }

    public class Knjiznica
    {
        private List<Knjiga> knjige = new List<Knjiga>();
        private List<Osoba> korisnici = new List<Osoba>();

        public List<Osoba> vratiKorisnike()
        {
            return this.korisnici;
        }

        public List<Knjiga> vratiKnjige()
        {
            return this.knjige;
        }

        public void dodajKnjigu(string naziv)
        {
            knjige.Add(new Knjiga(naziv));
        }

        public void dodajKorisnika(string ime)
        {
            korisnici.Add(new Osoba(ime));
        }

        public void ispisKnjiga()
        {
            foreach (Knjiga k in this.knjige)
            {
                Console.WriteLine(k.getId() + ": " + k.getNaziv());
            }
        }

        public void ispisKorisnika()
        {
            foreach (Osoba o in this.korisnici)
            {
                Console.WriteLine(o.getID() + ", " + o.getIme());
                o.ispisKnjiga(padding: "\t");
            }
        }

        public int korisnikIndex(string korisnik_id)
        {
            int i = 0;
            foreach (Osoba k in this.korisnici)
            {
                if (k.getID() == korisnik_id)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public int knjigaIndex(string knjiga_id)
        {
            int i = 0;
            foreach (Knjiga k in this.knjige)
            {
                if (k.getId() == knjiga_id)
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public void posudiKnjigu(string user_id, string knjiga_id)
        {
            int o_index = korisnikIndex(user_id);
            int k_index = knjigaIndex(knjiga_id);

            if (this.knjige[k_index].posudeno == false)
            {
                this.korisnici[o_index].knjige.Add(this.knjige[k_index]);
                this.knjige[k_index].posudeno = true;
            }
            else
            {
                Console.WriteLine("\t\t\tERROR: Knjiga " + knjiga_id + " je posudena");
            }

        }
    }
}
