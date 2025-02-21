using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace Lime.app.irc.chat
{
  //irc chat - Client.
  class Client
  {
    static Socket? clientSocket;
    static Thread? listenThread;

    public void Client_irc()
    {
      Console.WriteLine("Подключение к серверу...");
      clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
      clientSocket.Connect(ipEndPoint);

      Console.WriteLine("Подключение установлено!");

      listenThread = new Thread(ListenServer);
      listenThread.Start();

      while (true)
      {
        string message = Console.ReadLine()!;
        byte[] data = Encoding.UTF8.GetBytes(message);
        clientSocket.Send(data);
      }
    }

    static void ListenServer()
    {
      while (true)
      {
        byte[] buffer = new byte[1024];
        int bytesRead = clientSocket!.Receive(buffer);
        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine("Сервер: " + message);
      }
    }
  }
}