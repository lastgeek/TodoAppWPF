using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Models;

namespace ToDoApp.Services
{
    internal class FileIOService
    {
        private readonly string _filePath;

        public FileIOService(string filePath)
        {
            _filePath = filePath;
        }
        public BindingList<TodoModel> LoadData()
        {
            var fileExists = File.Exists(_filePath);
            if (!fileExists)
            {
                File.CreateText(_filePath).Dispose();
                return new BindingList<TodoModel>();
            }
            using (var reader = File.OpenText(_filePath))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
            }
        }

        public void SaveData(object todoDataList)
        {
            using (StreamWriter writer = File.CreateText(_filePath))
            {
                string output = JsonConvert.SerializeObject(todoDataList);
                writer.Write(output);
            }
        }
    }
}
