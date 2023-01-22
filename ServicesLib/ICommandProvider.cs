using System.Collections.Generic;
using System.Linq;

namespace CommonLib
{
    public interface ICommandProvider
    {
        IEnumerable<Command> GetCommands();
        Command GetCommand(string command);

    }

    public class BasicCommandProvider: ICommandProvider
    {
        private List<Command> Commands { get; set; }
        public BasicCommandProvider()
        {
            Commands = new List<Command> {new Command() {Cmd = "ping", Resp = "Pong"}, new Command(){Cmd = "date", Resp = "The date is: {{DATE}}"}};
        }


        public IEnumerable<Command> GetCommands()
        {
            
            return Commands;
        }

        public Command GetCommand(string command)
        {
            return Commands.FirstOrDefault((c => "!" + c.Cmd == command));
        }
    }
}
