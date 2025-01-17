using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_Concept.classes
{
    // PaireDeCartes (classe)
    public class PaireDeCartes
    {
        private List<Carte> cartes;

        public PaireDeCartes()
        {
            cartes = new List<Carte>();
            foreach (Valeur valeur in Enum.GetValues(typeof(Valeur)))
            {
                foreach (Couleur couleur in Enum.GetValues(typeof(Couleur)))
                {
                    cartes.Add(new Carte(valeur, couleur));
                }
            }
        }

        public List<Carte> DistribuerCartes(int nombre)
        {
            if (cartes.Count < nombre)
            {
                throw new InvalidOperationException("Pas assez de cartes pour distribuer.");
            }

            List<Carte> cartesDistribuées = cartes.Take(nombre).ToList();
            cartes.RemoveRange(0, nombre);
            return cartesDistribuées;
        }

        public Carte PiocherCarte()
        {
            if (cartes.Count == 0)
            {
                throw new InvalidOperationException("La pile de pioche est vide.");
            }

            var carte = cartes.First();
            cartes.RemoveAt(0);
            return carte;
        }

        public void Melanger()
        {
            cartes = cartes.OrderBy(x => Guid.NewGuid()).ToList();
        }

        public bool EstVide()
        {
            return cartes.Count == 0;
        }

        public void AjouterCarte(Carte carte)
        {
            cartes.Add(carte);
        }
    }

}
