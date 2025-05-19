public class User
{
    public int Id { get; set; }
    public string Name { get; set; }          // already there
    public string Email { get; set; }
    public string Password { get; set; }

    // ✅ New fields
    public bool IsActive { get; set; } = true; // default to active
    public DateTime? LastLogin { get; set; }   // nullable: no login yet
}
