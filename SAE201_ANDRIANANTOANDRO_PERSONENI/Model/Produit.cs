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
    public class Produit
    {
        private string codeProduit, nomProduit, cheminImage;
        private double prixVente;
        private int qteStock;
        private bool disponible;
        private TypePointe unTypePointe;
        private Type unType;
        private List<CouleurProduit> lesCouleurProduit;

        public Produit()
        {
        }

        public Produit(string codeProduit, string nomProduit, string cheminImage, double prixVente, int qteStock, bool disponible, TypePointe unTypePointe, Type unType, List<CouleurProduit> lesCouleurProduit)
        {
            this.CodeProduit = codeProduit;
            this.NomProduit = nomProduit;
            this.CheminImage = cheminImage;
            this.PrixVente = prixVente;
            this.QteStock = qteStock;
            this.Disponible = disponible;
            this.UnTypePointe = unTypePointe;
            this.UnType = unType;
            this.LesCouleurProduit = lesCouleurProduit;
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

        public double PrixVente
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

        public List<CouleurProduit> LesCouleurProduit
        {
            get
            {
                return this.lesCouleurProduit;
            }

            set
            {
                this.lesCouleurProduit = value;
            }
        }
        public List<Produit> FindAll(ObservableCollection<TypePointe> unTypePointe, ObservableCollection<Type> unType, ObservableCollection<CouleurProduit> lesCouleurProduits)
        {
            List<Produit> lesProduits = new List<Produit>();
            using (NpgsqlCommand cmdSelect = new NpgsqlCommand("select * from produit ;"))
            {
                DataTable dt = DataAccess.Instance.ExecuteSelect(cmdSelect);
                foreach (DataRow dr in dt.Rows)
                    lesProduits.Add(new Produit((String)dr["numproduit"], (String)dr["nomproduit"],
                   (String)dr["cheminimage"], (Double)dr["prixvente"], (Int32)dr["quantitestock"],
                   (Boolean)dr["disponible"], unTypePointe.SingleOrDefault(c => c.CodeTypePointe == (Int32)dr["numtypepointe"]),
                   unType.SingleOrDefault(c => c.CodeType == (Int32)dr["numtype"]),
                   lesCouleurProduits.Where(c => c.UnProduit.CodeProduit == (String)dr["numproduit"]).ToList()));
            }
            return lesProduits;
        }
    }
}