namespace KoiBet.DTO.User
{
    public class UpdateUserDTO
    {
       public Guid users_id { get; set; }

       public string? full_name { get; set; }

       public string? email { get; set; }

       public string? phone { get; set; }

       public string? role_id { get; set; }
    }
}
