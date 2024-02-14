using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace PL.Engineer
{

    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        public BO.Level levels { get; set; } = BO.Level.None;
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public EngineerListWindow()
        {
            InitializeComponent();
            var temp = s_bl?.Engineer.ReadMainDetailsEngineers();


            EngineerList = temp == null ? new() : new(temp);
        }
        public ObservableCollection<BO.EngineerMainDetails> EngineerList
        {
            get { return (ObservableCollection<BO.EngineerMainDetails>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }
        public static readonly DependencyProperty EngineerListProperty =
        DependencyProperty.Register("EngineerList", typeof(ObservableCollection<BO.EngineerMainDetails>),
        typeof(EngineerListWindow), new PropertyMetadata(null));

        /// <summary>
        /// sorting the engineers by level selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Level_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var temp = levels == BO.Level.None ?
            s_bl?.Engineer.ReadMainDetailsEngineers() :
            s_bl?.Engineer.ReadMainDetailsEngineers(item => (BO.Level)item.degree == levels);
            EngineerList = temp == null ? new() : new(temp);
        }

        /// <summary>
        /// open adding engineer window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_engineer_btn_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = Brushes.LightPink;
            new EngineerWindow(0).ShowDialog();
            var temp = s_bl?.Engineer.ReadMainDetailsEngineers();
            EngineerList = temp == null ? new() : new(temp);
        }

        /// <summary>
        /// open updating current engineer window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void update_engineer(object sender, RoutedEventArgs e)
        {
            BO.EngineerMainDetails? Engineer = (sender as ListView)?.SelectedItem as BO.EngineerMainDetails;
            new EngineerWindow(Engineer!.id).ShowDialog();
            var temp = s_bl?.Engineer.ReadMainDetailsEngineers();
            EngineerList = temp == null ? new() : new(temp);
        }
    }
}
