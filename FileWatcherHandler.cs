using System;
using System.IO;
using System.Linq;

namespace FileAndStreams
{
  public class FileWatcherHandler
  {
    private FileSystemWatcher _fileWatcher = new FileSystemWatcher("dir1");
    private FileSystemWatcher _dirWatcher = new FileSystemWatcher("dir1");
    private DirectoryInfo dir2 = new DirectoryInfo("dir2");

    private void initWatcher()
    {
      _fileWatcher.IncludeSubdirectories = true;
      _fileWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.LastWrite;
      _fileWatcher.EnableRaisingEvents = true;
      _fileWatcher.Deleted += OnDeleted;
      _fileWatcher.Changed += OnChanged;
      _fileWatcher.Created += OnCreated;
      _fileWatcher.Renamed += OnRenamed;

      _dirWatcher.IncludeSubdirectories = true;
      _dirWatcher.NotifyFilter = NotifyFilters.DirectoryName;
      _dirWatcher.EnableRaisingEvents = true;
      _dirWatcher.Deleted += OnDeleted;
      _dirWatcher.Changed += OnChanged;
      _dirWatcher.Created += OnCreated;
      _dirWatcher.Renamed += OnRenamed;

      Console.ReadLine();
    }
    public void Render()
    {
      initWatcher();
    }

    private void OnChanged(object source, FileSystemEventArgs e)
    {
      try
      {
        if (source == _dirWatcher)
        {
          DirectoryCopy(e.FullPath, e.FullPath.Replace("dir1", "dir2"));
        }
        else
        {
          File.Copy(e.FullPath, e.FullPath.Replace("dir1", "dir2"), true);
        }
      }
      catch (Exception ex)
      {
        System.Console.WriteLine(ex.Message);
      }
    }

    private void DirectoryCopy(string sourceDirName, string destDirName)
    {
      DirectoryInfo dir = new DirectoryInfo(sourceDirName);
      DirectoryInfo[] dirs = dir.GetDirectories();
      Directory.CreateDirectory(destDirName);

      FileInfo[] files = dir.GetFiles();
      foreach (FileInfo file in files)
      {
        string tempPath = Path.Combine(destDirName, file.Name);
        file.CopyTo(tempPath, false);
      }
      foreach (DirectoryInfo subdir in dirs)
      {
        string tempPath = Path.Combine(destDirName, subdir.Name);
        DirectoryCopy(subdir.FullName, tempPath);
      }
    }



    private void OnCreated(object source, FileSystemEventArgs e)
    {
      try
      {
        if (source == _dirWatcher)
        {
          Directory.CreateDirectory(e.FullPath.Replace("dir1", "dir2"));
        }
        else
        {
          File.Create(e.FullPath.Replace("dir1", "dir2"));
        }
      }
      catch (Exception ex)
      {
        System.Console.WriteLine(ex.Message);
      }
    }

    private void OnDeleted(object source, FileSystemEventArgs e)
    {
      try
      {
        if (source == _dirWatcher)
        {
          string dirName = dir2.GetDirectories(e.Name, SearchOption.AllDirectories).FirstOrDefault().FullName;
          Directory.Delete(dirName, true);
        }
        else
        {
          dir2.GetFiles(e.Name, SearchOption.AllDirectories).FirstOrDefault().Delete();
        }
      }
      catch (Exception ex)
      {
        System.Console.WriteLine(ex.Message);
      }
    }

    private void OnRenamed(object source, RenamedEventArgs e)
    {
      try
      {
        if (source == _dirWatcher)
        {
          var dir2PathD = dir2.GetDirectories(e.OldName, SearchOption.AllDirectories).FirstOrDefault().FullName;
          Directory.Move(dir2PathD, e.FullPath.Replace("dir1", "dir2"));
        }
        else
        {
          var dir2PathF = dir2.GetFiles(e.OldName, SearchOption.AllDirectories).FirstOrDefault().FullName;
          File.Move(dir2PathF, e.FullPath.Replace("dir1", "dir2"));
        }
      }
      catch (Exception ex)
      {
        System.Console.WriteLine(ex.Message);
      }

    }
  }
}