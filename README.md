# ユニティちゃん縦移動ゲーム

![ゲームシーン](https://raw.github.com/caorol/StepAsideUnityChan/master/gamescene.png)

---
## フォーカス処理（ボカシ）
### アセットをインポートする
Asset store で `post processing stack`を import（デフォルトチェックのままでOK）  

※ Assets - Import Package - Effects でインポートされる Standard Assets は今後メンテされないそうです

#### 詳細
https://qiita.com/Sase/items/c375348af2686c398c18
https://www.youtube.com/embed/r5mNmH68KPQ?rel=0&start=1320&end=1350

※ 動画の方が説明が細かい

### Post-Processing Stack 初期設定
1. Player Settings を変更
  File - Build Settings - PlayerSettings  
  Inspector - Other Settings - Color Space  
  -> Linearに変更  

  > On Android, linear colorspace requires OpenGL ES 3.0 or Vulkan, uncheck 'Automatic Graphics API' to remove OpenGL ES 2 API and 'Minimum API Level' must be at least Android 4.3

  という事なので、Android ユーザは以下手順も追加  
  1.1 Auto Graphics API のチェックを外す  
  1.2 Graphics APIｓ の一覧から OpenGLES2 削除  
  1.3 Minimum API Level を Android 4.3 に変更  

2. カメラ設定を変更  
  ・RenderingPathをDeferredに変更  
  ・HDRをONに変更

この設定で１００％ポストプロセスが使えるようになる、必ず設定しないと使えないわけではない

### アセットをアタッチ
1. HieraHierarchy ビューで `Main Camera` を選択
2. `Inspector` の `Add Component` で `post processing` を検索
3. `Post-Prosessing Behaviour` を追加
4. Project ビューで `Post-Processing Profile` アセット作成
5. 上記 `Post-Processing Profile` を `Main Camera` の `Post-Processing Behaviour` の `Profile` にアタッチ

### Post-Processing Profile 設定
#### Inspector - Depth of Field（被写界深度）
1. Use Camera FOV にチェック
2. Kernel Size を Small に変更
3. 他のパラメタをいじって適当にぼかす

### 実機に流し込む
ボケている箇所が白黒になるという結果に！Σ(ﾟДﾟ)

#### ひとまずタグを打っておく
タグ: v1.0-use-post-processing

#### 結論
GPUのスペック不足…らしい

モバイル対応のエフェクトを使うといいみたい（↓参考）
> [FxPro: Bloom&DOF, Mobile-Ready ($7.51)](https://www.assetstore.unity3d.com/en/#!/content/22189)  
> [Depth of Field Mobile Shader ($37.80)](https://www.assetstore.unity3d.com/en/#!/content/15434)


---
## UI Text などが小さく表示されてしまう問題
解像度に合わせて UI をスケーリングする場合、Text の上の Canvas オブジェクトにアタッチされている `Canvas Scaler`コンポーネントの設定を変更する。

![Canvas Scaler]((https://raw.github.com/caorol/StepAsideUnityChan/master/canvasscaler.png))

UI Scale Mode: `Scala With Screen Size`  
  → 解像度でスケールさせる

ReferenceResolution: `1280, 720`  
→ [MOBILE (IOS) HARDWARE STATS 2017-03](https://hwstats.unity3d.com/mobile/display-ios.htm)  
→ [MOBILE (ANDROID) HARDWARE STATS 2017-03](https://hwstats.unity3d.com/mobile/display-android.htm)  
  → 16:9 が多い

Screen MatchMode: `Expand`  
  → ゲーム画面内範囲で調整
