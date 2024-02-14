using BO;
using DO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {

        private int id_ = 0;
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public EngineerWindow(int id = 1)
        {
            id_= id;
            InitializeComponent();
            curEngineer = (id == 0) ? new BO.Engineer {engineer_id= 0,name= "",email= "",degree= BO.Level.None,cost_per_hour=0 ,is_active= false,task= null }:
                s_bl.Engineer.Read(id) ;
            
        }
        public BO.Engineer curEngineer
        {
            get { return (BO.Engineer)GetValue(curEngineerProperty); }
            set { SetValue(curEngineerProperty, value); }
        }
        public static readonly DependencyProperty curEngineerProperty =
        DependencyProperty.Register("curEngineer", typeof(BO.Engineer),
        typeof(EngineerWindow), new PropertyMetadata(null));
        private void add_update_button_click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                if (id_ == 0)
                {
                    s_bl.Engineer.Create(curEngineer);
                    MessageBox.Show("The engineer added successfully",
            "success",
                MessageBoxButton.OK,
                MessageBoxImage.None);
                }
                else
                {
                    s_bl.Engineer.Update(curEngineer);
                    MessageBox.Show("The engineer updated successfully",
            "success",
                MessageBoxButton.OK,
                MessageBoxImage.None);

                }

                this.Close();
                
            }
            catch(BlDoesNotExistException ex) {
                MessageBox.Show(ex.Message,
            "error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);
                
            }
            catch (DalAlreadyExistsNotActiveException ex)
            {
                MessageBox.Show(ex.Message,
            "error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            }
            catch (DalAlreadyExistsException ex)
            {
                MessageBox.Show(ex.Message,
            "error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            }
            catch (BlInvalidValueException ex)
            {
                MessageBox.Show(ex.Message,
            "error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,
           "error",
               MessageBoxButton.OK,
               MessageBoxImage.Error);
            }
            
        }

        
    }
}
