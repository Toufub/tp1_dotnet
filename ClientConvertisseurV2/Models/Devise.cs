using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConvertisseurV2.Models
{
    internal class Devise
    {
        private int id;
        private string? nomDevise;
        private double taux;

		public Devise()
        {

        }

        public Devise(int id, string? nomDevise, double taux)
        {
            this.id = id;
            this.nomDevise = nomDevise;
            this.taux = taux;
        }

        public double Taux
        {
            get { return taux; }
            set { taux = value; }
        }

        public string? NomDevise
        {
            get { return nomDevise; }
            set { nomDevise = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

    }
}
