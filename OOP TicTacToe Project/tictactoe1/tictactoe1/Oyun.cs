using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


class Oyun
{
    public int oyunTuru()
    {
        int oyunTur;

        Console.WriteLine("Yeni Oyun :1, Kayitli Oyun :2 -> 1/2: ");
        oyunTur = Int32.Parse(Console.ReadLine());

        return oyunTur;
    }
    public int boyut(int oyunTur)
    {
        int boyut = 0;

        StreamReader sr = new StreamReader("../../kayit.txt");
        string satir = sr.ReadLine();

        if (oyunTur == 2)
        {
            boyut = Int32.Parse(satir);
            Console.WriteLine("kayitli oyun boyut: " + boyut);
        }

        if (oyunTur == 1)
        {
            Console.Write("Oyun tahtasi boyutunu gir: \n");
            boyut = Int32.Parse(Console.ReadLine());
        }
        sr.Close();

        return boyut;
    }
    public void oyunTahtasi(string[,] tahta)
    {
        int i, j;
        ///bos oyun tahtasi olusturulur
        for (i = 0; i < tahta.GetLength(0); i++)
        {
            for (j = 0; j < tahta.GetLength(0); j++)
            {
                tahta[i, j] = " ";
            }
        }
    }
    public void hamleEkle(string[,] tahta, int x, int y, Oyuncu player)
    {
        tahta[x, y] = player.harf;
    }
    public void OyunTahtasiniYazdir(string[,] tahta, Oyun game, Oyuncu player, Oyuncu player2)
    {
        Console.Clear();
        Boolean kazanan = game.kazanan(tahta, player);
        Boolean kazanan2 = game.kazanan(tahta, player2);
        Boolean berabere = game.beraberlik(tahta);

        if (kazanan == false && kazanan2 == false && berabere == true)
            Console.WriteLine("Oyun berabere Sonlandi!");


        ///oyuncu karakterlerini ekrana yazdir.
        Console.WriteLine();
        Console.Write("player 1-> isim: " + player.isim + "\tharf: " + player.harf);
        if (player.tur == true)
            Console.Write("\ttur: insan");
        else
            Console.Write("\ttur:\tbilgisayar");

        Console.WriteLine();
        Console.Write("player 2-> isim: " + player2.isim + " \tharf: " + player2.harf);
        if (player2.tur == true)
            Console.Write("\ttur: insan");
        else
            Console.Write("\ttur: bilgisayar");

        Console.WriteLine("\n");

        for (int i = 0; i < tahta.GetLength(0); i++)
        {
            Console.Write("\t{0}", i);
        }
        Console.WriteLine();
        for (int i = 0; i < tahta.GetLength(0); i++)
        {
            Console.Write("_________");
        }
        Console.WriteLine();
        for (int i = 0; i < tahta.GetLength(0); i++)
        {
            Console.Write("{0} |\t", i);
            for (int j = 0; j < tahta.GetLength(0); j++)
            {
                Console.Write("{0}\t", tahta[i, j]);
            }
            Console.WriteLine();
        }

    }
    public Boolean hamleyiYaz(string[,] tahta, Oyuncu player, Oyuncu player2, Oyun game)
    {
        Boolean esit = false;

        while (esit == false)
        {
            esit = true;

            string hamle = player.insanOyuncuHamlesiKontrol(tahta, game, player, player2);
            int satirHamle = Int32.Parse(hamle.Substring(0, 1));
            int sutunHamle = Int32.Parse(hamle.Substring(1, 1));

            esit = string.Equals(tahta[satirHamle, sutunHamle], " ");

            if (esit == true)
            {
                hamleEkle(tahta, satirHamle, sutunHamle, player);
                player.Xhamle = satirHamle;
                player.Yhamle = sutunHamle;
                break;

            }
            else if (esit == false)
                Console.WriteLine("Kare Dolu Hamle Yapilamaz! ");
        }

        return esit;
    }
    public Boolean hamleyiYazCPU(string[,] tahta, Oyuncu player)
    {
        Boolean esit = false;

        while (esit == false)
        {
            esit = true;

            string hamle = player.bilgisayarHamlesiUret(tahta);
            int satirHamle = Int32.Parse(hamle.Substring(0, 1));
            int sutunHamle = Int32.Parse(hamle.Substring(1, 1));

            Console.WriteLine("satirHamle: " + satirHamle + " , sutunHamle: " + sutunHamle);

            esit = string.Equals(tahta[satirHamle, sutunHamle], " ");

            if (esit == true)
            {
                hamleEkle(tahta, satirHamle, sutunHamle, player);
                player.Xhamle = satirHamle;
                player.Yhamle = sutunHamle;
                break;
            }

            else if (esit == false)
                Console.WriteLine("Kare Dolu Hamle Yapilamaz! ");
        }

        return esit;
    }
    public Boolean beraberlik(string[,] tahta)
    {
        int i, j;
        Boolean berabere = false;
        Boolean sonuc = false;
        int bosAlan = 0;

        for (i = 0; i < tahta.GetLength(0); i++)
        {
            for (j = 0; j < tahta.GetLength(0); j++)
            {
                berabere = false;
                berabere = string.Equals(tahta[i, j], " ");

                if (berabere == true)
                    bosAlan++;
            }
        }

        if (bosAlan == 0)
            sonuc = true;

        return sonuc;
    }
    public Boolean kazanan(string[,] tahta, Oyuncu player)
    {
        int dikey = 0, yatay = 0, capraz = 0;
        Boolean esitMi = false;
        Boolean kazanan = false;
        int i;

        ///dikey kontrol
        for (i = 0; i < tahta.GetLength(0); i++)
        {
            //esitMi = string.Equals(tahta[i, player.Yhamle], tahta[player.Xhamle, player.Yhamle]);
            esitMi = string.Equals(tahta[i, player.Yhamle], player.harf);

            if (esitMi == true)
                dikey++;

            if (dikey == tahta.GetLength(0))
            {
                Console.WriteLine("____________________________________");
                Console.WriteLine("Oyun sona erdi. Kazanan: " + player.isim + "({0})", player.harf);
                Console.WriteLine("____________________________________");
                kazanan = true;
            }
        }
        esitMi = false;
        ///yatay kontrol
        for (i = 0; i < tahta.GetLength(0); i++)
        {
            //esitMi= string.Equals(tahta[player.Xhamle,i], tahta[player.Xhamle, player.Yhamle]);
            esitMi = string.Equals(tahta[player.Xhamle, i], player.harf);

            if (esitMi == true)
                yatay++;

            if (yatay == tahta.GetLength(0))
            {
                Console.WriteLine("____________________________________");
                Console.WriteLine("Oyun sona erdi. Kazanan: " + player.isim + "({0})", player.harf);
                Console.WriteLine("____________________________________");
                kazanan = true;
            }
        }
        esitMi = false;
        ///capraz kontrol
        for (i = 0; i < tahta.GetLength(0); i++)
        {
            esitMi = string.Equals(tahta[i, i], player.harf);

            if (esitMi == true)
                capraz++;

            if (capraz == tahta.GetLength(0))
            {
                Console.WriteLine("____________________________________");
                Console.WriteLine("Oyun sona erdi. Kazanan: " + player.isim + "({0})", player.harf);
                Console.WriteLine("____________________________________");
                kazanan = true;
            }
        }
        esitMi = false;
        capraz = 0;
        ///sagdan sola capraz
        for (i = 0; i < tahta.GetLength(0); i++)
        {
            esitMi = string.Equals(tahta[i, (tahta.GetLength(0) - 1) - i], player.harf);

            if (esitMi == true)
                capraz++;

            if (capraz == tahta.GetLength(0))
            {
                Console.WriteLine("____________________________________");
                Console.WriteLine("Oyun sona erdi. Kazanan: " + player.isim + "({0})", player.harf);
                Console.WriteLine("____________________________________");
                kazanan = true;
            }
        }
        return kazanan;
    }
    public void cikis(string[,] tahta, Oyuncu player, Oyuncu player2)
    {
        int i, j;
        Console.WriteLine("Oyunu kaydetmek ister misiniz? E/H: ?");
        string kayit = Console.ReadLine().ToUpper();

        if (kayit == "E")
        {
            FileStream fs = new FileStream("../../kayit.txt", FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            sw.WriteLine(tahta.GetLength(0));
            ///oyuncu bilgileri aktariliyor
            sw.WriteLine(player.isim + " " + player.harf + " " + player.tur + " player1 ");
            sw.WriteLine(player2.isim + " " + player2.harf + " " + player2.tur + " player2 ");
            ///oyun tahtasi aktariliyor
            for (i = 0; i < tahta.GetLength(0); i++)
            {
                for (j = 0; j < tahta.GetLength(0); j++)
                {
                    if (tahta[i, j] == "X" || tahta[i, j] == "O")
                        sw.Write(tahta[i, j] + " ");

                    if (tahta[i, j] == " ")
                        sw.Write("B ");

                }
                sw.WriteLine();
            }
            sw.Flush();
            sw.Close();
            fs.Close();
            Environment.Exit(0);
        }
        else if (kayit == "H")
            Environment.Exit(0);
    }
    public void kayitliOyun(string[,] tahta, Oyun game, Oyuncu player, Oyuncu player2)
    {
        int boyut, i, j, indis;
        string[] kelimeler;

        StreamReader sr = new StreamReader("../../kayit.txt");
        string satir = sr.ReadLine();

        boyut = Int32.Parse(satir);

        kelimeler = sr.ReadToEnd().Split(' ');
        ///player1
        player.isim = string.Copy(kelimeler[0]);
        player.harf = string.Copy(kelimeler[1]);
        player.tur = string.Equals(kelimeler[2], "True");
        ///player2
        player2.isim = /*string.Copy(kelimeler[4]);*/"CPU";
        player2.harf = string.Copy(kelimeler[5]);
        player2.tur = string.Equals(kelimeler[6], "True");
        Boolean kontrol;

        indis = 8; ///okunacak bir sonraki kelime
        for (i = 0; i < boyut; i++)
        {
            for (j = 0; j < boyut; j++)
            {
                kontrol = false;
                if ((kontrol = kelimeler[indis].Contains("X")) == true)
                    tahta[i, j] = "X";

                kontrol = false;
                if ((kontrol = kelimeler[indis].Contains("O")) == true)
                    tahta[i, j] = "O";

                kontrol = false;
                if ((kontrol = kelimeler[indis].Contains("B")) == true)
                    tahta[i, j] = " ";

                indis++;
            }
        }
        game.OyunTahtasiniYazdir(tahta, game, player, player2);
        Console.WriteLine();
        sr.Close();
    }
    public void yeniOyun(string[,] tahta, Oyun game, Oyuncu player, Oyuncu player2)
    {
        game.oyunTahtasi(tahta); //oyun tahtasini olusturur.

        Console.WriteLine("X || O = ?");
        while (true)
        {
            player.harf = Console.ReadLine().ToUpper();
            if (player.harf.ToUpper().Equals("X") || player.harf.ToUpper().Equals("O")) break;
        }

        if (player.harf.Equals("X"))
            player2.harf = "O";

        else if (player.harf.Equals("O"))
            player2.harf = "X";

        Console.WriteLine("oyuncu ismi gir:");
        player.isim = Console.ReadLine();
        player2.isim = "CPU";

        player.tur = true;
        player2.tur = false;

        game.OyunTahtasiniYazdir(tahta, game, player, player2);
    }
}
