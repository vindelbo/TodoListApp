using Microsoft.EntityFrameworkCore;
using TodoListApp.Data;

public class TodoListContext : DbContext
{
    public TodoListContext(DbContextOptions<TodoListContext> options) : base(options)
    {

    }

    public DbSet<TodoList> TodoLists { get; set; } = default!;
    public DbSet<TodoItem> TodoItems { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var todoList1 = new TodoList
        {
            Id = 1,
            Title = "New job at holion",
        };

        var tasks = new List<TodoItem>
        {
            new TodoItem { Id = 1, TodoListId = 1, Description = "Browse holion.dk", Completed = true },
            new TodoItem { Id = 2, TodoListId = 1, Description = "Send application", Completed = true },
            new TodoItem { Id = 3, TodoListId = 1, Description = "Get job" }
        };

        modelBuilder.Entity<TodoList>().HasData(todoList1);
        modelBuilder.Entity<TodoItem>().HasData(tasks);
        base.OnModelCreating(modelBuilder);
    }
}