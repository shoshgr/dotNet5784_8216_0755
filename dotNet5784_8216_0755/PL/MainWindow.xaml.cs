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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PL.Engineer;
using PL.Task;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// open engineers list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnEngineers_Click(object sender, RoutedEventArgs e)
        {
            new EngineerListWindow().Show();
        }

        /// <summary>
        /// initialize data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInitialization_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Do you want to initialize the data source?",
                "initialize",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                DalTest.Initialization.Do(Factory.Get);
            }
        }

        /// <summary>
        /// open task list window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTasks_Click(object sender, RoutedEventArgs e)
        {
            new TaskListWindow().Show();
        }
    }
}
