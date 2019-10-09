﻿using System;
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

namespace Lisman {
    /// <summary>
    /// Interaction logic for MultijugadorHome.xaml
    /// </summary>
    public partial class MultiplayerHome : Window {
        public MultiplayerHome()
        {
            InitializeComponent();
        }

        

        private void button_goBack_Click(object sender, RoutedEventArgs e)
        {
            MainMenu menu = new MainMenu();
            menu.Show();
            this.Close();
        }

        private void button_newGame_Click(object sender, RoutedEventArgs e)
        {
            MultiplayerGame game = new MultiplayerGame();
            game.Show();
            this.Close();
        }

        private void button_joinGame_Click(object sender, RoutedEventArgs e)
        {
            Lobby lobby = new Lobby();
            lobby.Show();
            this.Close();
        }
    }
}