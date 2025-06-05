using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAE201_ANDRIANANTOANDRO_PERSONENI.Model
{
    public class ModeTransport
    {
        private int numModeTransport;
        private string nomModeTransport;

        public ModeTransport()
        {
        }

        public ModeTransport(int numModeTransport, string nomModeTransport)
        {
            this.NumModeTransport = numModeTransport;
            this.NomModeTransport = nomModeTransport;
        }

        public int NumModeTransport
        {
            get
            {
                return numModeTransport;
            }

            set
            {
                this.numModeTransport = value;
            }
        }

        public string NomModeTransport
        {
            get
            {
                return this.nomModeTransport;
            }

            set
            {
                this.nomModeTransport = value;
            }
        }
    }
}
