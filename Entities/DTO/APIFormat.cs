namespace Entities.DTO
{
    public struct APIFormat
    {
        public object Data;
        public object Error;
    }
    public struct Error
    {
        public string Message;
        public string Exception;
    }
}
