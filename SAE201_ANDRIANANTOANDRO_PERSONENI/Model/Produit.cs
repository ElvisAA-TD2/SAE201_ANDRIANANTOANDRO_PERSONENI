using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public enum ActionProduit { Modifier, RendreIndisponible}
    public class Produit
    {
        private string codeProduit, nomProduit, cheminImage;
        private decimal prixVente;
        private int qteStock, numProduit;
        private bool disponible;
        private TypePointe unTypePointe;
        private Type unType;
        private List<Couleur> lesCouleurs;

        public Produit()
        {
        }

        public Produit(int numProduit, string codeProduit, string nomProduit, decimal prixVente, int qteStock, bool disponible, TypePointe unTypePointe, Type unType, List<Couleur> lesCouleur)
        {
            this.CodeProduit = codeProduit;
            this.NomProduit = nomProduit;
            this.PrixVente = prixVente;
            this.QteStock = qteStock;
            this.NumProduit = numProduit;
            this.Disponible = disponible;
            this.UnTypePointe = unTypePointe;
            this.UnType = unType;
            this.LesCouleurs = lesCouleur;

        }

        public string CodeProduit
        {
            get
            {
                return codeProduit;
            }

            set
            {
                this.codeProduit = value;
            }
        }

        public string NomProduit
        {
            get
            {
                return nomProduit;
            }

            set
            {
                this.nomProduit = value;
            }
        }

        public string CheminImage
        {
            get
            {
                return cheminImage;
            }

            set
            {
                this.cheminImage = value;
            }
        }

        public decimal PrixVente
        {
            get
            {
                return prixVente;
            }

            set
            {
                this.prixVente = value;
            }
        }

        public int QteStock
        {
            get
            {
                return qteStock;
            }

            set
            {
                this.qteStock = value;
            }
        }
        public int NumProduit
        {
            get
            {
                return numProduit;
            }

            set
            {
                this.numProduit = value;
            }
        }

        public bool Disponible
        {
            get
            {
                return this.disponible;
            }

            set
            {
                this.disponible = value;
            }
        }

        public TypePointe UnTypePointe
        {
            get
            {
                return unTypePointe;
            }

            set
            {
                this.unTypePointe = value;
            }
        }

        public Type UnType
        {
            get
            {
                return unType;
            }

            set
            {
                this.unType = value;
            }
        }

        public List<Couleur> LesCouleurs
        {
            get
            {
                return this.lesCouleurs;
            }

            set
            {
                this.lesCouleurs = value;
            }
        }

        public string NomCouleurConcatene
        {
            get
            {
                string couleurConcatene = "";
                foreach (Couleur uneCouleur in LesCouleurs)
                    couleurConcatene += uneCouleur.NomCouleur + ", ";
                return couleurConcatene;
            }
        }



        public List<Produit> FindAll(GestionPilot laGestion)
        {
            List<Produit> lesProduits = new List<Produit>();

            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM produit"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);

                foreach (DataRow dr in dt.Rows)
                {
                    var produitAInstancier = new Produit(
                        (int)dr["numproduit"],
                        (string)dr["codeproduit"],
                        (string)dr["nomproduit"],
                        (decimal)dr["prixvente"],
                        (int)dr["quantitestock"],
                        (bool)dr["disponible"],
                        laGestion.LesTypePointes.FirstOrDefault(tp => tp.CodeTypePointe == (int)dr["numtypepointe"]),
                        laGestion.LesTypes.FirstOrDefault(t => t.CodeType == (int)dr["numtype"]),
                        new List<Couleur>() // Important : liste vide pour pouvoir ajouter ensuite
                    );

                    // Ajout des couleurs liées au produit
                    var couleursAssociees = FindCouleurProduit(produitAInstancier.NumProduit);
                    foreach (var cp in couleursAssociees)
                    {
                        var couleur = laGestion.LesCouleurs.FirstOrDefault(c => c.NumCouleur == cp.CodeCouleur);
                        if (couleur != null)
                            produitAInstancier.LesCouleurs.Add(couleur);
                    }

                    lesProduits.Add(produitAInstancier);
                }
            }

            return lesProduits;
        }


        public List<CouleurProduit> FindCouleurProduit(int numProduit)
        {
            List<CouleurProduit> lesCouleurProduits = new List<CouleurProduit>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from couleurproduit where numproduit = @numproduit"))
            {
                cmdSelect.Parameters.AddWithValue("numproduit", numProduit);

                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesCouleurProduits.Add(new CouleurProduit((Int32)dr["numcouleur"], (Int32)dr["numproduit"]));
            }
            return lesCouleurProduits;
        }
        public int Update()
        {
            using (var cmdUpdate = new NpgsqlCommand("update produit set nomproduit =@nomproduit ,  numtypepointe = @numtypepointe,  prixvente = @prixvente , " +
                "qtestock =@quantitestock , numtype =@numtype  where numproduit =@numproduit;"))
            {
                cmdUpdate.Parameters.AddWithValue("nomproduit", this.NomProduit);
                cmdUpdate.Parameters.AddWithValue("numtypepointe", this.UnTypePointe.CodeTypePointe);
                cmdUpdate.Parameters.AddWithValue("prixvente", this.PrixVente);
                cmdUpdate.Parameters.AddWithValue("qtestock", this.QteStock);
                cmdUpdate.Parameters.AddWithValue("numtype", this.UnType.CodeType);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }
        public int RendreIndisponible()
        {
            using (var cmdUpdate = new NpgsqlCommand("update produit set disponible =false where numproduit =@numproduit;"))
            {
                cmdUpdate.Parameters.AddWithValue("numproduit", this.NumProduit);
                return DataAccess.Instance.ExecuteSet(cmdUpdate);
            }
        }
    }
}