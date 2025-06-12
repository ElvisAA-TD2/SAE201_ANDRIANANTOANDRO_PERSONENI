using SAE201_ANDRIANANTOANDRO_PERSONENI.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Utils
{
    public class CouleurEstDansProduitConverter : IMultiValueConverter
    {
        public object Convert(object[] values, System.Type targetType, object parameter, CultureInfo culture)
        {
            string nomCouleurAVerifier = values[0] as string;
            List<Couleur> lesCouleurs = (List<Couleur>)values[1];

            return lesCouleurs.Any(cp => cp.NomCouleur == nomCouleurAVerifier);
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { Binding.DoNothing, Binding.DoNothing };
        }

    }

}
