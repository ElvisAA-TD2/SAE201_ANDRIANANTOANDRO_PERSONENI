using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Utils
{
    public class CouleurEstSelectionneeConverter : IValueConverter
    {
        public object Convert(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            string nomCouleur = (string)value;
            List<string> lesCouleurs = (List<string>)parameter;
            return lesCouleurs.Contains(nomCouleur);
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
