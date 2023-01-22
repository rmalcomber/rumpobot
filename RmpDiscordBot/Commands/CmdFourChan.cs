using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Discord.WebSocket;
using FChan.Library;
using RmpDiscordBot.Dtos;

namespace RmpDiscordBot.Commands
{
    class CmdFourChan : ICommandMessage
    {

        readonly Random _random = new Random();

        public string GetCommand()
        {
            return "!ylyl";
        }

        public async Task RunCommand(SocketMessage socketMessage)
        {
            try
            {
                var thread = await FindAYlylThread();
                if (thread != null) {
                    int r = _random.Next(thread.Posts.Count);
                    var post = thread.Posts[r];
                    var imageUrl = Constants.GetImageUrl("b", post.FileName, post.FileExtension);
                    var path = $"C:\\Users\\royst\\Pictures\\Temp\\{post.FileName}{post.FileExtension}";
                    if (File.Exists(path) == false) {

                        using (var client = new WebClient()) {
                            await Task.Run(() => client.DownloadFile(new Uri(imageUrl), path));
                        }
                    }

                    await socketMessage.Channel.SendFileAsync(path);
                }
                else
                {
                    await socketMessage.Channel.SendMessageAsync("Nope, nothing at the moment");
                }
            }
            catch (Exception e)
            {
                await socketMessage.Channel.SendMessageAsync("Sorry I'm not funny at the moment");
            }
        }

        public async Task<Thread> FindAYlylThread() {
            for (var i = 1; i <= 10; i++) {
                var pageNo = i;
                var threadPage = await Chan.GetThreadPageAsync("b", pageNo);
                var res = SearchForYlyl(threadPage);
                if (res != null) {
                    return res;

                }
            }

            return null;
        }

        public Thread SearchForYlyl(ThreadRootObject thread) {
            foreach (var threadThread in thread.Threads) {
                if (threadThread.Posts.First().Comment.ToLower().Contains("ylyl")) {
                    return threadThread;
                }
            }

            return null;
        }
    }
}
