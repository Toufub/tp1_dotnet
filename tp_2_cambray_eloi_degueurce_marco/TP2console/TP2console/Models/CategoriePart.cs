using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2console.Models.EntityFramework
{
    public partial class Categorie
    {
        public override string ToString()
        {
            return Id + " : " + Nom;
        }
    }
}
