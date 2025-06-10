using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class Commande
    {
        private int numCommande;
        private DateTime dateCommande, dateLivraison;
        private Employe unEmploye;
        private ModeTransport unModeTransport;
        private Revendeur unRevendeur;
        private List<ProduitCommande> lesProduitCommande;
        private decimal prixTotal;

        public Commande()
        {
        }

        public Commande(int numCommande, Employe unEmploye, Revendeur unRevendeur, ModeTransport unModeTransport, DateTime dateCommande, DateTime dateLivraison, List<ProduitCommande> lesProduitCommande, decimal prixTotal)
        {
            this.NumCommande = numCommande;
            this.DateCommande = dateCommande;
            this.DateLivraison = dateLivraison;
            this.UnEmploye = unEmploye;
            this.UnModeTransport = unModeTransport;
            this.UnRevendeur = unRevendeur;
            this.LesProduitCommande = lesProduitCommande;
            this.PrixTotal = prixTotal;
        }
        public Commande(Employe unEmploye, Revendeur unRevendeur, ModeTransport unModeTransport, DateTime dateCommande, DateTime dateLivraion, List<ProduitCommande> lesProduitCommande, decimal prixTotal)
        {
            this.DateCommande = dateCommande;
            this.DateLivraison = dateLivraion;
            this.UnEmploye = unEmploye;
            this.UnModeTransport = unModeTransport;
            this.UnRevendeur = unRevendeur;
            this.LesProduitCommande = lesProduitCommande;
            this.PrixTotal = prixTotal;
        }

        public int NumCommande
        {
            get
            {
                return numCommande;
            }

            set
            {
                this.numCommande = value;
            }
        }

        public DateTime DateCommande
        {
            get
            {
                return dateCommande;
            }

            set
            {
                this.dateCommande = value;
            }
        }

        public DateTime DateLivraison
        {
            get
            {
                return this.dateLivraison;
            }

            set
            {
                this.dateLivraison = value;
            }
        }

        public Employe UnEmploye
        {
            get
            {
                return this.unEmploye;
            }

            set
            {
                this.unEmploye = value;
            }
        }

        public ModeTransport UnModeTransport
        {
            get
            {
                return this.unModeTransport;
            }

            set
            {
                this.unModeTransport = value;
            }
        }

        public Revendeur UnRevendeur
        {
            get
            {
                return this.unRevendeur;
            }

            set
            {
                this.unRevendeur = value;
            }
        }

        public List<ProduitCommande> LesProduitCommande
        {
            get
            {
                return this.lesProduitCommande;
            }

            set
            {
                this.lesProduitCommande = value;
            }
        }

        public decimal PrixTotal
        {
            get
            {
                return this.prixTotal;
            }

            set
            {
                this.prixTotal = value;
            }
        }

        public List<Commande> FindAll(GestionPilot gestionPilot)
        {
            List<Commande> lesCommandes = new List<Commande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from commande ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesCommandes.Add(new Commande((Int32)dr["numcommande"], gestionPilot.LesEmploye.FirstOrDefault(e => e.NumEmploye == (Int32)dr["numemploye"]),
                        gestionPilot.LesRevendeurs.SingleOrDefault(r => r.NumRevendeur == (Int32)dr["numrevendeur"]),
                        gestionPilot.LesModeTransports.FirstOrDefault(tr => tr.NumModeTransport == (Int32)dr["numtransport"]),
                        (DateTime)dr["datecommande"],
                        (DateTime)dr["datelivraison"],
                        gestionPilot.LesProduitCommandes.Where(pc => pc.NumCommande == (Int32)dr["numcommande"]).ToList(), 
                        (Decimal)dr["prixtotal"]));
            }
            return lesCommandes;
        }

        public int Delete()
        {
            using (var cmdUpdate = new NpgsqlCommand("delete from commande  where numcommande =@numcommande;"))
            {
                cmdUpdate.Parameters.AddWithValue("numcommande", this.NumCommande);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }

        public int Create()
        {
            int nb = 0;
            using (var cmdInsert = new NpgsqlCommand("insert into commande (numemploye,numtransport,numrevendeur,datecommande, datelivraison, prixtotal) " +
                "values ( @numemploye, @numtransport, @numrevendeur, @datecommande, @datelivraison, @prixtotal) RETURNING numcommande"))
            {
                cmdInsert.Parameters.AddWithValue("numemploye", this.UnEmploye.NumEmploye);
                cmdInsert.Parameters.AddWithValue("numtransport", this.UnModeTransport.NumModeTransport);
                cmdInsert.Parameters.AddWithValue("numrevendeur", this.UnRevendeur.NumRevendeur);
                cmdInsert.Parameters.AddWithValue("datecommande", this.DateCommande);
                cmdInsert.Parameters.AddWithValue("datelivraison", this.DateLivraison);
                cmdInsert.Parameters.AddWithValue("prixtotal", this.PrixTotal);
                nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
            }
            this.NumCommande = nb;
            return nb;
        }

        public override string? ToString()
        {
            return $"Num commande : {this.NumCommande} \ndateCommande : {this.DateCommande.ToShortDateString()} \nRevendeur : {this.UnRevendeur.RaisonSociale} \n" +
                $"employe : {this.UnEmploye.Nom} \nPrix : {this.PrixTotal} \nProduitCommandé : {this.LesProduitCommande.Count()}\nMode de transport : {this.UnModeTransport.NomModeTransport} ";
        }
    }
}
