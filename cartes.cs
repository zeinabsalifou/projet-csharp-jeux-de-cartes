using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_Concept.classes
{
    // Enum Valeur
    public enum Valeur
    {
        As = 11,
        Deux = 2,
        Trois = 3,
        Quatre = 4,
        Cinq = 5,
        Six = 6,
        Sept = 7,
        Huit = 8,
        Neuf = 9,
        Dix = 10,
        Valet = 2,
        Dame = 2,
        Roi = 2
    }

    // Enum Couleur
    public enum Couleur
    {
        Trèfle,
        Carreau,
        Cœur,
        Pique
    }

    // Classe Carte (struct)
    public struct Carte
    {
        public Valeur Valeur { get; }
        public Couleur Couleur { get; }

        public Carte(Valeur valeur, Couleur couleur)
        {
            Valeur = valeur;
            Couleur = couleur;
        }

        public override string ToString()
        {
            return $"{Valeur} de {Couleur}";
        }
    }

}
