using Microsoft.EntityFrameworkCore;

namespace TodoListApp.Data
{
    public class ToDoListService
    {
        private IDbContextFactory<TodoListContext> _dbContextFactory;

        public ToDoListService(IDbContextFactory<TodoListContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }
        /// <summary>
        /// Returns all the todos including the items
        /// </summary>
        /// <returns>List<TodoList></returns>
        public Task<List<TodoList>> GetToDosAsync()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return Task.FromResult(context.TodoLists.Include(x=> x.Items).ToList());

        }
        /// <summary>
        /// Set the done status of a todo item
        /// </summary>
        /// <param name="toDoListid"></param>
        /// <param name="toDoItemId"></param>
        /// <param name="done"></param>
        public void SetDoneStatus(int toDoListid, int toDoItemId, bool done)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var item = context.TodoItems.Where(x => x.TodoListId == toDoListid && x.Id == toDoItemId).FirstOrDefault();
            if (item != null)
            {
                item.Completed = done;
                context.TodoItems.Update(item);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Remove a todo list
        /// </summary>
        /// <param name="toDoListid"></param>
        public void RemoveToDoList(int toDoListid)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var item = context.TodoLists.Where(x => x.Id == toDoListid).FirstOrDefault();
            if (item != null)
            {
                context.TodoLists.Remove(item);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Add or update a todo list
        /// </summary>
        /// <param name="todoList"></param>
        public void AddOrUpdateTodoList(TodoList todoList)
        {
            using var context = _dbContextFactory.CreateDbContext();
            if (todoList.Id == 0)
            {
                context.TodoLists.Add(todoList);
            }
            else
            {
                context.TodoLists.Update(todoList);
            }
            if(todoList.Items != null)
            {
                foreach (var item in todoList.Items)
                {
                    AddOrUpdateTodoItem(item, true);
                }
            }
            

            context.SaveChanges();
        }
        /// <summary>
        /// Remove a todo item
        /// </summary>
        /// <param name="toDoListid"></param>
        /// <param name="toDoItemId"></param>
        public void RemoveToDoItem(int toDoListid, int toDoItemId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var item = context.TodoItems.Where(x => x.TodoListId == toDoListid && x.Id == toDoItemId).FirstOrDefault();
            if (item != null)
            {
                context.TodoItems.Remove(item);
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Add or update a todo item
        /// </summary>
        /// <param name="todoItem"></param>
        /// <param name="bulkUpdate"></param>
        public void AddOrUpdateTodoItem(TodoItem todoItem, bool bulkUpdate = false)
        {
            using var context = _dbContextFactory.CreateDbContext();
            if (todoItem.Id == 0)
            {
                context.TodoItems.Add(todoItem);
            }
            else
            {
                context.TodoItems.Update(todoItem);
            }
            if (!bulkUpdate)
            {
                context.SaveChanges();
            }
            
        }
        /// <summary>
        /// Get todo list by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TodoList?> GetToDosListItemAsync(int id)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return context.TodoLists
                .Include(todo => todo.Items.Where(item => item.TodoListId == id))
                .Where(todo => todo.Id == id)
                .FirstOrDefault();
        }
    }
}