using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_Concept.classes
{
    // Classe Personne (classe de base)
    public class Personne
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }

        public Personne(string nom, string prenom)
        {
            Nom = nom;
            Prenom = prenom;
        }
    }
}
