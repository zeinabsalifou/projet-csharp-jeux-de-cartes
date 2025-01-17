using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_Concept.classes
{
    // Joueur (classe dérivée de Personne)
    public class Joueur : Personne, IObservateur
    {
        public string Identifiant { get; }
        public List<Carte> Main { get; private set; }

        public Joueur(string nom, string prenom, string identifiant)
            : base(nom, prenom)
        {
            Identifiant = identifiant;
            Main = new List<Carte>();
        }

        public void AjouterCarte(Carte carte)
        {
            Main.Add(carte);
        }

        public void RetirerCarte(Carte carte)
        {
            Main.Remove(carte);
        }

        public Carte ChoisirCarteAPlacer(Carte derniereCartePileDeDepot, Couleur? couleurActuelle = null)
        {
            foreach (var carte in Main)
            {
                if (carte.Couleur == derniereCartePileDeDepot.Couleur || carte.Valeur == derniereCartePileDeDepot.Valeur)
                {
                    return carte;
                }
                if (couleurActuelle.HasValue && carte.Couleur == couleurActuelle.Value)
                {
                    return carte;
                }
            }
            return default; // Si aucune carte ne correspond, le joueur devra piocher
        }

        public void MiseAJour(string message)
        {
            Console.WriteLine($"Notification pour {Nom} {Prenom} : {message}");
        }

        public override string ToString()
        {
            return $"{Prenom} {Nom}";
        }
    }
}
