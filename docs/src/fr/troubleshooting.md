# Dépannage

Problèmes courants et solutions pour ReciPro. Beaucoup des entrées ci-dessous proviennent de questions et de rapports de bogues sur le [suivi des problèmes GitHub](https://github.com/seto77/ReciPro/issues) ; la version dans laquelle un bogue a été corrigé est indiquée le cas échéant.

> **La plupart des problèmes se résolvent simplement en effectuant une mise à jour vers la [dernière version](https://github.com/seto77/ReciPro/releases/latest).** ReciPro est mis à jour fréquemment, et beaucoup des bogues ci-dessous ont été corrigés dans les jours qui ont suivi leur signalement.

---

## Démarrage et lancement

### Symptôme : Le processus s'exécute mais aucune fenêtre n'apparaît

ReciPro démarre (il est visible dans le Gestionnaire des tâches), mais sa fenêtre ne s'affiche jamais à l'écran.

**Cause** : La fenêtre s'est ouverte hors de l'écran — un problème de coordonnées d'affichage Windows, généralement après un changement de moniteur ou de mise à l'échelle de l'affichage. (Issues [#50](https://github.com/seto77/ReciPro/issues/50), [#53](https://github.com/seto77/ReciPro/issues/53), [#55](https://github.com/seto77/ReciPro/issues/55))

**Solution** :

1. Ouvrez le **Gestionnaire des tâches**.
2. Trouvez **ReciPro** dans la liste des processus.
3. Cliquez dessus avec le bouton droit et choisissez **Agrandir**.

La fenêtre sera ramenée sur votre écran principal. Notez que **Basculer vers**, **Mettre au premier plan** et **Réduire** n'aident *pas* — seul **Agrandir** fonctionne.

### Symptôme : ReciPro ne démarre pas, plante ou se bloque au démarrage

**Cause** : Le plus souvent, l'initialisation d'OpenGL échoue, ou une valeur de registre/paramètre corrompue bloque le démarrage.

**Solution** (à essayer dans l'ordre) :

1. **Désactiver OpenGL** : maintenez la touche **Ctrl** enfoncée pendant le lancement de ReciPro pour démarrer avec OpenGL désactivé. Les versions récentes (v4.925 et ultérieures) renforcent l'initialisation d'OpenGL afin que l'application se lance même lorsqu'OpenGL échoue — dans ce cas, les fonctions 3D sont désactivées mais le reste de l'application fonctionne.
2. **Réinitialiser les paramètres** : dans l'éditeur du Registre, supprimez la clé `HKEY_CURRENT_USER\Software\Crystallography\ReciPro`, puis redémarrez. (Équivaut à **Option → Reset registry**.)
3. **Réinstallation propre** : désinstallez ReciPro, supprimez les dossiers suivants s'ils sont présents (remplacez `<user>` par le nom de votre compte), puis réinstallez :
   - `C:\Users\<user>\AppData\Local\Crystallography Software\ReciPro`
   - `C:\Users\<user>\AppData\Roaming\ReciPro\ReciPro`
4. **Mettez à jour** vers la dernière version.

Si rien de tout cela n'aide, la cause peut être l'environnement du système d'exploitation lui-même ; veuillez [ouvrir un issue](https://github.com/seto77/ReciPro/issues) avec les détails de votre PC (CPU, GPU, version de Windows).

---

## Problèmes OpenGL

### Symptôme : Écran noir ou plantage au démarrage

**Cause** : GPU incompatible ou environnement de bureau à distance.

**Solution** :

1. Allez dans **Option → Disable OpenGL (needs restart)** (ou maintenez **Ctrl** enfoncé pendant le lancement).
2. Redémarrez ReciPro.
3. Le Visualiseur de structure et certaines fonctions 3D utiliseront le rendu logiciel.

### Symptôme : Un GPU intégré ou ancien (Intel/AMD) ne parvient pas à effectuer le rendu

**Cause** : Certains GPU intégrés (par ex. AMD Radeon Vega, Intel UHD) avaient des problèmes d'initialisation d'OpenGL dans les anciens builds. (Issue [#2](https://github.com/seto77/ReciPro/issues/2))

**Solution** : Mettez à jour vers la dernière version. L'exigence de version d'OpenGL a été abaissée (v4.781), l'initialisation des GPU intégrés a été corrigée (v4.785), et l'initialisation a été davantage renforcée pour échouer de manière contrôlée (v4.925). Mettre à jour les pilotes de votre GPU aide également.

### Symptôme : Mauvaise qualité de rendu

**Solution** : Mettez à jour les pilotes de votre GPU. Un GPU externe (dédié) prenant en charge OpenGL 1.5 est recommandé.

---

## .NET Runtime

### Symptôme : L'application ne démarre pas

**Cause** : Le .NET Desktop Runtime requis n'est pas installé. Les versions actuelles requièrent le **.NET Desktop Runtime 10.0** (anciens builds : v4.895–v4.91x requéraient 9.0 ; voir l'issue [#43](https://github.com/seto77/ReciPro/issues/43)).

**Solution** : Téléchargez-le et installez-le depuis <https://dotnet.microsoft.com/download/dotnet/10.0> (choisissez le **Desktop Runtime**, x64 pour la plupart des PC).

### Symptôme : Impossible d'atteindre la page de téléchargement Microsoft

**Solution** : Vous pouvez télécharger le programme d'installation du runtime directement. Choisissez le **Windows Desktop Runtime X64** correspondant à votre architecture sur la [page de téléchargement de .NET 10.0](https://dotnet.microsoft.com/download/dotnet/10.0). (Issue [#49](https://github.com/seto77/ReciPro/issues/49))

---

## Installation

### Symptôme : Installer ou désinstaller sans droits d'administrateur

**Note** : Les droits d'administrateur ne sont pas requis. Les raccourcis et les fichiers propres à l'utilisateur sont placés dans vos propres dossiers utilisateur (par ex. `%AppData%\Microsoft\Windows\Start Menu\Programs\Crystallography Software\` et le Bureau). (Issue [#38](https://github.com/seto77/ReciPro/issues/38))

---

## Affichage et mise en page

### Symptôme : Des boutons ou des panneaux sont coupés / masqués, ou la mise en page semble cassée

Par exemple, le bouton **Peak Identification** dans Spot ID v2 est masqué, ou la page À propos et d'autres formulaires sont mal alignés, sur les versions récentes. (Issues [#56](https://github.com/seto77/ReciPro/issues/56), [#59](https://github.com/seto77/ReciPro/issues/59))

**Cause** : Une régression de mise à l'échelle DPI / de police d'interface introduite dans certains builds récents.

**Solution** :

- Réglez la **mise à l'échelle de l'affichage** Windows **sur 100 %** (cela restaure généralement la mise en page).
- Comme solution de contournement rapide, **redimensionnez la fenêtre** (par ex. réduisez-la verticalement) pour révéler les contrôles masqués.
- Mettez à jour vers la dernière version — les mises en page sont corrigées progressivement. Si un build récent semble pire, revenir à une version légèrement plus ancienne (par ex. v4.915) est une option temporaire. Veuillez signaler tout formulaire encore cassé.

---

## Calculs dynamiques

### Symptôme : Très lent ou mémoire insuffisante

**Cause** : Trop d'ondes de Bloch ou une image trop grande.

**Solution** :

- Réduisez **No. of Bloch waves** (50–200 suffisent généralement pour les calculs de routine)
- Utilisez le solveur **Eigen** pour ≤ 500 ondes ; **MKL** pour > 500 ondes
- Réduisez la résolution de l'image pour les simulations STEM
- Fermez les autres applications gourmandes en mémoire

### Symptôme : L'image HAADF-STEM est noire

**Cause** : Les facteurs de température atomiques (B) sont réglés à zéro.

**Solution** : Réglez B ≥ 0.5 Å² pour tous les atomes. L'intensité TDS nécessite des facteurs de température non nuls.

---

## Simulateur de diffraction

### Symptôme : Le diagramme de diffraction est vide / rien n'est dessiné

**Cause** : Habituellement, la vue est trop zoomée, ou l'énergie de l'onde incidente est hors plage. (Issue [#3](https://github.com/seto77/ReciPro/issues/3))

**Solution** :

- **Cliquez avec le bouton gauche** dans la zone de dessin principale pour dézoomer.
- Vérifiez l'énergie de l'onde incidente sur l'onglet **Wave** (en haut à gauche) : rayons X ≈ 1–100 keV, électrons ≈ 10–1000 keV sont appropriés.

---

## Entrées/sorties de fichiers

### Symptôme : Le fichier CIF ne se charge pas

**Solution** :

- Vérifiez que le fichier CIF est bien formé
- Essayez de glisser-déposer le fichier sur la zone **Informations sur le cristal**
- Certaines extensions CIF non standard peuvent ne pas être prises en charge

### Symptôme : Le fichier dm3/dm4 ne se charge pas, ou "unable to cast … 'System.Single' to 'System.Double'"

**Cause** : Il existe plusieurs variantes du format DM3/DM4, et les anciens builds ne pouvaient pas tous les lire. (Issue [#15](https://github.com/seto77/ReciPro/issues/15))

**Solution** : Mettez à jour vers la dernière version — la compatibilité de lecture DM3 a été améliorée dans la v4.835. Si un fichier ne se charge toujours pas, veuillez [l'envoyer](https://github.com/seto77/ReciPro/issues) afin que sa prise en charge puisse être ajoutée.

### Symptôme : Le fichier dm3/dm4 affiche une échelle incorrecte

**Solution** : Vérifiez la calibration dans le logiciel Digital Micrograph d'origine. ReciPro lit les métadonnées intégrées ; si les métadonnées sont incorrectes, réglez manuellement la taille de pixel et la longueur de caméra dans le panneau Optics.

---

## Réinitialisation du registre

Si les paramètres deviennent corrompus :

1. **Option → Reset registry (after restart)**
2. Redémarrez ReciPro — les positions des fenêtres, la longueur d'onde, la longueur de caméra, etc. seront réinitialisées aux valeurs par défaut

---

## Foire aux questions

### Existe-t-il une version Mac (ou Linux) ? {#mac-linux}

Il n'existe pas de version officielle Mac ou Linux. ReciPro dépend du **.NET Desktop Runtime**, qui ne fonctionne actuellement que sous Windows. (Issue [#12](https://github.com/seto77/ReciPro/issues/12))

Cependant, une voie non officielle a été signalée comme fonctionnant sous macOS : la distribution **win-x64 portable ZIP** (disponible sur la [page des releases](https://github.com/seto77/ReciPro/releases/latest)) s'exécute sous macOS (Apple Silicon) en utilisant le wrapper Wine **Sikarugir** combiné au pilote OpenGL **Mesa3D** — sans licence Windows ni machine virtuelle requise. Un guide d'installation pas à pas publié par un utilisateur est disponible à l'adresse <https://github.com/Ryo-fkushima/ReciPro_macOS_memo>.

Notez que cette configuration n'est pas officiellement prise en charge ni entièrement vérifiée. Une limitation connue est que certains symboles (Å, exposants, flèches) peuvent être affichés incorrectement.

**Corriger les symboles déformés (Å, exposants, flèches) :** La cause est que les polices Windows que ReciPro utilise normalement (Segoe UI, Yu Gothic UI, etc.) sont absentes de l'environnement Wine, et que les polices de substitution intégrées de Wine ne contiennent pas certains glyphes scientifiques. ReciPro bascule automatiquement vers des polices à large couverture **lorsqu'il détecte qu'il s'exécute sous Wine**, de sorte que la correction consiste simplement à rendre ces polices disponibles dans le préfixe Wine :

1. Installez **DejaVu Sans** / **DejaVu Serif** (couvre Å, exposants, flèches, étiquettes de fraction) et, pour l'interface japonaise, **Noto Sans CJK JP** (ou **Noto Sans JP**).
2. Le plus simple est de copier les fichiers `.ttf`/`.otf` téléchargés dans le dossier de polices du préfixe — `…/drive_c/windows/Fonts/` à l'intérieur du wrapper Sikarugir — puis de relancer ReciPro. (`winetricks` peut aussi en installer certaines.)
3. Au redémarrage, ReciPro les détecte automatiquement ; aucun paramètre de ReciPro ne doit être modifié.

Si les polices ne sont pas installées, ReciPro conserve ses noms de police par défaut, de sorte que rien n'empire — les symboles restent simplement déformés.

**Perspectives pour cette voie — deux remarques honnêtes :**

- Le ZIP expérimental **win-arm64** ne s'exécute **pas** sur les Mac actuels, même sur Apple Silicon : les builds Wine actuels pour macOS (y compris Sikarugir) exécutent les binaires Windows x86_64 via Rosetta 2 et n'ont aucun mécanisme pour exécuter les binaires Windows ARM64. Sur un Mac, utilisez toujours le ZIP portable **win-x64**.
- Apple supprime progressivement Rosetta 2. macOS 27 (automne 2026) est annoncé comme la dernière version avec une prise en charge complète de Rosetta 2, de sorte que la voie actuelle x64 + Rosetta devrait cesser de fonctionner à partir de macOS 28 (automne 2027). Un Wine ARM64 natif pour macOS est en cours de développement en amont ; s'il se concrétise, le ZIP win-arm64 pourrait devenir le successeur sur Mac, mais cela ne peut pas encore être promis.

### ReciPro fonctionne-t-il sous Windows on ARM (ARM64) ? {#windows-on-arm}

Oui — il existe deux voies :

- **Paquet ARM64 natif (expérimental, recommandé)** : à partir de la v4.938, un paquet portable ARM64 natif expérimental (`ReciPro-v.X_arm64.zip` ; nommé `ReciPro-v.X-arm64.zip` jusqu'à la v.4.939) est publié sur la [page des releases](https://github.com/seto77/ReciPro/releases/latest). Il est self-contained, de sorte qu'aucune installation du .NET Runtime n'est requise — extrayez le ZIP dans un dossier accessible en écriture par l'utilisateur et exécutez `ReciPro.exe`. Si Windows bloque le ZIP téléchargé (Mark of the Web), cliquez avec le bouton droit sur le ZIP → **Propriétés** → cochez **Débloquer** → **OK** *avant* d'extraire (ou exécutez `Unblock-File .\ReciPro-*arm64.zip` dans PowerShell). Les détails se trouvent dans le fichier `README-PORTABLE.txt` fourni.
- **Paquet x64 sous émulation** : le programme d'installation MSI classique et le ZIP portable win-x64 s'exécutent également sous Windows ARM64 grâce à l'émulation x64 intégrée, avec le .NET Desktop Runtime (x64) installé (confirmé à partir d'environ la v4.913 avec .NET 10). Les calculs intensifs s'exécutent plus lentement que le build natif. (Issue [#47](https://github.com/seto77/ReciPro/issues/47))

Remarques sur le paquet ARM64 natif :

- Intel MKL n'existe pas pour ARM64, c'est pourquoi les options de solveur et les éléments de menu correspondants sont masqués. Les calculs dynamiques utilisent la bibliothèque native optimisée pour NEON fournie ; dans des cas de validation représentatifs, ses résultats ont correspondu au build x64 dans la tolérance en virgule flottante attendue.
- Les vues 3D (Visualiseur de structure et fenêtres associées) peuvent fonctionner, mais Windows on ARM ne fournit OpenGL qu'à travers une couche de traduction Direct3D 12 (GLOn12 / Mesa), de sorte que le rendu 3D est nettement plus lent que sur un PC doté d'un pilote OpenGL natif — il s'agit d'une limitation de la plateforme, pas d'un bogue, et un build ARM64 natif ne peut rien y changer. Le mode de transparence **High quality (Per-Pixel Linked List)** du Visualiseur de structure est particulièrement lent sur cette pile de pilotes ; le mode **Approximate** par défaut est recommandé. Si les vues 3D ne démarrent pas, installez le "OpenCL, OpenGL, and Vulkan Compatibility Pack" depuis le Microsoft Store.
- Le paquet ARM64 ne s'exécute **pas** sous macOS + Wine (voir la question précédente). Sur un Mac, utilisez le ZIP portable win-x64.

### Comment dois-je citer ReciPro ?

Utilisez le lien **Cite this repository** sur la [page du dépôt GitHub](https://github.com/seto77/ReciPro) (les métadonnées sont fournies par `CITATION.cff`). La citation recommandée est :

> Seto, Y. & Ohtsuka, M. (2022). *J. Appl. Cryst.* **55**, 397–410. doi:[10.1107/S1600576722000139](https://doi.org/10.1107/S1600576722000139)

(Issue [#33](https://github.com/seto77/ReciPro/issues/33))

---

## Signaler des bogues

Signalez les problèmes à : <https://github.com/seto77/ReciPro/issues>

Veuillez inclure :

- Le numéro de version de ReciPro
- Les étapes pour reproduire le problème
- Tout message d'erreur ou capture d'écran
