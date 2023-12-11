using Microsoft.AspNetCore.Mvc;
using ToDoApi.Models;

namespace ToDoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private static List<work> TaskList = new List<work>();
        private static int counter = 1;

        [HttpGet]
        public ActionResult<IEnumerable<work>> GetTasks()
        {
            return Ok(TaskList);
        }

        [HttpGet]
        [Route("/GetTask/{id}")]
        public ActionResult <work> GetTask ([FromRoute]int id) 
        {
            if(id != null)
            {
                var task = TaskList.FirstOrDefault(t=>t.Id==id);
                if(task != null)
                {
                    return Ok(task);
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("/Create")]
        public ActionResult CreateTask([FromBody] string name)
        {
            if (name != null && name != string.Empty)
            {
                var newTask = new work();
                newTask.Name = name;
                newTask.Id = counter;
                counter++;
                TaskList.Add(newTask);
                return CreatedAtAction(nameof(CreateTask), new { id = newTask.Id }, newTask);
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("/Delete/{id}")]
        public ActionResult DeleteTask([FromRoute]int id)
        {
            if (id != null)
            {
                var taskToDelete = TaskList.FirstOrDefault(t => t.Id == id);
                if (taskToDelete == null)
                {
                    return NotFound();
                }

                TaskList.Remove(taskToDelete);
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [Route("/Update/{id}")]
        public ActionResult UpdateTask([FromRoute]int id, work work)
        {
            if (id != null)
            {
                var taskToUpdate = TaskList.FirstOrDefault(t => t.Id == id);
                if (taskToUpdate == null)
                {
                    return NotFound();
                }
                taskToUpdate.Name = work.Name;
                return Ok();
            }
            return BadRequest();
        }
    }
}