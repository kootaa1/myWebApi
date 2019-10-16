using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MyWebAPI.Models
{
    public class TodoTempStorage
    {

        private TodoTempStorage() { }

        private List<TodoItem> TodoStorage = new List<TodoItem>();

        private static TodoTempStorage _instance;

        public static TodoTempStorage GetInstance()
        {
            if (_instance == null)
            {
                _instance = new TodoTempStorage();

                _instance.AddItem(new TodoItem
                {
                    Id = 1,
                    IsComplete = true,
                    Name = "Do something 1"
                });
                _instance.AddItem(new TodoItem
                {
                    Id = 2,
                    IsComplete = false,
                    Name = "Do something 2"
                });
                _instance.AddItem(new TodoItem
                {
                    Id = 3,
                    IsComplete = true,
                    Name = "Do something 3"
                });
            }
            return _instance;
        }


        public bool AddItem(TodoItem item)
        {
            if (_instance.TodoStorage.Find(x => x.Name == item.Name) == null)
            {
                item.Id = _instance.TodoStorage.Count;
                item.IsComplete = false;
                _instance.TodoStorage.Add(item);
                return true;
            }
            return false;
        }

        public TodoItem GetItem(long ItemId)
        {
            return _instance.TodoStorage.Find(x => x.Id == ItemId);
        }

        public List<TodoItem> GetItems()
        {
            return _instance.TodoStorage;
        }

        public bool UpdateItem(TodoItem item)
        {
            int index = _instance.TodoStorage.FindIndex(x => x == item);
            if (index != -1)
            {
                _instance.TodoStorage[index].Id = item.Id;
                _instance.TodoStorage[index].Name = item.Name;
                _instance.TodoStorage[index].IsComplete = item.IsComplete;
                return true;
            }
            return false;
        }

        public TodoItem DeleteItem(long ItemId)
        {
            int index = _instance.TodoStorage.FindIndex(x => x.Id == ItemId);
            if (index != -1)
            {
                var result = _instance.TodoStorage[index];
                _instance.TodoStorage.Remove(_instance.TodoStorage[index]);
                for (int i = 0; i < _instance.TodoStorage.Count; i++)
                {
                    _instance.TodoStorage[i].Id = i;
                }
                return result;
            }
            return null;
        }
    }

}
