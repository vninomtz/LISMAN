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
using System.ServiceModel;
using Lisman.LismanService;

namespace Lisman {
    /// <summary>
    /// Interaction logic for MultiplayerGame.xaml
    /// </summary>

    public partial class MultiplayerGame : Window, IMultiplayerManagerCallback{

        int idgame;
        int[,] gameMap = new int[24, 23];
        Image[,] matrixPillsImages = new Image[24, 23];       

        const int LISMANYELLOW = 3;
        const int LISMANRED = 4;
        const int LISMANBLUE = 5;        
        const int LISMANGREEN = 6;
        Image lismanPlayerImage = null;

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

        InstanceContext instace = null;
        MultiplayerManagerClient client = null;

        public MultiplayerGame(int idgame)
        {
            InitializeComponent();
            instace = new InstanceContext(this);
            this.idgame = idgame;
            client = new MultiplayerManagerClient(instace);
            try
            {
                client.JoinMultiplayerGame(SingletonAccount.getSingletonAccount().User, idgame);
            }
            catch (Exception ex)
            {
                Logger.log.Error(ex);
                
            }
            UserConnected.Text = SingletonAccount.getSingletonAccount().User;



            runLeft.Tick +=  new EventHandler(RunLeft);
            runLeft.Interval = new TimeSpan(0, 0, 0, 0, 300);
            runUp.Tick += new EventHandler(RunUp);  
            runUp.Interval = new TimeSpan(0, 0, 0, 0,300);
            runRight.Tick += new EventHandler(RunRight);
            runRight.Interval = new TimeSpan(0, 0, 0, 0, 300);         
            runDown.Tick += new EventHandler(RunDown);
            runDown.Interval = new TimeSpan(0, 0, 0, 0, 300);
            
            MatrixGame();
            DrawPills();
            createMatrixPillsImages();
                        
        }

        public void MatrixGame() {
            using (StreamReader sr = new StreamReader(parentDirectory + "/Resources/Map.txt")) {

                for (int i = 0; i <= 23; i++) {
                    for (int j = 0; j <= 22; j++) {
                        int caracter = sr.Read();

                        if (caracter != -1) {
                            switch (caracter){
                                case 48:
                                    gameMap[i, j] = 0;
                                    break;
                                case 49:
                                    gameMap[i, j] = 1;
                                    break;
                                case 51:
                                    gameMap[i, j] = 3;
                                    break;
                                case 52:
                                    gameMap[i, j] = 4;
                                    break;
                                case 53:
                                    gameMap[i, j] = 5;
                                    break;
                                case 54:
                                    gameMap[i, j] = 6;
                                    break;
                                case 56:
                                    gameMap[i, j] = 8;
                                    break;
                                case 50:
                                    gameMap[i, j] = 2;
                                    break;                                
                            }                         
                        }                   

                    }                  
                }

            }

        }

        public void createMatrixPillsImages(){
            matrixPillsImages[2, 5] = pill0;
            matrixPillsImages[18, 11] = pill1;
            matrixPillsImages[5, 11] = pill2;
            matrixPillsImages[12, 17] = pill3;
            matrixPillsImages[12, 13] = pill4;
            matrixPillsImages[12, 21] = pill5;
            matrixPillsImages[18, 7] = pill6;
            matrixPillsImages[11, 9] = pill7;
            matrixPillsImages[22, 15] = pill8;
            matrixPillsImages[1, 16] = pill9;
            matrixPillsImages[10, 4] = pill10;
            matrixPillsImages[13, 2] = pill11;       
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
            panel_17_12.Children.Add(pill3);
            panel_13_12.Children.Add(pill4);
            panel_21_12.Children.Add(pill5);
            panel_7_18 .Children.Add(pill6);
            panel_9_11.Children.Add(pill7);
            panel_15_22.Children.Add(pill8);
            panel_16_1.Children.Add(pill9);
            panel_4_10.Children.Add(pill10);
            panel_2_13.Children.Add(pill11);
        }


        public bool canMove(int onX, int onY) {
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
            int initialPositionX = X;
            int initialpositionY = Y;
            if (X == 0 && Y == 11) {
                X = 23;
                Y = 11;
                MoveLismanInMap(initialPositionX, initialpositionY, X, Y);
                return;
            }
            if (canMove(X - 1, Y)) {
               
                X -= 1;
                MoveLismanInMap(initialPositionX, initialpositionY, X, Y);
                
            } else {
                StopLisman();
            }
        }

        private void RunUp(object sender, EventArgs e) {
            int initialPositionX = X;
            int initialpositionY = Y;

            if (canMove(X, Y - 1)) {
                Y -= 1;
                MoveLismanInMap(initialPositionX, initialpositionY, X, Y);
            } else {
                StopLisman();
            }
        }
        private void RunRight(object sender, EventArgs e) {
            int initialPositionX = X;
            int initialpositionY = Y;
            if (X == 23 && Y == 11) {
                X = 0;
                Y = 11;
                MoveLismanInMap(initialPositionX, initialpositionY, X, Y);
                return;
            }
            if (canMove(X + 1, Y)) {
                X += 1;
                MoveLismanInMap(initialPositionX, initialpositionY, X, Y);
            } else {
                StopLisman();
            }
        }
        private void RunDown(object sender, EventArgs e) {
            int initialPositionX = X;
            int initialpositionY = Y;
            if (canMove(X, Y + 1)) {
                Y += 1;
                MoveLismanInMap(initialPositionX,initialpositionY,X,Y);
            } else {
                StopLisman();
            }
        }
        public void MoveLismanInMap(int initialPositionX, int initialPositionY, int finalPositionX, int finalPositionY)
        {
            client.MoveLisman(this.idgame, SingletonAccount.getSingletonAccount().User, initialPositionX, initialPositionY, finalPositionX, finalPositionY);
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
                runRight.Stop();
                runDown.Stop();
                runUp.Start();
            }
            if (e.Key == Key.Right) {
                runLeft.Stop();
                runUp.Stop();
                runDown.Stop();
                runRight.Start();
            }
            if (e.Key == Key.Down) {
                runLeft.Stop();
                runUp.Stop();
                runRight.Stop();
                runDown.Start();
            }
        }

        public void NotifyColorPlayer(int colorPlayer, String user)
        {
            switch (colorPlayer)
            {
                case LISMANYELLOW:
                    lismanPlayerImage = LismanYellowImage;
                    X = 1;
                    Y = 1;
                    UserLismanYellow.Text = user;
                    break;
                case LISMANRED:
                    lismanPlayerImage = LismanRedImage;
                    X = 22;
                    Y = 1;
                    UserLismanRed.Text = user;
                    break;
                case LISMANBLUE:
                    lismanPlayerImage = LismanTurquoiseImage;
                    X = 1;
                    Y = 21;
                    UserLismanBlue.Text = user;
                    break;
                case LISMANGREEN:
                    lismanPlayerImage = LismanGreenImage;
                    X = 22;
                    Y = 21;
                    UserLismanGreen.Text = user;
                    break;
            }
        }

        public void NotifyLismanMoved(int colorPlayer, int positionX, int positionY)
        {
            Image lismanImageMoved = null;
            switch (colorPlayer)
            {
                case LISMANYELLOW:
                    lismanImageMoved = LismanYellowImage;
                    break;
                case LISMANRED:
                    lismanImageMoved = LismanRedImage;
                    break;
                case LISMANBLUE:
                    lismanImageMoved = LismanTurquoiseImage;
                    break;
                case LISMANGREEN:
                    lismanImageMoved = LismanGreenImage;
                    break;
            }
            Grid.SetColumn(lismanImageMoved, positionX);
            Grid.SetRow(lismanImageMoved, positionY);
        }

        public void PrintInformationPlayers(Dictionary<string, InformationPlayer> listPlayers)
        {
            foreach (KeyValuePair<String, InformationPlayer> player in listPlayers)
            {
                int colorPlayer = player.Value.colorLisman;
                switch (colorPlayer)
                {
                    case LISMANYELLOW:
                        UserLismanYellow.Text =player.Key;
                        UserLismanYellowLifes.Text = player.Value.lifesLisman.ToString();
                        UserLismanYellowScore.Text = player.Value.scoreLisman.ToString();
                        break;
                    case LISMANRED:
                        UserLismanRed.Text = player.Key;
                        UserLismanRedLifes.Text = player.Value.lifesLisman.ToString();
                        UserLismanRedScore.Text = player.Value.scoreLisman.ToString();
                        break;
                    case LISMANBLUE:
                        UserLismanBlue.Text = player.Key;
                        UserLismanBlueLifes.Text = player.Value.lifesLisman.ToString();
                        UserLismanBlueScore.Text = player.Value.scoreLisman.ToString();
                        break;
                    case LISMANGREEN:
                        UserLismanGreen.Text = player.Key;
                        UserLismanGreenLifes.Text = player.Value.lifesLisman.ToString();
                        UserLismanGreenScore.Text = player.Value.scoreLisman.ToString();
                        break;
                }
            }
        }

        public void NotifyDisappearedPowerPill(int positionX, int positionY)
        {
            matrixPillsImages[positionX, positionY].Visibility = Visibility.Hidden;
        }

        public void NotifyUpdateScore(int colorPlayer, int scorePlayer)
        {
            switch (colorPlayer)
            {
                case LISMANYELLOW:
                    UserLismanYellowScore.Text = scorePlayer.ToString();
                    break;
                case LISMANRED:
                    UserLismanRedScore.Text = scorePlayer.ToString();
                    break;
                case LISMANBLUE:
                    UserLismanBlueScore.Text = scorePlayer.ToString();
                    break;
                case LISMANGREEN:
                    UserLismanGreenScore.Text = scorePlayer.ToString();
                    break;
            }
        }

        public void NotifyUpdateLifes(int colorPlayer, int lifePlayer)
        {
            switch (colorPlayer)
            {
                case LISMANYELLOW:
                    UserLismanYellowLifes.Text = lifePlayer.ToString();
                    break;
                case LISMANRED:
                    UserLismanRedLifes.Text = lifePlayer.ToString();
                    break;
                case LISMANBLUE:
                    UserLismanBlueLifes.Text = lifePlayer.ToString();
                    break;
                case LISMANGREEN:
                    UserLismanGreenLifes.Text = lifePlayer.ToString();
                    break;
            }
        }

        public void NotifyPlayerIsDead(int colorPlayer)
        {
            switch (colorPlayer)
            {
                case LISMANYELLOW:
                    LismanYellowImage.Visibility = Visibility.Hidden;
                    break;
                case LISMANRED:
                    LismanRedImage.Visibility = Visibility.Hidden;
                    break;
                case LISMANBLUE:
                    LismanTurquoiseImage.Visibility = Visibility.Hidden;
                    break;
                case LISMANGREEN:
                    LismanGreenImage.Visibility = Visibility.Hidden;
                    break;
            }


        }

        public void ReturnLismanToInitialPosition(int colorPlayer, int positionX, int positionY)
        {
            Image lismanImageMoved = null;
            switch (colorPlayer)
            {
                case LISMANYELLOW:
                    lismanImageMoved = LismanYellowImage;
                    break;
                case LISMANRED:
                    lismanImageMoved = LismanRedImage;
                    break;
                case LISMANBLUE:
                    lismanImageMoved = LismanTurquoiseImage;
                    break;
                case LISMANGREEN:
                    lismanImageMoved = LismanGreenImage;
                    break;
            }
            X = positionX;
            Y = positionY;
            Grid.SetColumn(lismanImageMoved, positionX);
            Grid.SetRow(lismanImageMoved, positionY);
        }
    }
}
