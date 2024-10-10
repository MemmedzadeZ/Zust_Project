namespace Zust.WebUI.Models
{
    public class ProfileViewModel
    {
        public string? Image { get; set; }

        public IFormFile? FormFile { get; set; }
        public bool IsOnline { get; set; }
        public DateTime DisConnectTime { get; set; } = DateTime.Now;
        public string ConnectTime { get; set; } = "";


        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Fullname {
            get => Firstname + " " + Lastname;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var names = value.Split(" ");
                    Firstname = names.First();
                    Lastname = names.Length > 1 ? names.Last() : ""; 
                }
            }
        } 
        public DateTime Birthday { get; set; }
        public string? Occupation { get; set; }
        public string? Language { get; set; }
        public string? Blood { get; set; } 
        public string? RelationStatus { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Gender { get; set; }
        public string? AboutMe { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get;   set; }
        public string? Password {  get; set; }  
        public string? NewPassword {  get; set; }  
        public string? ChangePassword {  get; set; }  
        public string? Username { get; set; }
    }
}
