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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _3er_Parcial
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            PrologHandler.Instance.LoadFile(@"..\..\..\prologBasics.pl");
            PrologHandler.Instance.LoadFile(@"..\..\..\GOT.pl");
            InitializeComponent();
            charactersFrame.Content = new CharactersPage();
            enemiesFrame.Content = new EnemiesPage();
            canmarryFrame.Content = new CanMarryRulePage();
            herederoFrame.Content = new CanInheritPage();
               
        }
    }
}
