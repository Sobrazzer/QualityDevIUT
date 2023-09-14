using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thomas_FOUQUEROLLE.Medias
{
    public class CD : Media
    {
        #region Privates Members
        private string m_Artiste { get; set; }
        #endregion

        #region Constructeur
        public CD(string p_titre, int p_numeroReference, int p_nombreExemplairesDisponibles, string p_artiste)
            : base(p_titre, p_numeroReference, p_nombreExemplairesDisponibles)
        {
            m_Artiste = p_artiste;
        }
        #endregion

        #region Public Méthodes : accesseurs
        /// <summary>
        /// 
        /// </summary>
        /// <returns>string</returns>
        public string GetArtiste()
        {
            return m_Artiste;
        }
        #endregion

        #region Public Méthodes : Tools
        public override void AfficherInfos()
        {
            base.AfficherInfos();
            Console.WriteLine($"Artiste : {m_Artiste}");
        }
        #endregion
    }
}
