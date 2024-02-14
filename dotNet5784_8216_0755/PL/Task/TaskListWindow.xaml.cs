using System.Windows;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System;
using System.Windows.Input;
using System.Windows.Media;

namespace PL.Task
{

    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        public BO.Level levels { get; set; } = BO.Level.None;
        private static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public TaskListWindow()
        {
            InitializeComponent();
            var temp = s_bl?.Task.ReadMainDetailsTasks();
            TaskList = temp == null ? new() : new(temp);
        }
        public ObservableCollection<BO.TaskInList> TaskList
        {
            get { return (ObservableCollection<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        public static readonly DependencyProperty TaskListProperty =
        DependencyProperty.Register("TaskList", typeof(ObservableCollection<BO.TaskInList>),
        typeof(TaskListWindow), new PropertyMetadata(null));

        /// <summary>
        /// sorting the tasks by level selection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Level_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            var temp = levels == BO.Level.None ?
            s_bl?.Task.ReadMainDetailsTasks() :
            s_bl?.Task.ReadMainDetailsTasks(item => (BO.Level)item.level == levels);
            TaskList = temp == null ? new() : new(temp);
        }

        /// <summary>
        /// open adding task window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void add_task_btn_click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            button.Background = Brushes.LightPink;
            new TaskWindow(0).ShowDialog();
            var temp = s_bl?.Task.ReadMainDetailsTasks();
            TaskList = temp == null ? new() : new(temp);
        }

        /// <summary>
        /// open updating task window 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void update_task(object sender, RoutedEventArgs e)
        {
            BO.TaskInList? Task = (sender as ListView)?.SelectedItem as BO.TaskInList;
            new TaskWindow(Task!.id).ShowDialog();
            var temp = s_bl?.Task.ReadMainDetailsTasks();
            TaskList = temp == null ? new() : new(temp);
        }
    }
}
