namespace Dal;
using DO;

internal static class DataSource
{
    internal static class Config
    {
        // task Continuous number

        internal const int start_task_id = 1;
        private static int next_task_id = start_task_id;
        internal static int Next_task_id { get => next_task_id++; }
        // dependence Continuous number

        internal const int start_dependence_id = 1;
        private static int next_dependence_id = start_dependence_id;
        internal static int Next_dependence_id { get => next_dependence_id++; }

    }
    internal static List<DO.Task>? Tasks{ get; } = new(100);
    internal static List<DO.Engineer>? Engineers { get; } = new(40);
    internal static List<DO.Dependence>? Dependences { get; } = new(250);

}
