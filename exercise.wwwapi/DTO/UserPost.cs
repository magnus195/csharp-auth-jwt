namespace exercise.wwwapi.DTO;

public class UserPost
{
    public required string DisplayName { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}