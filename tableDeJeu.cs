using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_Concept.classes
{
    // TableDeJeu (classe, ISujet)
    public class TableDeJeu : ISujet
    {
        private List<IObservateur> observateurs;
        public PaireDeCartes PileDePioche { get; private set; }
        public List<Carte> PileDeDepot { get; private set; }

        public TableDeJeu()
        {
            observateurs = new List<IObservateur>();
            PileDePioche = new PaireDeCartes();
            PileDePioche.Melanger();
            PileDeDepot = new List<Carte>();
        }

        public void AjouterObservateur(IObservateur observateur)
        {
            observateurs.Add(observateur);
        }

        public void SupprimerObservateur(IObservateur observateur)
        {
            observateurs.Remove(observateur);
        }

        /*public void NotifierObservateurs(string message)
        {
            foreach (var observateur in observateurs)
            {
                observateur.MiseAJour(message);
            }
        }*/

        /* public void NotifierObservateurs(string message, Joueur joueurConcerné = null)
         {
             foreach (var observateur in observateurs)
             {
                 if (joueurConcerné == null || observateur != joueurConcerné)  // Exclure le joueur si spécifié
                 {
                     observateur.MiseAJour(message);
                 }
             }
         }*/

        public void NotifierObservateurs(string message)
        {
            foreach (var observateur in observateurs)
            {
                observateur.MiseAJour(message);  // Notifie tous les observateurs
            }
        }

        public void NotifierObservateurs(string message, Joueur joueurConcerné)
        {
            foreach (var observateur in observateurs)
            {
                if (observateur != joueurConcerné)  // Exclure le joueur concerné
                {
                    observateur.MiseAJour(message);
                }
            }
        }



        /* public void NotifierObservateurs(string message, Joueur joueurConcerné)
         {
             foreach (var observateur in observateurs)
             {
                 if (observateur != joueurConcerné)  // Exclure le joueur concerné
                 {
                     observateur.MiseAJour(message);
                 }
             }
         }*/


        public void AjouterCarteADepot(Carte carte)
        {
            PileDeDepot.Add(carte);
            Console.WriteLine($"Carte ajoutée à la pile de dépôt: {carte}");
        }

        public Carte ObtenirDerniereCarteDepot()
        {
            return PileDeDepot.LastOrDefault();
        }

        public void ReinitialiserPileDePioche()
        {
            if (PileDeDepot.Count <= 1)
            {
                throw new InvalidOperationException("Pas assez de cartes dans la pile de dépôt pour réinitialiser la pile de pioche.");
            }

            Carte derniereCarte = PileDeDepot.Last();
            PileDeDepot.RemoveAt(PileDeDepot.Count - 1);
            PileDePioche = new PaireDeCartes();
            PileDePioche.Melanger();
            PileDePioche.DistribuerCartes(0); // Initialize with empty list
            foreach (var carte in PileDeDepot)
            {
                PileDePioche.AjouterCarte(carte);
            }
            PileDeDepot.Clear();
            PileDeDepot.Add(derniereCarte);
            PileDePioche.Melanger();
        }

       

        public void AppliquerStrategieMinimisation(List<Joueur> joueurs, Joueur joueurConcerné, Carte derniereCartePileDeDepot)
        {
            // Filtrer les joueurs pour exclure le joueur concerné (celui avec une seule carte)
            var autresJoueurs = joueurs.Where(j => j != joueurConcerné).ToList();

            // Si aucun autre joueur n'est disponible, sortir
            if (autresJoueurs.Count == 0)
                return;

            // Sélection aléatoire d'un autre joueur pour appliquer la stratégie de minimisation des points
            Random rand = new Random();
            var joueurAffecte = autresJoueurs[rand.Next(autresJoueurs.Count)];

            Console.WriteLine($"{joueurAffecte.Nom} applique une stratégie pour minimiser ses points.");

            // Filtrer les cartes qui respectent la couleur ou la valeur de la dernière carte de la pile de dépôt
            var cartesJouables = joueurAffecte.Main
                                .Where(c => c.Couleur == derniereCartePileDeDepot.Couleur || c.Valeur == derniereCartePileDeDepot.Valeur)
                                .OrderByDescending(c => (int)c.Valeur)
                                .ToList();

            // Si une carte correspond, jouer la carte avec la plus grande valeur qui respecte la règle
            if (cartesJouables.Count > 0)
            {
                Carte carteMax = cartesJouables.First();
                joueurAffecte.RetirerCarte(carteMax);
                AjouterCarteADepot(carteMax);
                Console.WriteLine($"{joueurAffecte.Nom} joue {carteMax} pour minimiser ses points.");
            }
            else
            {
                // Si aucune carte ne correspond, le joueur doit piocher
                Console.WriteLine($"{joueurAffecte.Nom} ne peut pas jouer de carte et doit piocher.");
                Carte piochee = PileDePioche.PiocherCarte();
                joueurAffecte.AjouterCarte(piochee);
                Console.WriteLine($"{joueurAffecte.Nom} a pioché {piochee}.");
            }
        }

    }
}
