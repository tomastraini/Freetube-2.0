namespace REST.Models
{
    public class likes
    {
        public int id_like { get; set; }
        public int id_video { get; set; }
        public int id_user { get; set; }
        public bool liked { get; set; }
    }
}
