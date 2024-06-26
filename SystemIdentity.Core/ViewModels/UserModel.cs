﻿namespace SystemIdentity.Core.ViewModels
{

    public class UserModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string City { get; set; }
        public IList<string> Roles { get; set; } // Rolleri tutmak için yeni property
    }

}
