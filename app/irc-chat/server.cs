using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
namespace Lime.app.irc.chat
{
  //irc chat - Server.
  class Server
  {
    static Socket? serverSocket;
    static Socket? clientSocket;
    static Thread? listenThread;
    public void Server_irc()
    {
      Console.WriteLine("Запуск сервера...");
      serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 8888);
      serverSocket.Bind(ipEndPoint);
      serverSocket.Listen(10);

      Console.WriteLine("Ожидание подключения клиента...");
      clientSocket = serverSocket.Accept();
      Console.WriteLine("Клиент подключён!");

      listenThread = new Thread(ListenClient);
      listenThread.Start();

      while (true)
      {
        string message = Console.ReadLine()!;
        byte[] data = Encoding.UTF8.GetBytes(message);
        clientSocket.Send(data);
      }
    }
    static void ListenClient()
    {
      while (true)
      {
        byte[] buffer = new byte[1024];
        int bytesRead = clientSocket!.Receive(buffer);
        string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
        Console.WriteLine($"Клиент: {message}");
      }
    }
  }
}