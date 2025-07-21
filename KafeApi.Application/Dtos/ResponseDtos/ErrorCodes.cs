namespace KafeApi.Application.Dtos.ResponseDtos;

public static class ErrorCodes
{
    public const string NotFound = "NotFound";  //"İstenen veri bulunamadı.";
    public const string Exception = "Exception"; //"İstenen veri bulunamadı.";
    public const string ValidationError = "ValidationError";//"Geçersiz veya eksik veri gönderildi.";
    public const string Unauthorized = "Unauthorized";//"Bu işlemi yapmaya yetkiniz yok.";
    public const string Forbidden = "Forbidden";//Bu kaynağa erişiminiz yasak.";
    public const string InternalServerError = "InternalServerError";//"Sunucuda beklenmeyen bir hata oluştu.";
    public const string BadRequest = "BadRequest";//"Geçersiz istek.";
    public const string Conflict = "Conflict";//"Bu kayıt zaten mevcut.";
    public const string NullValue = "NullValue";// "Zorunlu alan boş bırakılamaz.";
    public const string SaveError = "SaveError";//"Veri kaydedilirken bir hata oluştu.";
    public const string UpdateError = "UpdateError" ;//"Veri güncellenirken bir hata oluştu.";
    public const string DeleteError = "DeleteError"; //"Veri silinirken bir hata oluştu.";
}
