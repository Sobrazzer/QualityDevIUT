using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thomas_FOUQUEROLLE.Medias;

namespace Thomas_FOUQUEROLLE.Tools
{
    public class Emprunt
    {
        #region Privates Members
        private Media m_Media { get; set; }
        private string m_NomUtilisateur { get; set; }
        private DateTime m_DateEmprunt { get; set; }
        private DateTime m_DateEcheance { get; set; }
        private bool m_Retourne { get; set; }
        private DateTime m_DateRetour { get; set; }
        #endregion

        #region Public Méthodes : accesseurs
        public Media GetMedia()
        {
            return m_Media;
        }

        public string GetNomUtilisateur()
        {
            return m_NomUtilisateur;
        }

        public DateTime GetDateEmprunt()
        {
            return m_DateEmprunt;
        }

        public DateTime GetDateEcheance()
        {
            return m_DateEcheance;
        }

        public DateTime GetDateRetour()
        {
            return m_DateRetour;
        }

        public void SetDateRetour(DateTime p_DateDeRetour)
        {
            m_DateRetour = p_DateDeRetour;
        }

        public bool IsRetourned()
        {
            return m_Retourne;
        }

        public void SetRetour(bool isRetourne)
        {
            m_Retourne = isRetourne;
        }
        #endregion

        #region Constructeurs
        public Emprunt(Media p_media, string p_nomUtilisateur, DateTime p_dateEmprunt, DateTime p_dateEcheance)
        {
            m_Media = p_media;
            m_NomUtilisateur = p_nomUtilisateur;
            m_DateEmprunt = p_dateEmprunt;
            m_DateEcheance = p_dateEcheance;
        }

        public Emprunt()
        {
        }

        public void AfficherInfos()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
