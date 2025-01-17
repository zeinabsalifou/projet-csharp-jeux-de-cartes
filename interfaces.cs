using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp_Concept.classes
{
    // Interface IObservateur
    public interface IObservateur
    {
        void MiseAJour(string message);
    }


    public interface ISujet
    {
        void AjouterObservateur(IObservateur observateur);
        void SupprimerObservateur(IObservateur observateur);
        void NotifierObservateurs(string message);
        void NotifierObservateurs(string message, Joueur joueurConcerné);  // Ajouter cette méthode
    }



}
