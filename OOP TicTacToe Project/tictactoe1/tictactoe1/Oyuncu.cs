using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Oyuncu
{
    public string harf;
    public string isim;
    public Boolean tur;
    public int Xhamle;
    public int Yhamle;
    //true insan //false CPU

    // yapici metodlar
    public Oyuncu()
    {
        this.harf = "X";
        this.tur = true;
    }
    public Oyuncu(Boolean insanMi)
    {
        if (insanMi == true)
        {
            this.tur = true;
            this.harf = "X";
        }
        else if (insanMi == false)
        {
            this.tur = false;
            this.harf = "O";
        }
    }
    public Oyuncu(Boolean insan, string karakter, string isim)
    {
        this.harf = karakter;
        this.isim = isim;

        if (insan == true)
            this.tur = true;

        else if (insan == false)
            this.tur = false;


    }
    public string karakteriAl()
    {
        string karakter = this.harf;
        return karakter;
    }
    public Boolean oyuncuTurunuAl()
    {
        Boolean karakterTuru = this.tur;
        return karakterTuru;
    }
    public string insanOyuncuHamlesiKontrol(string[,] tahta, Oyun game, Oyuncu player, Oyuncu player2)
    {
        string satirHamle, sutunHamle;
        string hamle;
        bool dokuz;

        Console.WriteLine("hamleni yap Satir: (9 Cikis Yapar)");
        satirHamle = Console.ReadLine();
        dokuz = string.Equals(satirHamle, "9");

        if (dokuz == true)
        {
            game.cikis(tahta, player, player2);
        }
        Console.WriteLine("hamleni yap Sutun: ");
        sutunHamle = Console.ReadLine();

        hamle = string.Concat(satirHamle, sutunHamle);

        return hamle;
    }
    public string bilgisayarHamlesiUret(string[,] tahta)
    {
        Random rastgele = new Random();
        int satir = rastgele.Next(tahta.GetLength(0));
        int sutun = rastgele.Next(tahta.GetLength(0));

        string satirHamle = satir.ToString();
        string sutunHamle = sutun.ToString();

        string rasthamle = string.Concat(satirHamle, sutunHamle);

        return rasthamle;
    }
}