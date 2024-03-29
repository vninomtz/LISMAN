﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.ServiceModel;
using Lisman.LismanService;
using WpfAnimatedGif;

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
        int speedLisman = 300;
        int timerValidation = 0;
        int playerColor;

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
        DispatcherTimer timePower = new DispatcherTimer();

        DispatcherTimer reconnectionTimer = new DispatcherTimer();

        InstanceContext instace = null;
        MultiplayerManagerClient client = null;

        public MultiplayerGame(String test)
        {

        }

        /// <summary>
        /// Une a los jugadores en la información del juego en el servidor al crear la ventana del juego multijugador
        /// </summary>
        /// <param name="idgame">Identificador del juego al cual el jugador pertenece y sera gaurdado en el sevidor</param>
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
                MessageBox.Show(Properties.Resources.server_connection_error);
                Logger.log.Error(ex);
                
            }
            UserConnected.Text = SingletonAccount.getSingletonAccount().User;



            runLeft.Tick +=  new EventHandler(RunLeft);
            runLeft.Interval = new TimeSpan(0, 0, 0, 0, speedLisman);
            runUp.Tick += new EventHandler(RunUp);  
            runUp.Interval = new TimeSpan(0, 0, 0, 0,speedLisman);
            runRight.Tick += new EventHandler(RunRight);
            runRight.Interval = new TimeSpan(0, 0, 0, 0, speedLisman);         
            runDown.Tick += new EventHandler(RunDown);
            runDown.Interval = new TimeSpan(0, 0, 0, 0, speedLisman);
            
            MatrixGame();
            DrawPills();
            createMatrixPillsImages();

        
            reconnectionTimer.Tick += new EventHandler(Reconnection);
            reconnectionTimer.Interval = new TimeSpan(0,0,0,1);
            reconnectionTimer.Start();


        }

        public void Reconnection(object sender, EventArgs e)
        {
            try
            {
                client.Reconntection(SingletonAccount.getSingletonAccount().User);
                ConnectionStatus.Text = "Connected";
            }
            catch (Exception ex)

            {

                ConnectionStatus.Text = "Diconnected";
                Logger.log.Error(ex);



            }
        }

        /// <summary>
        /// Lee los datos de la matriz del juego del archivo de recursos 
        /// </summary>
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

        /// <summary>
        /// Crea las asignaciones de las pill y la posición en la que se encontrarán en el mapa, respecto a la matriz 
        /// </summary>
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

        /// <summary>
        /// Lee y dibuja las imagenes de las pills en pantalla asignandolas a su posicion correspondiente 
        /// </summary>
        public void DrawPills() {
            BitmapImage imagePath = new BitmapImage();
            imagePath.BeginInit();
            imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/Pill.png");
            imagePath.EndInit();

            pill0.Source = imagePath;
            pill1.Source = imagePath;
            pill2.Source = imagePath;
            pill3.Source = imagePath;
            pill4.Source = imagePath;
            pill5.Source = imagePath;
            pill6.Source = imagePath;
            pill7.Source = imagePath;
            pill8.Source = imagePath;
            pill9.Source = imagePath;
            pill10.Source = imagePath;
            pill11.Source = imagePath;

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

        /// <summary>
        /// Determina si el jugador puede realizar un movimiento  a la posición que desea 
        /// </summary>
        /// <param name="onX">Coordenada respecto al eje X en el que el jugador se encuentra</param>
        /// <param name="onY">Coordenada respecto al eje Y en el que el jugador se encuentra</param>
        /// <returns>Devuelve true si el movimiento del jugador es válido y falseen casoz contrario</returns>

        public bool canMove(int onX, int onY) {
            bool can = true;
            if (gameMap[onX, onY] == 0) {
                can = false;
            }
            return can;
        }

        /// <summary>
        /// Detiene los temporizadores que permiten dezplazar al jugador por las coordenadas 
        /// </summary>
        public void StopLisman() {
            runLeft.Stop();
            runUp.Stop();
            runRight.Stop();
            runDown.Stop();
        }

        private void Button_ExitGame_Click(object sender, RoutedEventArgs e) {
            client.ExitGame(idgame,playerColor,X,Y);
        }

        /// <summary>
        /// Mueve al juador a la dirección izquierda y actualiza su posición en el servidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunLeft(object sender, EventArgs e) {
            int initialPositionX = X;
            int initialpositionY = Y;
            if (X == 0 && Y == 11) {
                X = 23;
                Y = 11;
                MoveLismanInMap(initialPositionX, initialpositionY, X, Y,"LEFT");
                return;
            }
            if (canMove(X - 1, Y)) {
               
                X -= 1;
                MoveLismanInMap(initialPositionX, initialpositionY, X, Y,"LEFT");
                
            } else {
                StopLisman();
            }
        }

        /// <summary>
        /// Mueve al juador a la dirección arriba y actualiza su posición en el servidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunUp(object sender, EventArgs e) {
            int initialPositionX = X;
            int initialpositionY = Y;

            if (canMove(X, Y - 1)) {
                Y -= 1;
                MoveLismanInMap(initialPositionX, initialpositionY, X, Y,"UP");
            } else {
                StopLisman();
            }
        }

        /// <summary>
        /// Mueve al juador a la dirección derecha y actualiza su posición en el servidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunRight(object sender, EventArgs e) {
            int initialPositionX = X;
            int initialpositionY = Y;
            if (X == 23 && Y == 11) {
                X = 0;
                Y = 11;
                MoveLismanInMap(initialPositionX, initialpositionY, X, Y,"RIGHT");
                return;
            }
            if (canMove(X + 1, Y)) {
                X += 1;
                MoveLismanInMap(initialPositionX, initialpositionY, X, Y,"RIGHT");
            } else {
                StopLisman();
            }
        }

        /// <summary>
        /// Mueve al juador a la dirección abajo y actualiza su posición en el servidor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RunDown(object sender, EventArgs e) {
            int initialPositionX = X;
            int initialpositionY = Y;
            if (canMove(X, Y + 1)) {
                Y += 1;
                MoveLismanInMap(initialPositionX,initialpositionY,X,Y,"DOWN");
            } else {
                StopLisman();
            }
        }

      
        static LismanMovement movement = new LismanMovement();

        /// <summary>
        /// Crea un objeto que contiene la información del dezplazamiento del jugador para enviarlo al servidor
        /// </summary>
        /// <param name="initialPositionX">posición en le que se encuentra el jugador respecto al eje X</param>
        /// <param name="initialPositionY">posición en le que se encuentra el jugador respecto al eje Y</param>
        /// <param name="finalPositionX">posición a la que desea ir el jugador respecto al eje X</param>
        /// <param name="finalPositionY">posición a la que desea ir el jugador respecto al eje Y</param>
        /// <param name="goTo">Dirección a la que el jugador esta realizando el giro</param>
        public void MoveLismanInMap(int initialPositionX, int initialPositionY, int finalPositionX, int finalPositionY, String goTo)
        {
            movement.idGame = this.idgame;
            movement.colorLisman = playerColor;
            movement.initialPositionX = initialPositionX;
            movement.initialPositionY = initialPositionY;
            movement.finalPositionX = finalPositionX;
            movement.finalPositionY = finalPositionY;
            movement.goTo = goTo;
            client.MoveLisman(movement);
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
        /// <summary>
        /// Escribe en pantalla el nombre del jugador le asigna la imagen que ocupará
        /// </summary>
        /// <param name="colorPlayer"> identificador del color que le toca usar al jugador</param>
        /// <param name="user">Nombre de usuario del jugador</param>
        public void NotifyColorPlayer(int colorPlayer, String user)
        {
            if(SingletonAccount.getSingletonAccount().User == user)
            {
                playerColor = colorPlayer;
            }
            
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
                    lismanPlayerImage = LismanBlueImage;
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

        /// <summary>
        /// Cambia la imagen de lisman a la derecha del color recibidó
        /// </summary>
        /// <param name="playerColor">Identificador del color que sera cambiada en pantalla </param>
        public void MoveLismanImageRight(int playerColor)
        {
            BitmapImage imagePath = new BitmapImage();
            switch (playerColor)
            {
                case LISMANYELLOW:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanYellow.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanYellowImage, imagePath);
                    break;
                case LISMANRED:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanRed.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanRedImage, imagePath);

                    break;
                case LISMANBLUE:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanBlue.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanBlueImage, imagePath);
                    break;
                case LISMANGREEN:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanGreen.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanGreenImage, imagePath);
                    break;
            }

        }

        /// <summary>
        /// Cambia la imagen de lisman a la izquierda del color recibidó
        /// </summary>
        /// <param name="playerColor">Identificador del color que sera cambiada en pantalla</param>
        public void MoveLismanImageLeft(int playerColor)
        {
            BitmapImage imagePath = new BitmapImage();
            switch (playerColor)
            {
                case LISMANYELLOW:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanYellowLeft.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanYellowImage, imagePath);
                    break;
                case LISMANRED:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanRedLeft.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanRedImage, imagePath);

                    break;
                case LISMANBLUE:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanBlueLeft.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanBlueImage, imagePath);
                    break;
                case LISMANGREEN:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanGreenLeft.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanGreenImage, imagePath);
                    break;
            }

        }

        /// <summary>
        /// Cambia la imagen de lisman hacia arriba del color recibidó
        /// </summary>
        /// <param name="playerColor">Identificador del color que sera cambiada en pantalla</param>
        public void MoveLismanImageUp(int playerColor)
        {
            BitmapImage imagePath = new BitmapImage();
            switch (playerColor)
            {
                case LISMANYELLOW:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanYellowUp.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanYellowImage, imagePath);
                    break;
                case LISMANRED:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanRedUp.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanRedImage, imagePath);

                    break;
                case LISMANBLUE:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanBlueUp.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanBlueImage, imagePath);
                    break;
                case LISMANGREEN:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanGreenUp.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanGreenImage, imagePath);
                    break;
            }

        }

        /// <summary>
        /// Cambia la imagen de lisman hacia arriba del color recibidó
        /// </summary>
        /// <param name="playerColor">Identificador del color que sera cambiada en pantalla</param>
        public void MoveLismanImageDown(int playerColor)
        {
            BitmapImage imagePath = new BitmapImage();
            switch (playerColor)
            {
                case LISMANYELLOW:                
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanYellowDown.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanYellowImage, imagePath);
                    break;
                case LISMANRED:                                   
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanRedDown.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanRedImage, imagePath);
                    
                    break;
                case LISMANBLUE:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanBlueDown.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanBlueImage, imagePath);
                    break;
                case LISMANGREEN:
                    imagePath.BeginInit();
                    imagePath.UriSource = new Uri(parentDirectory + "/Resources/img/LismanGreenDown.gif");
                    imagePath.EndInit();
                    ImageBehavior.SetAnimatedSource(LismanGreenImage, imagePath);
                    break;
            }

        }

        /// <summary>
        /// Callback que cambia la imagen a la posición y direccion determinada
        /// </summary>
        /// <param name="colorPlayer">Identificador de la imagen que sera cambiada</param>
        /// <param name="positionX">Posición a la que la imagen sera cambiada respecto al eje X</param>
        /// <param name="positionY">Posición a la que la imagen sera cambiada respecto al eje Y</param>
        /// <param name="goTo">Dirección a la que la imagen sera cambiada</param>
        public void NotifyLismanMoved(int colorPlayer, int positionX, int positionY,String goTo)
        {
            Image lismanImageMoved = null;

            switch (goTo){
                case "UP":
                    MoveLismanImageUp(colorPlayer);
                    break;
                case "DOWN":
                    MoveLismanImageDown(colorPlayer);
                    break;
                case "RIGHT":
                    MoveLismanImageRight(colorPlayer);
                    break;
                case "LEFT":
                    MoveLismanImageLeft(colorPlayer);
                    break;
            }

            switch (colorPlayer){
                case LISMANYELLOW:
                    lismanImageMoved = LismanYellowImage;
                    break;
                case LISMANRED:
                    lismanImageMoved = LismanRedImage;
                    break;
                case LISMANBLUE:
                    lismanImageMoved = LismanBlueImage;
                    break;
                case LISMANGREEN:
                    lismanImageMoved = LismanGreenImage;
                    break;
            }


            Grid.SetColumn(lismanImageMoved, positionX);
            Grid.SetRow(lismanImageMoved, positionY);
        }

        /// <summary>
        /// Escribe en pantalla la información de los jugadores (acerca del juego) que se encuentran ella 
        /// </summary>
        /// <param name="listPlayers">Diccionaro que contiene el color del asignado al jugador y la informacion de el</param>
        public void PrintInformationPlayers(Dictionary<int, InformationPlayer> listPlayers)
        {
            foreach (KeyValuePair<int, InformationPlayer> player in listPlayers)
            {
                int colorPlayer = player.Key;
                switch (colorPlayer)
                {
                    case LISMANYELLOW:
                        UserLismanYellow.Text = player.Value.userLisman;
                        UserLismanYellowLifes.Text = player.Value.lifesLisman.ToString();
                        UserLismanYellowScore.Text = player.Value.scoreLisman.ToString();
                        break;
                    case LISMANRED:
                        UserLismanRed.Text = player.Value.userLisman;
                        UserLismanRedLifes.Text = player.Value.lifesLisman.ToString();
                        UserLismanRedScore.Text = player.Value.scoreLisman.ToString();
                        break;
                    case LISMANBLUE:
                        UserLismanBlue.Text = player.Value.userLisman;
                        UserLismanBlueLifes.Text = player.Value.lifesLisman.ToString();
                        UserLismanBlueScore.Text = player.Value.scoreLisman.ToString();
                        break;
                    case LISMANGREEN:
                        UserLismanGreen.Text = player.Value.userLisman;
                        UserLismanGreenLifes.Text = player.Value.lifesLisman.ToString();
                        UserLismanGreenScore.Text = player.Value.scoreLisman.ToString();
                        break;
                }
            }
        }

        /// <summary>
        /// Callback que borra la imagen de la pill en el juego en la posición determianda
        /// </summary>
        /// <param name="positionX">Coordenada respecto al eje X</param>
        /// <param name="positionY">Coordenada respecto al eje Y </param>
        public void NotifyDisappearedPowerPill(int positionX, int positionY)
        {
            matrixPillsImages[positionX, positionY].Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Callback que actualiza en pantalla la puntuación del jugador
        /// </summary>
        /// <param name="colorPlayer">color del lisman que su puntuación sera actualizada </param>
        /// <param name="scorePlayer">puntuación del jugador</param>
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

        /// <summary>
        /// Callback que actualiza las vidas del jugador 
        /// </summary>
        /// <param name="colorPlayer">Identificador del color del lisman que su puntiación sera actualizada</param>
        /// <param name="lifePlayer">Vidas restantes del jugador</param>
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

        /// <summary>
        /// Callback que notifica a los jugadores cuando uno esta muerto y borra la imagen del mapa 
        /// </summary>
        /// <param name="colorPlayer">Identificador del color del lisman que sera borrado</param>
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
                    LismanBlueImage.Visibility = Visibility.Hidden;
                    break;
                case LISMANGREEN:
                    LismanGreenImage.Visibility = Visibility.Hidden;
                    break;
            }

            if (playerColor == colorPlayer){
                MessageBox.Show(Properties.Resources.message_lose_game);
                MultiplayerHome window = new MultiplayerHome();
                window.Show();
                this.Close();

            }
            

        }
        /// <summary>
        /// Retorna la imagen de lisman a su posición inicial 
        /// </summary>
        /// <param name="colorPlayer">Identificdor del color de la imagen del jugador que sera movida</param>
        /// <param name="positionX">Coordenada respecto al eje X en la cual se moverá</param>
        /// <param name="positionY">Coordenada respecto al eje Y en la cual se moverá</param>
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
                    lismanImageMoved = LismanBlueImage;
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

        /// <summary>
        /// Aumenta la velocidad de dezplazamiento de la imagen por un periodo de tiempo
        /// </summary>
        /// <param name="speed">Valor del tiempo en el cual la imagen se dezplazará </param>
        /// <param name="hasPower">Indica si el jugador tiene mas velocidad o no</param>
        public void UpdateLismanSpeed(int speed,bool hasPower)
        {
            if (hasPower)
            {
                timePower.Tick += new EventHandler(RemovePowerSpeed);
                timePower.Interval = new TimeSpan(0, 0, 10);
                timePower.Start();

            }
            runLeft.Interval = new TimeSpan(0, 0, 0, 0, speed);
            runUp.Interval = new TimeSpan(0, 0, 0, 0, speed);
            runRight.Interval = new TimeSpan(0, 0, 0, 0, speed);
            runDown.Interval = new TimeSpan(0, 0, 0, 0, speed);
            speedLisman = speed;

        }

       
        public void RemovePowerSpeed(object sender, EventArgs e)
        {
           if (timerValidation == 1 )
            {
                client.RemovePower(SingletonAccount.getSingletonAccount().User);
                timePower.Stop();
                timerValidation = 0;
            }
            else
            {
                timerValidation = 1;
            }
        }

        /// <summary>
        /// Callback que notifica al jugador restante que acabó la partida y ha ganado
        /// </summary>
        /// <param name="colorLisman">Identificador del color del jugador que ganó la partida</param>
        public void NotifyEndGame(int colorLisman)
        {
            MessageBox.Show(Properties.Resources.message_you_win);
            MultiplayerHome windowHome = new MultiplayerHome();
            reconnectionTimer.Stop();
            windowHome.Show();
            this.Close();
        }
        /// <summary>
        /// Callback que borra en pantalla la imagen de un lisman cuando abandonó la partida
        /// </summary>
        /// <param name="colorGame">Identificador del color del lisman que abandonó la partida</param>
        public void NotifyLismanLeaveGame(int colorGame)
        {
            
                switch (colorGame)
                {
                    case LISMANYELLOW:
                        LismanYellowImage.Visibility = Visibility.Hidden;
                        break;
                    case LISMANRED:
                        LismanRedImage.Visibility = Visibility.Hidden;
                        break;
                    case LISMANBLUE:
                        LismanBlueImage.Visibility = Visibility.Hidden;
                        break;
                    case LISMANGREEN:
                        LismanGreenImage.Visibility = Visibility.Hidden;
                        break;
                }

                if (playerColor == colorGame)
                {
                    MessageBox.Show(Properties.Resources.message_lisman_left_game);
                    MultiplayerHome window = new MultiplayerHome();
                    reconnectionTimer.Stop();
                    window.Show();
                    this.Close();
                }
            }
    }
}

