using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomas_FOUQUEROLLE.Medias
{
    public class Livre : Media
    {
        #region Privates Members
        private string m_Auteur { get; set; }
        #endregion

        #region Constructeur
        public Livre(string p_titre, int p_numeroReference, int p_nombreExemplairesDisponibles, string p_auteur)
            : base(p_titre, p_numeroReference, p_nombreExemplairesDisponibles)
        {
            m_Auteur = p_auteur;
        }
        #endregion

        #region Public Méthodes : accesseurs
        public string GetAuteur()
        {
            return m_Auteur;
        }
        #endregion

        #region Public Méthodes : Tools
        public override void AfficherInfos()
        {
            base.AfficherInfos();
            Console.WriteLine($"Auteur : {m_Auteur}");
        }
        #endregion
    }
}
