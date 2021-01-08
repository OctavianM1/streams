using System;
using System.IO;

namespace FileAndStreams
{
  public static class Train
  {
    public static void DriversInfo()
    {
      DriveInfo[] myDrives = DriveInfo.GetDrives();
      foreach (var d in myDrives)
      {
        Console.WriteLine("Name: {0}", d.Name);
        Console.WriteLine("Type: {0}", d.DriveType);
        if (d.IsReady)
        {
          Console.WriteLine("Free space: {0}", d.TotalFreeSpace);
          Console.WriteLine("Format: {0}", d.DriveFormat);
          Console.WriteLine("Label: {0}", d.VolumeLabel);
        }
      }
    }

    public static void FileSystemP()
    {
      FileSystemInfo fsi = new FileInfo("file.txt");
      Console.WriteLine(fsi.CreationTime);
      Console.WriteLine(fsi.LastAccessTime);
      System.Console.WriteLine(fsi.Name);
      System.Console.WriteLine(fsi.Extension);
    }

    public static void DirectoryInfoR()
    {
      DirectoryInfo di = new DirectoryInfo("directory");
      System.Console.WriteLine(di.Exists);
      var files = di.GetFiles();
      foreach (var file in files)
      {
        System.Console.WriteLine(file.Name);
      }
      di.Delete(true);
      DirectoryInfo dir = new DirectoryInfo("dir").Parent;
      System.Console.WriteLine(di.FullName);
      var dirs = di.GetFileSystemInfos("*f*");
      System.Console.WriteLine(dirs.Length);
      foreach(var d in dirs)
      {
        System.Console.WriteLine(d.Name);
      }
    }

    public static void FileInfoR()
    {
      FileInfo f1 = new FileInfo("file5.txt");
      System.Console.WriteLine(f1.Exists);
      System.Console.WriteLine(f1.Length);
      System.Console.WriteLine(f1.Name);
      System.Console.WriteLine(f1.DirectoryName);
      System.Console.WriteLine(f1.Directory);
      var a = f1.AppendText();
      a.Write("aaaaaaaaaaaaaaaaaaaaaaaaaa");
      a.WriteLine("qweeeeeeee");
      System.Console.WriteLine(f1.Exists);
      System.Console.WriteLine(f1.Length);
    }
  }
}