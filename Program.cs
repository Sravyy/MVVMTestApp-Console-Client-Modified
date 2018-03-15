using Microsoft.AspNet.SignalR.Client;
using MVVMTestApp.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMTestApp.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new HubConnection("http://localhost:52595/");
            var myHub = connection.CreateHubProxy("MyHub");

            connection.Start().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Console.WriteLine("There was an error opening the connection:{0}",
                                      task.Exception.GetBaseException());
                }
                else
                {
                    Console.WriteLine("Connected");
                }

            }).Wait();

            //myHub.On<List<SmsContactState>>("PassContactsToClient", contacts =>
            //{
            //    Console.WriteLine(contacts);
            //});
             var Contacts = myHub.Invoke<List<SmsContactState>>("GetContacts", "doesnt matter");
             Console.WriteLine(Contacts);
                Console.Read();
            connection.Stop();
        }
    }
}
