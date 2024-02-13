using BlApi;
using BO;
using DalApi;
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


namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {

        private int id_ = 0;
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public TaskWindow(int id = 1)
        {
            id_ = id;
            InitializeComponent();
            curTask = (id == 0) ? new BO.Task
            {
                task_id = 0,
                description = "",
                nickname = "",
                milestone = null,
                production_date = DateTime.MinValue,
                start_date = DateTime.MinValue,
                final_date = DateTime.MinValue,
                estimated_start = DateTime.MinValue,
                actual_end = DateTime.MinValue,
                product = "",
                remarks = "",
                engineer = null,
                level = BO.Level.None,
                tasks_list = new List<TaskInList>(),
                status = 0
            } : s_bl.Task.Read(id);
        }
        public BO.Task curTask
        {
            get { return (BO.Task)GetValue(curTaskProperty); }
            set { SetValue(curTaskProperty, value); }
        }
        public static readonly DependencyProperty curTaskProperty =
        DependencyProperty.Register("curTask", typeof(BO.Task),
        typeof(TaskWindow), new PropertyMetadata(null));
        private void add_update_button_click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (id_ == 0)
                {
                    s_bl.Task.Create(curTask);
                    MessageBox.Show("The task added successfully",
            "success",
                MessageBoxButton.OK,
                MessageBoxImage.None);
                }
                else
                {
                    s_bl.Task.Update(curTask);
                    MessageBox.Show("The task updated successfully",
            "success",
                MessageBoxButton.OK,
                MessageBoxImage.None);

                }

                this.Close();

            }
            catch (BlDoesNotExistException ex)
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

        }


    }
}
