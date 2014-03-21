using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BlogSite.Domain.Entities
{
    [Serializable]
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        public string Name { get; set; }

        public virtual List<User> Users { get; set; }
    }
}
