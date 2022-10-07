using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP3.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TP3.Models.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace TP3.Controllers.Tests
{
    [TestClass()]
    public class UtilisateursControllerTests
    {
        private readonly UtilisateursController _controller;
        private readonly SerieDBContext _context;
        public UtilisateursControllerTests()
        {
            var builder = new DbContextOptionsBuilder<SerieDBContext>().UseNpgsql("Server=localhost;port=5432;Database=FilmsDB;uid=postgres;password=postgres;");
            this._context = new SerieDBContext(builder.Options);
            this._controller = new UtilisateursController(_context);
        }

        [TestMethod()]
        public void GetUtilisateursTest()
        {
            Task<ActionResult<IEnumerable<Utilisateur>>> task = this._controller.GetUtilisateurs();
            task.Wait();
            var users_from_get = task.Result.Value.ToArray();
            var users_from_db = this._context.Utilisateurs.ToArray();
            Assert.IsTrue(users_from_get.SequenceEqual(users_from_db));
        }

        [TestMethod()]
        public void GetUtilisateurByIdTest()
        {
            Task<ActionResult<Utilisateur>> task = this._controller.GetUtilisateurById(24);
            task.Wait();
            var user_from_get = task.Result.Value;
            var user_from_db = this._context.Utilisateurs
                        .Where(u => u.UtilisateurId == 24)
                       .FirstOrDefault();
            var different_user_from_db = this._context.Utilisateurs
                        .Where(u => u.UtilisateurId == 42)
                       .FirstOrDefault();
            Assert.IsTrue(user_from_db == user_from_get);
            Assert.IsFalse(different_user_from_db == user_from_get);
        }

        [TestMethod()]
        public void GetUtilisateurByEmailTest()
        {
            Task<ActionResult<Utilisateur>> task = this._controller.GetUtilisateurByEmail("rrichings1@naver.com");
            task.Wait();
            var user_from_get = task.Result.Value;
            var user_from_db = this._context.Utilisateurs
                .Where(u => u.Mail.ToUpper().Equals(("rrichings1@naver.com").ToUpper()))
                .FirstOrDefault();
            var different_user_from_db = this._context.Utilisateurs
                .Where(u => u.Mail.ToUpper().Equals(("lrudland3@360.cn").ToUpper()))
                .FirstOrDefault();
            Assert.IsTrue(user_from_db == user_from_get);
            Assert.IsFalse(different_user_from_db == user_from_get);
        }

        [TestMethod]
        public void Postutilisateur_ModelValidated_CreationOK()
        {
            // Arrange
            Random rnd = new Random();
            int chiffre = rnd.Next(1, 1000000000);
            // Le mail doit être unique donc 2 possibilités :
            // 1. on s'arrange pour que le mail soit unique en concaténant un random ou un timestamp
            // 2. On supprime le user après l'avoir créé. Dans ce cas, nous avons besoin d'appeler la méthode DELETE du WS => la décommenter
            Utilisateur userAtester = new Utilisateur()
            {
                Nom = "MACHIN",
                Prenom = "Luc",
                Mobile = "0606070809",
                Mail = "machin" + chiffre + "@gmail.com",
                Pwd = "Toto1234!",
                Rue = "Chemin de Bellevue",
                CodePostale = "74940",
                Ville = "Annecy-le-Vieux",
                Pays = "France",
                Latitude = null,
                Longitude = null
            };
            // Act
            var result = _controller.PostUtilisateur(userAtester).Result;
            // .Result pour appeler la méthode async de manière synchrone, afin d'obtenir le résultat
            var result2 = _controller.GetUtilisateurByEmail(userAtester.Mail);
            var actionResult = result2.Result as ActionResult<Utilisateur>;
            // Assert
            Assert.IsInstanceOfType(actionResult.Value, typeof(Utilisateur), "Pas un utilisateur");
            Utilisateur? userRecupere = _context.Utilisateurs.Where(u => u.Mail.ToUpper() ==
            userAtester.Mail.ToUpper()).FirstOrDefault();
            // On ne connait pas l'ID de l’utilisateur envoyé car numéro automatique.
            // Du coup, on récupère l'ID de celui récupéré et on compare ensuite les 2 users
            userAtester.UtilisateurId = userRecupere.UtilisateurId;
            Assert.AreEqual(userRecupere, userAtester, "Utilisateurs pas identiques");
        }
    }
}