﻿using DalApi;
using Dal;
using DO;
using System.Reflection.Emit;

namespace DalTest
{
    internal class Program
    {

        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static IDependence? s_dalDependence = new DependenceImplementation(); //stage 1\
        private static void main_menu(string choice)
        {

            switch (choice)
            {
                case "0":

                    break;
                case "1":
                    //entity_menu("engineer");
                    break;
                case "2":
                    task_menu();
                    //entity_menu("task");

                    break;
                case "3":
                    //entity_menu("dependence");
                    break;
            }
        }

        private static void task_menu()
        {
            Console.WriteLine("Select the method you want to perform:\r\n  1 create new task\r\n 2  read the  task by id \r\n 3 read all the objects of the task type \r\n 4 update the task\r\n 5 delete the task\r\n 0 exit menu");
            string choice;
            choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    create_task();
                    break;
                case "2":
                    read_task();
                    break;
                case "3":
                    break;
                case "4":
                    update_task();
                    break;
                case "5":
                    delete_task();
                    break;

            }
        }
        private static void read_tasks()
        {
            List<DO.Task> tasks = s_dalTask!.ReadAll();
            int i = 1;
            foreach(var task in tasks)
            {
                Console.WriteLine("task number"+(i++)+ "\r\n"+task); 
            }
        }
        private static void read_task()
        {
            int _id;
            Console.WriteLine("enter engineer id");
            int.TryParse(Console.ReadLine(), out _id);

            DO.Task? task = s_dalTask!.Read(_id);
            Console.WriteLine(task);

        }
        private static void delete_task()
        {
            int id;
            string? input;
            Console.WriteLine("enter engineer id");
            input = Console.ReadLine();
            int.TryParse(input, out id);
            s_dalTask!.Delete(id);
        }
        private static DO.Task get_task(bool update, int id = 0)
        {
            int _engineer, i;
            string _description, _nickname, _product, _remarks;
            DateTime _production_date, _estimated_end, _start_date, _final_date, _actual_end;
            Task_level _level;
            Task_level[] levels = { Task_level.Super_easy, Task_level.Easy, Task_level.Moderate, Task_level.Hard, Task_level.Challenge };
            bool _milestone;
            Console.WriteLine("enter engineer id");
            int.TryParse(Console.ReadLine(), out _engineer);
            Console.WriteLine("write a description");
            _description = Console.ReadLine();
            Console.WriteLine("enter nickname");
            _nickname = Console.ReadLine();
            Console.WriteLine("enter product");
            _product = Console.ReadLine();
            Console.WriteLine("enter remarks");
            _remarks = Console.ReadLine();
            Console.WriteLine("enter Enter a production date in the form dd/mm/yy");
            DateTime.TryParse(Console.ReadLine(), out _production_date);
            Console.WriteLine(" Enter a estimated end date in the form dd/mm/yy");

            DateTime.TryParse(Console.ReadLine(), out _estimated_end);
            Console.WriteLine(" Enter a start date in the form dd/mm/yy");

            DateTime.TryParse(Console.ReadLine(), out _start_date);
            Console.WriteLine("Enter a final date in the form dd/mm/yy");
            DateTime.TryParse(Console.ReadLine(), out _final_date);
            Console.WriteLine("Enter a actual end date in the form dd/mm/yy");
            DateTime.TryParse(Console.ReadLine(), out _actual_end);
            Console.WriteLine("enter the task level");
            int.TryParse(Console.ReadLine(), out i);
            Console.WriteLine("enter _milestone");
            bool.TryParse(Console.ReadLine(), out _milestone);
            _level = levels[i];

            if (update)
                if ((_description == null) || (_level == null) || (_production_date == null) || (_estimated_end == null))
                    return null;
            DO.Task new_task = new(id, _description, _level, _production_date, _estimated_end, _milestone, _start_date, _final_date, _actual_end,
                   _nickname, _product, _remarks, _engineer);
            return new_task;
        }
        private static void create_task()
        {
            DO.Task new_task = get_task(false);
            s_dalTask!.Create(new_task);
        }
        private static void update_task()
        {
            int _id;
            Console.WriteLine("enter task id ");
            int.TryParse(Console.ReadLine(), out _id);
            DO.Task ?task = s_dalTask!.Read(_id);
            Console.WriteLine(task);
            DO.Task new_task = get_task(true, _id);
            if (new_task != null)
                s_dalTask.Update(new_task);
        }

        static void Main(string[] args)
        {
            Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependence);
            Console.WriteLine("Choose which entity you want to check:\r\n  1 engineer\r\n 2  task\r\n 3 dependence\r\n 0 exit menu");
            string choice;
            choice = Console.ReadLine();
            do
            {
               main_menu(choice);
                choice = Console.ReadLine();
            }
            while (choice != "0");

            
        }


    }
}
