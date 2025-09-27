# ⚙️ SecilStore Config Library

✅ Bu proje **SeçilStore** firmasının **Backend Geliştirici** pozisyonu için tarafıma verilmiş olan bir **case çalışmasıdır**. Kodlarımın kullanılması iznim dışında kesinlikle yasaktır.

✅ Merkezi konfigürasyon yönetimi sağlayan, **ölçeklenebilir** ve **test edilebilir** bir .NET Core kütüphanesidir.  
API ve MVC istemcisi ile birlikte gelir.

✅ Bu proje, **nTier Architecture** ve **Repository Pattern** ile geliştirilmiştir.

✅ Projede **Thread-Safe**, **Cache mekanizması**, **REST API**, **Unit Test**, **MVC** kullanılmıştır.

✅ UI üzerinde yapılan işlemler, backend'den dışarıya açılan **REST API**'lere call atarak işlemini gerçekleştirmektedir.

---

## 📁 Proje Yapısı
📡 SecilStore.Api/ → RESTful Web API (ConfigItemController)

🖥️ SecilStore.Mvc/ → MVC UI projesi (Listeleme, Ekleme, Silme)

🧠 SecilStore.Business/ → CRUD operasyonlarının logic tarafı

💾 SecilStore.Data/ → EF Core, DbContext ve Entity'ler

📦 SecilStore.Common/ → DTO'lar, Constants, Result Models, Validations, Enums

🧪 SecilStore.Test/ → Unit Test projesi (NUnit, Moq)

📚 SecilStore.Library/ → Projenin ana fikri olan bağımsız kütüphane

🧪 SecilStore.TestConsoleApp/ → Kütüphaneyi test ettiğim console uygulaması

---

## 🚀 Kurulum

### 1. Veritabanı

MSSQL üzerinde aşağıdaki tablo oluşturulmalıdır:

```sql
CREATE TABLE ConfigurationItem (
    Id UNIQUEIDENTIFIER PRIMARY KEY,
    Name NVARCHAR(255),
    Type NVARCHAR(100),
    Value NVARCHAR(MAX),
    IsActive BIT,
    ApplicationName NVARCHAR(255),
    CreatedDate DATETIMEOFFSET,
    ModifiedDate DATETIMEOFFSET
)
