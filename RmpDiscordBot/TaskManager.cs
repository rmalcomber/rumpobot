using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RmpDiscordBot
{
    public interface ITaskManager
    {
        Task[] GetAllTasks();
        void AddTask(Task task);
    }
    public class TaskManager : ITaskManager
    {
        private readonly IList<Task> _tasks = new List<Task>();
        public Task[] GetAllTasks()
        {
            return _tasks.ToArray();
        }

        public void AddTask(Task task)
        {
            _tasks.Add(task);
        }
    }
}
