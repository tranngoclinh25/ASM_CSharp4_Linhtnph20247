﻿namespace ASM_CSharp4_Linhtnph20247.Models
{
    public class RoleUser
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
