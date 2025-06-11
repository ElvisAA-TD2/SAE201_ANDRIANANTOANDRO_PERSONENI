using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class Revendeur : INotifyPropertyChanged
    {
        private int numRevendeur;
        private string raisonSociale, adresseRue, adresseCP, adresseVille;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Revendeur(int numRevendeur, string raisonSociale, string adresseRue, string adresseCP, string adresseVille)
        {
            this.NumRevendeur = numRevendeur;
            this.RaisonSociale = raisonSociale;
            this.AdresseRue = adresseRue;
            this.AdresseCP = adresseCP;
            this.AdresseVille = adresseVille;
        }
        public Revendeur()
        {
        }

        public int NumRevendeur
        {
            get
            {
                return numRevendeur;
            }

            set
            {
                //Check de nombre négatif déja fais dans la bd
                this.numRevendeur = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumRevendeur)));
            }
        }

        public string RaisonSociale
        {
            get
            {
                return raisonSociale;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La raison sociale ne peut pas être vide");
                this.raisonSociale = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RaisonSociale)));
            }
        }

        public string AdresseRue
        {
            get
            {
                return adresseRue;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("L'adresse rue ne peut pas être vide");
                this.adresseRue = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AdresseRue)));
            }
        }

        public string AdresseCP
        {
            get
            {
                return adresseCP;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Le codePostal ne peut pas être vide");
                this.adresseCP = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AdresseCP)));
            }
        }

        public string AdresseVille
        {
            get
            {
                return this.adresseVille;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("La ville ne peut pas être vide");
                this.adresseVille = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AdresseVille)));
            }
        }

        public List<Revendeur> FindAll()
        {
            List<Revendeur> lesRevendeurs = new List<Revendeur>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from revendeur order by numrevendeur ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesRevendeurs.Add(new Revendeur((Int32)dr["numrevendeur"], (String)dr["raisonsociale"], 
                        (String)dr["adresserue"], (String)dr["adressecp"], (String)dr["adresseville"]));
            }
            return lesRevendeurs;
        }

        public int Create()
        {
            try
            {
                int nb = 0;
                using (var cmdInsert = new NpgsqlCommand("insert into revendeur (raisonsociale, adresserue, adressecp , adresseville) " +
                    "values (@raisonsociale,@adresserue,@adressecp, @adresseville) RETURNING numrevendeur"))
                {
                    cmdInsert.Parameters.AddWithValue("raisonsociale", this.RaisonSociale);
                    cmdInsert.Parameters.AddWithValue("adresserue", this.AdresseRue);
                    cmdInsert.Parameters.AddWithValue("adressecp", this.AdresseCP);
                    cmdInsert.Parameters.AddWithValue("adresseville", this.AdresseVille);
                    nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
                }
                this.NumRevendeur = nb;
                return nb;
            }
            catch (Exception ex) { throw new ArgumentException("Problème sur la requête"); }
        }

        public void Read()
        {
            try
            {
                using (var cmdSelect = new NpgsqlCommand("select * from  revendeur  where numrevendeur =@id;"))
                {
                    cmdSelect.Parameters.AddWithValue("id", this.NumRevendeur);

                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                    this.RaisonSociale = (String)dt.Rows[0]["raisonsociale"];
                    this.AdresseRue = (String)dt.Rows[0]["adresserue"];
                    this.AdresseCP = (String)dt.Rows[0]["adressecp"];
                    this.adresseVille = (String)dt.Rows[0]["adresseville"];
                }
            }
            catch (Exception ex) { throw new ArgumentException("Problème sur la requête"); }

        }

        public int Update()
        {
            try
            {
                using (var cmdUpdate = new NpgsqlCommand("update revendeur set raisonsociale =@raisonsociale ,  adresserue = @adresserue,  adressecp = @adressecp, " +
                "adresseville = @adresseville  where numrevendeur =@id;"))
                {
                    cmdUpdate.Parameters.AddWithValue("raisonsociale", this.RaisonSociale);
                    cmdUpdate.Parameters.AddWithValue("adresserue", this.AdresseRue);
                    cmdUpdate.Parameters.AddWithValue("adressecp", this.AdresseCP);
                    cmdUpdate.Parameters.AddWithValue("adresseville", this.AdresseVille);
                    cmdUpdate.Parameters.AddWithValue("id", this.NumRevendeur);
                    return DataAccess.Instance.ExecuteSet(cmdUpdate);
                }
            }
            catch (Exception ex) { throw new ArgumentException("Problème sur la requête"); }
        }
    }
}
