using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class TypePointe
    {
        private int codeTypePointe;
        private string nomTypePointe;

        public int CodeTypePointe
        {
            get
            {
                return codeTypePointe;
            }

            set
            {
                this.codeTypePointe = value;
            }
        }

        public string NomTypePointe
        {
            get
            {
                return this.nomTypePointe;
            }

            set
            {
                this.nomTypePointe = value;
            }
        }
    }
}
