# 動力学計算の調査記録（2026-11-06）

EBSD・CBED・PED・STEM・HRTEM のコード、関連文献、公開実装を照合し、物理的な適用範囲、高速化、新しい出力の候補を調査した記録。確認できた不具合の修正と数値検証を含む。調査・保存: ChatGPT Codex（261106Ch）。

## 報告書

以下の4報告は省略せず保存した。まず総合報告を参照し、式・コード位置・文献・公開実装の固定コミットは各分野の報告で確認する。

- [総合報告：結論、実装、性能、検証、今後の優先順位](ReciPro_DynamicalAudit_261106.md)
- [CBED・PED・Bloch波コア：固有解キャッシュ、収束、重複ディスク](ReciPro_DynamicalAudit_CBED_PED_261106.md)
- [STEM・HRTEM・EDX：規格化、散乱模型、TCC画像合成](ReciPro_DynamicalAudit_Imaging_261106.md)
- [EBSD：非干渉源、輸送との接続、厚さ積分、流束](ReciPro_DynamicalAudit_EBSD_261106.md)

同じ物理模型での数値一致、近似の収束、実測との一致は別の検証である。本記録は全材料・全厚さ・全検出条件の定量精度を認定するものではない。

## 保存した検証資料

ログ・測定結果・PEDデータは元ファイルからバイト単位で複製した。旧コードの失敗ログも対照として残している。

| 内容 | 保存先 |
|---|---|
| 検証時のソース・DLL・一時成果物のSHA-256、実行環境、テスト集計 | [manifest.json](evidence/manifest.json) |
| 最終Releaseビルド、全6,703テスト | [build](evidence/solution-final-build.log)、[tests](evidence/tests-final.log) |
| 修正前後40条件、STEMの大きめの条件の比較 | [quick](evidence/compare-net11-quick.log)、[heavy](evidence/compare-net11-heavy.log) |
| 無吸収時の流束保存、EBSD独立参照、厚さ積分 | [BetheDivCheck](evidence/BetheDivCheck-final.log)、[EbsdSolverCheck](evidence/EbsdSolverCheck-final.log)、[BlochIntegralCheck](evidence/BlochIntegralCheck-final.log) |
| 弱吸収積分の旧実装とネイティブ3種類 | [旧実装](evidence/integral-native-old.log)、[generic](evidence/integral-native-generic.log)、[avx2](evidence/integral-native-avx2.log)、[avx512](evidence/integral-native-avx512.log) |
| HRTEM TCC画像合成のBenchmarkDotNet結果 | [測定表](evidence/ReciPro.Benchmarks.HrtemTccBenchmark-report-github.md) |
| PEDの収束診断、反射別生強度、CIF、条件・ハッシュ | [説明](evidence/ped/README.md)、[初回条件](evidence/ped/261106/metadata.json)、[追跡条件](evidence/ped/261106_refined/metadata.json) |

`evidence/` には修正前後のquick/heavy実行時間表も保存した。この手測り時間は精密な速度比較に使わない。HRTEMの速度比較は画像合成部分に限り、3 iterationの短い計測である。

PEDの初回は16/32/64方位、追跡は128/256方位まで増やした。初回・追跡とも生データを残し、256方位や反射数128を真値とは認定していない。最終の回帰試験は旧新とも.NET 11、HRTEMのBenchmarkDotNet測定は.NET 10で実施した。

## 来歴とローカル資料の扱い

- 報告原本は `.project-guidance/ReciPro/ReciPro_DynamicalAudit*_261106.md`。ローカル履歴コミットは `2e06b58`。
- 検証ログの原本は `tools/DynamicalAudit261106/evidence/`、PEDデータは `tools/PedConvergenceCheck/results/`。ローカル履歴コミットは `6064f3ed`。
- HRTEMベンチマークのソースは `ReciPro.Benchmarks/HrtemTccBenchmark.cs`。ローカル履歴コミットは `8f23785`。
- 元報告の相対リンクのうち、ローカル専用の「実装疑義一覧」と「Bethe技術基準」への2件だけを、所在を示すテキストへ変更した。調査結果・数値・結論は変更していない。
- 本文・ログ中の絶対パスは当時の実行環境の記録であり、閲覧者の環境に存在することを前提としない。`tools/`、`.project-guidance/`、`ReciPro.Benchmarks/` はローカル専用リポジトリなので、公開クローンには含まれない。文献PDFも同梱せず、各報告に原著リンクを残した。
- 完全なTRX、旧新の比較用バイナリ、隔離した旧ソースなどはローカルの `C:/tmp/recipro_dynamical_audit_261106/` にあり、本保存物には含めない。保存済みmanifest内の各パスは元のリポジトリルートまたは元の一時保存先を基準とする。

これは2026-11-06時点の調査・修正・検証の記録であり、コード変更のコミットやリリースを意味しない。今後の変更は本記録を上書きせず、別の日付の記録から差分を示す。
