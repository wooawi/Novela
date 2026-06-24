using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms; 

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
        private static readonly string Dir =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Saves");

        private static string PathSlot(int slot)
            => Path.Combine(Dir, $"save_{slot}.json");

        public static void Ensure()
        {
            if (!Directory.Exists(Dir))
                Directory.CreateDirectory(Dir);
        }

        public static bool Exists(int slot)
            => File.Exists(PathSlot(slot));

        public static bool SaveToSlot(Save data, int slot)
        {
            try
            {
                Ensure();
                data.SaveTime = DateTime.Now;
                string json = JsonSerializer.Serialize(data, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(PathSlot(slot), json);
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка сохранения: {ex.Message}");
                return false;
            }
        }

        public static Save Load(int slot)
        {
            string path = PathSlot(slot);
            if (!File.Exists(path)) return null;
            try
            {
                string json = File.ReadAllText(path);
                return JsonSerializer.Deserialize<Save>(json);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Ошибка загрузки: {ex.Message}");
                return null;
            }
        }
    }
}