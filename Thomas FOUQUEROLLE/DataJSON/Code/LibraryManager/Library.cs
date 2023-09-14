using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thomas_FOUQUEROLLE.Medias;
using Thomas_FOUQUEROLLE.Tools;

namespace Thomas_FOUQUEROLLE
{
    public class library
    {
        #region Private Members
        private List<Media> Medias { get; set; }
        private List<Emprunt> Emprunts { get; set; }

        private readonly DataManager DataManager = new DataManager();
        #endregion

        #region Constructeurs
        public library()
        {
            Medias = new List<Media>();
            Emprunts = new List<Emprunt>();
        }
        #endregion

        #region Public Méthodes : Indexeur
        // Indexeur pour accéder aux médias par leur numéro de référence
        public Media this[int p_numeroReference]
        {
            get
            {
                return (Medias.Count > 0) ? Medias.Find(media => media.GetNumeroReference() == p_numeroReference) : null;
            }
        }

        public Media this[string p_titre]
        {
            get
            {
                return (Medias.Count > 0) ? Medias.Find(media => media.GetTitre() == p_titre) : null;
            }
        }
        #endregion

        #region Public Methods
        // Méthode pour ajouter un média à la bibliothèque
        public void AjouterMedia(Media media)
        {
            Medias.Add(media);
            Console.WriteLine($"Ajout du média {media.GetTitre()} à la bibliothèque.");
        }

        // Méthode pour retirer un média de la bibliothèque
        public void RetirerMedia(Media media)
        {
            Medias.Remove(media);
            Console.WriteLine($"Retrait du média {media.GetTitre()} de la bibliothèque.");
        }

        // Méthode pour emprunter un média
        public void EmprunterMedia(Media media, string nomUtilisateur)
        {
            if (media.GetNExemplairesDispo() > 0)
            {
                media.TakeOffExemplaire();
                // Enregistrez les détails de l'emprunt
                Emprunt emprunt = new Emprunt(media, nomUtilisateur, DateTime.Now, DateTime.Now.AddDays(14));
                Emprunts.Add(emprunt);
            }
            else
            {
                throw new InvalidOperationException("Le média n'est pas disponible pour l'emprunt.");
            }
        }

        // Méthode pour retourner un média emprunté
        public void RetournerMedia(Media media)
        {
            media.AddExemplaire();
            // Mettez à jour les enregistrements d'emprunt pour marquer le média comme retourné.
            // Recherchez l'emprunt correspondant pour marquer le média comme retourné
            if (Emprunts != null && Emprunts.Count > 0)
            {
                Emprunt emprunt = Emprunts.Find(e => e.GetMedia() == media && !e.IsRetourned());
                if (emprunt != null)
                {
                    emprunt.SetRetour(true);
                    emprunt.SetDateRetour(DateTime.Now);
                }
            }
        }

        #region Affichage
        // Méthode pour afficher tous les médias dans la bibliothèque
        public void AfficherTousLesMedias()
        {
            Console.WriteLine("Médias dans la bibliothèque :");
            foreach (Media media in Medias)
            {
                media.AfficherInfos();
                Console.WriteLine();
            }
        }

        public void AfficherTousLesEmprunts()
        {
            Console.WriteLine("Médias dans la bibliothèque :");
            foreach (Emprunt emprunt in Emprunts)
            {
                emprunt.AfficherInfos();
                Console.WriteLine();
            }
        }

        // Méthode pour afficher les détails d'un média par son numéro de référence
        public void AfficherMediaParNumeroReference(int numeroReference)
        {
            Media media = this[numeroReference];
            if (media != null)
            {
                media.AfficherInfos();
            }
            else
            {
                Console.WriteLine("Aucun média trouvé avec ce numéro de référence.");
            }
        }

        // Méthode pour afficher les médias empruntés par un utilisateur
        public List<Media> MediasEmpruntesParUtilisateur(string nomUtilisateur)
        {
            List<Media> mediasEmpruntes = new List<Media>();

            foreach (Emprunt emprunt in Emprunts)
            {
                if (emprunt.GetNomUtilisateur() == nomUtilisateur && !emprunt.IsRetourned())
                {
                    mediasEmpruntes.Add(emprunt.GetMedia());
                }
            }

            return mediasEmpruntes;
        }

        // Méthode pour afficher les statistiques de la bibliothèque
        public void AfficherStatistiques()
        {
            int mediasDisponibles = 0;
            foreach (Media m in Medias)
            {
                mediasDisponibles += m.GetNExemplairesDispo();
            }

            Console.WriteLine($"Nombre de médias dans la bibliothèque : {Medias.Count}");
            Console.WriteLine($"Médias empruntés : {Emprunts.Count}");
            Console.WriteLine($"Médias disponibles actuellement : {mediasDisponibles}");
        }
        #endregion

        #region Recherche
        // Méthode pour rechercher un média par titre
        public List<Media> RechercherMediaParTitre(string titre)
        {
            return Medias.FindAll(media => media.GetTitre().Contains(titre, StringComparison.OrdinalIgnoreCase));
        }

        public List<Emprunt> RechercherEmpruntParTitre(string titre, string nomUtilisateur)
        {
            return Emprunts.FindAll(emprunt => emprunt.GetMedia().GetTitre().Contains(titre, StringComparison.OrdinalIgnoreCase) && emprunt.GetNomUtilisateur() == nomUtilisateur);
        }

        // Méthode pour rechercher un média par auteur
        public List<Media> RechercherMediaParAuteur(string auteur)
        {
            return Medias.FindAll(media => media is Livre && ((Livre)media).GetAuteur().Contains(auteur, StringComparison.OrdinalIgnoreCase));
        }
        #endregion                

        public void SaveLibrary()
        {
            DataManager.SauvegarderBibliothèque(Medias, Emprunts);
        }

        public void LoadLibrary()
        {
            List<Media> v_mediasTmp;
            List<Emprunt> v_empruntTmp;
            DataManager.ChargerBibliothèque(out v_mediasTmp, out v_empruntTmp);
            Medias = v_mediasTmp;
            Emprunts = v_empruntTmp;
        }
        #endregion

        #region Surcharge Opérateur
        public static library operator +(library library, Media media)
        {
            // Recherchez si le média existe déjà dans la bibliothèque par son numéro de référence
            Media existingMedia = library[media.GetNumeroReference()];

            if (existingMedia != null)
            {
                // Le média existe déjà, augmentez le nombre d'exemplaires disponibles
                existingMedia.AddExemplaire();
            }
            else
            {
                // Le média n'existe pas encore, ajoutez-le à la bibliothèque
                library.Medias.Add(media);
            }

            return library;
        }

        public static library operator -(library library, Media media)
        {
            // Recherchez si le média existe déjà dans la bibliothèque par son numéro de référence
            Media existingMedia = library[media.GetNumeroReference()];

            if (existingMedia != null)
            {
                if (existingMedia.GetNExemplairesDispo() > 0)
                {
                    // Décrémentez le nombre d'exemplaires disponibles
                    existingMedia.TakeOffExemplaire();

                    if (existingMedia.GetNExemplairesDispo() == 0)
                    {
                        // S'il ne reste plus d'exemplaires disponibles, retirez le média de la bibliothèque
                        library.Medias.Remove(existingMedia);
                    }
                }
                else
                {
                    // Lancez une exception si le média n'est pas disponible pour le retrait
                    throw new InvalidOperationException("Le média n'est pas disponible pour le retrait.");
                }
            }
            else
            {
                // Lancez une exception si le média n'est pas trouvé dans la bibliothèque
                throw new InvalidOperationException("Le média n'est pas trouvé dans la bibliothèque.");
            }

            return library;
        }
        #endregion
    }
}
