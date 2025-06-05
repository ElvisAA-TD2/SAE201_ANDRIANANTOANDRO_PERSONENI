using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class Role
    {
        private int numRole;
        private string nomRole;

        public Role(int numRole, string nomRole)
        {
            this.NumRole = numRole;
            this.NomRole = nomRole;
        }
        public Role()
        {
        }

        public int NumRole
        {
            get
            {
                return numRole;
            }

            set
            {
                this.numRole = value;
            }
        }

        public string NomRole
        {
            get
            {
                return this.nomRole;
            }

            set
            {
                this.nomRole = value;
            }
        }
    }
}
