namespace TodoListApp.Data
{
    public class TodoItem
    {
        public int TodoListId { get; set; }
        public int Id { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
    }
}