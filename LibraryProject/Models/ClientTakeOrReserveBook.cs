namespace LibraryProject.Models
{
    public class ClientTakeOrReserveBook
    {
        public bool IsReserved { get; set; } = false;

        public bool IsTaked { get; set; } = false;
    }
}
