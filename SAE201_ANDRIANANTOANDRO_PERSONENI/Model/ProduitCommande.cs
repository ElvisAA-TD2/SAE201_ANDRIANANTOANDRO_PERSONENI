using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class ProduitCommande
    {
        private int numCommande;
        private int quantiteCommande;
        private decimal prix;
        private Produit unProduit;


        public ProduitCommande()
        {
        }

        public ProduitCommande(int numCommande, Produit unProduit, int quantiteCommande, decimal prix)
        {
            this.NumCommande = numCommande;
            this.UnProduit = unProduit;
            this.QuantiteCommande = quantiteCommande;
            this.Prix = prix;
        }

        public int QuantiteCommande
        {
            get
            {
                return quantiteCommande;
            }

            set
            {
                this.quantiteCommande = value;
            }
        }

        public decimal Prix
        {
            get
            {
                return prix;
            }

            set
            {
                this.prix = value;
            }
        }

        public Produit UnProduit
        {
            get
            {
                return this.unProduit;
            }

            set
            {
                this.unProduit = value;
            }
        }

        public int NumCommande
        {
            get
            {
                return this.numCommande;
            }

            set
            {
                this.numCommande = value;
            }
        }

        public List<ProduitCommande> FindAll(GestionPilot gestionPilot)
        {
            List<ProduitCommande> lesProduitsCommandes = new List<ProduitCommande>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from produitcommande ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesProduitsCommandes.Add(new ProduitCommande((Int32)dr["numcommande"], 
                        gestionPilot.LesProduits.SingleOrDefault(p => p.NumProduit == (Int32)dr["numproduit"]), 
                        (Int32)dr["quantitecommande"], (Decimal)dr["prix"] ));
            }
            return lesProduitsCommandes;
        }
    }
}
