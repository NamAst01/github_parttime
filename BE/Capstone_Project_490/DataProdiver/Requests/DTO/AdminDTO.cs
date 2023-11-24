namespace DataProvider.Requests.DTO
{
    public class AdminDTO
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime LastLoginAt { get; set; }
        public short Status { get; set; }
        public string? Gender { get; set; }
        public int? IsBaned { get; set; }
    }
}
