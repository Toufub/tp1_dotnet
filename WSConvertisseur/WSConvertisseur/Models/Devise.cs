using System.ComponentModel.DataAnnotations;

namespace WSConvertisseur.Models
{
    public class Devise
    {
		private int id;
		private string? nomDevise;
		private double taux;

        /// <summary>
        /// Devise without params
        /// </summary>
        /// <returns>No return</returns>
		public Devise()
		{

		}

        /// <summary>
        /// Devise with params
        /// </summary>
        /// <returns>No return</returns>
        /// <param name="id">The devise id</param>
        /// <param name="nomDevise">The devise name</param>        
		/// <param name="taux">The devise taux</param>
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

		[Required]
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
