using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoStore.Models
{
    interface IToDoRepository
    {
        IEnumerable<ToDo> GetAll();
        void SaveAll();
        ToDo Get(int id);
        ToDo Add(ToDo item);
        void Remove(int id);
        bool Update(ToDo item);
    }
}
