using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

namespace ConsoleApplication1
{
    class Program
    {
        private static int nbr = 0;

        public void Act(string file)
        {
            string[] lines = System.IO.File.ReadAllLines(file);
            //string[] lines = System.IO.File.ReadAllLines(@"C:\Documents and Settings\OTHMAN\Bureau\edi_orders bricodepot 900 1 snx.txt");
            //String myString = "ORDERHDR v01.0     65330374                           50E3016016530001       9  3779000001450       9  3020409000050       9                                                                       3020400192000       9  20120913                                                    EUR                                                                                                                                                                                                      C ANINNN                                                                               '";
            foreach (string myString in lines)
            {
                if (myString != "")
                {
                    string motCle = myString.Substring(0, 8);
                    switch (motCle)
                    {
                        case "ORDERHDR":
                            try
                            {
                                Console.WriteLine("#############################################################");
                                Console.WriteLine("#####################      ORDERHDR    ######################");
                                Console.WriteLine("#############################################################");
                                Console.WriteLine("");
                                Console.WriteLine("Longeur : " + myString.Length);
                                Console.WriteLine("mot-clé : " + myString.Substring(0, 9));
                                Console.WriteLine("version : " + myString.Substring(9, 5));
                                Console.WriteLine("Indicateur test : " + myString.Substring(14, 5));
                                Console.WriteLine("Numéro de commande : " + myString.Substring(19, 35));
                                Console.WriteLine("Type de commande : " + myString.Substring(54, 3));
                                Console.WriteLine("Code d'identification de l'Acheteur : " + myString.Substring(57, 20));
                                Console.WriteLine("Type du code précédent : " + myString.Substring(77, 3));
                                Console.WriteLine("Code d'identification du Fournisseur : " + myString.Substring(80, 20));
                                Console.WriteLine("Type du code précédent : " + myString.Substring(100, 3));
                                Console.WriteLine("Code d'identification du Destinataire de la Livraison : " + myString.Substring(103, 20));
                                Console.WriteLine("Type du code précédent : " + myString.Substring(123, 3));
                                Console.WriteLine("Code d'identification du Destinataire de la Facturation : " + myString.Substring(126, 20));
                                Console.WriteLine("Type du code précédent : " + myString.Substring(146, 3));
                                Console.WriteLine("Code d'identification du Lieu de Livraison : " + myString.Substring(149, 20));
                                Console.WriteLine("Type du code précédent : " + myString.Substring(169, 3));

                                Console.WriteLine("Code d'identification du Transporteur : " + myString.Substring(172, 20));
                                Console.WriteLine("Type du code précédent : " + myString.Substring(192, 3));
                                Console.WriteLine("Code d'identification de l'Agent du Destinataire : " + myString.Substring(195, 20));
                                Console.WriteLine("Type du code précédent : " + myString.Substring(215, 3));

                                Console.WriteLine("Date & Heure de la commande : " + myString.Substring(218, 15));
                                Console.WriteLine("Date & Heure de la livraison : " + myString.Substring(233, 15));
                                Console.WriteLine("Date & Heure limite de la livraison : " + myString.Substring(248, 15));
                                Console.WriteLine("Date & Heure de l’enlèvement : " + myString.Substring(263, 15));
                                Console.WriteLine("Code devise commande : " + myString.Substring(278, 3));
                                Console.WriteLine("Code devise facturation : " + myString.Substring(281, 3));
                                Console.WriteLine("Adresse de livraison 1 : " + myString.Substring(284, 35));
                                Console.WriteLine("Adresse de livraison 2 : " + myString.Substring(319, 35));
                                Console.WriteLine("Adresse de livraison 3 : " + myString.Substring(354, 35));
                                Console.WriteLine("Adresse de livraison 4 : " + myString.Substring(389, 35));

                                Console.WriteLine("Montant total HT commande : " + myString.Substring(425, 15));
                                Console.WriteLine("Code Type de transport : " + myString.Substring(439, 3));
                                Console.WriteLine("Mode de transport souhaité : " + myString.Substring(442, 5));
                                Console.WriteLine("Moyen de transport souhaité : " + myString.Substring(447, 8));
                                Console.WriteLine("Code paiement du transport : " + myString.Substring(455, 3));
                                Console.WriteLine("Filler : " + myString.Substring(458, 21));
                                Console.WriteLine("Zone de contrôle SERES : " + myString.Substring(479, 20));
                                Console.WriteLine("Code de la station EDI Emettrice  : " + myString.Substring(499, 14));
                                Console.WriteLine("Code de la station EDI Destinataire : " + myString.Substring(513, 14));
                                Console.WriteLine("Code du moyen de paiement : " + myString.Substring(527, 15));
                                Console.WriteLine("Fonction du Message : " + myString.Substring(542, 3));
                                Console.WriteLine("Type de réponse : " + myString.Substring(545, 3));
                                Console.WriteLine("Date & Heure limite avant annulation : " + myString.Substring(548, 15));
                                Console.WriteLine("Conditions de Livraison : " + myString.Substring(563, 3));
                                //Console.WriteLine("Code Gestion des reliquats : " + myString.Substring(568,	3));
                                //Console.WriteLine("Code Livraison Partielle Autorisée : " + myString.Substring(569,	3));
                                Console.WriteLine("Caractère de terminaison : " + myString.Substring(566, 1));
                                Console.WriteLine("");
                            }
                            catch (IOException e)
                            {
                                throw new Exceptions(e.Message, e);
                            }
                            break;
        
                        case "ORDERHD2":
                            try {
                            Console.WriteLine("#############################################################");
                            Console.WriteLine("#####################      ORDERHD2    ######################");
                            Console.WriteLine("#############################################################");
                            Console.WriteLine("");
                            Console.WriteLine("En-Tête de ligne : " + myString.Substring(0, 9));
                            Console.WriteLine("Enseigne : " + myString.Substring(9, 35));
                            Console.WriteLine("Nom de l'émetteur de la commande : " + myString.Substring(44, 35));
                            Console.WriteLine("Emetteur - Adresse 1 : " + myString.Substring(79, 35));
                            Console.WriteLine("Emetteur - Adresse 2 : " + myString.Substring(114, 35));
                            Console.WriteLine("Emetteur - Adresse 3 : " + myString.Substring(149, 35));
                            Console.WriteLine("Emetteur - Adresse 4 : " + myString.Substring(184, 35));
                            Console.WriteLine("Nom du fournisseur : " + myString.Substring(219, 35));
                            Console.WriteLine("Fournisseur - Adresse 1 : " + myString.Substring(254, 35));
                            Console.WriteLine("Fournisseur - Adresse 2 : " + myString.Substring(289, 35));
                            Console.WriteLine("Fournisseur - Adresse 3 : " + myString.Substring(324, 35));
                            Console.WriteLine("Fournisseur - Adresse 4 : " + myString.Substring(359, 35));
                            Console.WriteLine("Nom du destinataire de livraison : " + myString.Substring(394, 35));
                            Console.WriteLine("Destinataire Livraison - Adresse 1 : " + myString.Substring(429, 35));
                            Console.WriteLine("Destinataire Livraison - Adresse 2 : " + myString.Substring(464, 35));
                            Console.WriteLine("Destinataire Livraison - Adresse 3 : " + myString.Substring(499, 35));
                            Console.WriteLine("Destinataire Livraison - Adresse 4 : " + myString.Substring(534, 35));
                            Console.WriteLine("Nom du destinataire de facturation : " + myString.Substring(569, 35));


                            Console.WriteLine("Destinataire Facturation - Adresse 1 : " + myString.Substring(604, 35));
                            Console.WriteLine("Destinataire Facturation - Adresse 2 : " + myString.Substring(639, 35));
                            Console.WriteLine("Destinataire Facturation - Adresse 3 : " + myString.Substring(674, 35));
                            Console.WriteLine("Destinataire Facturation - Adresse 4 : " + myString.Substring(709, 35));
                            Console.WriteLine("Nom du lieu de livraison : " + myString.Substring(744, 35));
                            Console.WriteLine("Lieu de Livraison - Adresse 1 : " + myString.Substring(779, 35));
                            Console.WriteLine("Lieu de Livraison - Adresse 2 : " + myString.Substring(814, 35));
                            Console.WriteLine("Lieu de Livraison - Adresse 3 : " + myString.Substring(849, 35));
                            Console.WriteLine("Lieu de Livraison - Adresse 4 : " + myString.Substring(884, 35));
                            Console.WriteLine("Nom du transporteur : " + myString.Substring(919, 35));
                            Console.WriteLine("Transporteur - Adresse 1 : " + myString.Substring(954, 35));
                            Console.WriteLine("Transporteur - Adresse 2 : " + myString.Substring(989, 35));
                            Console.WriteLine("Transporteur - Adresse 3 : " + myString.Substring(1024, 35));
                            Console.WriteLine("Transporteur - Adresse 4 : " + myString.Substring(1059, 35));
                            Console.WriteLine("Nom de l'agent du destinataire : " + myString.Substring(1094, 35));
                            Console.WriteLine("Agent du destinataire - Adresse 1 : " + myString.Substring(1129, 35));
                            Console.WriteLine("Agent du destinataire - Adresse 2 : " + myString.Substring(1164, 35));
                            Console.WriteLine("Agent du destinataire - Adresse 3 : " + myString.Substring(1199, 35));
                            Console.WriteLine("Agent du destinataire - Adresse 4 : " + myString.Substring(1234, 35));
                            Console.WriteLine("Nom du contact chez l’acheteur : " + myString.Substring(1269, 35));
                            Console.WriteLine("Contact chez l’Acheteur - Téléphone : " + myString.Substring(1304, 20));
                            Console.WriteLine("Contact chez l’Acheteur - Fax : " + myString.Substring(1324, 20));
                            Console.WriteLine("Contact chez l’Acheteur - e-mail : " + myString.Substring(1344, 35));
                            Console.WriteLine("Emetteur du message : " + myString.Substring(1379, 20));
                            Console.WriteLine("Type du code précédent : " + myString.Substring(1399, 3));
                            Console.WriteLine("Emetteur du message - Adresse 1 : " + myString.Substring(1402, 35));
                            Console.WriteLine("Emetteur du message - Adresse 2 : " + myString.Substring(1437, 35));
                            Console.WriteLine("Emetteur du message - Adresse 3 : " + myString.Substring(1472, 35));
                            Console.WriteLine("Emetteur du message - Adresse 4 : " + myString.Substring(1507, 35));
                            Console.WriteLine("Emetteur du message - Adresse 5 : " + myString.Substring(1542, 35));
                            Console.WriteLine("Destinataire du message : " + myString.Substring(1577, 20));
                            Console.WriteLine("Type du code précédent : " + myString.Substring(1597, 3));
                            Console.WriteLine("Destinataire du message - Adresse 1 : " + myString.Substring(1600, 35));
                            Console.WriteLine("Destinataire du message - Adresse 2 : " + myString.Substring(1635, 35));
                            Console.WriteLine("Destinataire du message - Adresse 3 : " + myString.Substring(1670, 35));
                            Console.WriteLine("Destinataire du message - Adresse 4 : " + myString.Substring(1705, 35));
                            Console.WriteLine("Destinataire du message - Adresse 5 : " + myString.Substring(1740, 35));
                            Console.WriteLine("Siège Social de l'acheteur : " + myString.Substring(1775, 20));
                            Console.WriteLine("Type du code précédent : " + myString.Substring(1795, 3));
                            Console.WriteLine("Siège Social de l'acheteur - Adresse 1 : " + myString.Substring(1798, 35));
                            Console.WriteLine("Siège Social de l'acheteur - Adresse 2 : " + myString.Substring(1833, 35));
                            Console.WriteLine("Siège Social de l'acheteur - Adresse 3 : " + myString.Substring(1868, 35));
                            Console.WriteLine("Siège Social de l'acheteur - Adresse 4 : " + myString.Substring(1903, 35));
                            Console.WriteLine("Siège Social de l'acheteur - Adresse 5 : " + myString.Substring(1938, 35));
                            Console.WriteLine("Siège Social du fournisseur : " + myString.Substring(1973, 20));
                            Console.WriteLine("Type du code précédent : " + myString.Substring(1993, 3));
                            Console.WriteLine("Siège Social du fournisseur - Adresse 1 : " + myString.Substring(1996, 35));
                            Console.WriteLine("Siège Social du fournisseur - Adresse 2 : " + myString.Substring(2031, 35));
                            Console.WriteLine("Siège Social du fournisseur - Adresse 3 : " + myString.Substring(2066, 35));
                            Console.WriteLine("Siège Social du fournisseur - Adresse 4 : " + myString.Substring(2101, 35));
                            Console.WriteLine("Siège Social du fournisseur - Adresse 5 : " + myString.Substring(2136, 35));
                            Console.WriteLine("Caractère de terminaison : " + myString.Substring(2171, 1));
                            Console.WriteLine("");
                            }
                            catch (IOException e)
                            {
                                throw new Exceptions(e.Message, e);
                            }
                            break;

                        case "ORDERPRT":
                            Console.WriteLine("#############################################################");
                            Console.WriteLine("#####################      ORDERPRT    ######################");
                            Console.WriteLine("#############################################################");
                            Console.WriteLine("");
                            Console.WriteLine("En-Tête de ligne : " + myString.Substring(0, 9));
                            Console.WriteLine("Type de partenaire : " + myString.Substring(9, 3));
                            Console.WriteLine("Code d'identification du partenaire : " + myString.Substring(12, 35));
                            Console.WriteLine("Type du code ci-dessus : " + myString.Substring(47, 3));
                            Console.WriteLine("Agence responsable de la liste de codes : " + myString.Substring(50, 3));
                            Console.WriteLine("Raison Sociale du partenaire 1 : " + myString.Substring(53, 35));
                            Console.WriteLine("Raison Sociale du partenaire 2 : " + myString.Substring(88, 35));
                            Console.WriteLine("Raison Sociale du partenaire 3 : " + myString.Substring(123, 35));
                            Console.WriteLine("Raison Sociale du partenaire 4 : " + myString.Substring(158, 35));
                            Console.WriteLine("Raison Sociale du partenaire 5 : " + myString.Substring(193, 35));
                            Console.WriteLine("Numéro et nom de voie 1 : " + myString.Substring(228, 35));
                            Console.WriteLine("Numéro et nom de voie 2 : " + myString.Substring(263, 35));
                            Console.WriteLine("Numéro et nom de voie 3 : " + myString.Substring(298, 35));
                            Console.WriteLine("Numéro et nom de voie 4 : " + myString.Substring(333, 35));
                            Console.WriteLine("Ville : " + myString.Substring(368, 35));
                            Console.WriteLine("Région : " + myString.Substring(403, 9));
                            Console.WriteLine("Code Postal : " + myString.Substring(412, 9));
                            Console.WriteLine("Code ISO du pays : " + myString.Substring(421, 3));
                            Console.WriteLine("Type de contact : " + myString.Substring(424, 3));
                            Console.WriteLine("Service du contact : " + myString.Substring(427, 17));
                            Console.WriteLine("Nom du contact : " + myString.Substring(444, 35));
                            Console.WriteLine("Contact - Numéro de Téléphone (Fixe) : " + myString.Substring(479, 20));
                            Console.WriteLine("Contact - Numéro de Fax : " + myString.Substring(499, 20));
                            Console.WriteLine("Contact - E-mail : " + myString.Substring(519, 250));
                            Console.WriteLine("Contact - Numéro de Téléphone (Mobile) : " + myString.Substring(769, 20));
                            Console.WriteLine("Autre code pour le partenaire 1 : " + myString.Substring(789, 35));
                            Console.WriteLine("Autre code pour le partenaire 2 : " + myString.Substring(824, 35));
                            Console.WriteLine("Autre code pour le partenaire 3 : " + myString.Substring(859, 35));
                            Console.WriteLine("Caractère de terminaison : " + myString.Substring(894, 1));
                            Console.WriteLine("");
                            break;

                        case "ORDERLIN":
                            try {
                            Console.WriteLine("#############################################################");
                            Console.WriteLine("#####################      ORDERLIN : " + nbr + "     ######################");
                            Console.WriteLine("#############################################################");
                            Console.WriteLine("");
                            Console.WriteLine("Longeur : " + myString.Length);
                            Console.WriteLine("En-Tête de ligne : " + myString.Substring(0, 9));
                            Console.WriteLine("Numéro de ligne : " + myString.Substring(9, 5));
                            Console.WriteLine("Filler : " + myString.Substring(14, 5));
                            Console.WriteLine("Code EAN Article : " + myString.Substring(19, 35));
                            Console.WriteLine("Code Article Fournisseur : " + myString.Substring(54, 35));
                            Console.WriteLine("Numéro de Lot : " + myString.Substring(89, 35));
                            Console.WriteLine("Numéro de Série : " + myString.Substring(124, 35));
                            Console.WriteLine("Variante Promotionnelle : " + myString.Substring(159, 35));
                            Console.WriteLine("Description de l'article : " + myString.Substring(194, 35));
                            Console.WriteLine("Quantité commandée : " + myString.Substring(229, 15));
                            Console.WriteLine("Unité de mesure de la quantité : " + myString.Substring(244, 3));
                            Console.WriteLine("PCB (Unité de commande) : " + myString.Substring(247, 15));
                            Console.WriteLine("Unité de mesure du PCB : " + myString.Substring(262, 3));

                            Console.WriteLine("Prix unitaire net HT : " + myString.Substring(265, 15));
                            Console.WriteLine("Assiette du Prix unitaire HT : " + myString.Substring(280, 15));
                            Console.WriteLine("Unité de mesure de l'assiette : " + myString.Substring(295, 3));
                            Console.WriteLine("Prix unitaire promo net HT : " + myString.Substring(298, 15));
                            Console.WriteLine("Assiette du Prix unitaire promo net HT : " + myString.Substring(313, 15));
                            Console.WriteLine("Unité de mesure de l'assiette : " + myString.Substring(328, 3));
                            Console.WriteLine("Prix total ligne HT : " + myString.Substring(331, 15));
                            Console.WriteLine("Prix de vente au détail (pour étiquetage) : " + myString.Substring(346, 15));
                            Console.WriteLine("Nombre d’Unités Logistiques : " + myString.Substring(361, 15));
                            Console.WriteLine("Type d’Unité Logistique : " + myString.Substring(376, 7));
                            Console.WriteLine("Dimension 1 : " + myString.Substring(383, 15));
                            Console.WriteLine("Dimension2 : " + myString.Substring(398, 15));
                            Console.WriteLine("Dimension 3 : " + myString.Substring(413, 15));
                            Console.WriteLine("Unité des dimensions : " + myString.Substring(428, 3));

                            Console.WriteLine("Code d'identification du Client final : " + myString.Substring(431, 20));
                            Console.WriteLine("Type du code précédent : " + myString.Substring(451, 3));
                            Console.WriteLine("Client final - Adresse 1 : " + myString.Substring(454, 35));
                            Console.WriteLine("Client final - Adresse 2 : " + myString.Substring(489, 35));
                            Console.WriteLine("Client final - Adresse 3 : " + myString.Substring(524, 35));
                            Console.WriteLine("Client final - Adresse 4 : " + myString.Substring(559, 35));
                            Console.WriteLine("A utiliser de préférence avant (DLUO) : " + myString.Substring(594, 15));
                            Console.WriteLine("Numéro d’opération promotionnelle : " + myString.Substring(609, 35));
                            Console.WriteLine("Code Produit de l’unité Logistique : " + myString.Substring(644, 20));
                            Console.WriteLine("Numéro de commande magasin : " + myString.Substring(664, 35));
                            Console.WriteLine("Date de livraison : " + myString.Substring(699, 15));
                            Console.WriteLine("Code Article Client : " + myString.Substring(714, 35));
                            Console.WriteLine("Prix unitaire brut HT : " + myString.Substring(749, 15));

                            Console.WriteLine("Assiette du Prix unitaire brut HT : " + myString.Substring(764, 15));
                            Console.WriteLine("Unité de mesure de l'assiette : " + myString.Substring(779, 3));
                            Console.WriteLine("Code Produit contructeur ( numéro d'OE ) : " + myString.Substring(782, 35));
                            Console.WriteLine("Code Produit fabriquant : " + myString.Substring(817, 35));
                            Console.WriteLine("Date d'annulation en cas de non livraison : " + myString.Substring(852, 15));
                            Console.WriteLine("Date de livraison au plus tard : " + myString.Substring(867, 15));
                            Console.WriteLine("Date de livraison au plus tot : " + myString.Substring(882, 15));
                            Console.WriteLine("Date de livraison promise : " + myString.Substring(897, 15));
                            Console.WriteLine("Numéro de Contrat : " + myString.Substring(912, 35));
                            Console.WriteLine("Date & Heure de l'acte ci-dessus : " + myString.Substring(947, 15));
                            Console.WriteLine("Quantité commandée gratuite : " + myString.Substring(962, 15));
                            Console.WriteLine("Quantité commandée en Unité de Vente : " + myString.Substring(977, 15));
                            Console.WriteLine("UDV (Unité de Vente) : " + myString.Substring(992, 3));
                            Console.WriteLine("Nombre d'UDV par unité d'achat : " + myString.Substring(995, 15));
                            Console.WriteLine("Numéro de ligne dans la commande interne : " + myString.Substring(1010, 5));
                            //Console.WriteLine("Description de l'article, compléments : " + myString.Substring(1015, 35));
                            Console.WriteLine("Caractère de terminaison : " + myString.Substring(1015, 1));
                            Console.WriteLine("");
                            nbr++;
                            }
                            catch (IOException e)
                            {
                                throw new Exceptions(e.Message, e);
                            }
                            break;

                        case "ORDEREND":
                            Console.WriteLine("#############################################################");
                            Console.WriteLine("#####################      ORDEREND    ######################");
                            Console.WriteLine("#############################################################");
                            Console.WriteLine("");
                            Console.WriteLine("En-Tête de ligne : " + myString.Substring(0, 9));
                            Console.WriteLine("Nombre de lignes produit : " + myString.Substring(9, 5));
                            Console.WriteLine("Filler : " + myString.Substring(14, 5));
                            Console.WriteLine("Numéro de commande : " + myString.Substring(19, 35));
                            Console.WriteLine("Montant Total net HT pour la commande : " + myString.Substring(54, 15));
                            Console.WriteLine("Montant Total HT pour la commande : " + myString.Substring(69, 15));
                            Console.WriteLine("Poids brut total en kg : " + myString.Substring(84, 15));
                            Console.WriteLine("Caractère de terminaison : " + myString.Substring(99, 1));
                            Console.WriteLine("");
                            Console.WriteLine("..................   Fin : ORDEREND  ........................");
                            Console.WriteLine("");
                            break;

                        default:
                            break;









                    }

                }
            }

        }
    }
}

