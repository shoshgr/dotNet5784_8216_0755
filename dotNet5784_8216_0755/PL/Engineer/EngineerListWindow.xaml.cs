

using System.Windows;
using System.Collections.ObjectModel;

namespace PL.Engineer
{

    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
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


    }
}
