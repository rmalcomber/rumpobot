using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CommonLib
{
    public interface ICommandParser
    {
        string ParseCommand(Command command);

    }

    public class CommandParser:ICommandParser
    {
        private delegate string ParseCommandDelegate(string attribute, Command command);
        private IDictionary<string, ParseCommandDelegate> CommandDictionary = new ConcurrentDictionary<string, ParseCommandDelegate>();
        public CommandParser()
        {
            CommandDictionary.Add("{{DATE}}", ParseDate);
        }



        public string ParseCommand(Command command)
        {
            var response = command.Resp;

            var commandsInResponse = GetCommandsInString(response);

            foreach (var commandInResponse in commandsInResponse) {
                response = RunCommandParse(command, response, commandInResponse);
            }


            return response;
        }

        private string RunCommandParse(Command command, string response, string commandInResponse) {
            if (CommandDictionary.ContainsKey(commandInResponse)) {
                response = CommandDictionary[commandInResponse](commandInResponse, command);
            }

            return response;
        }

        private static IEnumerable<string> GetCommandsInString(string value)
        {
            var listOfCommands = new List<string>();

            var pattern = @"(\{\{\w*\}\})";

            var result = Regex.Matches(value, pattern);

            foreach (Match match in result)
            {
                listOfCommands.Add(match.Value);
            }

            return listOfCommands;
        }

        private static string ParseDate(string attribute, Command command)
        {
            return command.Resp.Replace(attribute, DateTime.Now.ToString(CultureInfo.InvariantCulture));
        }
    }
}
