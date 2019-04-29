using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace ToDoStore.Models
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly string file = "todos.xml";
        private List<ToDo> todos = new List<ToDo>();
        private int _nextId = 1;

        public ToDoRepository()
        {
            LoadToDoList();
        }

        private List<ToDo> LoadToDoList()
        {
            if (File.Exists(file))
            {
                var serializer = new XmlSerializer(typeof(List<ToDo>));
                using (var s = File.OpenRead(file))
                {
                    todos = (List<ToDo>)serializer.Deserialize(s);
                }
            }
            else
            {
                var defaultToDoList = new[]
                {
                    "Use ToDo app to add, edit, and delete my to do list.",
                };
                todos = defaultToDoList.Select(f => new ToDo() { Description = f, Id = 0, Title = "Start My List", DueDate = DateTime.Today }).ToList();
            }
            return todos;
        }

        public void SaveAll()
        {
            //Write the todolist back to storage...

            var serializer = new XmlSerializer(typeof(List<ToDo>));
            using (var s = File.Create(file))
            {
                serializer.Serialize(s, todos);
            }
        }


        public IEnumerable<ToDo> GetAll()
        {
            return todos;
        }

        public ToDo Get(int id)
        {
            return todos.Find(p => p.Id == id);
        }

        public ToDo Add(ToDo item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            item.Id = _nextId++;
            todos.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            todos.RemoveAll(p => p.Id == id);
        }

        public bool Update(ToDo item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            int index = todos.FindIndex(p => p.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            todos.RemoveAt(index);
            todos.Add(item);
            return true;
        }
    }
}