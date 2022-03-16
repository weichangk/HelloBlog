namespace Blog.Hosting.Models
{
    public class TimeLineDto
    {
        public int Year { get; set; }

        public Dictionary<string, IEnumerable<LineItem>> Items { get; set; }
    }
}