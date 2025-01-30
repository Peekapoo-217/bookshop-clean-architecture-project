
## Hướng Dẫn Tích Hợp Google Authentication trong Clean Architecture Project

### Cài Đặt DotEnv.Net(Environment Variable)

Install package **DotEnv.Net**(recommend 3.2.1).  

```sh
dotnet add package DotEnv.Net
````

## 2. Tạo File .env Lưu Google ID Key
Tạo file `.env` trong thư mục gốc của dự án và lưu các env variable:

```env

Tạo file .env lưu google id key(tạo trên google cloud)

GOOGLE_CLIENT_ID=[CLient_Id]
GOOGLE_CLIENT_SECRET=[Client_Secret_Id]
````
---

## 3. Cấu hình AppSetting.json

```AppSetting.json
{
  "GoogleKeys": {
    "ClientId": "${GOOGLE_CLIENT_ID}",
    "ClientSecret": "${GOOGLE_CLIENT_SECRET}"
  }
}
```
---
## 4.Cấu hình trong Program.
````Program
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddGoogle(options =>
{
    options.ClientId = clientId ?? throw new Exception("Google CLientId is missing in appSetting.json");
    options.ClientSecret = clientSecret ?? throw new Exception("Google CLientSecret is missing in appSetting.json");
});
````

