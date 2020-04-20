using TextCopy;
using CommandLine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json.Linq;

namespace ToClipboard
{
    class Program
    {
        static string CONFIG_FILE_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "customs.json");
        static string CONFIG_BASE_FORMAT = @"{}";
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
              .WithParsed(o =>
              {
                  if (o.Custom)
                  {
                      if (File.Exists(CONFIG_FILE_PATH))
                      {
                          JObject jo = JObject.Parse(File.ReadAllText(CONFIG_FILE_PATH));
                          string val = jo[o.CustomKey].Value<string>();
                          Clipboard.SetText(val);
                          Console.WriteLine("Copy \"" + val + "\" To Clipboard");
                      }
                  }
                  else if (o.DeleteCustom)
                  {
                      if (!File.Exists(CONFIG_FILE_PATH))
                      {
                          File.WriteAllText(CONFIG_FILE_PATH, CONFIG_BASE_FORMAT);
                      }
                      string key = o.CustomKey ?? throw new ArgumentNullException();
                      JObject jo = JObject.Parse(File.ReadAllText(CONFIG_FILE_PATH));
                      if(!jo.ContainsKey(key))
                      {
                          Console.WriteLine("No such a custom key exist");
                          return;   
                      }
                      jo.Remove(key);
                      File.WriteAllText(CONFIG_FILE_PATH, jo.ToString());
                      Console.WriteLine("Successfully deleted");
                  }
                  else if (o.SetCustom)
                  {
                      if (!File.Exists(CONFIG_FILE_PATH))
                      {
                          File.WriteAllText(CONFIG_FILE_PATH, CONFIG_BASE_FORMAT);
                      }
                      string key = o.CustomKey ?? throw new ArgumentNullException();
                      string value = o.CustomValue ?? throw new ArgumentNullException();
                      JObject jo = JObject.Parse(File.ReadAllText(CONFIG_FILE_PATH));
                      if(jo.ContainsKey(key))
                      {
                          Console.WriteLine("Same key already exits");
                          return;   
                      }
                      jo[key] = value;

                      File.WriteAllText(CONFIG_FILE_PATH, jo.ToString());
                      Console.WriteLine("Successfully created");
                  }
                  else if (o.EnvVariable)
                  {
                      var dic = new Dictionary<string, string>();
                      foreach (DictionaryEntry env in Environment.GetEnvironmentVariables())
                      {
                          dic.Add(env.Key.ToString(), env.Value.ToString());
                      }
                      try
                      {
                          var val = dic[o.Input];
                          Clipboard.SetText(val);
                          Console.WriteLine("Copy \"" + val + "\" To Clipboard");
                      }
                      catch (KeyNotFoundException)
                      {
                          Console.WriteLine("No Enviroment Variable");
                      }
                  }
                  else if (o.Print)
                  {
                      string text = Clipboard.GetText();
                      Console.WriteLine(text ?? "No clipboard contents"); 
                  }
              });
        }
    }
}
