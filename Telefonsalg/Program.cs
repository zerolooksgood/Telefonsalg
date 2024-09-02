using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telefonsalg
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            DirectoryInfo directoryInfo = new DirectoryInfo(currentPath);
            DirectoryInfo trueParentDirectory = directoryInfo.Parent.Parent; //Henter true parent directory (folderen som program.cs ligger i)

            StreamReader file = new StreamReader(trueParentDirectory.FullName + "\\salgstall.txt"); //definerer filen vi skal lese fra som file ved å ta parent directory + navnet på filen vi skal lese

            //Oppg 5
            Dictionary<string, int> avlest = innlesning(file);
            Console.WriteLine($"Innleste selgere: {avlest.Count}");
            Console.WriteLine($"Totale salg: {totalSalg(avlest)}");
            Console.WriteLine($"Gjennomsnittlig salg per selger: {avgSalg(avlest)}");
            topAnsatt(avlest);

            //Jeg gjorde ikke oppgave 6 fordi jeg teller Main() som hovedmetoden

            Console.ReadLine();
        }

        //Oppg 1
        static Dictionary<string, int> innlesning(StreamReader file)
        {
            Dictionary<string, int> read = new Dictionary<string, int>();
            string line = file.ReadLine();
            string[] temp = new string[2]; //bruker en array med 2 i lengde slik at jeg ikke trenger å hente fra filen en gang per variabel
            while (line != null) //Fortsetter å kjøre helt til linjen er tom
            {
                temp = line.Split(' ');
                read.Add(temp[0], Convert.ToInt32(temp[1]));
                line = file.ReadLine();
            }
            return read; //returnerer ordboken med alle de avleste verdiene
        }

        //Oppg 2
        static void topAnsatt(Dictionary<string, int> list)
        {
            KeyValuePair<String, int> topAnsatt = new KeyValuePair<string, int>("Placeholder", 0); //Lager en mildertidig variabel som skal oppdateres når vi finner en selger som har solgt mer en den nåværende beste
            foreach (KeyValuePair<string, int> i in list)
            {
                if (i.Value > topAnsatt.Value) //Sjekekr om ansatt i har solgt mer en topAnsatt
                {
                    topAnsatt = i;
                }
            }
            Console.WriteLine($"Månedens ansatt er {topAnsatt.Key} med {topAnsatt.Value} salg");
        }

        //Oppg 3
        static int totalSalg(Dictionary<string, int> list)
        {
            int temp = 0; //Definerer en midlertidig variabel som vil være totalen
            foreach (KeyValuePair<string, int> i in list)
            {
                temp += i.Value; //Legger til verdien av hvert par til totalen
            }
            return temp;
        }

        //Oppg 4
        static int avgSalg(Dictionary<string, int> list)
        {
            int total = totalSalg(list);
            return total / list.Count;
        }
    }
}
