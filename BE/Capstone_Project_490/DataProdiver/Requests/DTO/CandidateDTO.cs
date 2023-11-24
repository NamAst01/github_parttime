using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class CandidateDTO
    {
        public CandidateDTO() { }
        public int Id { get; set; }
        public string Phone { get; set; } = null!;
        public DateTime Dob { get; set; }
        public string Address { get; set; } = null!;
        public string Image { get; set; } = null!;
        public short? IsReport { get; set; }
        public int? AccountId { get; set; }
        public short Status { get; set; }
        [JsonPropertyName("Emailcan")]
        public string? Email { get; set; }
        [JsonPropertyName("Passcan")]
        public string? Password { get; set; }
        public virtual Account? Account { get; set; }
    }
}
