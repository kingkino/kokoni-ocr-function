# kokoni-ocr-function 構築方法

## 前提

・利用可能なAzureアカウントを持っていること

・Lineアカウントを取得済みであること

・Cognitive ServiceでComputer Vision APIとTranslater Text APIを作成しKEYを取得すること

・AzureStorageの接続文字列を取得すること

※最近AzureFUnctionsのポータルが刷新したので新しいポータルでの作成手順を説明します。

## 参考

MessageAPIの開設は下記を参考にしてください。

[LINE BOTの作り方を世界一わかりやすく解説（１）【アカウント準備編】](http://qiita.com/yoshizaki_kkgk/items/bd4277d3943200beab26)

AzureFunctionsの環境構築方法は下記を参考にしてください。

[Visual Studioで始めるサーバーレス「Azure Functions」開発入門](http://www.buildinsider.net/pr/microsoft/azure/dictionary06)

Cogtnitive ServiceのKEYの作成方法は下記を参考にしてください。

[Azure Portal で Cognitive Services APIs アカウントを作成する](https://docs.microsoft.com/ja-jp/azure/cognitive-services/cognitive-services-apis-create-account)


## カスタムデプロイ

ARMテンプレートによるカスタムデプロイリンクで環境構築できるようにしました。
下記のリンクをクリックしてデプロイ環境を構築してみてください。

|デプロイ方法|デプロイリンク|
| --------------- |:---------------:|
| Direct Portal Deploy | [![Deploy to Azure](http://azuredeploy.net/deploybutton.png)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Fkingkino%2Fkokoni-ocr-function%2Fmaster%2FAzureDeploy.json) |


## 設定手順

![参考画像01](https://github.com/kingkino/kokoni-ocr-function/blob/master/refer01.png)

作成したFuntionsを開き新規Functionを作成します。

ここではWebhook+APIを選択して作成してください。

![参考画像02](https://github.com/kingkino/kokoni-ocr-function/blob/master/refer02.png)

次にFunctionのリソースをアップロードします。

アップロードするリソースは「kokoni-ocr-linebot」配下もしくは「kokoni-ocr-translate-linebot」配下のファイルになりますのでCloneなりダウンロードなりしておいてください。

![参考画像03](https://github.com/kingkino/kokoni-ocr-function/blob/master/refer03.png)

作成したFunctionを選択しファイルの表示からアップロードを選択します。

対象のファイルを選択してアップロードします。

同名ファイルは上書きになります。

アップロードが完了するとproject.json内の設定によりNugetが実行され必要な参照がrestoreされます。

この時点でログを確認して「Compiled Success」と表示されることを確認してください。

コンパイルが成功していない場合は手順に誤りがあるかFunctionsが進化して現行ソースが対応しなくなってしまったかのどちらかです。

![参考画像04](https://github.com/kingkino/kokoni-ocr-function/blob/master/refer04.png)

リソースの配置が完了したら必要なアプリケーション設定を追加します。

Functionのプラットフォーム機能からアプリケーション設定を開きます。

![参考画像05](https://github.com/kingkino/kokoni-ocr-function/blob/master/refer05.png)

アプリケーションの設定に下記のKeyValueの組み合わせを追加していきます。

|Key|value|
| --------------- | --------------- |
|ChannelAccessTokenOCR|「kokoni-ocr-linebot」向けに作成したLINE APIのKEYを取得する|
|ChannelAccessTokenOCRTranslate|「kokoni-ocr-translate-linebot」向けに作成したLINE APIのKEYを取得する|
|AuthToken|Text Translate APIのkey|
|SubscriptionKey|Comupter Vision APIのkey|
|AzureStorageAccount|接続文字列形式(DefaultEndpointsProtocol=https;AccountName=***;AccountKey=***;EndpointSuffix=core.windows.net)|

追加したら上のほうにある保存ボタンをクリックして保存してください。


設定が完了したらLINEクライアントから文字を含む画像を投稿して試してみてください。



