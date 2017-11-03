using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class ToDoDatabase
    {// SQLite connection
        private SQLiteAsyncConnection database;

        public ToDoDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ToDo>().Wait();
        }

        // Query
        public Task<List<ToDo>> GetItemsAsync()
        {
            return database.Table<ToDo>().ToListAsync();
        }

        // Query using SQL query string
        public Task<List<ToDo>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<ToDo>("SELECT * FROM [ToDo]");
        }

        // Query using LINQ
        public Task<ToDo> GetItemAsync(int id)
        {
            return database.Table<ToDo>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ToDo item)
        {

            /*if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {*/
                return database.InsertAsync(item);
           // }

        }

        public Task<int> DeleteItemAsync(ToDo item)
        {
            return database.DeleteAsync(item);
        }
    }
}