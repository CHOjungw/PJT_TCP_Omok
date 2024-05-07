namespace Omok_Lib.Models
{
    public class ConnectionDetails
    {
        public string UserName { get; set; } = string.Empty;
        public string RoomId { get; set; }

        public override string ToString()
        {
            return $"RoomId: {RoomId}, UserName: {UserName}";
        }
    }
}
