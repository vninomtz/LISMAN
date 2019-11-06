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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Lisman {
    /// <summary>
    /// Interaction logic for MultiplayerGame.xaml
    /// </summary>
    public partial class MultiplayerGame : Window {
        int idgame;
        int[,] gameMap = new int[24,24];
       

        int X = 0;
        int Y = 0;
        DispatcherTimer runLeft = new DispatcherTimer();
        DispatcherTimer runUp = new DispatcherTimer();
        DispatcherTimer runRight = new DispatcherTimer();
        DispatcherTimer runDown = new DispatcherTimer();
        public MultiplayerGame(int idgame)
        {
            InitializeComponent();
            this.idgame = idgame;
            
            runLeft.Tick +=  new EventHandler(RunLeft);
            runLeft.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //runLeft.Start();

            
            runUp.Tick += new EventHandler(RunUp);
            runUp.Interval = new TimeSpan(0, 0, 0, 0, 500);
           // runUp.Start();

            
            runRight.Tick += new EventHandler(RunRight);
            runRight.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //runRight.Start();

            
            runDown.Tick += new EventHandler(RunDown);
            runDown.Interval = new TimeSpan(0, 0, 0, 0, 500);
            //runDown.Start();

            MatrizGame();
            PrintMap();
        }

        public void MatrizGame()
        {
            for (int i = 0; i < 24; i++)
            {
                gameMap[0, i] = 1;
            }
            for (int i = 0; i < 24; i++)
            {
                gameMap[i, 0] = 1;
            }
        }
        public void PrintMap()
        {
            for(int i = 0; i < 24; i++)
            {
                for(int j = 0; j < 24; j++)
                {
                    if (gameMap[i, j] == 1)
                    {
                        /*Image wall = new Image();
                        
                        BitmapImage myBitmapImage = new BitmapImage();
                        myBitmapImage.BeginInit();
                        myBitmapImage.UriSource = new Uri("C:/Users/Vik-t/Documents/Software Engineering/5to Semestre/Tecnologías para la Construcción/Proyecto/LISMAN/Lisman/Lisman/Resources/img/Muro.png");
                       
                        myBitmapImage.EndInit();
                        wall.Source = myBitmapImage;*/
                        StackPanel backGround = new StackPanel();
                        backGround.Background = Brushes.Blue;
                        Grid.SetRow(backGround, i);
                        Grid.SetColumn(backGround, j);
                    }
                }
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
            if(X > 0) {
                X -= 1;
            }

            Grid.SetColumn(Pacman, X);
            Grid.SetRow(Pacman, Y);
            
        }
        private void RunUp(object sender, EventArgs e)
        {
            if (Y > 0) {
                Y -= 1;
            }

            Grid.SetColumn(Pacman, X);
            Grid.SetRow(Pacman, Y);
        }
        private void RunRight(object sender, EventArgs e)
        {
            if (X < 26) {
                X += 1;
            }

            Grid.SetColumn(Pacman, X);
            Grid.SetRow(Pacman, Y);
        }

        private void RunDown(object sender, EventArgs e)
        {
            if (Y < 26) {
                Y += 1;
            }

            Grid.SetColumn(Pacman, X);
            Grid.SetRow(Pacman, Y);
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
