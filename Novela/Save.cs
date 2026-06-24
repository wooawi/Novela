using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace project
{

    public class Save
    {
        public int DialogListIndex { get; set; }
        public int DialogueIndex { get; set; }
        public int CurrentSprites { get; set; } = -1;
        public int CurrentBackground { get; set; } = -1;
        public string HistoryText { get; set; } = "";
        public List<string> UnlockedEndings { get; set; } = new();
        public List<string> ChoicesMade { get; set; } = new();
        public DateTime SaveTime { get; set; }
    }

    
    public static class SaveSystem
    {
        private static readonly string SaveDirectory =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Saves");

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            WriteIndented = true
        };

        private static string PathForSlot(int slot) =>
            Path.Combine(SaveDirectory, $"save_{slot}.json");

        private static void EnsureDirectory()
        {
            if (!Directory.Exists(SaveDirectory))
                Directory.CreateDirectory(SaveDirectory);
        }

        public static bool Exists(int slot)
        {
            return File.Exists(PathForSlot(slot));
        }

        public static bool SaveToSlot(Save data, int slot)
        {
            try
            {
                EnsureDirectory();
                data.SaveTime = DateTime.Now;
                string json = JsonSerializer.Serialize(data, JsonOptions);
                File.WriteAllText(PathForSlot(slot), json);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка сохранения (слот {slot}): {ex.Message}");
                return false;
            }
        }

        public static Save Load(int slot)
        {
            string path = PathForSlot(slot);
            if (!File.Exists(path)) return null;

            try
            {
                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<Save>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки (слот {slot}): {ex.Message}");
                return null;
            }
        }

        public static bool DeleteSave(int slot)
        {
            string path = PathForSlot(slot);
            if (!File.Exists(path)) return false;
            File.Delete(path);
            return true;
        }

        public static List<int> GetSaves()
        {
            var slots = new List<int>();
            if (!Directory.Exists(SaveDirectory)) return slots;

            foreach (var file in Directory.GetFiles(SaveDirectory, "save_*.json"))
            {
                string name = Path.GetFileNameWithoutExtension(file); 
                string numPart = name.Replace("save_", "");
                if (int.TryParse(numPart, out int slot))
                    slots.Add(slot);
            }

            slots.Sort();
            return slots;
        }
    }
}
