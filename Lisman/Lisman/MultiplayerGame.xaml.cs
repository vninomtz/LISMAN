using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lisman {
    /// <summary>
    /// Interaction logic for MultiplayerGame.xaml
    /// </summary>
    public partial class MultiplayerGame : Window {
        int idgame;
        int[,] gameMap = new int[24, 23];

        Image lismanPlayer = null;
        int X = 1;
        int Y = 1;

        String parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;

        Image pill0 = new Image();
        Image pill1 = new Image();
        Image pill2 = new Image();
        Image pill3 = new Image();
        Image pill4 = new Image();
        Image pill5 = new Image();
        Image pill6 = new Image();
        Image pill7 = new Image();
        Image pill8 = new Image();
        Image pill9 = new Image();
        Image pill10 = new Image();
        Image pill11 = new Image();

        DispatcherTimer runLeft = new DispatcherTimer();
        DispatcherTimer runUp = new DispatcherTimer();
        DispatcherTimer runRight = new DispatcherTimer();
        DispatcherTimer runDown = new DispatcherTimer();

        public MultiplayerGame(int idgame) {
            InitializeComponent();
            this.idgame = idgame;

            runLeft.Tick += new EventHandler(RunLeft);
            runLeft.Interval = new TimeSpan(0, 0, 0, 0, 300);
            //runLeft.Start();


            runUp.Tick += new EventHandler(RunUp);
            runUp.Interval = new TimeSpan(0, 0, 0, 0, 300);
            // runUp.Start();


            runRight.Tick += new EventHandler(RunRight);
            runRight.Interval = new TimeSpan(0, 0, 0, 0, 300);
            //runRight.Start();


            runDown.Tick += new EventHandler(RunDown);
            runDown.Interval = new TimeSpan(0, 0, 0, 0, 300);
            //runDown.Start();

            MatrizGame();
            DrawPills();
            NotifyColor();
            //PrintMap();
        }

        public void MatrizGame() {
            using (StreamReader sr = new StreamReader(parentDirectory + "/Resources/Map.txt")) {

                for (int i = 0; i <= 23; i++) {
                    for (int j = 0; j <= 22; j++) {
                        int caracter = sr.Read();

                        if (caracter != -1) {

                            if (caracter == 48) {
                                gameMap[i, j] = 0;
                            }

                            if (caracter == 49) {
                                gameMap[i, j] = 1;
                            }

                            if (caracter == 51) {
                                gameMap[i, j] = 3;
                            }

                            if (caracter == 52) {
                                gameMap[i, j] = 4;
                            }

                            if (caracter == 53) {
                                gameMap[i, j] = 5;
                            }

                            if (caracter == 54) {
                                gameMap[i, j] = 6;
                            }
                            if (caracter == 56) {
                                gameMap[i, j] = 8;
                            }
                        }

                    }
                    Console.WriteLine();
                }

            }

        }

        public void DrawPills() {
            BitmapImage bmp1 = new BitmapImage();
            bmp1.BeginInit();
            bmp1.UriSource = new Uri(parentDirectory + "/Resources/img/Pill.png");
            bmp1.EndInit();

            pill0.Source = bmp1;
            pill1.Source = bmp1;
            pill2.Source = bmp1;
            pill3.Source = bmp1;
            pill4.Source = bmp1;
            pill5.Source = bmp1;
            pill6.Source = bmp1;
            pill7.Source = bmp1;
            pill8.Source = bmp1;
            pill9.Source = bmp1;
            pill10.Source = bmp1;
            pill11.Source = bmp1;

            panel_5_2.Children.Add(pill0);
            panel_11_18.Children.Add(pill1);
            panel_11_5.Children.Add(pill2);
            panel_12_12.Children.Add(pill3);
            panel_13_12.Children.Add(pill4);
            panel_21_12.Children.Add(pill5);
            panel_7_7.Children.Add(pill6);
            panel_9_9.Children.Add(pill7);
            panel_15_22.Children.Add(pill8);
            panel_16_1.Children.Add(pill9);
            panel_4_10.Children.Add(pill10);
            panel_2_13.Children.Add(pill11);
        }

        public void NotifyColor() {
            lismanPlayer = LismanYellow;
        }

        public bool canMove(int onX, int onY) {
            //Console.WriteLine("Mover a: [{0},{1}]= {2}",onX,onY,gameMap[onX,onY]);
            bool can = true;
            if (gameMap[onX, onY] == 0) {
                can = false;
            }

            return can;

        }

        public void StopLisman() {
            runLeft.Stop();
            runUp.Stop();
            runRight.Stop();
            runDown.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            StopLisman();
            runRight.Start();
        }

        private void RunLeft(object sender, EventArgs e) {
            if (X == 0 && Y == 11) {
                X = 23;
                Y = 11;
                Grid.SetColumn(lismanPlayer, X);
                Grid.SetRow(lismanPlayer, Y);
                return;
            }
            if (canMove(X - 1, Y)) {
                if (X > 0) {
                    X -= 1;
                    Grid.SetColumn(lismanPlayer, X);
                    Grid.SetRow(lismanPlayer, Y);

                }
            } else {
                StopLisman();
            }


        }

        private void RunUp(object sender, EventArgs e) {

            if (canMove(X, Y - 1)) {
                Y -= 1;
                Grid.SetColumn(lismanPlayer, X);
                Grid.SetRow(lismanPlayer, Y);
            } else {
                StopLisman();
            }


        }

        private void RunRight(object sender, EventArgs e) {
            if (X == 23 && Y == 11) {
                X = 0;
                Y = 11;
                Grid.SetColumn(lismanPlayer, X);
                Grid.SetRow(lismanPlayer, Y);
                return;
            }
            if (canMove(X + 1, Y)) {
                X += 1;
                Grid.SetColumn(lismanPlayer, X);
                Grid.SetRow(lismanPlayer, Y);
            } else {
                StopLisman();
            }


        }

        private void RunDown(object sender, EventArgs e) {
            if (canMove(X, Y + 1)) {
                Y += 1;
                Grid.SetColumn(lismanPlayer, X);
                Grid.SetRow(lismanPlayer, Y);
            } else {
                StopLisman();
            }

        }

        private void Matriz_KeyDown(object sender, KeyEventArgs e) {
            if (e.Key == Key.Left) {
                runUp.Stop();
                runRight.Stop();
                runDown.Stop();
                runLeft.Start();
            }
            if (e.Key == Key.Up) {
                runLeft.Stop();
                //runUp.Stop();
                runRight.Stop();
                runDown.Stop();
                runUp.Start();
            }
            if (e.Key == Key.Right) {
                runLeft.Stop();
                runUp.Stop();
                // runRight.Stop();
                runDown.Stop();
                runRight.Start();
            }
            if (e.Key == Key.Down) {
                runLeft.Stop();
                runUp.Stop();
                runRight.Stop();
                //runDown.Stop();
                runDown.Start();
            }
        }

    }
}
