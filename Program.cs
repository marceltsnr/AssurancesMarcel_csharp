namespace AssurancesMarcel
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int agePersonne;               // âge de la personne
            int DureeObtentionPermis;      // depuis combien de temps il a son permis
            int nbrAccident;               // nombre d'accidents
            int nbrAnneesAnciennete;       // nombre d'années assurées dans cette société
            string message;                // résultat à afficher
            string repeatProg;             // info pour savoir si on recommence

            string saisie;                 // variable temporaire pour lire en string

            do // boucle de reprise du programme
            {
                Console.WriteLine("Bienvenue dans ce programme d'attribution de contrat d'assurance voiture.");
                Console.WriteLine("-------------------------------------------------------------------------");

                // ================================
                // LECTURE DE L'ÂGE (17 à 98 ans)
                // ================================
                do
                {
                    Console.Write("Quel âge avez-vous ? ");
                    saisie = Console.ReadLine(); // lecture en string

                } while (saisie == ""
                         || !int.TryParse(saisie, out agePersonne)
                         || agePersonne < 17
                         || agePersonne > 98);

                // ================================
                // LECTURE DES ANNÉES DE PERMIS
                // entre 1 et une valeur plausible selon l'âge
                // max = âge - 16 (on ne peut pas avoir le permis avant 16 ans)
                // ================================
                int maxPermis = agePersonne - 16;
                if (maxPermis < 1)
                {
                    maxPermis = 1;
                }

                do
                {
                    Console.Write("Depuis combien de temps avez-vous le permis ? ");
                    saisie = Console.ReadLine();

                } while (saisie == ""
                         || !int.TryParse(saisie, out DureeObtentionPermis)
                         || DureeObtentionPermis < 1
                         || DureeObtentionPermis > maxPermis);

                // ================================
                // LECTURE DU NOMBRE D'ACCIDENTS
                // nbAccident >= 0
                // ================================
                do
                {
                    Console.Write("Combien d'accidents avez-vous déjà fait ? ");
                    saisie = Console.ReadLine();

                } while (saisie == ""
                         || !int.TryParse(saisie, out nbrAccident)
                         || nbrAccident < 0);

                // ================================
                // LECTURE DE L'ANCIENNETÉ DANS LA SOCIÉTÉ
                // ancienneté >= 0
                // ancienneté <= années de permis
                // ancienneté <= 25 (la société a 25 ans)
                // ================================
                int maxAnciennete = DureeObtentionPermis;
                if (maxAnciennete > 25)
                {
                    maxAnciennete = 25;
                }

                do
                {
                    Console.Write("Depuis combien d'années êtes-vous assuré chez nous ? ");
                    saisie = Console.ReadLine();

                } while (saisie == ""
                         || !int.TryParse(saisie, out nbrAnneesAnciennete)
                         || nbrAnneesAnciennete < 0
                         || nbrAnneesAnciennete > maxAnciennete);

                // ================================
                // TRAITEMENT : calcul du contrat
                // ================================
                CalculeContrat(agePersonne, DureeObtentionPermis, nbrAccident, nbrAnneesAnciennete, out message);

                // ================================
                // AFFICHAGE DU RÉSULTAT
                // ================================
                if (message == "Refus")
                {
                    message = "Désolé, nous ne pouvons pas vous assurer.";
                }
                else
                {
                    message = "Nous vous attribuons le contrat de type " + message;
                }

                Console.WriteLine();
                Console.WriteLine(message);
                Console.WriteLine();

                // ================================
                // DEMANDE POUR RECOMMENCER
                // ================================
                Console.WriteLine("Voulez-vous recommencer ? 'espace' = oui, autre = non ");
                repeatProg = Console.ReadLine();

                Console.Clear(); // optionnel : nettoie l'écran entre deux essais

            } while (repeatProg == " ");
        }

        /// <summary>
        /// Déterminer le tarif attribué à un candidat à l'assurance.
        /// </summary>
        /// <param name="age">âge de la personne</param>
        /// <param name="obtenuPermis">années depuis obtention du permis</param>
        /// <param name="nbAccident">nombre d'accidents</param>
        /// <param name="nbAnneeAnciennete">ancienneté dans la société</param>
        /// <param name="contrat">type de contrat</param>
        static void CalculeContrat(int age, int obtenuPermis, int nbAccident, int nbAnneeAnciennete, out string contrat)
        {
            if (age < 25 && obtenuPermis < 2)
            {
                if (nbAccident == 0)
                {
                    contrat = "Rouge";
                }
                else
                {
                    contrat = "Refus";
                }
            }
            else if ((age < 25 && obtenuPermis >= 2) || (age >= 25 && obtenuPermis < 2))
            {
                if (nbAccident == 0)
                {
                    contrat = "Orange";
                }
                else if (nbAccident == 1)
                {
                    contrat = "Rouge";
                }
                else
                {
                    contrat = "Refus";
                }
            }
            else
            {
                if (nbAccident == 0)
                {
                    contrat = "Vert";
                }
                else if (nbAccident == 1)
                {
                    contrat = "Orange";
                }
                else if (nbAccident == 2)
                {
                    contrat = "Rouge";
                }
                else
                {
                    contrat = "Refus";
                }
            }

            // BONUS ANCIENNETÉ : si plus de 5 ans, on améliore le contrat (mais pas si refus)
            if (nbAnneeAnciennete > 5 && contrat != "Refus")
            {
                if (contrat == "Rouge")
                {
                    contrat = "Orange";
                }
                else if (contrat == "Orange")
                {
                    contrat = "Vert";
                }
                else if (contrat == "Vert")
                {
                    contrat = "Bleu";
                }
            }
        }
    }
}
