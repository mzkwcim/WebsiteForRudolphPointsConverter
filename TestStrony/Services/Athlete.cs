namespace TestStrony.Services
{
    internal class Athlete
    {
        private string _imie = "";
        public string Imie
        {
            get { return _imie; }
            set { _imie = value; }
        }

        private string _nazwisko = "";
        public string Nazwisko
        {
            get { return _nazwisko; }
            set { _nazwisko = value; }
        }

        public Athlete()
        {

        }

        public void AthleteInserter()
        {
            int counter = 0;
            do
            {
                if (counter > 0)
                {
                    Console.Clear();
                    Console.WriteLine("Please Enter Name and Surname of Athlete");
                }
                Console.WriteLine("Please Enter Athlete surname");
                this._nazwisko = Console.ReadLine() ?? "";
                Console.WriteLine("Please Enter Athlete name");
                this._imie = Console.ReadLine() ?? "";
                counter++;
            } while (this._nazwisko == "" || this._imie == "");
            Console.Clear();
        }
    }

}
