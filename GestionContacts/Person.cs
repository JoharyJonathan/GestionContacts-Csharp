using System;

namespace GestionContacts
{
    class Person
    {
        private string nom;
        private string prenom;
        private string number;

        public string Nom
        {
            get { return nom; }
            set { nom = value; }
        }

        public string Prenom
        {
            get { return prenom; }
            set { prenom = value; }
        }

        public string Number
        {
            get { return number; }
            set { number = value; }
        }

        public Person(string nom, string prenom, string number)
        {
            this.nom = nom;
            this.prenom = prenom;
            this.number = number;
        }

        public Person() { }

        public void PersonalInfo()
        {
            Console.WriteLine("Je suis " + prenom + " " + nom + " et mon numero est " + number);
        }
    }
}
