namespace Domain.Shared;

public sealed record Response<T>(
    bool Success,
    string? Message,
    T? Data,
    List<ErrorDetail>? Errors);

public sealed record ErrorDetail(
    string Field, 
    string Error);