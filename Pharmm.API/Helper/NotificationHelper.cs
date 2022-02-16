using Pharmm.API.Helper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility.Notification.Helper
{
    public class NotificationHelper
    {
        private static SocketIO _client;

        public static async Task<bool> Send(string eventName, object data)
        {
            try
            {
                _client = new SocketIO(UrlStaticHelper.socketIOServer);
                bool result = false;

                _client.OnConnected += async (sender, e) =>
                {

                    await _client.EmitAsync(eventName, res =>
                    {
                        result = true;


                        Console.WriteLine(res);
                        Console.ReadLine();

                    }, data);

                };


                await _client.ConnectAsync();

                //#problem solving : jika emit datanya duplicate (2x atau lebih) penyebabnya task.delay tidak boleh lebih dari 1, letakkan task.delay sebelum return
                await Task.Delay(400);
                await _client.DisconnectAsync();

                _client.Dispose();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<T> Receive<T>(string eventName)
        {
            try
            {
                T result = default(T);

                _client.OnConnected += (sender, e) =>
                {
                    _client.On(eventName, response =>
                    {
                        result = response.GetValue<T>();
                        Console.WriteLine(result);
                        Console.ReadLine();
                    });
                };

                if (!_client.Connected)
                    await _client.ConnectAsync();

                await _client.DisconnectAsync();

                await Task.Delay(400);
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
