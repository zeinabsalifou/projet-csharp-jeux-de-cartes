// Programme principal
using Tp_Concept.classes;

public class Program
{
    public static async Task Main(string[] args)
    {
        JeuxDePêche jeu = new JeuxDePêche();
        jeu.AjouterJoueur(new Joueur("Dupont", "Jean", "J1"));
        jeu.AjouterJoueur(new Joueur("Martin", "Paul", "J2"));
        jeu.AjouterJoueur(new Joueur("Durand", "Pierre", "J3"));

        await jeu.DemarrerJeuAsync();
    }

}