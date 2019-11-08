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
    public partial class MultiplayerGame : Window{
        int idgame;
        int[,] gameMap = new int[24,23];

        Image lismanPlayer = null;
        int X = 1;
        int Y = 1;
        DispatcherTimer runLeft = new DispatcherTimer();
        DispatcherTimer runUp = new DispatcherTimer();
        DispatcherTimer runRight = new DispatcherTimer();
        DispatcherTimer runDown = new DispatcherTimer();
        public MultiplayerGame(int idgame)
        {
            InitializeComponent();
            this.idgame = idgame;
            
            runLeft.Tick +=  new EventHandler(RunLeft);
            runLeft.Interval = new TimeSpan(0, 0, 0, 0, 300);
            //runLeft.Start();

            
            runUp.Tick += new EventHandler(RunUp);
            runUp.Interval = new TimeSpan(0, 0, 0, 0,300);
           // runUp.Start();

            
            runRight.Tick += new EventHandler(RunRight);
            runRight.Interval = new TimeSpan(0, 0, 0, 0, 300);
            //runRight.Start();

            
            runDown.Tick += new EventHandler(RunDown);
            runDown.Interval = new TimeSpan(0, 0, 0, 0, 300);
            //runDown.Start();

            MatrizGame();
            NotifyColor();
            //PrintMap();
        }

        public void MatrizGame() {

            String parentDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            using (StreamReader sr = new StreamReader(parentDirectory+ "/Resources/Map.txt")) {
                
            
              
           
                for (int i = 0; i <= 23; i++) {
                    for (int j = 0; j <= 22; j++) {
                        int caracter = sr.Read();
                        if (caracter != -1) {

                            if (caracter == 48) {
                                gameMap[i,j] = 0;
                            }

                            if (caracter == 49) {
                                gameMap[i,j] = 1;
                            }

                            if (caracter == 51) {
                                gameMap[i,j] = 3;
                            }

                            if (caracter == 52) {
                                gameMap[i,j] = 4;
                            }

                            if (caracter == 53) {
                                gameMap[i,j] = 5;
                            }

                            if (caracter == 54) {
                                gameMap[i,j] = 6;
                            }
                            if (caracter == 56) {
                                gameMap[i,j] = 8;
                            }
                        }

                        Console.Write("[{0}]", gameMap[i, j]);
                        
                        

                    
                }
                    Console.WriteLine();
            }
        }
            
            
        }

        public void NotifyColor() {
            lismanPlayer = LismanYellow;
        }

        public bool canMove(int onX, int onY) {
            //Console.WriteLine("Mover a: [{0},{1}]= {2}",onX,onY,gameMap[onX,onY]);

            if (gameMap[onX,onY] == 0 ){
                return false;
            } else {
                return true;
            }               
        }

        public void StopLisman()
        {
            runLeft.Stop();
            runUp.Stop();
            runRight.Stop();
            runDown.Stop();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StopLisman();
            runRight.Start();
        }
        private void RunLeft(object sender, EventArgs e)
        {
            
            if(X==0 && Y == 11) {
                X = 23;
                Y = 11;
                Grid.SetColumn(lismanPlayer, X);
                Grid.SetRow(lismanPlayer, Y);
                return;
            }
            if (canMove(X-1,Y)){
                if (X > 0) {
                    X -= 1;
                    Grid.SetColumn(lismanPlayer, X);
                    Grid.SetRow(lismanPlayer, Y );
                    

                }
            } else {
                StopLisman();
            }
            
    
        }
        private void RunUp(object sender, EventArgs e)
        {
           
            if (canMove(X,Y-1)) {
                Y -= 1;
                Grid.SetColumn(lismanPlayer, X);
                Grid.SetRow(lismanPlayer, Y);
            } else {
                StopLisman();
            }

            
        }
        private void RunRight(object sender, EventArgs e)
        {
            if (X == 23 && Y == 11) {
                X = 0;
                Y = 11;
                Grid.SetColumn(lismanPlayer, X);
                Grid.SetRow(lismanPlayer, Y);
                return;
            }
            if (canMove(X+1,Y)) {
                X += 1;
                Grid.SetColumn(lismanPlayer, X);
                Grid.SetRow(lismanPlayer, Y);
            } else {
                StopLisman();
            }


        }

        private void RunDown(object sender, EventArgs e)
        {
            if (canMove(X,Y+1)) {
                Y += 1;
                Grid.SetColumn(lismanPlayer, X);
                Grid.SetRow(lismanPlayer, Y);
            } else {
                StopLisman();
            }


            //RotateTransform downTransform = new RotateTransform(90);
            //Pacman.RenderTransform = downTransform;
        }

        private void Matriz_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Left) {
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
