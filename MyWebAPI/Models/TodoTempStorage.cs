using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyWebAPI.Models
{
    public class TodoTempStorage
    {
        private List<TodoItem> TodoStorage = new List<TodoItem>();

        public bool AddItem(TodoItem item)
        {
            if (TodoStorage.Find(x => x == item) == null)
            {
                TodoStorage.Add(item);
                return true;
            }
            return false;
        }

        public TodoItem GetItem(long ItemId)
        {
            return TodoStorage.Find(x => x.Id == ItemId);
        }

        public List<TodoItem> GetItems()
        {
            return TodoStorage;
        }

        public bool UpdateItem(TodoItem item)
        {
            int index = TodoStorage.FindIndex(x => x == item);
            if (index != -1)
            {
                TodoStorage[index].Id = item.Id;
                TodoStorage[index].Name = item.Name;
                TodoStorage[index].IsComplete = item.IsComplete;
                return true;
            }
            return false;
        }

        public bool DeleteItem(long ItemId)
        {
            int index = TodoStorage.FindIndex(x => x.Id == ItemId);
            if (index != -1)
            {
                TodoStorage.Remove(TodoStorage[index]);
                return true;
            }
            return false;
        }
    }

}
