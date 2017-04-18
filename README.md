# kokoni-ocr-function 構築方法

## 前提

・利用可能なAzureアカウントを持っていること

・Lineアカウントを取得済みであること

※最近AzureFUnctionsのポータルが刷新したので新しいポータルでの作成手順を説明します。

## 参考

MessageAPIの開設は下記を参考にしてください。

[LINE BOTの作り方を世界一わかりやすく解説（１）【アカウント準備編】](http://qiita.com/yoshizaki_kkgk/items/bd4277d3943200beab26)

AzureFunctionsの環境構築方法は下記を参考にしてください。

[Visual Studioで始めるサーバーレス「Azure Functions」開発入門](http://www.buildinsider.net/pr/microsoft/azure/dictionary06)

## カスタムデプロイ

ARMテンプレートによるカスタムデプロイリンクで環境構築できるようにしました。
下記のリンクをクリックしてデプロイ環境を構築してみてください。

|デプロイ方法|デプロイリンク|
| --------------- |:---------------:|
| Direct Portal Deploy | [![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fkingkino%2Fkokoni-SmapleLineEchoBot%2Fmaster%2FAzureDeploy.json) |


## AzurePortalからAzure Functionsを作成する


## 設定手順
