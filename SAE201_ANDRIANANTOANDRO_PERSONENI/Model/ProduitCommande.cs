﻿using Npgsql;
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
        private Produit unProduit;

        public ProduitCommande()
        {
        }

        public ProduitCommande(int numCommande, Produit unProduit, int quantiteCommande)
        {
            this.NumCommande = numCommande;
            this.UnProduit = unProduit;
            this.QuantiteCommande = quantiteCommande;
        }

        public int QuantiteCommande
        {
            get
            {
                return quantiteCommande;
            }

            set
            {
                if (value < 0) { throw new ArgumentOutOfRangeException("Qte commander suprérieur à 0"); }
                else
                    this.quantiteCommande = value;
            }
        }

        public decimal Prix
        {
            get
            {
                return this.QuantiteCommande * this.UnProduit.PrixVente;
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
                //Check de nombre négatif déja fais dans la bd
                this.numCommande = value;
            }
        }

        public Produit Produit
        {
            get
            {
                throw new System.NotImplementedException();
            }

            set
            {
            }
        }

        public List<ProduitCommande> FindAll(GestionPilot gestionPilot)
        {
            try
            {
                List<ProduitCommande> lesProduitsCommandes = new List<ProduitCommande>();
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from produitcommande ;"))
                {
                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                    foreach (DataRow dr in dt.Rows)
                        lesProduitsCommandes.Add(new ProduitCommande((Int32)dr["numcommande"],
                            gestionPilot.LesProduits.SingleOrDefault(p => p.NumProduit == (Int32)dr["numproduit"]),
                            (Int32)dr["quantitecommande"]));
                }
                return lesProduitsCommandes;
            }
            catch (Exception ex) 
            {
                LogError.Log(ex, "Erreur");
                throw new ArgumentException("problème sur la requête"); 
            }
        }

        public int Create(int numCommande)
        {
            try
            {
                int nb = 0;
                using (var cmdInsert = new NpgsqlCommand("insert into produitcommande (numcommande, numproduit, quantitecommande, prix) " +
                    "values (@numcommande, @numproduit, @quantitecommande, @prix) RETURNING numcommande"))
                {
                    cmdInsert.Parameters.AddWithValue("numcommande", numCommande);
                    cmdInsert.Parameters.AddWithValue("numproduit", this.UnProduit.NumProduit);
                    cmdInsert.Parameters.AddWithValue("quantitecommande", this.QuantiteCommande);
                    cmdInsert.Parameters.AddWithValue("prix", this.Prix);
                    nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
                }
                this.NumCommande = nb;
                return nb;
            }
            catch (Exception ex) 
            {
                LogError.Log(ex, "Erreur");
                throw new ArgumentException("problème sur la requête"); 
            }
        }
    }
}
