<<<<<<< HEAD
﻿using System;
=======
﻿using IdentityServer4.Models;
using System;
>>>>>>> ea324b603656c308964d6ef2f3fd279357191eee
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ParqueAPICentral.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }
<<<<<<< HEAD
=======

        [JsonIgnore]
        public List<RefreshToken> RefreshTokens { get; set; }
>>>>>>> ea324b603656c308964d6ef2f3fd279357191eee
    }
}
