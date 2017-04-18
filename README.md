# kokoni-ocr-function 構築方法

## 前提

・利用可能なAzureアカウントを持っていること

・Lineアカウントを取得済みであること

・Cognitive ServiceでComputer Vision APIとTranslater Text APIのKEYを取得すること

・AzureStorageの接続文字列を取得すること

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
| Direct Portal Deploy | [![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fgithub.com%2Fkingkino%2Fkokoni-ocr-function%2Fedit%2Fmaster%2FAzureDeploy.json) |


## 設定手順

![参考画像01](https://github.com/kingkino/kokoni-ocr-function/blob/master/refer01.png)

![参考画像02](https://github.com/kingkino/kokoni-ocr-function/blob/master/refer02.png)

![参考画像03](https://github.com/kingkino/kokoni-ocr-function/blob/master/refer03.png)

![参考画像04](https://github.com/kingkino/kokoni-ocr-function/blob/master/refer04.png)

![参考画像05](https://github.com/kingkino/kokoni-ocr-function/blob/master/refer05.png)
