namespace KoiBet.DTO.User
{
    public class UpdateUserDTO
    {
        public string users_id { get; set; }

       public string? full_name { get; set; } = string.Empty.ToString();

        public string? email { get; set; } = string.Empty.ToString();

       public string? phone { get; set; } = string.Empty.ToString();

       public string? role_id { get; set; } = string.Empty.ToString();
    }
}
