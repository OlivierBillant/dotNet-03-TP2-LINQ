namespace DotNet._03.TP2.Linq.BO
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class Livre
    {
        public Livre(int id, string titre, string synopsis, Auteur auteur, int nbPages)
        {
            Id = id;
            Titre = titre;
            Synopsis = synopsis;
            Auteur = auteur;
            NbPages = nbPages;
        }

        public int Id { get; set; }

        public string Titre { get; set; }

        public string Synopsis { get; set; }

        public Auteur Auteur { get; set; }

        public int NbPages { get; set; }
        public override string ToString()
        {
            return $"Livre - Id : {Id}, Titre : {Titre}, Auteur : {Auteur}";
        }
    }

}
