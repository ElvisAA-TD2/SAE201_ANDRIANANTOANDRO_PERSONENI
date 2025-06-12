using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public enum ActionProduit { Modifier, RendreIndisponible}
    public class Produit : INotifyPropertyChanged
    {
        private string codeProduit, nomProduit, cheminImage;
        private decimal prixVente;
        private int qteStock, numProduit;
        private bool disponible;
        private TypePointe unTypePointe;
        private Type unType;
        private List<Couleur> lesCouleurs;

        public event PropertyChangedEventHandler? PropertyChanged;

        public Produit()
        {
        }
        public Produit(int numProduit, string codeProduit, string nomProduit, decimal prixVente, int qteStock, bool disponible, TypePointe unTypePointe, Type unType, List<Couleur> lesCouleur, string cheminImage)
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
            this.CheminImage = cheminImage;
        }

        public string CodeProduit
        {
            get
            {
                return codeProduit;
            }

            set
            {
                if (String.IsNullOrWhiteSpace(value)) { throw new ArgumentNullException("code produit null"); }
                if (value.Substring(0,1) != "C") { throw new ArgumentOutOfRangeException("Code produit non valide"); }
                else                  
                    this.codeProduit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CodeProduit)));
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
                if (String.IsNullOrEmpty(value)) { throw new ArgumentNullException("Nom produit non valide"); }
                else
                    this.nomProduit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NomProduit)));
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
                if (String.IsNullOrEmpty(value)) { throw new ArgumentNullException("Chemin image non valide"); }
                else
                    this.cheminImage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CheminImage)));
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
                if (value <= 0) { throw new ArgumentOutOfRangeException("Prix vente négatif ou égale à 0 impossible"); }
                else
                    this.prixVente = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PrixVente)));
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
                    if (value < 0) {throw new ArgumentOutOfRangeException("Stock négatif impossible"); }
                    else
                        this.qteStock = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QteStock)));
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
                //Check de nombre négatif déja fais dans la bd
                this.numProduit = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NumProduit)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Disponible)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UnTypePointe)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UnType)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LesCouleurs)));
            }
        }

        public string NomCouleurConcatene
        {
            get
            {
                string couleurConcatene = "";
                foreach (Couleur uneCouleur in this.LesCouleurs)
                    couleurConcatene += uneCouleur.NomCouleur + ", ";
                return couleurConcatene;
            }
        }

        public List<Produit> FindAll(GestionPilot laGestion)
        {
            try
            {
                List<Produit> lesProduits = new List<Produit>();

                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("SELECT * FROM produit"))
                {
                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);

                    foreach (DataRow dr in dt.Rows)
                    {
                        Produit produitAInstancier = new Produit(
                            (int)dr["numproduit"],
                            (string)dr["codeproduit"],
                            (string)dr["nomproduit"],
                            (decimal)dr["prixvente"],
                            (int)dr["quantitestock"],
                            (bool)dr["disponible"],
                            laGestion.LesTypePointes.FirstOrDefault(tp => tp.CodeTypePointe == (int)dr["numtypepointe"]),
                            laGestion.LesTypes.FirstOrDefault(t => t.CodeType == (int)dr["numtype"]),
                            new List<Couleur>(),
                            (string)dr["cheminimage"]
                        );

                        // Ajout des couleurs liées au produit
                        List<CouleurProduit> couleursAssociees = FindCouleurProduit(produitAInstancier.NumProduit);
                        foreach (CouleurProduit uneCouleurProduit in couleursAssociees)
                        {
                            Couleur couleur = laGestion.LesCouleurs.FirstOrDefault(c => c.NumCouleur == uneCouleurProduit.CodeCouleur);
                            if (couleur != null)
                                produitAInstancier.LesCouleurs.Add(couleur);
                        }

                        lesProduits.Add(produitAInstancier);
                    }
                }

                return lesProduits;
            }
            catch (Exception ex) { throw new ArgumentException("problème sur la requête"); }
            
        }


        public List<CouleurProduit> FindCouleurProduit(int numProduit)
        {
            try
            {
                List<CouleurProduit> lesCouleurProduits = new List<CouleurProduit>();
                using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from couleurproduit where numproduit = @numproduit;"))
                {
                    cmdSelect.Parameters.AddWithValue("numproduit", numProduit);

                    DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                    foreach (DataRow dr in dt.Rows)
                        lesCouleurProduits.Add(new CouleurProduit((Int32)dr["numcouleur"], (Int32)dr["numproduit"]));
                }
                return lesCouleurProduits;
            }
            catch (Exception ex) { throw new ArgumentException("problème sur la requête"); }

        }
        public int Update()
        {
            try
            {
                using (var cmdDelete = new NpgsqlCommand("delete from couleurproduit where numproduit =@numproduit;"))
                {
                    cmdDelete.Parameters.AddWithValue("numproduit", this.NumProduit);
                }

                foreach (Couleur uneCouleur in this.LesCouleurs)
                {
                    using (var cmdInsert = new NpgsqlCommand("insert into couleurproduit values (numproduit, numcouleur) values (@numproduit, @numcouleur);"))
                    {
                        cmdInsert.Parameters.AddWithValue("numproduit", this.NumProduit);
                        cmdInsert.Parameters.AddWithValue("numcouleur", uneCouleur);
                    }
                }
                

                using (var cmdUpdate = new NpgsqlCommand("update produit set nomproduit =@nomproduit ,  numtypepointe = @numtypepointe,  prixvente = @prixvente , " +
                    "quantitestock =@quantitestock , numtype =@numtype  where numproduit =@numproduit;"))
                {
                    cmdUpdate.Parameters.AddWithValue("nomproduit", this.NomProduit);
                    cmdUpdate.Parameters.AddWithValue("numtypepointe", this.UnTypePointe.CodeTypePointe);
                    cmdUpdate.Parameters.AddWithValue("prixvente", this.PrixVente);
                    cmdUpdate.Parameters.AddWithValue("quantitestock", this.QteStock);
                    cmdUpdate.Parameters.AddWithValue("numtype", this.UnType.CodeType);
                    cmdUpdate.Parameters.AddWithValue("numproduit", this.NumProduit);
                    return DataAccess.Instance.ExecuteSet(cmdUpdate);
                }
            }
            catch (Exception ex) {
                LogError.Log(ex, "Erreur");
                throw new ArgumentException("Problème sur la requête"); 
            }
        }
        public int RendreIndisponible()
        {
            try
            {
                using (var cmdUpdate = new NpgsqlCommand("update produit set disponible =false where numproduit =@numproduit;"))
                {
                    cmdUpdate.Parameters.AddWithValue("numproduit", this.NumProduit);
                    return DataAccess.Instance.ExecuteSet(cmdUpdate);
                }
            }
            catch (Exception ex) { throw new ArgumentException("Problème sur la requête"); }
        }

        public int Create()
        {
            try
            {
                int nb = 0;
                using (var cmdInsert = new NpgsqlCommand("insert into produit (numtypepointe, numtype, codeproduit, nomproduit, prixvente, quantitestock, disponible, cheminimage) " +
                    "values (@numtypepointe, @numtype, @codeproduit, @nomproduit, @prixvente, @quantitestock, @disponible, @cheminimage) " +
                    "RETURNING numrevendeur;"))
                {
                    cmdInsert.Parameters.AddWithValue("numtypepointe", this.UnTypePointe.CodeTypePointe);
                    cmdInsert.Parameters.AddWithValue("numtype", this.UnType.NomType);
                    cmdInsert.Parameters.AddWithValue("codeproduit", this.CodeProduit);
                    cmdInsert.Parameters.AddWithValue("nomproduit", this.NomProduit);
                    cmdInsert.Parameters.AddWithValue("prixvente", this.PrixVente);
                    cmdInsert.Parameters.AddWithValue("quantitestock", this.QteStock);
                    cmdInsert.Parameters.AddWithValue("disponible", this.Disponible);
                    cmdInsert.Parameters.AddWithValue("cheminimage", this.CheminImage);
                    nb = DataAccess.Instance.ExecuteInsert(cmdInsert);
                }
                this.NumProduit = nb;
                return nb;
            }
            catch (Exception ex) { throw new ArgumentException("Problème sur la requête"); }
        }
    }
}