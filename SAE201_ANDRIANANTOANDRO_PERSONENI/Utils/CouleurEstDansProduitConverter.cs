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
            if (values == null || values.Length < 2 || values[0] == null || values[1] == null)
                return false;

            string nomCouleurAVerifier = values[0] as string;
            if (string.IsNullOrWhiteSpace(nomCouleurAVerifier))
                return false;

            if (values[1] is List<Couleur> lesCouleurs)
            {
                return lesCouleurs.Any(c =>
                    string.Equals(c.NomCouleur, nomCouleurAVerifier, StringComparison.OrdinalIgnoreCase));
            }

            return false;
        }

        public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return new object[] { Binding.DoNothing, Binding.DoNothing };
        }

    }

}
