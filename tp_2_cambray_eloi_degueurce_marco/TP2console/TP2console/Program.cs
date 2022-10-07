using System;
using TP2console.Models.EntityFramework;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace TP2console
{
    class Program
    {
        static void Main(string[] args)
        {
            Exo2Q2();
            Console.ReadKey();
        }
        public static void Exo2Q1()
        {
            var ctx = new FilmsDBContext();
            foreach (var film in ctx.Films)
            {
                Console.WriteLine(film.ToString());
            }
        }

        public static void Exo2Q2()
        {
            var ctx = new FilmsDBContext();
            foreach (var utilisateur in ctx.Utilisateurs)
            {
                Console.WriteLine(utilisateur.Email);
            }
        }
    }
}