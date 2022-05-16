ShortURL
=================

## 用途

後端以 .NET 6.0 WebAPI + Service 架構為主體的縮網址小型服務。

## Features

### API 文件

#### 測試用驗證 Token 
` "token": "29cc3e11-b87b-443c-b9fd-365631bb8f59"` 
（需加在 API Headers）


#### 縮網址範本

`https://{DomainName}/9BAHQ`


#### 建立縮網址內容

[POST] https://{DomainName}/api/urlRedirection

Sample:

```
Request
{
  "toUrl": "https://www.google.com"
}
```
```
Response
{
    "statusCode": 201,
    "contents": {
        "id": 1,
        "code": "9BAHQ",
        "url": "https://www.google.com",
        "enable": true,
        "createdAt": "2022-03-07T20:25:47.5529805+08:00",
        "updatedAt": "2022-03-07T20:25:47.5529805+08:00"
    },
    "message": ""
}
```


#### 更新縮網址內容

[PATCH] https://{DomainName}/api/urlRedirection

Sample:

```
Request
{
  "code": "9BAHQ",
  "url": "https://www.google.com",
  "enable": true
}
```
```
Response
{
    "statusCode": 200,
    "contents": {
        "id": 1,
        "code": "9BAHQ",
        "url": "https://www.google.com",
        "enable": true,
        "createdAt": "2022-03-07T20:25:47.5529805+08:00",
        "updatedAt": "2022-03-07T20:25:47.5529805+08:00"
    },
    "message": ""
}
```


#### API StatusCode

`200` 執行成功

`201` 建立成功

`400` 格式錯誤、找無資料

`401` 驗證失敗


### Back-end
* .NET 6.0.2
* .NET Standard 2.1
* Entity Framework Core 6.0.2
* NUnit 3.12.2
* Swashbuckle.AspNetCore 6.2.3
* Serilog.AspNetCore 5.0.0
