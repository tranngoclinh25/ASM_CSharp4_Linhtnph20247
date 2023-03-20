namespace ASM_CSharp4_Linhtnph20247.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<RoleUser> RoleUsers { get; set; }
    }
}
