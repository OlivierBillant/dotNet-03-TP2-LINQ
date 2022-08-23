// See https://aka.ms/new-console-template for more information
using DotNet._03.TP2.Linq.BO;
using System.Text.RegularExpressions;

List<Auteur> ListeAuteurs = new List<Auteur>();
List<Livre> ListeLivres = new List<Livre>();

void InitialiserDatas()
{
    ListeAuteurs.Add(new Auteur("GROUSSARD", "Thierry"));
    ListeAuteurs.Add(new Auteur("GABILLAUD", "Jérôme"));
    ListeAuteurs.Add(new Auteur("HUGON", "Jérôme"));
    ListeAuteurs.Add(new Auteur("ALESSANDRI", "Olivier"));
    ListeAuteurs.Add(new Auteur("de QUAJOUX", "Benoit"));
    ListeLivres.Add(new Livre(1, "C# 4", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 533));
    ListeLivres.Add(new Livre(2, "VB.NET", "Les fondamentaux du langage", ListeAuteurs.ElementAt(0), 539));
    ListeLivres.Add(new Livre(3, "SQL Server 2008", "SQL, Transact SQL", ListeAuteurs.ElementAt(1), 311));
    ListeLivres.Add(new Livre(4, "ASP.NET 4.0 et C#", "Sous visual studio 2010", ListeAuteurs.ElementAt(3), 544));
    ListeLivres.Add(new Livre(5, "C# 4", "Développez des applications windows avec visual studio 2010", ListeAuteurs.ElementAt(2), 452));
    ListeLivres.Add(new Livre(6, "Java 7", "les fondamentaux du langage", ListeAuteurs.ElementAt(0), 416));
    ListeLivres.Add(new Livre(7, "SQL et Algèbre relationnelle", "Notions de base", ListeAuteurs.ElementAt(1), 216));
    ListeAuteurs.ElementAt(0).AddFacture(new Facture(3500, ListeAuteurs.ElementAt(0)));
    ListeAuteurs.ElementAt(0).AddFacture(new Facture(3200, ListeAuteurs.ElementAt(0)));
    ListeAuteurs.ElementAt(1).AddFacture(new Facture(4000, ListeAuteurs.ElementAt(1)));
    ListeAuteurs.ElementAt(2).AddFacture(new Facture(4200, ListeAuteurs.ElementAt(2)));
    ListeAuteurs.ElementAt(3).AddFacture(new Facture(3700, ListeAuteurs.ElementAt(3)));
}

InitialiserDatas();

foreach (var auteur in ListeAuteurs)
{
    Console.WriteLine(auteur);
}
Console.WriteLine();


// o	Afficher la liste des prénoms des auteurs dont le nom commence par "G"
var ListeAuteursG = ListeAuteurs.Where(auteur => auteur.Nom[0] == 'G');
Console.WriteLine("Liste des auteurs dont le nom commence par un G");
foreach (var auteur in ListeAuteursG)
{
    Console.WriteLine(auteur.Prenom);
}
Console.WriteLine();

// o	Afficher l’auteur ayant écrit le plus de livres
Console.WriteLine("Liste des livres groupés par Auteur");
var ListeLivresGroupedByAuteur = ListeLivres.GroupBy(livre => livre.Auteur);

var AuteurProlifique = ListeLivres.GroupBy(livre => livre.Auteur).OrderByDescending(livres => livres.Count()).FirstOrDefault().FirstOrDefault().Auteur;
Console.WriteLine($"L'auteur le plus prolifique est {AuteurProlifique.Prenom} {AuteurProlifique.Nom} \n");

// o	Afficher le nombre moyen de pages par livre par auteur
Console.WriteLine("Nombre moyen de pages par Auteur");
foreach (var LivresParAuteur in ListeLivresGroupedByAuteur)
{
    var NbPages = 0;
    var NbLivres = 0;
    foreach (var Livres in LivresParAuteur)
    {
        NbPages += Livres.NbPages;
        NbLivres++;
    }
    Console.WriteLine($"Le nombre de pages moyen de {LivresParAuteur.Key.Prenom} {LivresParAuteur.Key.Nom} est de {NbPages / NbLivres}");
}
Console.WriteLine();

// o	Afficher le titre du livre avec le plus de pages
Console.WriteLine($"Le livre le plus long est : {ListeLivres.OrderByDescending(livre => livre.NbPages).FirstOrDefault().Titre} \n");
    //On pourrait également utiliser la méthode MaxBy(livre => livre.NbPage)


// o	Afficher combien ont gagné les auteurs en moyenne (moyenne des factures)
Console.WriteLine("Montant gagné par les auteurs en moyenne :");
foreach (var auteur in ListeAuteurs)
{
    decimal totalFacture = 0;
    foreach (var facture in auteur.Factures)
    {
        totalFacture += facture.Montant;
    }
    if (auteur.Factures.Count() > 0)
    {
        Console.WriteLine($"{auteur.Prenom} {auteur.Nom} : {totalFacture / (auteur.Factures.Count())} euros");
    }
    else
    {

        Console.WriteLine($"{auteur.Prenom} {auteur.Nom} : 0 euros");
    }
}
Console.WriteLine();

// Alternative : moyenne globale des revenus
decimal totalGlobalFacture = 0;
foreach (var auteur in ListeAuteurs)
{
    foreach (var facture in auteur.Factures)
    {
        totalGlobalFacture += facture.Montant;
    }
}
Console.WriteLine($"Le montant moyen gagné par les auteurs est de : {totalGlobalFacture/ListeAuteurs.Count()} euros \n");

// o	Afficher les auteurs et la liste de leurs livres
Console.WriteLine("Liste des auteurs et de leurs livres");

var AuteursProductif = ListeLivres.Select(livre => livre.Auteur);

var AuteurNonProductif = ListeAuteurs.Where(p => !AuteursProductif.Any(p2 => p2.Nom == p.Nom));
foreach (var auteur in AuteurNonProductif)
{
    Console.WriteLine(auteur);
    Console.WriteLine("Cet auteur n'a écrit aucun livre");
}

foreach (var LivresParAuteur in ListeLivresGroupedByAuteur)
{
    Console.WriteLine(LivresParAuteur.Key);
    foreach (var livre in LivresParAuteur)
    {
        Console.WriteLine(livre);
    }
    Console.WriteLine($"Nombre de livre de cet auteur : {LivresParAuteur.Count()}");
}

Console.WriteLine();

// o	Afficher les titres de tous les livres triés par ordre alphabétique
Console.WriteLine("Liste des lives triés par ordre alphabétique");
var LivresOrderByAlphabet = ListeLivres.OrderBy(livre => livre.Titre[0]);
foreach (var Livre in LivresOrderByAlphabet)
{
    Console.WriteLine(Livre);
}
Console.WriteLine();

// o	Afficher la liste des livres dont le nombre de pages est supérieur à la moyenne
Console.WriteLine("Listes des livres plus longs que la moyenne");
var NbPageTotal = 0;
foreach (var livre in ListeLivres)
{
    NbPageTotal += livre.NbPages;
}
var LivresInfPageMoy = ListeLivres.Where(livre => livre.NbPages > (NbPageTotal / ListeLivres.Count));
foreach (var livre in LivresInfPageMoy) { Console.WriteLine($"{livre}"); }
Console.WriteLine();

// o	Afficher l'auteur ayant écrit le moins de livres
foreach (var auteur in AuteurNonProductif)
{
    Console.WriteLine($"L'auteur le moins prolifique est {auteur.Prenom} {auteur.Nom} \n");
}

