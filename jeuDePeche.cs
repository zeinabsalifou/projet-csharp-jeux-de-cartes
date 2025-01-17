using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_Concept.classes
{
    // JeuxDePêche (classe principale)
    public class JeuxDePêche
    {
        private List<Joueur> joueurs;
        private TableDeJeu tableDeJeu;
        private int direction; // 1 pour sens horaire, -1 pour sens antihoraire
        private int currentPlayerIndex;

        public JeuxDePêche()
        {
            tableDeJeu = new TableDeJeu();
            joueurs = new List<Joueur>();
            direction = 1; // Initialement dans le sens horaire
        }

        public void AjouterJoueur(Joueur joueur)
        {
            joueurs.Add(joueur);
            tableDeJeu.AjouterObservateur(joueur);
        }

        public async Task DemarrerJeuAsync()
        {
            DistribuerCartes();
            InitialiserPileDeDepot();
            await JouerAsync();
        }



        private void DistribuerCartes()
        {
            foreach (var joueur in joueurs)
            {
                joueur.Main.AddRange(tableDeJeu.PileDePioche.DistribuerCartes(5));
            }
            Console.WriteLine("\nLA PARTIE COMMENCE!!");  // Message simplifié
            Console.WriteLine("\nCartes distribués!\n");
        }

       /* private void DistribuerCartes()
        {
            foreach (var joueur in joueurs)
            {
                joueur.Main.AddRange(tableDeJeu.PileDePioche.DistribuerCartes(5));
                Console.WriteLine($"{joueur} a été distribué 5 cartes.");
            }
        }*/

        private void InitialiserPileDeDepot()
        {
            // Le premier joueur fixe (index 0) commence en posant une carte sur la pile de dépôt
            Joueur premierJoueur = joueurs[0];
            Carte premiereCarte = premierJoueur.ChoisirCarteAPlacer(new Carte(), null);
            if (premiereCarte.Valeur == default)
            {
                premiereCarte = premierJoueur.Main.First();
            }

            premierJoueur.RetirerCarte(premiereCarte);
            tableDeJeu.AjouterCarteADepot(premiereCarte);
            currentPlayerIndex = 0;

            Console.WriteLine($"{premierJoueur} commence en posant {premiereCarte} sur la pile de dépôt.");
            AppliquerReglesSpeciales(premiereCarte);
        }

        /*private void Jouer()
        {
            while (true)
            {
                Joueur joueurActuel = joueurs[currentPlayerIndex];
                Console.WriteLine($"\nC'est au tour de {joueurActuel}.");

                Carte derniereCarte = tableDeJeu.ObtenirDerniereCarteDepot();
                Couleur? couleurActuelle = null;

                // Si la dernière carte posée est un Valet, la couleur a été changée
                if (derniereCarte.Valeur == Valeur.Valet)
                {
                    // On suppose que la couleur a été changée automatiquement lors du changement de couleur
                    // Vous pouvez stocker la couleur actuelle si nécessaire
                    // Pour simplifier, on ne stocke pas la couleur actuelle, mais on pourrait le faire
                }

                Carte carteJouee = joueurActuel.ChoisirCarteAPlacer(derniereCarte, couleurActuelle);

                if (carteJouee.Valeur != default)
                {
                    // Appliquer les règles spéciales
                    AppliquerReglesSpeciales(carteJouee);

                    joueurActuel.RetirerCarte(carteJouee);
                    tableDeJeu.AjouterCarteADepot(carteJouee);
                }
                else
                {
                    Console.WriteLine($"{joueurActuel} ne peut pas jouer et doit piocher.");
                    try
                    {
                        Carte piochee = tableDeJeu.PileDePioche.PiocherCarte();
                        joueurActuel.AjouterCarte(piochee);
                        Console.WriteLine($"{joueurActuel} a pioché {piochee}.");
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("La pile de pioche est vide. Réinitialisation de la pile de pioche à partir de la pile de dépôt.");
                        tableDeJeu.ReinitialiserPileDePioche();
                        try
                        {
                            Carte piochee = tableDeJeu.PileDePioche.PiocherCarte();
                            joueurActuel.AjouterCarte(piochee);
                            Console.WriteLine($"{joueurActuel} a pioché {piochee}.");
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("Impossible de piocher des cartes. Fin du jeu.");
                            break;
                        }
                    }
                }

                if (joueurActuel.Main.Count == 1)
                {
                    tableDeJeu.NotifierObservateurs($"{joueurActuel} n'a plus qu'une carte !");
                    tableDeJeu.AppliquerStrategieMinimisation(joueurs);
                }

                if (joueurActuel.Main.Count == 0)
                {
                    Console.WriteLine($"\n{joueurActuel} a gagné !");
                    await Task.Delay(1000);  // Attendre une seconde avant de signaler la fin de la partie
                    Console.WriteLine("FIN DE LA PARTIE!!");
                    break;
                }


                // Changer de joueur en fonction de la direction
                currentPlayerIndex = (currentPlayerIndex + direction + joueurs.Count) % joueurs.Count;
            }
        }*/

        private async Task JouerAsync()
        {
            int nombreDeJoueurs = joueurs.Count;
            int joueurActuelIndex = 0;  // Initialiser avec le premier joueur

            while (true)
            {
                Joueur joueurActuel = joueurs[joueurActuelIndex];
                Console.WriteLine($"\nC'est au tour de {joueurActuel}.");

                Carte derniereCarte = tableDeJeu.ObtenirDerniereCarteDepot();
                Carte carteJouee = joueurActuel.ChoisirCarteAPlacer(derniereCarte);

                if (carteJouee.Valeur != default)
                {
                    // Joueur joue une carte
                    AppliquerReglesSpeciales(carteJouee);

                    joueurActuel.RetirerCarte(carteJouee);
                    tableDeJeu.AjouterCarteADepot(carteJouee);
                }
                else
                {
                    // Le joueur pioche s'il ne peut pas jouer
                    Console.WriteLine($"{joueurActuel} ne peut pas jouer et doit piocher.");
                    Carte piochee = tableDeJeu.PileDePioche.PiocherCarte();
                    joueurActuel.AjouterCarte(piochee);
                    Console.WriteLine($"{joueurActuel} a pioché {piochee}.");
                }
              

                if (joueurActuel.Main.Count == 1)
                {
                    string message = $"{joueurActuel} n'a plus qu'une carte !";
                    tableDeJeu.NotifierObservateurs(message, joueurActuel);  // Ne pas notifier le joueur concerné
                                                                             //tableDeJeu.AppliquerStrategieMinimisation(joueurs);
                                                                            
                  

                    // Appel de la méthode avec tous les paramètres nécessaires
                    tableDeJeu.AppliquerStrategieMinimisation(joueurs, joueurActuel, derniereCarte);  // Passer la dernière carte
                
            }


                if (joueurActuel.Main.Count == 0)
                {
                    Console.WriteLine("\n--- Résultats de la Partie ---");
                    Console.WriteLine($"\nLe gagnant est {joueurActuel} !");
                    await Task.Delay(1000);  // Attendre une seconde avant de signaler la fin de la partie
                    Console.WriteLine("\nFIN DE LA PARTIE!!!");
                    break;
                }


                // Attendre 2 secondes avant de passer au tour suivant
                await Task.Delay(1000);

                // Passer au joueur suivant de manière cyclique (tour à tour)
                joueurActuelIndex = (joueurActuelIndex + 1) % nombreDeJoueurs;
            }
        }


        private void AppliquerReglesSpeciales(Carte carte)
        {
            switch (carte.Valeur)
            {
                case Valeur.Valet:
                    Console.WriteLine("Le Valet permet de changer la couleur.");
                    // Logique pour changer la couleur (choix automatique)
                    Random random = new Random();
                    Couleur nouvelleCouleur = (Couleur)random.Next(Enum.GetValues(typeof(Couleur)).Length);
                    Console.WriteLine($"Le joueur change la couleur en {nouvelleCouleur}.");
                    // Vous pouvez stocker la nouvelle couleur si nécessaire
                    break;

                case Valeur.As:
                    Console.WriteLine("L'As fait sauter le tour du joueur suivant.");
                    // Logique pour sauter le tour du joueur suivant
                    // Incrémenter l'index du joueur suivant
                    currentPlayerIndex = (currentPlayerIndex + direction + joueurs.Count) % joueurs.Count;
                    break;

                case Valeur.Dix:
                    Console.WriteLine("Le 10 change la direction du jeu.");
                    // Logique pour changer la direction du jeu
                    direction *= -1;
                    Console.WriteLine($"La direction du jeu a maintenant {(direction == 1 ? "horaire" : "changée")}.");
                    break;

                case Valeur.Sept:
                    Console.WriteLine("Le 7 oblige le joueur suivant à piocher 2 cartes.");
                    // Logique pour faire piocher 2 cartes au joueur suivant
                    int joueurSuivantIndex = (currentPlayerIndex + direction + joueurs.Count) % joueurs.Count;
                    Joueur joueurSuivant = joueurs[joueurSuivantIndex];

                    // Vérifier si le joueur suivant a un autre Sept pour contrer
                    Carte sept = joueurSuivant.Main.FirstOrDefault(c => c.Valeur == Valeur.Sept);
                    if (sept.Valeur != default)
                    {
                        joueurSuivant.RetirerCarte(sept);
                        tableDeJeu.AjouterCarteADepot(sept);
                        Console.WriteLine($"{joueurSuivant} a contré l'attaque en jouant un Sept.");
                    }
                    else
                    {
                        try
                        {
                            joueurSuivant.AjouterCarte(tableDeJeu.PileDePioche.PiocherCarte());
                            joueurSuivant.AjouterCarte(tableDeJeu.PileDePioche.PiocherCarte());
                            Console.WriteLine($"{joueurSuivant} a pioché 2 cartes.");
                        }
                        catch (InvalidOperationException)
                        {
                            Console.WriteLine("La pile de pioche est vide. Impossible de piocher des cartes.");
                        }
                    }

                    break;

                default:
                    // Aucune règle spéciale
                    break;
            }
        }

       
    
    }
}
