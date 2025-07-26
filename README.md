# Kafe API - .NET Core 9 ile  API Geliştirme

Bu proje, .NET Core 9 kullanılarak geliştirilmiş, gerçek dünya senaryolarına uygun bir **Restful API mimarisi** içermektedir. Katmanlı mimari, güvenlik, validation, mapping, logging gibi modern yazılım geliştirme pratiklerini barındırır.

---

## Kimler Fayda Sağlayabilir?

- .NET Core ile API geliştirmek isteyenler  
-  Gerçek projelerle kendini geliştirmek isteyen yazılımcılar  
-  Kafe, restoran, menü sistemini kendi geliştirmek isteyenlere 

---

## Kullanılan Teknolojiler ve Katmanlar

Katman | Açıklama 

- ASP.NET Core 9 Web API | Ana API projesi |
- Entity Framework Core | Veritabanı işlemleri |
- Onion Architecture | Temiz, sürdürülebilir mimari |
- FluentValidation | Model doğrulama |
- AutoMapper | DTO ↔ Entity dönüşümleri |
- Serilog | Loglama işlemleri |
- JWT Authentication | Token bazlı kimlik doğrulama |
- ASP.NET Identity | Kullanıcı yönetimi |
- Rate Limiting** | API güvenliği için istek sınırlama |
- Postman / Scalar | API dökümantasyonu ve test arayüzü |

---

##  Öne Çıkan İşlemler

- .NET Core 9 ile CRUD işlemleri  
-  RESTful API geliştirme örnekleri  
- JWT ile kimlik doğrulama  
- Identity ile kullanıcı yönetimi  
-  AutoMapper ile veri transferi kolaylaştırma  
- FluentValidation ile sağlam veri doğrulama  
-  Serilog ile gelişmiş loglama  
-  Rate Limiting ile saldırılara karşı koruma  
- Scalar ve postman modern API arayüzü  
-  Gerçek bir kafe sistemi: Masa, Sipariş, Menü, Kullanıcı, Yorum yönetimi


##  Proje Yapısı
KafeApi
- KafeApi.Application/ # İş mantığı ve servisler
- KafeApi.Domain/ # Entity'ler ve arayüzler
- KafeApi.Infrastructure/ # EF Core, veri erişim katmanı
- KafeApi.Persistence/ # DbContext, veritabanı bağlantısı
- KafeApi.WebAPI/ # Controller ve API giriş noktası

## Kurulum
"ConnectionStrings": {
  "DefaultConnection": "Data Source=Veritabanı adresinizi yazını;Initial Catalog=KafeApiDb;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False"
},

update-database işlemi yazın 

## Ekran görüntüleri
<img width="1892" height="909" alt="image" src="https://github.com/user-attachments/assets/109422e5-cd5b-4deb-afb5-3718eab8584b" />
<img width="1920" height="910" alt="image" src="https://github.com/user-attachments/assets/4663022e-f6e4-41c9-ae66-7029949d2bc8" />
<img width="1920" height="902" alt="image" src="https://github.com/user-attachments/assets/ba15c281-f699-4db1-b435-5985b7da7c03" />
<img width="1920" height="875" alt="image" src="https://github.com/user-attachments/assets/c3c6e76e-5277-45ae-93a0-f71d73b3f026" />
<img width="1920" height="872" alt="image" src="https://github.com/user-attachments/assets/5d2b198d-3e75-4b95-be40-8b40d9a56596" />
