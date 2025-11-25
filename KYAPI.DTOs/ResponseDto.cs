using System.ComponentModel.DataAnnotations;

namespace KYAPI.DTOs;

public class GlobalApiResponse<T>
{
    public bool IsSuccess { get; set; }
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Value { get; set; }
}