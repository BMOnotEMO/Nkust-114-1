# Nkust-114-1

## 專案說明

.NET 9.0 控制台應用程式，分析並處理高雄主要觀光景點的遊客人數，並使用ScottPlot將數據具象化，使用者可以選擇顯示的年份，也可以依照自己的習慣放大或縮小圖表的比例。

## 資料來源

資料集:高雄市主要觀光遊憩區遊客人次
資料來源: [政府資料開放平台](https://data.gov.tw/dataset/159684)

## 工作區分析與示範說明

下面的說明依據你後面附的兩張示範圖（第一張為 README 的網頁預覽截圖，第二張為原始 Markdown 編輯檢視的截圖）撰寫，說明專案要點、建置/執行步驟，以及兩張圖示的內容如何當作範例使用。

- 專案總覽
	- 此工作區主要是 `ConsoleApp` WinForms/桌面應用（來源碼位於 `ConsoleApp/`）。專案會讀取 `App_Data` 目錄下的 JSON 資料（例如 `KH_visitor.json`）來繪製圖表。主要程式檔包含 `MainForm.cs`、`Program.cs`，以及 `Services/JsonDataService.cs` 等。
	- 專案的 `*.csproj` 目標框架已更新為 `.NET 9.0`（`net9.0-windows10.0.19041`），因此在開發機需安裝 .NET 9 SDK 才能成功 build/run（可用 `dotnet --list-sdks` 檢查）。
	- 輸出 (bin) 與中介檔 (obj) 資料夾中可能保留多個目標框架的產物（net8.0 與 net9.0 的資料夾），這是正常的開發產物；若需要可以清除 `bin/` 與 `obj/` 後重新建置以避免舊檔干擾。

- 建置與執行（範例）
	1. 確認已安裝 .NET 9 SDK。
	2. 在專案根目錄或 `ConsoleApp` 子目錄執行：

		 - 建置：

			 ```bash
			 dotnet build
			 ```

		 - 以專案執行（或指定 project）：

			 ```bash
			 dotnet run --project ConsoleApp
			 ```

		 - 或先切換目錄再執行：

			 ```bash
			 cd ConsoleApp
			 dotnet run
			 ```

	3. 執行前請確保 `ConsoleApp/App_Data/` 目錄存在，並放入所需的 JSON 檔（例如 `KH_visitor.json`）。

## 專案結構

```
Nkust-114-1/
├── ConsoleApp/
│   ├── ConsoleApp.csproj
│   ├── MainForm.cs
│   ├── MainForm.Designer.cs
│   ├── JsonDataService.cs
│   └── App_Data/          			# 資料檔案目錄（需手動建立）
│       └── HK_Visitor.json         # 高雄主要景點遊客資料檔案
├── ConsoleApp.sln
└── README.md
```
