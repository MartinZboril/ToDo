using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class dataService
    {
        HttpClient client = new HttpClient();

        public async Task<ObservableCollection<ToDo>> GetToDoListAsync()
        {
            var uri = new Uri(string.Format("http://martinzboril.cz/ToDo_API/API.php/GET", string.Empty));
            var response = await client.GetAsync(uri);
            ObservableCollection<ToDo> todo = new ObservableCollection<ToDo>();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                todo = JsonConvert.DeserializeObject<ObservableCollection<ToDo>>(content);
            }

            return todo;
        }

        public async Task <ToDo> GetOneToDoAsync(int ID)
        {
            var uri = new Uri(string.Format("http://martinzboril.cz/ToDo_API/API.php/"+ID, string.Empty));
            var response = await client.GetAsync(uri);
            ToDo todo = new ToDo();
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                todo = JsonConvert.DeserializeObject<ToDo>(content);
            }

            return todo;
        }

        public async Task PostToDo(ToDo item)
        {
            var uri = new Uri(string.Format("http://martinzboril.cz/ToDo_API/API.php/POST", string.Empty));

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {

            }
        }

        public async Task UpdateToDoasync(ToDo item)
        {
            var uri = new Uri(string.Format("http://martinzboril.cz/ToDo_API/API.php/UPDATE", string.Empty));

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {

            }

        }

        public async Task DeleteToDoAsync(ToDo item)
        {
            var uri = new Uri(string.Format("http://martinzboril.cz/ToDo_API/API.php/DELETE", string.Empty));

            var json = JsonConvert.SerializeObject(item);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {

            }

        }
    }
}
