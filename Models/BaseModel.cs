namespace smartclinic.Models
{
    public class BaseModel
    {
       public DateTime created_at { get; set; }
       public DateTime updated_at { get; set; }
       public DateTime? deleted_at { get; set; }
       public bool is_deleted { get; set; }
    }
}