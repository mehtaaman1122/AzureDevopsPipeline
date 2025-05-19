// Models/UpdateUserRequest.cs
namespace ProductCrubMS.Models
{
    public class UpdateUserRequest
    {
        public int Id { get; set; }  // Required for identifying user
        public string? Name { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
    }
}
