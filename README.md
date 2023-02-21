# UpgradeProjectSample
## 説明
ASP.NET Core(+ Entity Framework Core)のプロジェクトを、.NET Core ver.2.2.207 から .NET ver.6 以降に更新するサンプルです。

[【オンライン】.NET 5 終了目前！ C# Tokyo イベント](https://csharp-tokyo.connpass.com/event/243622/) で使用したものを継続して更新しています。

Current SDK Version: .NET 7.0.200

## Frameworks & Libraries
* ASP.NET Core
* Entity Framework Core
* NLog

## DbConnection
接続文字列を appsettings.Development.json に下記の形式で設定しています。

```json
{
    "DbConnection": "Host=localhost;Port=5432;Database={DB Name};Username={User Name};Password={Password};"
}
```