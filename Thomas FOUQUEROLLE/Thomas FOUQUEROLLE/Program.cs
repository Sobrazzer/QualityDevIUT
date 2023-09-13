using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace TD1
{
    public class Media
    {
        public string Titre;
        public int NumRef;
        public int NbExemplaireDispo;

        public Media(string titre, int numeroReference, int nombreExemplaires)
        {
            Titre = titre;
            NumRef = numeroReference;
            NbExemplaireDispo = nombreExemplaires;
        }

        public virtual void AfficherInfos()
        {
            Console.WriteLine($"Titre: {Titre}");
            Console.WriteLine($"Numéro de référence: {NumRef}");
            Console.WriteLine($"Nombre d'exemplaires disponibles: {NbExemplaireDispo}");
        }

        public void AjouterMedia(int quantite)
        {
            if (quantite < 0)
            {
                Console.WriteLine("La quantité doit être positive.");
                return;
            }

            NbExemplaireDispo += quantite;
            Console.WriteLine($"{quantite} exemplaire(s) de \"{Titre}\" ont été ajoutés.");
        }

        public void RetirerMedia(int quantite)
        {
            if (quantite < 0)
            {
                Console.WriteLine("La quantité doit être positive.");
                return;
            }

            if (NbExemplaireDispo < quantite)
            {
                Console.WriteLine($"Impossible de retirer {quantite} exemplaires de de \"{Titre}\" car le stock est insuffisant.");
                return;
            }

            NbExemplaireDispo -= quantite;
            Console.WriteLine($"{quantite} exemplaire(s) de \"{Titre}\" ont été retirés.");
        }
    }

    public class Livre : Media
    {
        public string Auteur;

        public Livre(string titre, int numeroReference, int nombreExemplaires, string auteur) : base(titre, numeroReference, nombreExemplaires)
        {
            Auteur = auteur;
        }

        public override void AfficherInfos()
        {
            base.AfficherInfos();
            Console.WriteLine($"Auteur: {Auteur}");
        }

    }

    public class DVD : Media
    {
        public int Duree;

        public DVD(string titre, int numeroReference, int nombreExemplaires, int duree) : base(titre, numeroReference, nombreExemplaires)
        {
            Duree = duree;
        }

        public override void AfficherInfos()
        {
            base.AfficherInfos();
            Console.WriteLine($"Duree: {Duree}");
        }
    }

    public class CD : Media
    {
        public string Artiste;

        public CD(string titre, int numeroReference, int nombreExemplaires, string artiste) : base(titre, numeroReference, nombreExemplaires)
        {
            Artiste = artiste;
        }

        public override void AfficherInfos()
        {
            base.AfficherInfos();
            Console.WriteLine($"Artiste: {Artiste}");
        }
    }

    public class Tools
    {
        public static Media AjouterMedia(Media media, int quantite)
        {
            media.AjouterMedia(quantite);
            return media;
        }

        public static Media RetirerMedia(Media media, int quantite)
        {
            media.RetirerMedia(quantite);
            return media;
        }
    }

    public class Library
    {
        private List<Media> medias = new List<Media>();

        public Media this[int numRef]
        {
            get
            {
                return medias.Find(media => media.NumRef == numRef);
            }
        }

        public void AjouterMedia(Media media)
        {
            medias.Add(media);
        }

        public void RetirerMedia(Media media)
        {
            medias.Remove(media);
        }

        public void EmprunterMedia(Media media)
        {
            if (media.NbExemplaireDispo > 0)
            {
                media.NbExemplaireDispo--;
                Console.WriteLine($"Emprunt de \"{media.Titre}\" effectué.");
            }
            else
            {
                Console.WriteLine($"Aucun exemplaire disponible de \"{media.Titre}\".");
            }
        }

        public void RetournerMedia(Media media)
        {
            media.NbExemplaireDispo++;
            Console.WriteLine($"Retour de \"{media.Titre}\" effectué.");
        }

        public List<Media> RechercherMedia(string recherche)
        {
            return medias.FindAll(media =>
                media.Titre.Contains(recherche) ||
                (media is Livre livre && livre.Auteur.Contains(recherche)) ||
                (media is CD cd && cd.Artiste.Contains(recherche))
            );
        }

        public List<Media> MediasEmpruntesParUtilisateur()
        {
            return new List<Media>();
        }

        public void AfficherStatistiques()
        {
            int totalMedias = medias.Count;
            int totalEmpruntes = medias.Count(media => media.NbExemplaireDispo == 0);
            int totalDisponibles = totalMedias - totalEmpruntes;

            Console.WriteLine($"Nombre total de médias dans la bibliothèque: {totalMedias}");
            Console.WriteLine($"Nombre d'exemplaires empruntés: {totalEmpruntes}");
            Console.WriteLine($"Nombre d'exemplaires disponibles: {totalDisponibles}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Library bibliotheque = new Library();

            Livre livre_1 = new Livre("Deepside", 101, 25, "Sarah Epstein");
            DVD dvd_1 = new DVD("Batman", 102, 8, 148);
            CD cd_1 = new CD("Nevermind", 103, 1, "Nirvana");

            bibliotheque.AjouterMedia(livre_1);
            bibliotheque.AjouterMedia(dvd_1);
            bibliotheque.AjouterMedia(cd_1);

            Console.WriteLine("Informations sur le livre :");
            livre_1.AfficherInfos();

            Console.WriteLine("\nInformations sur le DVD :");
            dvd_1.AfficherInfos();

            Console.WriteLine("\nInformations sur le CD :");
            cd_1.AfficherInfos();

            Console.WriteLine("\n");

            bibliotheque.EmprunterMedia(livre_1);
            bibliotheque.EmprunterMedia(dvd_1);
            bibliotheque.EmprunterMedia(cd_1);

            Console.WriteLine("\n");

            Console.WriteLine("\nInformations sur le livre après l'emprunt :");
            livre_1.AfficherInfos();

            Console.WriteLine("\nInformations sur le DVD après l'emprunt :");
            dvd_1.AfficherInfos();

            Console.WriteLine("\nInformations sur le CD après l'emprunt :");
            cd_1.AfficherInfos();

            Console.WriteLine("\n");

            Console.WriteLine("Recherche de médias par titre ou artiste :");
            List<Media> resultatsRecherche = bibliotheque.RechercherMedia("Deepside");
            foreach (var media in resultatsRecherche)
            {
                media.AfficherInfos();
            }

            Console.WriteLine("\n");

            Console.WriteLine("Statistiques de la bibliothèque :");
            bibliotheque.AfficherStatistiques();

        }
    }
}