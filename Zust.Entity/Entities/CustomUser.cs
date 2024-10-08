﻿using Microsoft.AspNetCore.Identity;
using Zust.Core.Entities;

namespace Zust.Entity.Entities
{
    public class CustomUser:IdentityUser<string>,IEntity
    {
        public string? Image { get; set; }
        public string? BackgroundImage { get; set; }
        public bool IsOnline { get; set; }
        public DateTime DisConnectTime { get; set; } = DateTime.Now;
        public string ConnectTime { get; set; } = "";

        public string? Firstname {  get; set; }
        public string? Lastname {  get; set; }
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
        public string? Blood {  get; set; }
        public string? RelationStatus {  get; set; }
        public string? Address {  get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? Gender { get; set; }
        public string? AboutMe { get; set; }

        public List<Comment>? Comments { get; set; }
        public List<Comment>? Posts { get; set; }

        public CustomUser()
        {

            Id = Guid.NewGuid().ToString();
        }
    }
}
