# UpgradeProjectSample
## 説明
ASP.NET Core(+ Entity Framework Core)のプロジェクトを、.NET Core ver.2.2.207 から .NET ver.6 以降に更新するサンプルです。

[【オンライン】.NET 5 終了目前！ C# Tokyo イベント](https://csharp-tokyo.connpass.com/event/243622/) で使用したものを継続して更新しています。

Current SDK Version: .NET 8.0.404

## Frameworks & Libraries
* ASP.NET Core
* Entity Framework Core
* NLog

### Environment variables
接続文字列をシステム環境変数に設定しています。

|Name|Value|
|--|--|
|ConnectionStrings__BookShelf|Host=localhost;Port=5432;Database=book_shelf;Username={User Name};Password={Password};|
