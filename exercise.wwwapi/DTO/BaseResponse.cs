using exercise.wwwapi.Enums;

namespace exercise.wwwapi.DTO;

public class BaseResponse<T>
{
    public string Status { get; set; }
    public T Data { get; set; }
    
    public BaseResponse(string status, T data)
    {
        Status = status;
        Data = data;
    }
    
    public BaseResponse()
    {
        Status = Consts.ErrorStatus;
        Data = default(T)!;
    }
}