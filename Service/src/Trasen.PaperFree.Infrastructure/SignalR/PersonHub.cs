using Microsoft.AspNetCore.SignalR;
using Trasen.PaperFree.Infrastructure.SeedWork.Redis;

namespace Trasen.PaperFree.Infrastructure.SignalR
{
    public class PersonHub : Hub
    {
        private readonly ICurrentUser currentUser;
        private readonly IRedisService redisService;

        public PersonHub(ICurrentUser currentUser, IRedisService redisService)
        {
            this.currentUser = currentUser;
            this.redisService = redisService;
        }

        public override async Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId}:已链接");
            await redisService.HSetAsync(RedisKeys.SignalRConnection, currentUser.Id, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            Console.WriteLine($"{Context.ConnectionId}:已断开");
            await redisService.HDelAsync(RedisKeys.SignalRConnection, new string[] { currentUser.Id });
            await base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// 接收客户端发来的信息，并向客户端发送信息
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public Task Send(string str)
        {
            return Clients.All.SendAsync("SendMessage", str);
        }

        public Task SendUser(string str)
        {
            return Clients.Client(Context.ConnectionId).SendAsync("SendMessage", str);
        }
    }
}