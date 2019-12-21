using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///Selda Yapal - 140202121
///Nedret Gegeoglu - 150202114

namespace tictactoe1
{
    class Program
    {
        static void oyna(int oyunT, string[,] matris, Oyun GameBoard, Oyuncu player1, Oyuncu player2)
        {
            if (oyunT == 1)///yeni oyun acar
                GameBoard.yeniOyun(matris, GameBoard, player1, player2);

            if (oyunT == 2)///kayitli oyun acar
                GameBoard.kayitliOyun(matris, GameBoard, player1, player2);

            var kazanan = string.Empty;
            var siradakiOyuncu = player1;
            Boolean hamleKontrol = true;
            Boolean hamleKontrolCPU = true;
            Boolean beraberlik = false;
            Boolean winner = false;
            Boolean gameOver = false;

            ///Oyun bitene kadar doner
            while (gameOver == false)
            {
                hamleKontrol = true;
                hamleKontrolCPU = true;

                while (hamleKontrol == true)
                {
                    beraberlik = GameBoard.beraberlik(matris);

                    if (beraberlik == true)
                    {
                        gameOver = true;
                        break;
                    }
                    hamleKontrol = false;
                    hamleKontrolCPU = false;

                    ///kullanici hamlesi
                    if (siradakiOyuncu == player1)
                    {
                        hamleKontrol = GameBoard.hamleyiYaz(matris, player1, player2, GameBoard);
                        if (hamleKontrol == true)
                        {
                            Console.WriteLine("Hamleniz yazildi. ");
                            winner = GameBoard.kazanan(matris, player1);

                            if (winner == true)
                                gameOver = true;
                        }
                        break;
                    }
                    ///bilgisayar hamlesi
                    if (siradakiOyuncu == player2)
                    {
                        hamleKontrolCPU = GameBoard.hamleyiYazCPU(matris, player2);
                        if (hamleKontrolCPU == true)
                        {
                            Console.WriteLine("Hamleniz yazildi CPU. ");
                            winner = GameBoard.kazanan(matris, player2);

                            if (winner == true)
                                gameOver = true;
                        }
                        break;
                    }
                }
                ///siradaki oyuncuya gec
                if (siradakiOyuncu == player1)
                    siradakiOyuncu = player2;

                else if (siradakiOyuncu == player2)
                    siradakiOyuncu = player1;

                GameBoard.OyunTahtasiniYazdir(matris, GameBoard, player1, player2);
            }
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Oyun tahta1 = new Oyun();
                Oyuncu p1 = new Oyuncu(/*true,"X","Nedret"*/);
                Oyuncu p2 = new Oyuncu(/*false, "O", "CPU"*/);

                int oyunT = tahta1.oyunTuru();
                int boyut = tahta1.boyut(oyunT);
                string[,] oyunTahtasi = new string[boyut, boyut];

                Console.WriteLine("\n");
                oyna(oyunT, oyunTahtasi, tahta1, p1, p2);
            }
        }
    }
}