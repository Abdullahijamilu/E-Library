namespace E_Library.DTO
{
    public class ResponseModel<T>
    {
        public bool IsSuccess {  get; set; }
        public string Message { get; set; }
        public object Data { get; internal set; }
    }
}
