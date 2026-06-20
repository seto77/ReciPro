namespace Crystallography;

// 260621Cl 自動生成 (tools/gen_l10n_data.py 由来)。手で編集しない。
// §2.5: Localizable=false フォーム/UC のハードコード可視ラベルの 11 言語訳テーブル。
// 非翻訳 (全言語=英語) の項目と FormSymmetryInformation(個別実装)・GLControlAlpha(OpenGL 個別) は除外。
internal static class L10nData
{
    public static void Populate(System.Collections.Generic.Dictionary<string, L10n.Entry[]> reg)
    {
        reg["AtomCoordinateTable"] = new L10n.Entry[]
        {
            new("label1", "Text", "Target Atom", "対象原子", "Zielatom", "Atome cible", "Átomo objetivo", "Átomo-alvo", "Atomo bersaglio", "Целевой атом", "目标原子", "目標原子", "대상 원자"),
            new("atomLabelDataGridViewTextBoxColumn", "HeaderText", "Atom Label", "原子ラベル", "Atombezeichnung", "Étiquette de l'atome", "Etiqueta del átomo", "Rótulo do átomo", "Etichetta atomo", "Метка атома", "原子标签", "原子標籤", "원자 라벨"),
            new("lengthÅDataGridViewTextBoxColumn", "HeaderText", "Length (Å)", "長さ (Å)", "Länge (Å)", "Longueur (Å)", "Longitud (Å)", "Comprimento (Å)", "Lunghezza (Å)", "Длина (Å)", "长度 (Å)", "長度 (Å)", "길이 (Å)"),
            new("label2", "Text", "Bar Wdth", "バー幅", "Balkenbr.", "Largeur barre", "Anch. barra", "Larg. da barra", "Largh. barra", "Ширина штриха", "条宽", "長條寬度", "막대 폭"),
            new("label3", "Text", "Max. distance", "最大距離", "Max. Abstand", "Distance max.", "Distancia máx.", "Distância máx.", "Distanza max.", "Макс. расстояние", "最大距离", "最大距離", "최대 거리"),
        };
        reg["ChemicalFormulaInputControl"] = new L10n.Entry[]
        {
            new("label4", "Text", "Element", "元素", "Element", "Élément", "Elemento", "Elemento", "Elemento", "Элемент", "元素", "元素", "원소"),
            new("label5", "Text", "Valence", "価数", "Valenz", "Valence", "Valencia", "Valência", "Valenza", "Валентность", "价态", "價數", "원자가"),
            new("checkBoxCompound", "Text", "Compound", "化合物", "Verbindung", "Composé", "Compuesto", "Composto", "Composto", "Соединение", "化合物", "化合物", "화합물"),
            new("label6", "Text", "Weight", "重み", "Gewicht", "Poids", "Peso", "Peso", "Peso", "Вес", "权重", "權重", "가중치"),
            new("label7", "Text", "Molar ratio", "モル比", "Molverhältnis", "Rapport molaire", "Relación molar", "Razão molar", "Rapporto molare", "Мольное отношение", "摩尔比", "莫耳比", "몰 비율"),
        };
        reg["CrystalDatabaseControl"] = new L10n.Entry[]
        {
            new("nameDataGridViewTextBoxColumn", "HeaderText", "Name", "結晶名", "Name", "Nom", "Nombre", "Nome", "Nome", "Имя", "名称", "名稱", "이름"),
            new("densityDataGridViewTextBoxColumn", "HeaderText", "Density", "密度", "Dichte", "Densité", "Densidad", "Densidade", "Densità", "Плотность", "密度", "密度", "밀도"),
            new("crystalSystemDataGridViewTextBoxColumn", "HeaderText", "Crystal System", "結晶系", "Kristallsystem", "Système cristallin", "Sistema cristalino", "Sistema cristalino", "Sistema cristallino", "Кристаллическая система", "晶系", "晶系", "결정계"),
            new("pointGroupDataGridViewTextBoxColumn", "HeaderText", "Point Group", "点群", "Punktgruppe", "Groupe ponctuel", "Grupo puntual", "Grupo pontual", "Gruppo puntuale", "Точечная группа", "点群", "點群", "점군"),
            new("spaceGroupDataGridViewTextBoxColumn", "HeaderText", "Space Group", "空間群", "Raumgruppe", "Groupe d'espace", "Grupo espacial", "Grupo espacial", "Gruppo spaziale", "Пространственная группа", "空间群", "空間群", "공간군"),
            new("authorsDataGridViewTextBoxColumn", "HeaderText", "Authors", "著者", "Autoren", "Auteurs", "Autores", "Autores", "Autori", "Авторы", "作者", "作者", "저자"),
            new("titleDataGridViewTextBoxColumn", "HeaderText", "Title", "タイトル", "Titel", "Titre", "Título", "Título", "Titolo", "Заглавие", "标题", "標題", "제목"),
            new("journalDataGridViewTextBoxColumn", "HeaderText", "Journal", "雑誌名", "Zeitschrift", "Revue", "Revista", "Revista", "Rivista", "Журнал", "期刊", "期刊", "저널"),
        };
        reg["DiffractionPatternControl"] = new L10n.Entry[]
        {
            new("label36", "Text", "Min.", "最小", "Min.", "Min.", "Mín.", "Mín.", "Min.", "Мин.", "Min.", "最小值", "Min."),
            new("label25", "Text", "Max.", "最大", "Max.", "Max.", "Máx.", "Máx.", "Max.", "Макс.", "最大值", "最大值", "최대"),
            new("checkBoxSimulation", "Text", "Simulation", "シミュレーション", "Simulation", "Simulation", "Simulación", "Simulação", "Simulazione", "Моделирование", "模拟", "模擬", "시뮬레이션"),
            new("checkBoxResidual", "Text", "Residual", "残差", "Restwert", "Résidu", "Residual", "Resíduo", "Residuo", "Невязка", "残差", "殘差", "잔차"),
            new("checkBoxMaskDonut", "Text", "Donut Mask", "ドーナツマスク", "Donut-Maske", "Masque annulaire", "Máscara anular", "Máscara anelar", "Maschera a ciambella", "Кольцевая маска", "圆环掩模", "環形遮罩", "도넛 마스크"),
            new("checkBoxMaskRectangle", "Text", "Rectangle Mask", "矩形マスク", "Rechteckmaske", "Masque rectangulaire", "Máscara rectangular", "Máscara retangular", "Maschera rettangolare", "Прямоугольная маска", "矩形掩模", "矩形遮罩", "직사각형 마스크"),
            new("label9", "Text", "Start", "開始", "Start", "Démarrer", "Iniciar", "Iniciar", "Avvia", "Старт", "开始", "開始", "시작"),
            new("label21", "Text", "Start", "開始", "Start", "Démarrer", "Iniciar", "Iniciar", "Avvia", "Старт", "开始", "開始", "시작"),
            new("buttonUnmaskAll", "Text", "Unmask All", "すべてマスク解除", "Alle Maskierungen aufheben", "Tout démasquer", "Desenmascarar todo", "Remover toda a máscara", "Rimuovi tutte le maschere", "Снять все маски", "取消所有掩模", "取消所有遮罩", "모두 마스크 해제"),
            new("label22", "Text", "Angle", "角度", "Winkel", "Angle", "Ángulo", "Ângulo", "Angolo", "Угол", "角度", "角度", "각도"),
            new("label23", "Text", "Band Width", "バンド幅", "Bandbreite", "Largeur de bande", "Ancho de banda", "Largura da banda", "Larghezza banda", "Ширина полосы", "带宽", "條帶寬度", "밴드 폭"),
            new("label24", "Text", "Direction", "方向", "Richtung", "Direction", "Dirección", "Direção", "Direzione", "Направление", "方向", "方向", "방향"),
            new("checkBoxRectangleIsBothSide", "Text", "Both Side", "両側", "Beide Seiten", "Les deux côtés", "Ambos lados", "Ambos os lados", "Entrambi i lati", "Обе стороны", "双侧", "兩側", "양면"),
            new("buttonSaveMask", "Text", "Save mask", "マスクを保存", "Maske speichern", "Enregistrer le masque", "Guardar máscara", "Salvar máscara", "Salva maschera", "Сохранить маску", "保存掩模", "儲存遮罩", "마스크 저장"),
            new("buttonApplyMask", "Text", "Mask", "マスク", "Maske", "Masque", "Máscara", "Máscara", "Maschera", "Маска", "掩模", "遮罩", "마스크"),
            new("groupBoxGeometry", "Text", "Geometry", "SEM-EBSD設定", "Geometrie", "Géométrie", "Geometría", "Geometria", "Geometria", "Геометрия", "几何", "幾何", "기하구조"),
            new("checkBoxInitialBackground", "Text", "Set initial background", "初期背景を設定", "Anfangshintergrund festlegen", "Définir le fond initial", "Definir fondo inicial", "Definir fundo inicial", "Imposta sfondo iniziale", "Задать начальный фон", "设置初始背景", "設定初始背景", "초기 배경 설정"),
            new("buttonSaveBackGround", "Text", "Save", "保存", "Speichern", "Enregistrer", "Guardar", "Salvar", "Salva", "Сохранить", "保存", "儲存", "저장"),
            new("tabPage1", "Text", "Detector condition", "検出器の条件", "Detektorbedingung", "Condition du détecteur", "Condición del detector", "Condição do detector", "Condizione del rivelatore", "Условия детектора", "探测器条件", "偵測器條件", "검출기 조건"),
            new("groupBoxWaveSource", "Text", "Wave source", "線源", "Wellenquelle", "Source d'onde", "Fuente de ondas", "Fonte de ondas", "Sorgente d'onda", "Источник волн", "波源", "波源", "파원"),
            new("label46", "Text", "Monochromaticity", "単色性", "Monochromasie", "Monochromaticité", "Monocromaticidad", "Monocromaticidade", "Monocromaticità", "Монохроматичность", "单色性", "單色性", "단색성"),
            new("label45", "Text", "Convergence", "収束角", "Konvergenz", "Convergence", "Convergencia", "Convergência", "Convergenza", "Сходимость", "会聚", "會聚", "수렴"),
            new("groupBoxDetectorProperty", "Text", "Detector property", "検出器の特性", "Detektoreigenschaft", "Propriété du détecteur", "Propiedad del detector", "Propriedade do detector", "Proprietà del rivelatore", "Свойства детектора", "探测器属性", "偵測器性質", "검출기 속성"),
            new("sizeControl1", "HeaderText", "Size", "サイズ", "Größe", "Taille", "Tamaño", "Tamanho", "Dimensione", "Размер", "大小", "大小", "크기"),
            new("label19", "Text", "Monitor Resolution", "モニタ解像度", "Monitorauflösung", "Résolution de l'écran", "Resolución del monitor", "Resolução do monitor", "Risoluzione monitor", "Разрешение монитора", "显示器分辨率", "螢幕解析度", "모니터 해상도"),
            new("label2", "Text", "Camera Length", "カメラ長", "Kameralänge", "Longueur de caméra", "Longitud de cámara", "Comprimento da câmera", "Lunghezza camera", "Длина камеры", "相机长度", "相機長度", "카메라 길이"),
            new("label31", "Text", "Image Resolution", "画像解像度", "Bildauflösung", "Résolution de l'image", "Resolución de imagen", "Resolução da imagem", "Risoluzione immagine", "Разрешение изображения", "图像分辨率", "影像解析度", "이미지 해상도"),
            new("label37", "Text", "Center Y", "中心 Y", "Zentrum Y", "Centre Y", "Centro Y", "Centro Y", "Centro Y", "Центр Y", "中心 Y", "中心 Y", "중심 Y"),
            new("label33", "Text", "Center X", "中心 X", "Zentrum X", "Centre X", "Centro X", "Centro X", "Centro X", "Центр X", "中心 X", "中心 X", "중심 X"),
            new("checkBoxFilmBlur", "Text", "Film Blur", "フィルムぼかし", "Filmunschärfe", "Flou du film", "Desenfoque de película", "Desfoque do filme", "Sfocatura pellicola", "Размытие плёнки", "胶片模糊", "底片模糊", "필름 블러"),
            new("tabPage2", "Text", "Mask", "マスク", "Maske", "Masque", "Máscara", "Máscara", "Maschera", "Маска", "掩模", "遮罩", "마스크"),
            new("checkBoxMaskDiffractionPeaks", "Text", "Diffraction peak", "回折ピーク", "Beugungsreflex", "Pic de diffraction", "Pico de difracción", "Pico de difração", "Picco di diffrazione", "Дифракционный пик", "衍射峰", "繞射峰", "회절 피크"),
            new("checkBoxMaskManual", "Text", "Manual Mask", "手動マスク", "Manuelle Maske", "Masque manuel", "Máscara manual", "Máscara manual", "Maschera manuale", "Ручная маска", "手动掩模", "手動遮罩", "수동 마스크"),
            new("buttonCheckAllIndices", "Text", "Check All", "すべて選択", "Alle auswählen", "Tout cocher", "Marcar todo", "Marcar tudo", "Seleziona tutto", "Отметить все", "全选", "全選", "모두 선택"),
            new("buttonUnmaskSelectedPeaks", "Text", "Select area around the peaks", "ピーク周辺の領域を選択", "Bereich um die Peaks auswählen", "Sélectionner la zone autour des pics", "Seleccionar área alrededor de los picos", "Selecionar área ao redor dos picos", "Seleziona area intorno ai picchi", "Выбрать область вокруг пиков", "选择峰周围区域", "選取峰值周圍區域", "피크 주변 영역 선택"),
            new("buttonUncheckAllIndices", "Text", "Uncheck All", "すべてチェック解除", "Auswahl aufheben", "Tout décocher", "Desmarcar todo", "Desmarcar tudo", "Deseleziona tutto", "Снять все отметки", "取消全选", "取消全選", "모두 선택 해제"),
            new("radioButtonManualDonut", "Text", "Donut", "ドーナツ", "Donut", "Anneau", "Anillo", "Anel", "Ciambella", "Кольцо", "圆环", "甜甜圈", "도넛"),
            new("radioButtonManualCircle", "Text", "Circle", "円", "Kreis", "Cercle", "Círculo", "Círculo", "Cerchio", "Окружность", "圆", "圓形", "원"),
            new("radioButtonManualRectangle", "Text", "Rectangle", "矩形", "Rechteck", "Rectangle", "Rectángulo", "Retângulo", "Rettangolo", "Прямоугольник", "矩形", "矩形", "직사각형"),
            new("radioButtonManualSpot", "Text", "Spot", "スポット", "Spot", "Spot", "Punto", "Ponto", "Spot", "Рефлекс", "斑点", "斑點", "스폿"),
            new("label17", "Text", "Size (radius)", "サイズ (半径)", "Größe (Radius)", "Taille (rayon)", "Tamaño (radio)", "Tamanho (raio)", "Dimensione (raggio)", "Размер (радиус)", "大小 (半径)", "大小（半徑）", "크기 (반지름)"),
            new("buttonUnmask", "Text", "Unmask", "マスク解除", "Maskierung aufheben", "Démasquer", "Desenmascarar", "Remover máscara", "Rimuovi maschera", "Снять маску", "取消掩模", "取消遮罩", "마스크 해제"),
            new("buttonMaskAll", "Text", "Mask All", "すべてマスク", "Alles maskieren", "Tout masquer", "Enmascarar todo", "Mascarar tudo", "Maschera tutto", "Маскировать всё", "全部掩蔽", "全部遮罩", "전체 마스크"),
            new("tabPage3", "Text", "Background", "背景", "Hintergrund", "Fond", "Fondo", "Fundo", "Sfondo", "Фон", "背景", "背景", "배경"),
            new("tabPage4", "Text", "Diffraction information", "回折情報", "Beugungsinformation", "Informations sur la diffraction", "Información de difracción", "Informação de difração", "Informazioni sulla diffrazione", "Информация о дифракции", "衍射信息", "繞射資訊", "회절 정보"),
            new("checkBoxShowMaskedArea", "Text", "Show masked area", "マスク領域を表示", "Maskierten Bereich anzeigen", "Afficher la zone masquée", "Mostrar área enmascarada", "Mostrar área mascarada", "Mostra area mascherata", "Показать маскированную область", "显示掩模区域", "顯示遮罩區域", "마스크 영역 표시"),
            new("buttonSaveImage", "Text", "Save Image", "画像を保存", "Bild speichern", "Enregistrer l'image", "Guardar imagen", "Salvar imagem", "Salva immagine", "Сохранить изображение", "保存图像", "儲存影像", "이미지 저장"),
        };
        reg["DistributionGraphControl"] = new L10n.Entry[]
        {
            new("labelY", "Text", "Y:", "Y:", "Y:", "Y :", "Y:", "Y:", "Y:", "Y:", "Y:", "Y：", "Y:"),
            new("labelX", "Text", "X:", "X:", "X:", "X :", "X:", "X:", "X:", "X:", "X:", "X：", "X:"),
        };
        reg["EOSControl"] = new L10n.Entry[]
        {
            new("label83", "Text", "Note", "メモ", "Anmerkung", "Note", "Nota", "Nota", "Nota", "Примечание", "备注", "備註", "참고"),
            new("checkBoxUseEOS", "Text", "Use EOS", "EOS を使用", "EOS verwenden", "Utiliser l'EOS", "Usar EOS", "Usar EOS", "Usa EOS", "Использовать EOS", "使用 EOS", "使用 EOS", "EOS 사용"),
            new("groupBoxThermalPressure", "Text", "Thermal pressure", "熱圧力", "Thermischer Druck", "Pression thermique", "Presión térmica", "Pressão térmica", "Pressione termica", "Термическое давление", "热压", "熱壓力", "열압력"),
            new("radioButtonTdependenceK0andV0", "Text", "T-dependence K₀ && V₀", "K₀ と V₀ の温度依存性", "T-Abhängigkeit K₀ && V₀", "Dépendance en T de K₀ && V₀", "Dependencia con T de K₀ && V₀", "Dependência de T de K₀ && V₀", "Dipendenza da T di K₀ && V₀", "T-зависимость K₀ && V₀", "K₀ && V₀ 的温度依赖性", "K₀ 與 V₀ 的溫度相依性", "K₀ && V₀의 T 의존성"),
            new("groupBoxIsothermalPressure", "Text", "Isothermal pressure at T₀", "T₀ における等温圧力", "Isothermer Druck bei T₀", "Pression isotherme à T₀", "Presión isotérmica a T₀", "Pressão isotérmica em T₀", "Pressione isoterma a T₀", "Изотермическое давление при T₀", "T₀ 下的等温压力", "T₀ 時的等溫壓力", "T₀에서의 등온 압력"),
            new("numericBoxEOS_Kp0", "HeaderText", "K'₀", "K'₀", "K'₀", "K'₀", "K'₀", "K'₀", "K\\'₀", "K'₀", "K'₀", "K'₀", "K'₀"),
            new("numericBoxEOS_Kpp0", "HeaderText", "K''₀", "K''₀", "K''₀", "K''₀", "K''₀", "K''₀", "K\\'\\'₀", "K''₀", "K''₀", "K''₀", "K''₀"),
            new("numericBoxEOS_KpInfinity", "HeaderText", "K'∞", "K'∞", "K'∞", "K'∞", "K'∞", "K'∞", "K\\'∞", "K'∞", "K'∞", "K'∞", "K'∞"),
        };
        reg["ElasticityControl"] = new L10n.Entry[]
        {
            new("radioButtonCompliance", "Text", "Elastic compliance constant", "弾性コンプライアンス定数", "Elastische Nachgiebigkeitskonstante", "Constante de souplesse élastique", "Constante de elasticidad (compliancia)", "Constante de complacência elástica", "Costante di cedevolezza elastica", "Константа упругой податливости", "弹性柔顺常数", "彈性柔量常數", "탄성 컴플라이언스 상수"),
            new("radioButtonStiffness", "Text", "Elastic stiffness constant", "弾性スティフネス定数", "Elastische Steifigkeitskonstante", "Constante de rigidité élastique", "Constante de rigidez elástica", "Constante de rigidez elástica", "Costante di rigidezza elastica", "Константа упругой жёсткости", "弹性刚度常数", "彈性剛度常數", "탄성 강성 상수"),
        };
        reg["FormAnotherSpaceGroup"] = new L10n.Entry[]
        {
            new("buttonCancel", "Text", "Cancel", "キャンセル", "Abbrechen", "Annuler", "Cancelar", "Cancelar", "Annulla", "Отмена", "取消", "取消", "취소"),
            new("this", "Text", "Convert to another spacegroup", "別の空間群へ変換", "In andere Raumgruppe umwandeln", "Convertir vers un autre groupe d'espace", "Convertir a otro grupo espacial", "Converter para outro grupo espacial", "Converti in un altro gruppo spaziale", "Преобразовать в другую пространственную группу", "转换到其他空间群", "轉換為其他空間群", "다른 공간군으로 변환"),
        };
        reg["FormAtomDetailedInfo"] = new L10n.Entry[]
        {
            new("this", "Text", "Atom Positions", "原子位置", "Atompositionen", "Positions atomiques", "Posiciones atómicas", "Posições atômicas", "Posizioni atomiche", "Позиции атомов", "原子位置", "原子位置", "원자 위치"),
        };
        reg["FormCTF"] = new L10n.Entry[]
        {
            new("radioButtonCTF_coherent", "Text", "Coherent imaging", "コヒーレント結像", "Kohärente Abbildung", "Imagerie cohérente", "Imagen coherente", "Imagem coerente", "Imaging coerente", "Когерентное изображение", "相干成像", "同調成像", "결맞음 결상"),
            new("radioButtonCTF_Incoherent", "Text", "Incoherent imaging", "インコヒーレント結像", "Inkohärente Abbildung", "Imagerie incohérente", "Imagen incoherente", "Imagem incoerente", "Imaging incoerente", "Некогерентное изображение", "非相干成像", "非同調成像", "비결맞음 결상"),
            new("buttonCopyGraph", "Text", "Copy", "コピー", "Kopieren", "Copier", "Copiar", "Copiar", "Copia", "Копировать", "复制", "複製", "복사"),
            new("numericBoxMaxU1", "HeaderText", "Max u", "最大 u", "Max. u", "u max.", "u máx.", "u máx.", "u max", "Макс. u", "最大 u", "最大 u", "최대 u"),
        };
        reg["FormCalculator"] = new L10n.Entry[]
        {
            new("label1", "Text", "Calculate:", "計算:", "Berechnen:", "Calculer :", "Calcular:", "Calcular:", "Calcola:", "Вычислить:", "计算：", "計算:", "계산:"),
            new("label2", "Text", "Available functions: ", "使用可能な関数: ", "Verfügbare Funktionen: ", "Fonctions disponibles : ", "Funciones disponibles: ", "Funções disponíveis: ", "Funzioni disponibili: ", "Доступные функции: ", "可用函数： ", "可用函數: ", "사용 가능한 함수: "),
            new("label4", "Text", "Variable assignments:", "変数の割り当て:", "Variablenzuweisungen:", "Affectations de variables :", "Asignaciones de variables:", "Atribuições de variáveis:", "Assegnazioni di variabili:", "Присвоения переменных:", "变量赋值:", "變數指定：", "변수 할당:"),
            new("label3", "Text", " e.g. \r\n     a1 = 3*2^5\r\n     a2 = Sin(60)\r\n     a1^a2      //Shift+Enter", " 例 \r\n     a1 = 3*2^5\r\n     a2 = Sin(60)\r\n     a1^a2      //Shift+Enter", " z. B. \r\n     a1 = 3*2^5\r\n     a2 = Sin(60)\r\n     a1^a2      //Shift+Enter", " ex. \\r\\n     a1 = 3*2^5\\r\\n     a2 = Sin(60)\\r\\n     a1^a2      //Shift+Entrée", " p. ej. \r\n     a1 = 3*2^5\r\n     a2 = Sin(60)\r\n     a1^a2      //Shift+Enter", " ex. \r\n     a1 = 3*2^5\r\n     a2 = Sin(60)\r\n     a1^a2      //Shift+Enter", " es. \\r\\n     a1 = 3*2^5\\r\\n     a2 = Sin(60)\\r\\n     a1^a2      //Shift+Enter", " напр. \r\n     a1 = 3*2^5\r\n     a2 = Sin(60)\r\n     a1^a2      //Shift+Enter", " e.g. \r\n     a1 = 3*2^5\r\n     a2 = Sin(60)\r\n     a1^a2      //Shift+Enter", " e.g. \r\n     a1 = 3*2^5\r\n     a2 = Sin(60)\r\n     a1^a2      //Shift+Enter", " 예: \r\n     a1 = 3*2^5\r\n     a2 = Sin(60)\r\n     a1^a2      //Shift+Enter"),
            new("label5", "Text", "Shift+Enter", "Shift+Enter", "Umschalt+Eingabe", "Maj+Entrée", "Mayús+Entrar", "Shift+Enter", "Shift+Invio", "Shift+Enter", "Shift+Enter", "Shift+Enter", "Shift+Enter"),
            new("label9", "Text", " +, -, *, /,  ^ ,\r\nSin, Cos, Tan, \r\nAsin, Acos, Atan,\r\nSqrt, Ln, Log, Abs ", " +, -, *, /,  ^ ,\r\nSin, Cos, Tan, \r\nAsin, Acos, Atan,\r\nSqrt, Ln, Log, Abs ", " +, -, *, /,  ^ ,\r\nSin, Cos, Tan, \r\nAsin, Acos, Atan,\r\nSqrt, Ln, Log, Abs ", " +, -, *, /,  ^ ,\\r\\nSin, Cos, Tan, \\r\\nAsin, Acos, Atan,\\r\\nSqrt, Ln, Log, Abs ", " +, -, *, /,  ^ ,\r\nSin, Cos, Tan, \r\nAsin, Acos, Atan,\r\nSqrt, Ln, Log, Abs ", " +, -, *, /,  ^ ,\r\nSin, Cos, Tan, \r\nAsin, Acos, Atan,\r\nSqrt, Ln, Log, Abs ", " +, -, *, /,  ^ ,\\r\\nSin, Cos, Tan, \\r\\nAsin, Acos, Atan,\\r\\nSqrt, Ln, Log, Abs ", " +, -, *, /,  ^ ,\r\nSin, Cos, Tan, \r\nAsin, Acos, Atan,\r\nSqrt, Ln, Log, Abs ", " +, -, *, /,  ^ ,\r\nSin, Cos, Tan, \r\nAsin, Acos, Atan,\r\nSqrt, Ln, Log, Abs ", " +, -, *, /,  ^ ,\r\nSin, Cos, Tan, \r\nAsin, Acos, Atan,\r\nSqrt, Ln, Log, Abs ", " +, -, *, /,  ^ ,\r\nSin, Cos, Tan, \r\nAsin, Acos, Atan,\r\nSqrt, Ln, Log, Abs "),
            new("this", "Text", "Easy Calculator", "簡易計算機", "Einfacher Rechner", "Calculatrice simple", "Calculadora sencilla", "Calculadora simples", "Calcolatrice rapida", "Простой калькулятор", "简易计算器", "簡易計算機", "간편 계산기"),
        };
        reg["FormCaptureGUI"] = new L10n.Entry[]
        {
            new("labelTargetForm", "Text", "Target form:", "対象フォーム:", "Zielformat:", "Format cible :", "Formato de destino:", "Formulário de destino:", "Formato di destinazione:", "Целевая форма:", "目标形式:", "目標格式：", "대상 형식:"),
            new("buttonSelectAll", "Text", "Select All", "すべて選択", "Alle auswählen", "Tout sélectionner", "Seleccionar todo", "Selecionar tudo", "Seleziona tutto", "Выбрать всё", "全选", "全選", "모두 선택"),
            new("buttonDeselectAll", "Text", "Deselect All", "すべて解除", "Auswahl aufheben", "Tout désélectionner", "Deseleccionar todo", "Desmarcar tudo", "Deseleziona tutto", "Снять выделение со всех", "取消全选", "取消全選", "모두 선택 해제"),
            new("buttonRefresh", "Text", "Refresh", "更新", "Aktualisieren", "Actualiser", "Actualizar", "Atualizar", "Aggiorna", "Обновить", "刷新", "重新整理", "새로 고침"),
            new("buttonSelectDir", "Text", "Select...", "選択...", "Auswählen...", "Sélectionner...", "Seleccionar...", "Selecionar...", "Seleziona...", "Выбрать...", "选择...", "選取...", "선택..."),
            new("buttonCapture", "Text", "Capture", "キャプチャ", "Aufnehmen", "Capturer", "Capturar", "Capturar", "Cattura", "Захват", "捕获", "擷取", "캡처"),
            new("labelStatus", "Text", "Ready", "準備完了", "Bereit", "Prêt", "Listo", "Pronto", "Pronto", "Готово", "就绪", "就緒", "준비됨"),
        };
        reg["FormCrystalSelection"] = new L10n.Entry[]
        {
            new("buttonCheckAll", "Text", "Check All Items", "すべての項目を選択", "Alle Einträge auswählen", "Cocher tous les éléments", "Marcar todos los elementos", "Marcar todos os itens", "Seleziona tutti gli elementi", "Отметить все элементы", "全选所有项", "勾選所有項目", "모든 항목 선택"),
            new("buttonUnchekAll", "Text", "Uncheck All Items", "すべての項目をチェック解除", "Auswahl aller Elemente aufheben", "Décocher tous les éléments", "Desmarcar todos los elementos", "Desmarcar todos os itens", "Deseleziona tutti gli elementi", "Снять отметки со всех элементов", "取消勾选所有项", "取消勾選所有項目", "모든 항목 선택 해제"),
            new("label1", "Text", "The checked items are being loaded / saved.", "チェックした項目が読み込み / 保存されます。", "Die markierten Elemente werden geladen / gespeichert.", "Les éléments cochés sont en cours de chargement / d'enregistrement.", "Los elementos marcados se están cargando / guardando.", "Os itens marcados estão sendo carregados / salvos.", "Gli elementi selezionati vengono caricati / salvati.", "Отмеченные элементы загружаются / сохраняются.", "已勾选的项正在载入 / 保存。", "勾選的項目正在載入／儲存。", "체크된 항목을 불러오는 / 저장하는 중입니다."),
            new("buttonLoadOrSave", "Text", "Load", "開く", "Laden", "Charger", "Cargar", "Carregar", "Carica", "Загрузить", "载入", "載入", "불러오기"),
            new("button1", "Text", "Cancel", "キャンセル", "Abbrechen", "Annuler", "Cancelar", "Cancelar", "Annulla", "Отмена", "取消", "取消", "취소"),
            new("this", "Text", "Select load / save items", "読み込み / 保存項目を選択", "Lade-/Speicherelemente auswählen", "Sélectionner les éléments à charger / enregistrer", "Seleccionar elementos a cargar / guardar", "Selecionar itens de carregar / salvar", "Seleziona elementi da caricare / salvare", "Выбрать элементы загрузки / сохранения", "选择载入 / 保存项", "選取載入／儲存項目", "불러오기 / 저장 항목 선택"),
        };
        reg["FormDiffractionSimulatorDynamicCompression"] = new L10n.Entry[]
        {
            new("label2", "Text", "Front", "前面", "Vorne", "Avant", "Frente", "Frente", "Fronte", "Спереди", "正面", "前面", "앞면"),
            new("label3", "Text", "Back", "戻る", "Zurück", "Arrière", "Atrás", "Voltar", "Indietro", "Назад", "返回", "返回", "뒤로"),
            new("groupBoxCompressedRotation", "Text", "Rotation distribution", "回転分布", "Rotationsverteilung", "Distribution de rotation", "Distribución de rotación", "Distribuição de rotação", "Distribuzione di rotazione", "Распределение поворотов", "旋转分布", "旋轉分布", "회전 분포"),
            new("groupBoxReleasedRotation", "Text", "Rotation distribution", "回転分布", "Rotationsverteilung", "Distribution de rotation", "Distribución de rotación", "Distribuição de rotação", "Distribuzione di rotazione", "Распределение поворотов", "旋转分布", "旋轉分布", "회전 분포"),
            new("numericBoxEOS_Kprime", "HeaderText", "K'0", "K'0", "K'0", "K'0", "K'0", "K'0", "K\\'0", "K'0", "K'0", "K'0", "K'0"),
            new("groupBoxShockedPlane", "Text", "Shocked plane", "衝撃面", "Stoßwellenebene", "Plan choqué", "Plano de choque", "Plano de choque", "Piano d'urto", "Ударная плоскость", "冲击面", "衝擊面", "충격면"),
            new("buttonSimulate", "Text", "Simulate", "開始", "Simulieren", "Simuler", "Simular", "Simular", "Simula", "Моделировать", "模拟", "模擬", "시뮬레이션"),
            new("checkBoxSkipDrawing", "Text", "Skip drawing during execution", "実行中は描画を省略", "Zeichnen während der Ausführung überspringen", "Ignorer le tracé pendant l'exécution", "Omitir el dibujo durante la ejecución", "Pular desenho durante a execução", "Salta il disegno durante l'esecuzione", "Пропускать отрисовку во время выполнения", "执行期间跳过绘制", "執行期間略過繪製", "실행 중 그리기 건너뛰기"),
            new("groupBoxCompressedArea", "Text", "Compressed area", "圧縮領域", "Komprimierter Bereich", "Zone comprimée", "Área comprimida", "Área comprimida", "Area compressa", "Область сжатия", "压缩区域", "壓縮區域", "압축 영역"),
            new("radioButtonCompressedIsotropic", "Text", "Isotropic compression", "等方圧縮", "Isotrope Kompression", "Compression isotrope", "Compresión isotrópica", "Compressão isotrópica", "Compressione isotropa", "Изотропное сжатие", "各向同性压缩", "等向壓縮", "등방 압축"),
            new("radioButtonCompressedUniaxial", "Text", "Uniaxial compression", "一軸圧縮", "Einachsige Kompression", "Compression uniaxiale", "Compresión uniaxial", "Compressão uniaxial", "Compressione uniassiale", "Одноосное сжатие", "单轴压缩", "單軸壓縮", "단축 압축"),
            new("groupBoxReleasedArea", "Text", "Released area", "解放領域", "Freigegebener Bereich", "Zone libérée", "Área liberada", "Área liberada", "Area rilasciata", "Освобождённая область", "释放区域", "釋放區域", "해방 영역"),
            new("radioButtonReleasedIsotropic", "Text", "Isotropic compression", "等方圧縮", "Isotrope Kompression", "Compression isotrope", "Compresión isotrópica", "Compressão isotrópica", "Compressione isotropa", "Изотропное сжатие", "各向同性压缩", "等向壓縮", "등방 압축"),
            new("radioButtonReleasedUniaxial", "Text", "Uniaxial compression", "一軸圧縮", "Einachsige Kompression", "Compression uniaxiale", "Compresión uniaxial", "Compressão uniaxial", "Compressione uniassiale", "Одноосное сжатие", "单轴压缩", "單軸壓縮", "단축 압축"),
            new("groupBoxSampleParameters", "Text", "Sample parameters", "試料パラメータ", "Probenparameter", "Paramètres de l'échantillon", "Parámetros de la muestra", "Parâmetros da amostra", "Parametri del campione", "Параметры образца", "样品参数", "試樣參數", "시료 매개변수"),
            new("label4", "Text", "Mass absorption\r\ncoefficient", "質量吸収\r\n係数", "Massen-\r\nabsorptionskoeffizient", "Coefficient d'absorption\\r\\nmassique", "Coeficiente de absorción\r\nmásica", "Coeficiente de absorção\r\nde massa", "Coefficiente di assorbimento\\r\\ndi massa", "Коэффициент массового\r\nпоглощения", "质量吸收\r\n系数", "質量吸收\r\n係數", "질량 흡수\r\n계수"),
            new("checkBoxSaveSimulatedPattern", "Text", "Save patterns", "パターンを保存", "Muster speichern", "Enregistrer les motifs", "Guardar patrones", "Salvar padrões", "Salva pattern", "Сохранить картины", "保存图样", "儲存圖樣", "패턴 저장"),
            new("buttonSetFolder", "Text", "Set  folder", "フォルダを設定", "Ordner festlegen", "Définir le dossier", "Definir  carpeta", "Definir  pasta", "Imposta cartella", "Задать  папку", "设置文件夹", "設定資料夾", "폴더 설정"),
            new("textBoxFileName", "Text", "pattern", "パターン", "Muster", "motif", "patrón", "padrão", "pattern", "картина", "图样", "圖樣", "패턴"),
            new("label8", "Text", "File name: ", "ファイル名: ", "Dateiname: ", "Nom du fichier : ", "Nombre de archivo: ", "Nome do arquivo: ", "Nome file: ", "Имя файла: ", "文件名： ", "檔名: ", "파일 이름: "),
            new("groupBoxOutputParameters", "Text", "Output parameters", "出力パラメータ", "Ausgabeparameter", "Paramètres de sortie", "Parámetros de salida", "Parâmetros de saída", "Parametri di output", "Выходные параметры", "输出参数", "輸出參數", "출력 매개변수"),
            new("checkBoxOmegaStep", "Text", "Increment Ω", "増分 Ω", "Schrittweite Ω", "Incrément Ω", "Incremento Ω", "Incremento Ω", "Incremento Ω", "Шаг Ω", "增量 Ω", "增量 Ω", "증분 Ω"),
            new("numericBoxDivisionOfRotationAngle", "HeaderText", "rotation speed", "回転速度", "Rotationsgeschwindigkeit", "vitesse de rotation", "velocidad de rotación", "velocidade de rotação", "velocità di rotazione", "скорость вращения", "转速", "旋轉速度", "회전 속도"),
            new("numericBoxDivisionOfRotationAxis", "HeaderText", "Division of rotation axis", "回転軸の分割数", "Unterteilung der Rotationsachse", "Division de l'axe de rotation", "División del eje de rotación", "Divisão do eixo de rotação", "Divisione dell'asse di rotazione", "Деление оси вращения", "旋转轴分割数", "旋轉軸分割數", "회전축 분할"),
            new("trackBarAdvancedTime", "HeaderText", "Time", "時間", "Zeit", "Temps", "Tiempo", "Tempo", "Tempo", "Время", "时间", "時間", "시간"),
            new("groupBoxCompressionModel", "Text", "Compression && rotation model", "圧縮・回転モデル", "Kompressions- && Rotationsmodell", "Modèle de compression && rotation", "Modelo de compresión y rotación", "Modelo de compressão && rotação", "Modello di compressione e rotazione", "Модель сжатия и поворота", "压缩与旋转模型", "壓縮與旋轉模型", "압축 && 회전 모델"),
            new("groupBoxSlipPlane", "Text", "Slip plane", "すべり面", "Gleitebene", "Plan de glissement", "Plano de deslizamiento", "Plano de deslizamento", "Piano di scorrimento", "Плоскость скольжения", "滑移面", "滑移面", "슬립면"),
        };
        reg["FormDiffractionSimulatorGeometry"] = new L10n.Entry[]
        {
            new("buttonClearPicture", "Text", "Clear", "クリア", "Leeren", "Effacer", "Borrar", "Limpar", "Cancella", "Очистить", "清除", "清除", "지우기"),
            new("buttonRot90", "Text", "Rot 90°", "90°回転", "90° drehen", "Rot 90°", "Rot 90°", "Girar 90°", "Ruota 90°", "Поворот 90°", "旋转 90°", "旋轉 90°", "90° 회전"),
            new("buttonLoadPicture", "Text", "Load", "開く", "Laden", "Charger", "Cargar", "Carregar", "Carica", "Загрузить", "载入", "載入", "불러오기"),
            new("label2", "Text", "Min int.", "最小強度", "Min. Int.", "Int. min.", "Int. mín.", "Int. mín.", "Int. min", "Мин. инт.", "最小强度", "最小強度", "최소 강도"),
            new("label1", "Text", "Max int.", "最大強度", "Max. Int.", "Int. max.", "Int. máx.", "Int. máx.", "Int. max", "Макс. инт.", "最大强度", "最大強度", "최대 강도"),
            new("label4", "Text", "Brightness", "明るさ", "Helligkeit", "Luminosité", "Brillo", "Brilho", "Luminosità", "Яркость", "亮度", "亮度", "밝기"),
            new("label10", "Text", "Opacity", "透明度", "Deckkraft", "Opacité", "Opacidad", "Opacidade", "Opacità", "Непрозрачность", "不透明度", "不透明度", "불투명도"),
            new("label22", "Text", "Scale 2", "スケール 2", "Maßstab 2", "Échelle 2", "Escala 2", "Escala 2", "Scala 2", "Масштаб 2", "比例 2", "比例尺 2", "배율 2"),
            new("label23", "Text", "Scale 1", "スケール 1", "Maßstab 1", "Échelle 1", "Escala 1", "Escala 1", "Scala 1", "Масштаб 1", "比例 1", "比例尺 1", "배율 1"),
            new("label24", "Text", "Gradient", "スケール", "Gradient", "Dégradé", "Gradiente", "Gradiente", "Gradiente", "Градиент", "渐变", "漸層", "그라데이션"),
            new("sizeControl1", "HeaderText", "Detector", "検出器", "Detektor", "Détecteur", "Detector", "Detector", "Rivelatore", "Детектор", "探测器", "偵測器", "검출기"),
            new("numericBoxFootX", "HeaderText", "Foot:  fx", "裾:  fx", "Fuß:  fx", "Pied :  fx", "Pie:  fx", "Pé:  fx", "Coda:  fx", "Подножие:  fx", "底部:  fx", "基底:  fx", "푸트:  fx"),
            new("numericBoxPixelSize", "HeaderText", "pix. size", "画素サイズ", "Pix.-Größe", "taille pix.", "tam. píx.", "tam. pix.", "dim. pixel", "разм. пикс.", "像素大小", "像素大小", "픽셀 크기"),
            new("checkBoxDetectorSizePosition", "Text", "Set detector area && overlapped image.", "検出器領域と重畳画像を設定", "Detektorbereich && überlagertes Bild festlegen.", "Définir la zone du détecteur && l'image superposée.", "Definir área del detector && imagen superpuesta.", "Definir área do detector && imagem sobreposta.", "Imposta area del rivelatore && immagine sovrapposta.", "Задать область детектора && наложенное изображение.", "设置探测器区域 && 叠加图像。", "設定偵測器區域與重疊影像。", "검출기 영역 && 중첩 이미지 설정."),
            new("numericBoxCameraLength2", "HeaderText", "Camera length 2", "カメラ長 2", "Kameralänge 2", "Longueur de caméra 2", "Longitud de cámara 2", "Comprimento da câmera 2", "Lunghezza camera 2", "Длина камеры 2", "相机长度 2", "相機長度 2", "카메라 길이 2"),
            new("checkBoxSchematicDiagram", "Text", "Schematic diagram", "模式図", "Schemazeichnung", "Schéma de principe", "Diagrama esquemático", "Diagrama esquemático", "Schema", "Схематическая диаграмма", "示意图", "示意圖", "개략도"),
        };
        reg["FormDiffractionSpotInfo"] = new L10n.Entry[]
        {
            new("label1", "Text", "Wavelength (= 1/k_vac): ", "波長 (= 1/k_vac): ", "Wellenlänge (= 1/k_vac): ", "Longueur d'onde (= 1/k_vac) : ", "Longitud de onda (= 1/k_vac): ", "Comprimento de onda (= 1/k_vac): ", "Lunghezza d'onda (= 1/k_vac): ", "Длина волны (= 1/k_vac): ", "波长 (= 1/k_vac): ", "波長 (= 1/k_vac)： ", "파장 (= 1/k_vac): "),
            new("buttonCopyToClipboard", "Text", "Copy to clipboard", "クリップボードにコピー", "In Zwischenablage kopieren", "Copier dans le presse-papiers", "Copiar al portapapeles", "Copiar para a área de transferência", "Copia negli appunti", "Копировать в буфер обмена", "复制到剪贴板", "複製到剪貼簿", "클립보드로 복사"),
            new("label4", "Text", "v/c: ", "v/c: ", "v/c: ", "v/c : ", "v/c: ", "v/c: ", "v/c: ", "v/c: ", "v/c: ", "v/c： ", "v/c: "),
            new("label5", "Text", "Acc. Voltage: ", "加速電圧: ", "Beschl.-Spannung: ", "Tension d'accél. : ", "Voltaje de acel.: ", "Tensão de aceleração: ", "Tensione acc.: ", "Уск. напряжение: ", "加速电压： ", "加速電壓: ", "가속 전압: "),
            new("label7", "Text", "Lattice volume: ", "格子体積: ", "Gittervolumen: ", "Volume de la maille : ", "Volumen de la red: ", "Volume da rede: ", "Volume reticolare: ", "Объём решётки: ", "晶格体积： ", "晶格體積: ", "격자 부피: "),
            new("label9", "Text", "Vg or Ug : Crystal potential for elastic scattering.\r\nVg' or Ug': Imaginary  (absorption) potential for thermal diffuse scattering.\r\nΦ: Amplitude of the diffracted wave on the exit surface.", "Vg または Ug : 弾性散乱に対する結晶ポテンシャル。\r\nVg' または Ug': 熱散漫散乱に対する虚数 (吸収) ポテンシャル。\r\nΦ: 出射面における回折波の振幅。", "Vg oder Ug : Kristallpotential für elastische Streuung.\r\nVg' oder Ug': Imaginäres (Absorptions-)Potential für thermisch diffuse Streuung.\r\nΦ: Amplitude der gebeugten Welle an der Austrittsfläche.", "Vg ou Ug : potentiel cristallin pour la diffusion élastique.\r\nVg' ou Ug' : potentiel imaginaire (absorption) pour la diffusion thermique diffuse.\r\nΦ : amplitude de l'onde diffractée sur la surface de sortie.", "Vg o Ug : Potencial del cristal para dispersión elástica.\r\nVg' o Ug': Potencial imaginario (de absorción) para dispersión térmica difusa.\r\nΦ: Amplitud de la onda difractada en la superficie de salida.", "Vg ou Ug : Potencial cristalino para espalhamento elástico.\\r\\nVg' ou Ug': Potencial imaginário  (de absorção) para espalhamento térmico difuso.\\r\\nΦ: Amplitude da onda difratada na superfície de saída.", "Vg o Ug : Potenziale cristallino per la diffusione elastica.\r\nVg' o Ug': Potenziale immaginario (di assorbimento) per la diffusione termica diffusa.\r\nΦ: Ampiezza dell'onda diffratta sulla superficie di uscita.", "Vg или Ug : кристаллический потенциал для упругого рассеяния.\\r\\nVg' или Ug': мнимый  (поглощающий) потенциал для теплового диффузного рассеяния.\\r\\nΦ: амплитуда дифрагированной волны на выходной поверхности.", "Vg 或 Ug : 弹性散射的晶体势。\\r\\nVg' 或 Ug': 热漫散射的虚 (吸收) 势。\\r\\nΦ: 出射面上衍射波的振幅。", "Vg 或 Ug：彈性散射的晶體電位。\\r\\nVg' 或 Ug'：熱漫散射的虛部（吸收）電位。\\r\\nΦ：出射面上繞射波的振幅。", "Vg 또는 Ug : 탄성 산란에 대한 결정 전위.\\r\\nVg' 또는 Ug': 열확산 산란에 대한 허수 (흡수) 전위.\\r\\nΦ: 출사면에서의 회절파 진폭."),
            new("label12", "Text", "Thickness: ", "厚さ: ", "Dicke: ", "Épaisseur : ", "Espesor: ", "Espessura: ", "Spessore: ", "Толщина: ", "厚度: ", "厚度： ", "두께: "),
            new("label15", "Text", "Unit of potential:", "ポテンシャルの単位:", "Einheit des Potentials:", "Unité de potentiel :", "Unidad de potencial:", "Unidade de potencial:", "Unità di potenziale:", "Единица потенциала:", "势的单位:", "電位單位：", "전위 단위:"),
            new("label16", "Text", "Note 1: Unit of length is \"nm\", not \"Å\".   Note 2: Unit of wavenumber  is \"1/nm\", not \"2π/nm\".", "注1: 長さの単位は \"Å\" ではなく \"nm\" です。   注2: 波数の単位は \"2π/nm\" ではなく \"1/nm\" です。", "Anmerkung 1: Längeneinheit ist \"nm\", nicht \"Å\".   Anmerkung 2: Einheit der Wellenzahl ist \"1/nm\", nicht \"2π/nm\".", "Note 1 : l'unité de longueur est « nm », pas « Å ».   Note 2 : l'unité du nombre d'onde est « 1/nm », pas « 2π/nm ».", "Nota 1: La unidad de longitud es \"nm\", no \"Å\".   Nota 2: La unidad de número de onda es \"1/nm\", no \"2π/nm\".", "Nota 1: A unidade de comprimento é \"nm\", não \"Å\".   Nota 2: A unidade de número de onda é \"1/nm\", não \"2π/nm\".", "Nota 1: l'unità di lunghezza è \"nm\", non \"Å\".   Nota 2: l'unità del numero d'onda è \"1/nm\", non \"2π/nm\".", "Примечание 1: единица длины — \"nm\", а не \"Å\".   Примечание 2: единица волнового числа — \"1/nm\", а не \"2π/nm\".", "注 1: 长度单位为 \"nm\"，而非 \"Å\"。   注 2: 波数单位为 \"1/nm\"，而非 \"2π/nm\"。", "註 1：長度單位為「nm」，非「Å」。   註 2：波數單位為「1/nm」，非「2π/nm」。", "참고 1: 길이 단위는 \"Å\"가 아니라 \"nm\"입니다.   참고 2: 파수 단위는 \"2π/nm\"가 아니라 \"1/nm\"입니다."),
            new("label10", "Text", "Max semiangle of electron beam (CBED mode): ", "電子ビームの最大半収束角 (CBEDモード): ", "Max. Halbwinkel des Elektronenstrahls (CBED-Modus): ", "Demi-angle max. du faisceau d'électrons (mode CBED) : ", "Semiángulo máx. del haz de electrones (modo CBED): ", "Semiângulo máx. do feixe de elétrons (modo CBED): ", "Semiangolo max del fascio elettronico (modalità CBED): ", "Макс. полуугол электронного пучка (режим CBED): ", "电子束最大半角 (CBED 模式)： ", "電子束最大半張角 (CBED 模式): ", "전자빔 최대 반각 (CBED 모드): "),
            new("checkBoxAutoRowSize", "Text", "Auto resize row width", "行の幅を自動調整", "Zeilenbreite automatisch anpassen", "Ajuster auto. la largeur des lignes", "Ajustar ancho de fila automáticamente", "Ajustar largura da linha automaticamente", "Adatta larghezza riga automaticamente", "Автоподбор ширины строки", "自动调整行宽", "自動調整列寬", "행 너비 자동 조정"),
            new("numericBoxEffectiveDigit", "HeaderText", "Effective digit", "有効桁数", "Effektive Stelle", "Chiffre significatif", "Dígito efectivo", "Dígito efetivo", "Cifra significativa", "Значащая цифра", "有效位数", "有效位數", "유효 자릿수"),
        };
        reg["FormIsotopeComposition"] = new L10n.Entry[]
        {
            new("ColumnAtomicWeight", "HeaderText", "Atomic Weight", "原子量", "Atomgewicht", "Masse atomique", "Peso atómico", "Peso atômico", "Peso atomico", "Атомный вес", "原子量", "原子量", "원자량"),
            new("ColumnNaturalAbundance", "HeaderText", "Natural Abundance (%)", "天然存在比 (%)", "Natürliche Häufigkeit (%)", "Abondance naturelle (%)", "Abundancia natural (%)", "Abundância natural (%)", "Abbondanza naturale (%)", "Природная распространённость (%)", "天然丰度 (%)", "天然豐度 (%)", "천연 존재비 (%)"),
            new("ColumnCustomAbundance", "HeaderText", "Custom Abundance (%)", "存在比を指定 (%)", "Benutzerdef. Häufigkeit (%)", "Abondance personnalisée (%)", "Abundancia personalizada (%)", "Abundância personalizada (%)", "Abbondanza personalizzata (%)", "Пользовательская распространённость (%)", "自定义丰度 (%)", "自訂豐度 (%)", "사용자 지정 존재비 (%)"),
            new("buttonCancel", "Text", "Cancel", "キャンセル", "Abbrechen", "Annuler", "Cancelar", "Cancelar", "Annulla", "Отмена", "取消", "取消", "취소"),
        };
        reg["FormMovie"] = new L10n.Entry[]
        {
            new("buttonCancel", "Text", "Cancel", "キャンセル", "Abbrechen", "Annuler", "Cancelar", "Cancelar", "Annulla", "Отмена", "取消", "取消", "취소"),
            new("numericBoxSpeed", "HeaderText", "Speed", "速度", "Geschwindigkeit", "Vitesse", "Velocidad", "Velocidade", "Velocità", "Скорость", "速度", "速度", "속도"),
            new("numericBoxDuration", "HeaderText", "Duration", "継続時間", "Dauer", "Durée", "Duración", "Duração", "Durata", "Длительность", "持续时间", "持續時間", "지속 시간"),
            new("radioButtonAxis", "Text", "Direction index", "方向指数", "Richtungsindex", "Indice de direction", "Índice de dirección", "Índice de direção", "Indice di direzione", "Индекс направления", "方向指数", "方向指數", "방향 지수"),
            new("radioButtonPlane", "Text", "Lattice plane", "格子面", "Gitterebene", "Plan réticulaire", "Plano reticular", "Plano reticular", "Piano reticolare", "Плоскость решётки", "晶面", "晶面", "격자면"),
            new("radioButtonCurrent", "Text", "Current projection", "現在の投影", "Aktuelle Projektion", "Projection actuelle", "Proyección actual", "Projeção atual", "Proiezione corrente", "Текущая проекция", "当前投影", "目前投影", "현재 투영"),
            new("groupBoxDirection", "Text", "Direction", "方向", "Richtung", "Direction", "Dirección", "Direção", "Direzione", "Направление", "方向", "方向", "방향"),
            new("label1", "Text", "Encode speed", "エンコード速度", "Kodiergeschwindigkeit", "Vitesse d'encodage", "Velocidad de codificación", "Velocidade de codificação", "Velocità di codifica", "Скорость кодирования", "编码速度", "編碼速度", "인코딩 속도"),
        };
        reg["FormNumericUpdownControl"] = new L10n.Entry[]
        {
            new("label1", "Text", "Increment", "増分", "Schrittweite", "Incrément", "Incremento", "Incremento", "Incremento", "Шаг", "增量", "增量", "증분"),
            new("label2", "Text", "Decimal Places", "小数点以下の桁数", "Dezimalstellen", "Décimales", "Decimales", "Casas decimais", "Cifre decimali", "Знаков после запятой", "小数位数", "小數位數", "소수 자릿수"),
            new("buttonCancel", "Text", "Cancel", "キャンセル", "Abbrechen", "Annuler", "Cancelar", "Cancelar", "Annulla", "Отмена", "取消", "取消", "취소"),
            new("this", "Text", "NumericUpdown Control", "NumericUpdown コントロール", "NumericUpdown-Steuerelement", "Contrôle NumericUpdown", "Control NumericUpdown", "Controle NumericUpdown", "Controllo NumericUpdown", "NumericUpdown Control", "NumericUpdown 控件", "NumericUpdown 控制項", "NumericUpdown 컨트롤"),
        };
        reg["FormPeriodicTable"] = new L10n.Entry[]
        {
            new("labelLa", "Text", "La: lanthanide", "La: ランタノイド", "La: Lanthanoid", "La : lanthanide", "La: lantánido", "La: lantanídeo", "La: lantanide", "La: лантаноид", "La：镧系元素", "La: 鑭系元素", "La: 란타넘족"),
            new("labelAc", "Text", "Ac: actinide", "Ac: アクチノイド", "Ac: Actinoid", "Ac : actinide", "Ac: actínido", "Ac: actinídeo", "Ac: attinide", "Ac: актиноид", "Ac：锕系元素", "Ac: 錒系元素", "Ac: 악티늄족"),
            new("label3", "Text", "must include", "含める", "muss enthalten", "doit inclure", "debe incluir", "deve incluir", "deve includere", "должно включать", "必须包含", "必須包含", "반드시 포함"),
            new("label4", "Text", "must exclude", "除外する", "muss ausschließen", "doit exclure", "debe excluir", "deve excluir", "deve escludere", "должно исключать", "必须排除", "必須排除", "반드시 제외"),
            new("label5", "Text", "may or not include", "含んでも含まなくてもよい", "kann enthalten oder nicht", "peut inclure ou non", "puede incluir o no", "pode ou não incluir", "può includere o meno", "может включать или нет", "可包含可不包含", "可包含或不包含", "포함할 수도 있음"),
        };
        reg["FormPolycrystallineDiffractionSimulator"] = new L10n.Entry[]
        {
            new("groupBoxCrystalProperty", "Text", "Crystal property", "結晶の物性", "Kristalleigenschaft", "Propriété du cristal", "Propiedad del cristal", "Propriedade do cristal", "Proprietà del cristallo", "Свойства кристалла", "晶体属性", "晶體性質", "결정 물성"),
            new("buttonSimulateDebyeRing", "Text", "Simulate Debye ring pattern", "デバイリングパターンをシミュレート", "Debye-Ring-Muster simulieren", "Simuler le diagramme d'anneaux de Debye", "Simular patrón de anillos de Debye", "Simular padrão de anéis de Debye", "Simula pattern ad anelli di Debye", "Моделировать картину колец Дебая", "模拟德拜环图样", "模擬 Debye 環圖樣", "디바이 링 패턴 시뮬레이션"),
            new("groupBoxOrientationFitting", "Text", "Fitting orientations", "フィッティング方位", "Fit-Orientierungen", "Orientations d'ajustement", "Orientaciones de ajuste", "Orientações de ajuste", "Orientazioni di fitting", "Подгонка ориентаций", "拟合取向", "擬合取向", "피팅 방위"),
            new("buttonLoadSetting", "Text", "Load setting", "設定を開く", "Einstellung laden", "Charger les réglages", "Cargar configuración", "Carregar configuração", "Carica impostazioni", "Загрузить настройки", "载入设置", "載入設定", "설정 불러오기"),
            new("buttonSaveCurrentSetting", "Text", "Save current setting", "現在の設定を保存", "Aktuelle Einstellung speichern", "Enregistrer le réglage actuel", "Guardar configuración actual", "Salvar configuração atual", "Salva impostazione corrente", "Сохранить текущую настройку", "保存当前设置", "儲存目前設定", "현재 설정 저장"),
            new("buttonSearch", "Text", "Search Orientations", "方位を探索", "Orientierungen suchen", "Rechercher les orientations", "Buscar orientaciones", "Buscar orientações", "Cerca orientazioni", "Поиск ориентаций", "搜索取向", "搜尋取向", "방위 검색"),
            new("tabPage4", "Text", "Refinement option", "精密化オプション", "Verfeinerungsoption", "Option d'affinement", "Opción de refinamiento", "Opção de refinamento", "Opzione di affinamento", "Параметры уточнения", "精修选项", "精修選項", "정밀화 옵션"),
            new("groupBoxPreferredOrientation", "Text", "Fitting parameters for preferred orientation", "選択配向のフィッティングパラメータ", "Fit-Parameter für Vorzugsorientierung", "Paramètres d'ajustement pour l'orientation préférentielle", "Parámetros de ajuste para orientación preferente", "Parâmetros de ajuste para orientação preferencial", "Parametri di fitting per l'orientazione preferenziale", "Параметры подгонки преимущественной ориентации", "择优取向拟合参数", "擇優取向的擬合參數", "우선 배향 피팅 매개변수"),
            new("checkBoxCrystalNumPerStepThreshold", "Text", "Threshold", "閾値", "Schwellenwert", "Seuil", "Umbral", "Limiar", "Soglia", "Порог", "阈值", "閾值", "임계값"),
            new("numericBoxCrystalNumPerStep", "HeaderText", "Num per Step", "ステップあたりの数", "Anzahl pro Schritt", "Nbre par pas", "Núm. por paso", "Núm. por passo", "Num. per passo", "Число на шаг", "每步数量", "每步數目", "스텝당 개수"),
            new("numericBoxInheritabiliry", "HeaderText", "Inheritability", "継承性", "Vererbbarkeit", "Transmissibilité", "Heredabilidad", "Herdabilidade", "Ereditabilità", "Наследуемость", "可继承性", "可繼承性", "상속성"),
            new("checkBoxInheritabiliryThreshold", "Text", "Threshold", "閾値", "Schwellenwert", "Seuil", "Umbral", "Limiar", "Soglia", "Порог", "阈值", "閾值", "임계값"),
            new("checkBoxDirectionalDensityThreshold", "Text", "Threshold", "閾値", "Schwellenwert", "Seuil", "Umbral", "Limiar", "Soglia", "Порог", "阈值", "閾值", "임계값"),
            new("numericBoxDirectionalDensity", "HeaderText", "Directional density", "方向密度", "Richtungsdichte", "Densité directionnelle", "Densidad direccional", "Densidade direcional", "Densità direzionale", "Плотность по направлениям", "方向密度", "方向密度", "방향 밀도"),
            new("groupBoxFittingOptions", "Text", "Fitting option", "フィッティングオプション", "Fit-Option", "Option d'ajustement", "Opción de ajuste", "Opção de ajuste", "Opzioni di fitting", "Параметры подгонки", "拟合选项", "擬合選項", "피팅 옵션"),
            new("checkBoxRefineConvergence", "Text", "Beam\r\n convergence", "ビームの\r\n収束角", "Strahl-\r\n konvergenz", "Convergence\\r\\n du faisceau", "Convergencia\r\n del haz", "Convergência\r\n do feixe", "Convergenza\\r\\n del fascio", "Сходимость\r\n пучка", "束\r\n 会聚", "電子束\r\n會聚", "빔\r\n 수렴"),
            new("checkBoxRefineStress", "Text", "Stress", "応力", "Spannung", "Contrainte", "Tensión", "Tensão", "Sforzo", "Напряжение", "应力", "應力", "응력"),
            new("checkBoxRefineCenterOffset", "Text", "Center offset", "中心オフセット", "Zentrumsversatz", "Décalage du centre", "Desplazamiento del centro", "Deslocamento do centro", "Scostamento centro", "Смещение центра", "中心偏移", "中心偏移", "중심 오프셋"),
            new("checkBoxRefinePreferredOrientation", "Text", "Preferred \r\n orientation", "優先 \r\n 配向", "Vorzugs-\r\n orientierung", "Orientation \r\n préférentielle", "Orientación \r\n preferente", "Orientação \\r\\n preferencial", "Orientazione \r\n preferenziale", "Преимущественная \\r\\n ориентация", "择优 \\r\\n 取向", "擇優 \\r\\n 取向", "우선 \\r\\n 배향"),
            new("checkBoxRefineFilmBlur", "Text", "Film blur", "フィルムぼかし", "Filmunschärfe", "Flou du film", "Desenfoque de película", "Desfoque do filme", "Sfocatura pellicola", "Размытие плёнки", "胶片模糊", "底片模糊", "필름 블러"),
            new("checkBox6", "Text", "Automatically save setting", "設定を自動的に保存", "Einstellung automatisch speichern", "Enregistrer automatiquement les réglages", "Guardar configuración automáticamente", "Salvar configuração automaticamente", "Salva automaticamente le impostazioni", "Автоматически сохранять настройки", "自动保存设置", "自動儲存設定", "설정 자동 저장"),
            new("checkBoxAutomaticallyChangeParameter", "Text", "Automatically change parameter", "パラメータを自動的に変更", "Parameter automatisch ändern", "Modifier automatiquement le paramètre", "Cambiar parámetro automáticamente", "Alterar parâmetro automaticamente", "Cambia automaticamente il parametro", "Автоматически изменять параметр", "自动更改参数", "自動變更參數", "매개변수 자동 변경"),
            new("tabPage6", "Text", "Refinement results", "精密化結果", "Verfeinerungsergebnisse", "Résultats d'affinement", "Resultados del refinamiento", "Resultados do refinamento", "Risultati dell'affinamento", "Результаты уточнения", "精修结果", "精修結果", "정밀화 결과"),
            new("tabPage8", "Text", "Debug", "デバッグ", "Debug", "Débogage", "Depurar", "Depurar", "Debug", "Отладка", "调试", "偵錯", "디버그"),
            new("buttonSearchUnrelatedOrientations", "Text", "Search unrelated orientation", "無関係な方位を探索", "Unabhängige Orientierung suchen", "Rechercher une orientation indépendante", "Buscar orientación no relacionada", "Buscar orientação não relacionada", "Cerca orientazione non correlata", "Поиск несвязанной ориентации", "搜索无关取向", "搜尋無關取向", "무관한 방위 검색"),
            new("checkBoxYusaGonioScan", "Text", "Use YusaGonio Scan", "YusaGonio スキャンを使用", "YusaGonio-Scan verwenden", "Utiliser le balayage YusaGonio", "Usar barrido YusaGonio", "Usar varredura YusaGonio", "Usa scansione YusaGonio", "Использовать сканирование YusaGonio", "使用 YusaGonio 扫描", "使用 YusaGonio 掃描", "YusaGonio 스캔 사용"),
            new("label52", "Text", "motor speed", "モータ速度", "Motordrehzahl", "vitesse du moteur", "velocidad del motor", "vel. do motor", "velocità motore", "скорость мотора", "电机转速", "馬達轉速", "모터 속도"),
            new("label51", "Text", "oscillation", "振動", "Oszillation", "oscillation", "oscilación", "oscilação", "oscillazione", "осцилляция", "振荡", "擺動", "진동"),
            new("label50", "Text", "oscillation", "振動", "Oszillation", "oscillation", "oscilación", "oscilação", "oscillazione", "осцилляция", "振荡", "擺動", "진동"),
            new("radioButtonZigzagScan", "Text", "+θ > +ω > -θ > +ω .... (Zigzag scan)", "+θ > +ω > -θ > +ω .... (ジグザグ走査)", "+θ > +ω > -θ > +ω .... (Zickzackscan)", "+θ > +ω > -θ > +ω .... (balayage en zigzag)", "+θ > +ω > -θ > +ω .... (Barrido en zigzag)", "+θ > +ω > -θ > +ω .... (Varredura em ziguezague)", "+θ > +ω > -θ > +ω .... (Scansione a zigzag)", "+θ > +ω > -θ > +ω .... (Зигзагообразное сканирование)", "+θ > +ω > -θ > +ω .... (锯齿扫描)", "+θ > +ω > -θ > +ω .... (鋸齒掃描)", "+θ > +ω > -θ > +ω .... (지그재그 스캔)"),
            new("label54", "Text", "motor speed", "モータ速度", "Motordrehzahl", "vitesse du moteur", "velocidad del motor", "vel. do motor", "velocità motore", "скорость мотора", "电机转速", "馬達轉速", "모터 속도"),
            new("label49", "Text", "step", "ステップ", "Schritt", "pas", "paso", "passo", "passo", "шаг", "步长", "步進", "스텝"),
            new("label56", "Text", "motor speed", "モータ速度", "Motordrehzahl", "vitesse du moteur", "velocidad del motor", "vel. do motor", "velocità motore", "скорость мотора", "电机转速", "馬達轉速", "모터 속도"),
            new("groupBoxPatterns", "Text", "Simulated / refference patterns", "シミュレーション / 参照パターン", "Simulierte / Referenzmuster", "Motifs simulés / de référence", "Patrones simulados / de referencia", "Padrões simulados / de referência", "Pattern simulati / di riferimento", "Смоделированные / эталонные картины", "模拟 / 参考图样", "模擬／參考圖樣", "시뮬레이션 / 참조 패턴"),
            new("groupBoxAppearance", "Text", "Appearance", "表示", "Darstellung", "Apparence", "Apariencia", "Aparência", "Aspetto", "Вид", "外观", "外觀", "외관"),
            new("label26", "Text", "Scale 2", "スケール 2", "Maßstab 2", "Échelle 2", "Escala 2", "Escala 2", "Scala 2", "Масштаб 2", "比例 2", "比例尺 2", "배율 2"),
            new("label30", "Text", "Gradient", "スケール", "Gradient", "Dégradé", "Gradiente", "Gradiente", "Gradiente", "Градиент", "渐变", "漸層", "그라데이션"),
            new("label28", "Text", "Scale 1", "スケール 1", "Maßstab 1", "Échelle 1", "Escala 1", "Escala 1", "Scala 1", "Масштаб 1", "比例 1", "比例尺 1", "배율 1"),
            new("buttonRemoveReferrencePattern", "Text", "Remove", "削除", "Entfernen", "Supprimer", "Quitar", "Remover", "Rimuovi", "Удалить", "删除", "移除", "제거"),
            new("tabPage1", "Text", "Simulated Pattern", "シミュレーションパターン", "Simuliertes Muster", "Motif simulé", "Patrón simulado", "Padrão simulado", "Pattern simulato", "Смоделированная картина", "模拟图样", "模擬圖樣", "시뮬레이션 패턴"),
        };
        reg["FormPresets"] = new L10n.Entry[]
        {
            new("label1", "Text", "Name", "結晶名", "Name", "Nom", "Nombre", "Nome", "Nome", "Имя", "名称", "名稱", "이름"),
            new("buttonDelete", "Text", "Delete", "削除", "Löschen", "Supprimer", "Eliminar", "Excluir", "Elimina", "Удалить", "删除", "刪除", "삭제"),
            new("buttonReplace", "Text", "Replace", "更新", "Ersetzen", "Remplacer", "Reemplazar", "Substituir", "Sostituisci", "Заменить", "替换", "取代", "교체"),
            new("buttonRename", "Text", "Rename", "名前の変更", "Umbenennen", "Renommer", "Cambiar nombre", "Renomear", "Rinomina", "Переименовать", "重命名", "重新命名", "이름 바꾸기"),
            new("buttonCancel", "Text", "Cancel", "キャンセル", "Abbrechen", "Annuler", "Cancelar", "Cancelar", "Annulla", "Отмена", "取消", "取消", "취소"),
            new("checkBoxManageList", "Text", "Manage the preset list", "プリセット一覧を管理", "Voreinstellungsliste verwalten", "Gérer la liste des préréglages", "Gestionar la lista de preajustes", "Gerenciar a lista de predefinições", "Gestisci l'elenco dei preset", "Управление списком пресетов", "管理预设列表", "管理預設清單", "프리셋 목록 관리"),
        };
        reg["FormSpotIDv1Results"] = new L10n.Entry[]
        {
            new("zoneAxisDataGridViewTextBoxColumn", "HeaderText", "Zone Axis", "晶帯軸", "Zonenachse", "Axe de zone", "Eje de zona", "Eixo de zona", "Asse di zona", "Ось зоны", "晶带轴", "晶帶軸", "정대축"),
            new("dataGridViewTextBoxColumn1", "HeaderText", "Phase", "相", "Phase", "Phase", "Fase", "Fase", "Fase", "Фаза", "相", "相", "상"),
            new("photo1ZoneAxisDataGridViewTextBoxColumn1", "HeaderText", "Photo 1 Zone Axis", "写真1 晶帯軸", "Foto 1 Zonenachse", "Axe de zone photo 1", "Eje de zona de la foto 1", "Eixo de zona da foto 1", "Asse di zona foto 1", "Ось зоны фото 1", "照片 1 晶带轴", "相片 1 晶帶軸", "사진 1 정대축"),
            new("photo2ZoneAxisDataGridViewTextBoxColumn", "HeaderText", "Photo 2 Zone Axis", "写真2 晶帯軸", "Foto 2 Zonenachse", "Axe de zone photo 2", "Eje de zona de la foto 2", "Eixo de zona da foto 2", "Asse di zona foto 2", "Ось зоны фото 2", "照片 2 晶带轴", "相片 2 晶帶軸", "사진 2 정대축"),
            new("photo3ZoneAxisDataGridViewTextBoxColumn", "HeaderText", "Photo 3 Zone Axis", "写真3 晶帯軸", "Foto 3 Zonenachse", "Axe de zone photo 3", "Eje de zona de la foto 3", "Eixo de zona da foto 3", "Asse di zona foto 3", "Ось зоны фото 3", "照片 3 晶带轴", "相片 3 晶帶軸", "사진 3 정대축"),
            new("angleBetweenPhoto12DataGridViewTextBoxColumn", "HeaderText", "Angle Between Photo 1 & 2", "写真1と2の間の角度", "Winkel zwischen Foto 1 & 2", "Angle entre photos 1 et 2", "Ángulo entre foto 1 y 2", "Ângulo entre fotos 1 e 2", "Angolo tra foto 1 e 2", "Угол между фото 1 и 2", "照片 1 与 2 间夹角", "相片 1 與 2 之間的夾角", "사진 1 & 2 사이 각도"),
            new("angleBetweenPhoto23DataGridViewTextBoxColumn", "HeaderText", "Angle Between Photo 2 & 3", "写真2と3の間の角度", "Winkel zwischen Foto 2 & 3", "Angle entre photos 2 et 3", "Ángulo entre foto 2 y 3", "Ângulo entre fotos 2 e 3", "Angolo tra foto 2 e 3", "Угол между фото 2 и 3", "照片 2 与 3 间夹角", "相片 2 與 3 之間的夾角", "사진 2 & 3 사이 각도"),
            new("angleBetweenPhoto31DataGridViewTextBoxColumn", "HeaderText", "Angle Between Photo 3 & 1", "写真3と1の間の角度", "Winkel zwischen Foto 3 & 1", "Angle entre photos 3 et 1", "Ángulo entre foto 3 y 1", "Ângulo entre fotos 3 e 1", "Angolo tra foto 3 e 1", "Угол между фото 3 и 1", "照片 3 与 1 间夹角", "相片 3 與 1 之間的夾角", "사진 3 & 1 사이 각도"),
        };
        reg["FormSpotIDv2Details"] = new L10n.Entry[]
        {
            new("label1", "Text", "N to S", "N → S", "N nach S", "N vers S", "N a S", "N para S", "N a S", "С на Ю", "N to S", "N 至 S", "N → S"),
            new("label2", "Text", "W to E", "W → E", "W nach O", "O vers E", "O a E", "W para E", "O a E", "З на В", "W to E", "W 至 E", "W → E"),
            new("label3", "Text", "SW to NE ", "SW → NE ", "SW nach NO ", "SO vers NE ", "SO a NE ", "SW para NE ", "SO a NE ", "ЮЗ на СВ ", "SW to NE ", "SW 至 NE ", "SW → NE "),
            new("label4", "Text", "NW to SE", "NW → SE", "NW nach SO", "NO vers SE", "NO a SE", "NW para SE", "NO a SE", "СЗ на ЮВ", "NW to SE", "NW 至 SE", "NW → SE"),
        };
        reg["FormStrain"] = new L10n.Entry[]
        {
            new("groupBoxStrain", "Text", "Strain", "ひずみ", "Verzerrung", "Déformation", "Deformación", "Deformação", "Deformazione", "Деформация", "应变", "應變", "변형"),
            new("groupBoxStress", "Text", "Stress", "応力", "Spannung", "Contrainte", "Tensión", "Tensão", "Sforzo", "Напряжение", "应力", "應力", "응력"),
            new("groupBoxElasticConstant", "Text", "Elastic constant", "弾性定数", "Elastische Konstante", "Constante élastique", "Constante elástica", "Constante elástica", "Costante elastica", "Константа упругости", "弹性常数", "彈性常數", "탄성 상수"),
            new("groupBoxCellConstants", "Text", "Cell constants", "格子定数", "Gitterkonstanten", "Constantes de maille", "Constantes de celda", "Constantes da célula", "Costanti di cella", "Параметры ячейки", "晶胞参数", "晶胞常數", "격자 상수"),
            new("buttonApply", "Text", "Apply", "適用", "Anwenden", "Appliquer", "Aplicar", "Aplicar", "Applica", "Применить", "应用", "套用", "적용"),
            new("this", "Text", "Strain Control", "格子ひずみ (invisible)", "Verzerrungssteuerung", "Contrôle de la déformation", "Control de deformación", "Controle de deformação", "Controllo della deformazione", "Управление деформацией", "应变控制", "應變控制", "변형 제어"),
        };
        reg["FormSuperStructure"] = new L10n.Entry[]
        {
            new("buttonCancel", "Text", "Cancel", "キャンセル", "Abbrechen", "Annuler", "Cancelar", "Cancelar", "Annulla", "Отмена", "取消", "取消", "취소"),
        };
        reg["GraphControl"] = new L10n.Entry[]
        {
            new("buttonCopy", "Text", "Copy", "コピー", "Kopieren", "Copier", "Copiar", "Copiar", "Copia", "Копировать", "复制", "複製", "복사"),
            new("labelY1", "Text", "Y:", "Y:", "Y:", "Y :", "Y:", "Y:", "Y:", "Y:", "Y:", "Y：", "Y:"),
            new("labelX1", "Text", "X:", "X:", "X:", "X :", "X:", "X:", "X:", "X:", "X:", "X：", "X:"),
            new("toolStripMenuItemLogScaleX", "Text", "Log Scale", "対数スケール", "Logarithmische Skala", "Échelle log.", "Escala logarítmica", "Escala logarítmica", "Scala logaritmica", "Лог. шкала", "对数刻度", "對數刻度", "로그 스케일"),
            new("toolStripMenuItemLogScaleY", "Text", "Log Scale", "対数スケール", "Logarithmische Skala", "Échelle log.", "Escala logarítmica", "Escala logarítmica", "Scala logaritmica", "Лог. шкала", "对数刻度", "對數刻度", "로그 스케일"),
            new("toolStripMenuItemScaleLineX", "Text", "Scale Line", "スケールバー", "Maßstabslinie", "Ligne d'échelle", "Línea de escala", "Linha de escala", "Linea di scala", "Масштабная линия", "比例尺", "比例尺線", "눈금선"),
            new("toolStripMenuItemScaleLineY", "Text", "Scale Line", "スケールバー", "Maßstabslinie", "Ligne d'échelle", "Línea de escala", "Linha de escala", "Linea di scala", "Масштабная линия", "比例尺", "比例尺線", "눈금선"),
            new("label1", "Text", "Range  ", "範囲  ", "Bereich  ", "Plage  ", "Intervalo  ", "Intervalo  ", "Intervallo  ", "Диапазон  ", "范围  ", "範圍  ", "범위  "),
            new("labelX2", "Text", "X:", "X:", "X:", "X :", "X:", "X:", "X:", "X:", "X:", "X：", "X:"),
            new("labelY2", "Text", "Y:", "Y:", "Y:", "Y :", "Y:", "Y:", "Y:", "Y:", "Y:", "Y：", "Y:"),
        };
        reg["NumericBoxWithMenu"] = new L10n.Entry[]
        {
            new("incrementToolStripMenuItem", "Text", "Increment", "増分", "Schrittweite", "Incrément", "Incremento", "Incremento", "Incremento", "Шаг", "增量", "增量", "증분"),
            new("smartIncrementToolStripMenuItem", "Text", "Smart increment", "スマートインクリメント", "Intelligentes Inkrement", "Incrément intelligent", "Incremento inteligente", "Incremento inteligente", "Incremento intelligente", "Умный инкремент", "智能增量", "智慧型遞增", "스마트 증분"),
            new("decimalPlacesToolStripMenuItem", "Text", "DecimalPlaces", "小数点以下の桁数", "Dezimalstellen", "Décimales", "Decimales", "Casas decimais", "Cifre decimali", "Знаков после запятой", "小数位数", "小數位數", "소수 자릿수"),
            new("thousandsSeparatorToolStripMenuItem", "Text", "Thousands Separator", "桁区切り", "Tausendertrennzeichen", "Séparateur de milliers", "Separador de miles", "Separador de milhares", "Separatore delle migliaia", "Разделитель тысяч", "千位分隔符", "千位分隔符號", "천 단위 구분 기호"),
            new("toolStripMenuItemRestrictLimit", "Text", "Restrict limit", "範囲を制限", "Grenze einschränken", "Restreindre la limite", "Restringir límite", "Restringir limite", "Limite di restrizione", "Ограничить предел", "限制范围", "限制範圍", "한계 제한"),
            new("toolStripMenuItem1", "Text", "      Maximum", "      最大", "      Maximum", "      Maximum", "      Máximo", "      Máximo", "      Massimo", "      Максимум", "      最大值", "      最大值", "      최대"),
            new("toolStripMenuItem2", "Text", "      Mimimum", "      最小", "      Minimum", "      Minimum", "      Mínimo", "      Mínimo", "      Minimo", "      Минимум", "      最小值", "      最小值", "      최소"),
            new("allowMouseContlolToolStripMenuItem", "Text", "Allow Mouse Contlol", "マウス操作を許可", "Maussteuerung erlauben", "Autoriser le contrôle par la souris", "Permitir control con el ratón", "Permitir controle pelo mouse", "Consenti controllo del mouse", "Разрешить управление мышью", "允许鼠标控制", "允許滑鼠控制", "마우스 제어 허용"),
            new("toolStripMenuItem3", "Text", "      Mouse Speed", "      マウス速度", "      Mausgeschwindigkeit", "      Vitesse de la souris", "      Velocidad del ratón", "      Velocidade do mouse", "      Velocità del mouse", "      Скорость мыши", "      鼠标速度", "      滑鼠速度", "      마우스 속도"),
            new("toolStripMenuItem4", "Text", "      Direction", "      方向", "      Richtung", "      Direction", "      Dirección", "      Direção", "      Direzione", "      Направление", "      方向", "      方向", "      방향"),
        };
        reg["SaclaControl"] = new L10n.Entry[]
        {
            new("label3", "Text", "Foot", "裾", "Fuß", "Pied", "Pie", "Pé", "Coda", "Подножие", "底部", "基底", "푸트"),
            new("groupBoxDetectorProperty", "Text", "Detector property", "検出器の特性", "Detektoreigenschaft", "Propriété du détecteur", "Propiedad del detector", "Propriedade do detector", "Proprietà del rivelatore", "Свойства детектора", "探测器属性", "偵測器性質", "검출기 속성"),
            new("numericBoxPixelWidth", "HeaderText", "Width", "線幅", "Breite", "Largeur", "Anchura", "Largura", "Larghezza", "Ширина", "宽度", "寬度", "너비"),
            new("numericBoxPixelHeight", "HeaderText", "Height", "高さ", "Höhe", "Hauteur", "Altura", "Altura", "Altezza", "Высота", "高度", "高度", "높이"),
            new("numericBoxPixelSize", "HeaderText", "Pix. size", "画素サイズ", "Pix.-Größe", "Taille pix.", "Tam. píx.", "Tam. pix.", "Dim. pixel", "Разм. пикс.", "像素大小", "像素大小", "픽셀 크기"),
            new("groupBoxOpticalProperty", "Text", "Optical property", "光学的性質", "Optische Eigenschaft", "Propriété optique", "Propiedad óptica", "Propriedade óptica", "Proprietà ottica", "Оптические свойства", "光学性质", "光學性質", "광학적 성질"),
            new("numericBoxDistance", "HeaderText", "Cameralength 2", "カメラ長 2", "Kameralänge 2", "Longueur de caméra 2", "Longitud de cámara 2", "Comprimento da câmera 2", "Lunghezza camera 2", "Длина камеры 2", "相机长度 2", "相機長度 2", "카메라 길이 2"),
        };
    }
}
