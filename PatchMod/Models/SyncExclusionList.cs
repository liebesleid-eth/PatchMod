﻿using PatchMod.Modules;
using System.Collections.Generic;
using System.IO;

namespace PatchMod.Models
{
    public class SyncExclusionList
    {
        public List<SyncExclusion> Exclusions = new List<SyncExclusion>();

        public void LoadFromFile(string path)
        {
            Exclusions = new List<SyncExclusion>();
            foreach(string _ExLen in File.ReadAllLines(path))
            {
                string Line = _ExLen.Trim(' ');
                if (Line.Length != 0 && !Line.StartsWith("#"))
                {
                    string Name = Line.TrimStart('!');
                    SyncExclusion NE = new SyncExclusion();
                    NE.Path = Name;
                    System.Console.WriteLine($">{Line}");
                    NE.Mode = Line.StartsWith("!");
                    Exclusions.Add(NE);
                }
            }
        }
        public bool SyncPath(string LocalPath)
        {
            string Rel = LocalPath.ToLower().Trim('\\');
            bool Stat = true;
            foreach (SyncExclusion Exclusion in Exclusions)
            {
                if (Exclusion.Path.ToLower() == Rel)
                {
                    Stat = Exclusion.Mode;
                    break;
                }
            }
            if (!Stat)
            {
                LogClient.LogMessage($"Sync Exlcusion for file {Rel}");
            }
            return Stat;
        }
    }
}