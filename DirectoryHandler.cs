using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace FileAndStreams
{
  public static class DirectoryHandler
  {
    public static void Render()
    {
      DirectoryInfo dir1 = new DirectoryInfo("dir1");
      DirectoryInfo dir2 = new DirectoryInfo("dir2");
      var allFilesInDir1 = dir1.GetFiles("*", SearchOption.AllDirectories);
      var allFilesInDir2 = dir2.GetFiles("*", SearchOption.AllDirectories);
      Dictionary<string, FileInfo> dictionaryDir1 = new Dictionary<string, FileInfo>();
      Dictionary<string, FileInfo> dictionaryDir2 = new Dictionary<string, FileInfo>();

      foreach (var fileDir1 in allFilesInDir1)
      {
        dictionaryDir1.Add(fileDir1.Name, fileDir1);
      }
      foreach (var fileDir2 in allFilesInDir2)
      {
        dictionaryDir2.Add(fileDir2.Name, fileDir2);
      }

      foreach (KeyValuePair<string, FileInfo> fileDir1 in dictionaryDir1)
      {
        if (dictionaryDir2.ContainsKey(fileDir1.Key) &&
        !GetFileHash(dictionaryDir2.GetValueOrDefault(fileDir1.Key).FullName)
        .Equals(GetFileHash(fileDir1.Value.FullName)))
        {
          OverriteFile(fileDir1.Value, dictionaryDir2.GetValueOrDefault(fileDir1.Key));
        }
        else
        {
          fileDir1.Value.CopyTo($"{dir2.Name}/{fileDir1.Key}");
        }
      }
      foreach (KeyValuePair<string, FileInfo> fileDir2 in dictionaryDir2)
      {
        if (!dictionaryDir1.ContainsKey(fileDir2.Key))
        {
          fileDir2.Value.Delete();
        }
      }
    }

    static byte[] GetFileHash(string path)
    {
      using (var md5 = MD5.Create())
      {
        using (var stream = File.OpenRead(path))
        {
          return md5.ComputeHash(stream);
        }
      }
    }

    static async void OverriteFile(FileInfo file1, FileInfo file2)
    {
      using (StreamWriter streamWriter = file2.CreateText())
      {
        using (StreamReader reader = file1.OpenText())
        {
          var text = await reader.ReadToEndAsync();
          await streamWriter.WriteAsync(text);
        }
      }
    }
  }
}