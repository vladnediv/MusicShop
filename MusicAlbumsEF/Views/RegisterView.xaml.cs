﻿using MusicAlbumsEF.ViewModels;
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

namespace MusicAlbumsEF.Views
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : Window
    {
        
        public RegisterView(RegisterViewModel registerViewModel)
        {
            registerViewModel.CloseEvent = () => { this.Close(); };
            DataContext = registerViewModel;
            InitializeComponent();
        }
    }
}