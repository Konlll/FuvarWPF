using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Dolgozat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Fuvar> fuvars = new List<Fuvar>();
        public MainWindow()
        {
            InitializeComponent();

            // File beolvasása és annak megfelelő adatszerkezetben eltárolása
            StreamReader sr = new StreamReader("fuvar.csv");

            sr.ReadLine();

            while(!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split(";");
                Fuvar fuvar = new(int.Parse(line[0]), DateTime.Parse(line[1]), int.Parse(line[2]), double.Parse(line[3]), double.Parse(line[4]), double.Parse(line[5]), line[6]);
                
                // Hozzáadjuk a taxiazonosítókat a megfelelő ComboBox-hoz, ami a 4-es feladatnál fog kelleni.
                if (!cmbIds.Items.Contains(line[0]))
                {
                    cmbIds.Items.Add(line[0]);
                }

                fuvars.Add(fuvar);
            }
            cmbIds.SelectedIndex = 0;
            sr.Close();

        }

        private void btnTask3_Click(object sender, RoutedEventArgs e)
        {
            // Megszámolja a fuvarok darabszámát
            MessageBox.Show($"3. feladat: {fuvars.Count()} fuvar");
        }

        private void btnTask4_Click(object sender, RoutedEventArgs e)
        {
            // Kiválasztja a megfelelő taxi Id-t
            int selectedDriver = int.Parse(cmbIds.Text);
            List<Fuvar> fileteredFuvars = fuvars.Where(x => x.Id == selectedDriver).ToList();

            // Megszámolja, hogy mennyi fuvar tartozott ehhez a taxi Id-hoz, és hogy mennyi bevétel származott ebből.
            int numberOfFuvars = fileteredFuvars.Count();
            double sumOfFuvars = fileteredFuvars.Sum(x => x.Price + x.Tip);

            MessageBox.Show($"4. feladat: {numberOfFuvars} fuvar alatt: {sumOfFuvars}$");
        }

        private void btnTask5_Click(object sender, RoutedEventArgs e)
        {
            // Csoportosítja a fizetési metódusokat, és ezeket megszámolja egy dictionary-ben.
            Dictionary<string, int> payMethodTypes = new Dictionary<string, int>();
            foreach(var fuvar in fuvars)
            {
                if (payMethodTypes.ContainsKey(fuvar.PayMethod))
                {
                    payMethodTypes[fuvar.PayMethod] += 1;
                }
                else
                {
                    payMethodTypes[fuvar.PayMethod] = 1;
                }
            }

            // Majd beleteszi őket a ListBox-ba
            foreach(var payMethod in payMethodTypes)
            {
                lbPayMethods.Items.Add($"{payMethod.Key}: {payMethod.Value} fuvar");
            }
        }

        private void btnTask6_Click(object sender, RoutedEventArgs e)
        {
            // Megszámolja mennyi kilométert mentek összesen a fuvarokkal.
            double allDistance = Math.Round(fuvars.Sum(x => x.Distance * 1.6), 2);
            MessageBox.Show($"6. feladat: {allDistance} km.");
        }

        private void btnTask7_Click(object sender, RoutedEventArgs e)
        {
            // Megnézi hogy mi a leghosszabb fuvar a megtett idő szerint majd szépen beteszi őket a megfelelő ListBox-ba
            Fuvar longestFuvar = fuvars.MaxBy(x => x.TravelTime);
            lbLongestFuvar.Items.Add($"Fuvar hossza: {longestFuvar.TravelTime} másodperc");
            lbLongestFuvar.Items.Add($"Taxi azonosító: {longestFuvar.Id}");
            lbLongestFuvar.Items.Add($"Megtett távolság: {longestFuvar.Distance} km");
            lbLongestFuvar.Items.Add($"Viteldíj: {longestFuvar.Price}$");
        }

        private void btnTask8_Click(object sender, RoutedEventArgs e)
        {
            // A feladatban megadott feltétel alapján kíszűri, hogy mik a hibás fuvarok, majd ezeket dátum szerint növekvő sorrendbe helyezi
            List<Fuvar> incorrectFuvars = fuvars.Where(x => x.Distance == 0 && x.TravelTime > 0 && x.Price > 0).OrderBy(x => x.StartDate).ToList();

            // Ezek után létrehozza a filet és először beleírja az első sort, majd utána ForEach metódussal meg az összes hibás fuvart ;-vel elválasztva
            StreamWriter sw = new StreamWriter("hibak.txt");

            sw.WriteLine("taxi_id;indulas;idotartam;tavolsag;viteldij;borravalo;fizetes_modja");

            incorrectFuvars.ForEach(x => sw.WriteLine($"{x.Id};{x.StartDate};{x.TravelTime};{x.Distance};{x.Price};{x.Tip};{x.PayMethod}"));

            sw.Close();

            MessageBox.Show("A hibak.txt file sikeresen létrehozva!");
        }
    }
}
