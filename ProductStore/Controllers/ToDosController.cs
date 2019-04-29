using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ToDoStore.Models;

namespace ToDoStore.Controllers
{
    public class ToDosController : ApiController
    {
        static readonly IToDoRepository repository = new ToDoRepository();

        /// <summary>
        /// GetAllToDos will return all of the items in the to do list.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ToDo> GetAllToDos()
        {
            return repository.GetAll();
        }

        /// <summary>
        /// GetToDo handles the [HttpGet] verb and returns a specific To Do by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ToDo GetToDo(int id)
        {
            ToDo item = repository.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        /// <summary>
        /// GetToDosByDueDate shows how to return a list of todo's for a given due date.
        /// </summary>
        /// <param name="duedate"></param>
        /// <returns></returns>
        public IEnumerable<ToDo> GetToDosByDueDate(DateTime duedate)
        {
            return repository.GetAll().Where(
                p => DateTime.Equals(p.DueDate, duedate));
        }

        /// <summary>
        /// PostToDo handles the [HttpPost] verb and adds a new to do list item
        /// and returns the new item's Id value.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public HttpResponseMessage PostToDo(ToDo item)
        {
            item = repository.Add(item);
            var response = Request.CreateResponse<ToDo>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        /// <summary>
        /// PutToDo handles the [HttpPut] verb and updated a to list item based on Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todo"></param>
        public void PutToDo(int id, ToDo todo)
        {
            todo.Id = id;
            if (!repository.Update(todo))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// DeleteToDo handles the [HttpDelete] verb and accepts an ID to delete a To List item.
        /// </summary>
        /// <param name="id"></param>
        public void DeleteToDo(int id)
        {
            repository.Remove(id);
        }
    }
}
