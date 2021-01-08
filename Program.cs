using System;
using System.IO;

namespace FileAndStreams
{
  class Program
  {

    static void Main(string[] args)
    {
      // DirectoryHandler.Render();
      
      FileWatcherHandler fileWatcher = new FileWatcherHandler();
      fileWatcher.Render();
      Console.ReadLine();
    }
  }
}
