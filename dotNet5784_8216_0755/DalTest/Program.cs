using DalApi;
using Dal;
using DO;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace DalTest
{
    internal class Program
    {
        private static readonly IDal s_dal = new DalList();
        private static void main_menu(string choice)
        {
            try
            {
                switch (choice)
                {
                    case "0":
                        break;
                    case "1":
                        engineer_menu();
                        break;
                    case "2":
                        task_menu();
                        break;
                    case "3":
                        dependence_menu();
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// <summary>
        /// The functions <entity>_menu represent the menu of the specific entity
        /// </summary>
        private static void engineer_menu()
        {
            Console.WriteLine("Select the method you want to perform:\r\n  1 create new engineer\r\n 2  read the  engineer by id \r\n 3 read all the objects of the engineer type \r\n 4 update the engineer\r\n 5 delete the engineer\r\n 0 exit menu");
            string choice;
            choice = Console.ReadLine()!;
            switch (choice)
            {
                case "1":
                    create_engineer();
                    break;
                case "2":
                    read_engineer();
                    break;
                case "3":
                    read_engineers();
                    break;
                case "4":
                    update_engineer();
                    break;
                case "5":
                    delete_engineer();
                    break;

            }
        }
        private static void dependence_menu()
        {
            Console.WriteLine("Select the method you want to perform:\r\n  1 create new dependence\r\n 2  read the  dependence by id \r\n 3 read all the objects of the dependence type \r\n 4 update the dependence\r\n 5 delete the dependence\r\n 0 exit menu");
            string choice;
            choice = Console.ReadLine()!;
            switch (choice)
            {
                case "1":
                    create_dependence();
                    break;
                case "2":
                    read_dependence();
                    break;
                case "3":
                    read_dependences();
                    break;
                case "4":
                    update_dependence();
                    break;
                case "5":
                    delete_dependence();
                    break;

            }
        }
        private static void task_menu()
        {
            Console.WriteLine("Select the method you want to perform:\r\n  1 create new task\r\n 2  read the  task by id \r\n 3 read all the objects of the task type \r\n 4 update the task\r\n 5 delete the task\r\n 0 exit menu");
            string? choice;
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
                    read_tasks();
                    break;
                case "4":
                    update_task();
                    break;
                case "5":
                    delete_task();
                    break;

            }
        }
        /// <summary>
        /// The functions read_<entitys> prints the entity list
        /// </summary>
        private static void read_tasks()
        {
            List<DO.Task> tasks = s_dal.task!.ReadAll().ToList();
            int i = 1;
            foreach (var task in tasks)
            {
                Console.WriteLine("task number" + (i++) + "\r\n" + task + "\r\n");
            }
        }
        private static void read_dependences()
        {
            List<DO.Dependence> dependences = s_dal.dependence!.ReadAll().ToList();
            int i = 1;
            foreach (var dependence in dependences)
            {
                Console.WriteLine("dependence number" + (i++) + "\r\n" + dependence + "\r\n");
            }
        }
        private static void read_engineers()
        {
            List<DO.Engineer> engineers = s_dal.engineer!.ReadAll().ToList();
            int i = 1;
            foreach (var engineer in engineers)
            {
                Console.WriteLine("engineer number" + (i++) + "\r\n" + engineer + "\r\n");
            }
        }
        /// <summary>
        /// The functions read_<entity> print the chosen entity by id
        /// </summary>
        private static void read_engineer()
        {
            int _id;
            Console.WriteLine("enter engineer id");
            int.TryParse(Console.ReadLine(), out _id);
            DO.Engineer? engineer = s_dal.engineer!.Read(_id);
            Console.WriteLine(engineer);
        }
        private static void read_dependence()
        {
            int _id;
            Console.WriteLine("enter dependence id");
            int.TryParse(Console.ReadLine(), out _id);
            DO.Dependence? dependence = s_dal.dependence!.Read(_id);
            Console.WriteLine(dependence);
        }
        private static void read_task()
        {
            int _id;
            Console.WriteLine("enter task id");
            int.TryParse(Console.ReadLine(), out _id);
            DO.Task? task = s_dal.task!.Read(_id);
            Console.WriteLine(task);
        }
        /// <summary>
        /// The functions delete_<entity> deletes the chosen entity by id
        /// </summary>
        private static void delete_task()
        {
            try
            {
                int id;
                Console.WriteLine("enter task id");
                int.TryParse(Console.ReadLine(), out id);
                s_dal.task!.Delete(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private static void delete_dependence()
        {
            try
            {
                int id;
                Console.WriteLine("enter dependence id");
                int.TryParse(Console.ReadLine(), out id);
                s_dal.dependence!.Delete(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private static void delete_engineer()
        {
            try
            {
                int id;
                Console.WriteLine("enter engineer id");
                int.TryParse(Console.ReadLine(), out id);
                s_dal.engineer!.Delete(id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        /// <summary>
        /// The functions get_<entity> gets the entity param from the user
        /// </summary>
        private static DO.Dependence get_dependence()
        {
            int _next_task, _prev_task;

            Console.WriteLine("enter next task id");
            int.TryParse(Console.ReadLine(), out _next_task);
            Console.WriteLine("enter  prev task id");
            int.TryParse(Console.ReadLine(), out _prev_task);

            DO.Dependence new_dependence = new(0, _next_task, _prev_task);
            return new_dependence;

        }
        private static DO.Engineer get_engineer(bool update = false)
        {
            int _engineer_id = 0, _cost_per_hour;
            string _name, _email;
            Level _degree;
            bool _is_active = true;
            if (!update)
            {
                Console.WriteLine("enter engineer id");
                int.TryParse(Console.ReadLine(), out _engineer_id);
            }
            Console.WriteLine("enter engineer name");
            _name = Console.ReadLine();
            Console.WriteLine("enter engineer email");
            _email = Console.ReadLine();
            Console.WriteLine("enter engineer cost per hour");
            int.TryParse(Console.ReadLine(), out _cost_per_hour);
            Console.WriteLine("enter engineer dgree");
            Level.TryParse(Console.ReadLine(), out _degree);
            DO.Engineer new_engineer = new(_engineer_id, _name, _email, _degree, _cost_per_hour, _is_active);
            return new_engineer;
        }
        private static DO.Task get_task(bool update = false)
        {

            int _engineer;
            string _description, _nickname, _product, _remarks, mailston;
            DateTime _production_date, _estimated_end, _start_date, _final_date, _actual_end;
            Level _level;
            bool _milestone;
            Console.WriteLine("write a description");
            _description = Console.ReadLine();
            Console.WriteLine("enter nickname");
            _nickname = Console.ReadLine()!;
            Console.WriteLine("enter engineer id");
            int.TryParse(Console.ReadLine(), out _engineer);
            Console.WriteLine("enter product");
            _product = Console.ReadLine();
            Console.WriteLine("enter remarks");
            _remarks = Console.ReadLine();
            Console.WriteLine(" Enter a production date in the form dd/mm/yy");
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
            Level.TryParse(Console.ReadLine(), out _level);
            if (!update)
            {
                Console.WriteLine("enter yes or no if you have any  milestone");
                mailston = Console.ReadLine();
                _milestone = mailston == "yes" ? true : false;
            }
            else
            {
                Console.WriteLine("enter yes or no if you want to change the milestone status");
                mailston = Console.ReadLine();
                _milestone = mailston == "yes" ? true : false;
            }
            DO.Task new_task = new(0, _description, _level, _production_date, _estimated_end, _milestone, _start_date, _final_date, _actual_end,
                   _nickname, _product, _remarks, _engineer);
            return new_task;
        }
        /// <summary>
        /// The functions create_<entity>  creates an entity with all is param which we got in the get functions
        /// </summary>
        private static void create_engineer()
        {
            DO.Engineer new_engineer = get_engineer();
            s_dal.engineer!.Create(new_engineer);
        }
        private static void create_dependence()
        {
            DO.Dependence new_dependence = get_dependence();
            s_dal.dependence!.Create(new_dependence);
        }
        private static void create_task()
        {
            DO.Task new_task = get_task();
            s_dal.task!.Create(new_task);
        }
        /// <summary>
        /// The functions update_<entity>  updates an entity by id with  get functions that takes the param to change
        /// </summary>
        private static void update_engineer()
        {
            try
            {
                int _id;
                Console.WriteLine("enter engineer id");
                int.TryParse(Console.ReadLine(), out _id);
                DO.Engineer? engineer = s_dal.engineer!.Read(_id);
                Console.WriteLine(engineer);
                DO.Engineer new_engineer = get_engineer(true);
                bool _is_active = true;
                int _cost_per_hour = new_engineer.cost_per_hour != 0 ? new_engineer.cost_per_hour : engineer.cost_per_hour;
                string _name = new_engineer.name != "" ? new_engineer.name : engineer.name;
                string _email = new_engineer.email != "" ? new_engineer.email : engineer.email;
                Level _degree = new_engineer.degree != 0 ? new_engineer.degree : engineer.degree;
                DO.Engineer updated_engineer = new(_id, _name, _email, _degree, _cost_per_hour, _is_active);
                s_dal.engineer!.Update(updated_engineer);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private static void update_dependence()
        {
            try
            {
                int _id;
                Console.WriteLine("enter dependence id");
                int.TryParse(Console.ReadLine(), out _id);
                DO.Dependence? dependence = s_dal.dependence!.Read(_id);
                Console.WriteLine(dependence);
                DO.Dependence? new_dependence = get_dependence();
                int _next_task = new_dependence.next_task != 0 ? new_dependence.next_task : dependence.next_task;
                int _prev_task = new_dependence.prev_task != 0 ? new_dependence.prev_task : dependence.prev_task;
                DO.Dependence updated_dependence = new(_id, _next_task, _prev_task);
                s_dal.dependence!.Update(updated_dependence);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private static void update_task()
        {
            try
            {
                int _id;
                Console.WriteLine("enter task id");
                int.TryParse(Console.ReadLine(), out _id);
                DO.Task? task = s_dal.task!.Read(_id);
                Console.WriteLine(task);
                DO.Task new_task = get_task();
                Level _level = new_task.level != 0 ? new_task.level : task.level;
                int? _engineer = new_task.engineer != 0 ? new_task.engineer : task.engineer;
                string? _description = new_task.description != "" ? new_task.description : task.description;
                string? _nickname = new_task.nickname != "" ? new_task.nickname : task.nickname;
                string? _product = new_task.product != "" ? new_task.product : task.product;

                string? _remarks = new_task.remarks != "" ? new_task.remarks : task.remarks;
                bool _milestone = new_task.milestone == true ? !task.milestone : task.milestone;
                DateTime _production_date = new_task.production_date != DateTime.MinValue ? new_task.production_date : task.production_date;
                DateTime _estimated_end = new_task.estimated_end != DateTime.MinValue ? new_task.estimated_end : task.estimated_end;
                DateTime? _start_date = new_task.start_date != DateTime.MinValue ? new_task.start_date : task.start_date;
                DateTime? _final_date = new_task.final_date != DateTime.MinValue ? new_task.final_date : task.final_date;
                DateTime? _actual_end = new_task.actual_end != DateTime.MinValue ? new_task.actual_end : task.actual_end;
                DO.Task updated_task = new(_id, _description, _level, _production_date, _estimated_end, _milestone, _start_date, _final_date, _actual_end,
                       _nickname, _product, _remarks, _engineer);
                s_dal.task!.Update(updated_task);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }
        static void Main(string[] args)
        {
            Initialization.Do(s_dal);
            Console.WriteLine("Choose which entity you want to check:\r\n  1 engineer\r\n 2  task\r\n 3 dependence\r\n 0 exit menu");
            string? choice;
            choice = Console.ReadLine();
            do
            {
                main_menu(choice);
                Console.WriteLine("Choose which entity you want to check:\r\n  1 engineer\r\n 2  task\r\n 3 dependence\r\n 0 exit menu");
                choice = Console.ReadLine();
            }
            while (choice != "0");
        }
    }
}
