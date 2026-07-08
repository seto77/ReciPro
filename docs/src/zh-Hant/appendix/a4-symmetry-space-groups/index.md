# 附錄 A4. 對稱性與空間群

主視窗章節 [2. 對稱性資訊](../../2-symmetry-information.md) 是 GUI 的操作指南：它告訴你哪個索引標籤顯示什麼、哪個按鈕複製哪張示意圖。本附錄則彙整這些表格與圖像背後的**結晶學與群論背景** — Hermann–Mauguin 符號實際上編碼了什麼、如何閱讀 *International Tables for Crystallography*（ITA）Vol. A 樣式的對稱元素與一般位置示意圖，以及 **群關係...** 視窗的超群／子群表與術語（*translationengleiche*、*klassengleiche*、共軛類、疇、雙晶律、…）究竟是什麼意思。

![對稱性資訊](../../../assets/cap-zh-Hant-auto/FormSymmetryInformation.png)

本附錄涵蓋兩個視窗，理論最好依下列順序閱讀：

1. **[A4.1. 空間群符號與對稱性示意圖](symbols-and-diagrams.md)** — Hermann–Mauguin、Schoenflies 與 Hall 符號；**群性質** 索引標籤上顯示的群論分類（中心對稱、Sohncke、簡單型、極性、算術晶類、Patterson 對稱、…）；**對稱操作** 索引標籤上以座標三元組／Seitz 符號／幾何類型描述的每一個對稱操作；以及 [對稱性資訊](../../2-symmetry-information.md) 視窗下方對稱元素與一般位置示意圖的繪圖慣例。
2. **[A4.2. 群與子群的關係](group-subgroup-relations.md)** — 什麼是*極大子群*／*極小超群*、Hermann 的 *t*-／*k*- 之分，以及如何閱讀從對稱性資訊的 **選項** 面板開啟的 **群關係...** 瀏覽器的每個索引標籤（系統圖、變換矩陣、軌道分裂、疇與雙晶、新反射）。

A4.1 之所以排在前面，是因為 A4.2 會不斷回頭引用它：每一個子群／超群關係本身，都是以那裡介紹的同一套 Hermann–Mauguin 符號、Seitz 符號與幾何類型措辭（*「3-fold rotation」*、*「c-glide plane」*、*「screw axis」*、…）來標示的。

---

## 涵蓋範圍與資料來源

ReciPro 的內建資料庫完全依照 *International Tables for Crystallography* **Volume A**（空間群對稱性）與 **Volume A1**（空間群的極大子群）的收錄內容，涵蓋 230 種空間群類型（含 530 種收錄的設定／原點選擇）。本附錄說明的是 ReciPro 對這些資料的「呈現方式」— 記號、示意圖、瀏覽工具 — 並假定讀者已具備關於點陣、點群與對稱操作概念的大學部程度素養。它不能取代 ITA 本身；收錄資料的權威依據仍是 ITA（基於著作權考量，ReciPro 無法逐字複製 ITA 的表格 — 特定空間群類型的替代原點／設定一覽，請見 ReciPro 自行整理的 **設定一覽** 索引標籤）。

!!! note "群關係... 是仍在積極開發中的功能"
    **群關係...** 瀏覽器（A4.2）的 *translationengleiche*（t-）與 *klassengleiche*（k-，含*同型* isomorphic）子群與超群，都是直接由空間群自身的對稱操作計算而得（而非取自預先製表的清單），因此顯示的每一個關係都經過獨立驗證，而非自表格轉錄。尚存的限制 — 例如同型系列僅列舉至 index ≤ 4 — 在 A4.2 的**目前的限制**中逐條說明。

---

## 另請參閱

- [2. 對稱性資訊](../../2-symmetry-information.md) — 本附錄所解說的 GUI 指南。
- [A4.1. 空間群符號與對稱性示意圖](symbols-and-diagrams.md) · [A4.2. 群與子群的關係](group-subgroup-relations.md)
- [附錄 A1. 座標系](../a1-coordinate-system/1-orientation.md)
- [附錄 A2. 電子束交互作用（固態物理背景）](../a2-beam-interaction/index.md) — 空間群的反射條件（系統消光）如何進入結構因子。
