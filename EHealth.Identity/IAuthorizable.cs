namespace EHealth.Identity
{
    public interface IAuthorizable
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
    }
}
