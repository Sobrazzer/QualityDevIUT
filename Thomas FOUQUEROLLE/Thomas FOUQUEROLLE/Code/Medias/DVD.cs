using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomas_FOUQUEROLLE.Medias
{
    public class DVD : Media
    {
        #region Privates Members
        private int m_Duree { get; set; }
        #endregion

        #region Constructeur
        public DVD(string p_titre, int p_numeroReference, int p_nombreExemplairesDisponibles, int p_duree)
            : base(p_titre, p_numeroReference, p_nombreExemplairesDisponibles)
        {
            m_Duree = p_duree;
        }
        #endregion

        #region Public Méthodes : accesseurs
        public int GetDuree()
        {
            return m_Duree;
        }
        #endregion

        #region Public Méthodes : Tools
        public override void AfficherInfos()
        {
            base.AfficherInfos();
            Console.WriteLine($"Durée : {m_Duree} minutes");
        }
        #endregion
    }
}
